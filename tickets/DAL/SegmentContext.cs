using Microsoft.EntityFrameworkCore;
using System;
using tickets.Model;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace tickets.DAL
{
    public class SegmentContext : DbContext
    {
        public SegmentContext(DbContextOptions<SegmentContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine)
            .UseNpgsql()
            .UseSnakeCaseNamingConvention();

        public DbSet<Segment> Segments { get; set; }
    }

    public class CamelCaseHistoryContext : NpgsqlHistoryRepository
    {
        public CamelCaseHistoryContext(HistoryRepositoryDependencies dependencies) : base(dependencies)
        {
        }

        protected override void ConfigureTable(EntityTypeBuilder<HistoryRow> history)
        {
            base.ConfigureTable(history);

            history.Property(h => h.MigrationId).HasColumnName("MigrationId");
            history.Property(h => h.ProductVersion).HasColumnName("ProductVersion");
        }
    }
}
