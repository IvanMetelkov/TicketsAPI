﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using tickets.DAL;

namespace tickets.Migrations
{
    [DbContext(typeof(SegmentContext))]
    [Migration("20220623125404_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("tickets.Model.Segment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AirlineCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("airline_code");

                    b.Property<DateTimeOffset>("ArriveDatetime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("arrive_datetime");

                    b.Property<string>("ArrivePlace")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("arrive_place");

                    b.Property<short>("ArriveTimeTimezone")
                        .HasColumnType("smallint")
                        .HasColumnName("arrive_time_timezone");

                    b.Property<DateTimeOffset>("DepartDatetime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("depart_datetime");

                    b.Property<string>("DepartPlace")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("depart_place");

                    b.Property<short>("DepartTimeTimezone")
                        .HasColumnType("smallint")
                        .HasColumnName("depart_time_timezone");

                    b.Property<string>("DocNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("doc_number");

                    b.Property<string>("DocType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("doc_type");

                    b.Property<int>("FlightNum")
                        .HasColumnType("integer")
                        .HasColumnName("flight_num");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("patronymic");

                    b.Property<string>("PnrId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("pnr_id");

                    b.Property<bool>("Refunded")
                        .HasColumnType("boolean")
                        .HasColumnName("refunded");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("integer")
                        .HasColumnName("serial_number");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("surname");

                    b.Property<string>("TicketNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ticket_number");

                    b.HasKey("Id")
                        .HasName("pk_segments");

                    b.HasIndex("TicketNumber", "SerialNumber")
                        .IsUnique()
                        .HasDatabaseName("ix_segments_ticket_number_serial_number");

                    b.ToTable("segments");
                });
#pragma warning restore 612, 618
        }
    }
}
