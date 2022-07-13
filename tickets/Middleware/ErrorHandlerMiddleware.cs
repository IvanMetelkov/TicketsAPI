using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Net;
using System.Threading.Tasks;
using tickets.Exceptions;

namespace tickets.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            try
            {
                await _next(context);
            }
            catch(DbUpdateException e)
            {
                if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                }
            }
            catch(InvalidOperationException e)
            {
                if (e.InnerException is DbUpdateException dbex && dbex.InnerException is PostgresException npgex &&
                                npgex.SqlState == PostgresErrorCodes.LockNotAvailable)
                {
                    response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                }
            }
            catch(BadHttpRequestException e)
            {
                response.StatusCode = e.StatusCode;
            }
            catch (JsonValidationException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch (RefundException)
            {
                response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            await response.WriteAsync(string.Empty);
            /*catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                switch (error)
                {
                    case DbUpdateException e:
                        {
                            if (e.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                            {
                                response.StatusCode = (int)HttpStatusCode.Conflict;
                            }
                            break;
                        }
                    case InvalidOperationException e:
                        {
                            if (e.InnerException is DbUpdateException dbex && dbex.InnerException is PostgresException npgex && 
                                npgex.SqlState == PostgresErrorCodes.LockNotAvailable)
                            {
                                response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                            }
                            break;
                        }
                    case BadHttpRequestException e:
                        {
                            response.StatusCode = e.StatusCode;
                        }
                        break;
                    default:
                        //an unhandled error defaults to 500
                        break;
                }

                //var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(string.Empty);
            }*/
        }
    }
}
