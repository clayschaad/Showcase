﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Showcase.Infrastructure.Persistence.Database;

#nullable disable

namespace Showcase.Infrastructure.Persistence.Database.Migrations
{
    [DbContext(typeof(MeasurementDbContext))]
    [Migration("20220727214015_AddPressure")]
    partial class AddPressure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Showcase.Domain.Measurements.Weather.Pressure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Pressures");
                });

            modelBuilder.Entity("Showcase.Domain.Measurements.Weather.Temperature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Temperatures");
                });

            modelBuilder.Entity("Showcase.Domain.Measurements.Weather.Pressure", b =>
                {
                    b.OwnsOne("Showcase.Domain.Measurements.Coordinates", "Coordinates", b1 =>
                        {
                            b1.Property<Guid>("PressureId")
                                .HasColumnType("TEXT");

                            b1.Property<double>("Latitude")
                                .HasColumnType("REAL");

                            b1.Property<double>("Longitude")
                                .HasColumnType("REAL");

                            b1.HasKey("PressureId");

                            b1.ToTable("Pressures");

                            b1.WithOwner()
                                .HasForeignKey("PressureId");
                        });

                    b.Navigation("Coordinates");
                });

            modelBuilder.Entity("Showcase.Domain.Measurements.Weather.Temperature", b =>
                {
                    b.OwnsOne("Showcase.Domain.Measurements.Coordinates", "Coordinates", b1 =>
                        {
                            b1.Property<Guid>("TemperatureId")
                                .HasColumnType("TEXT");

                            b1.Property<double>("Latitude")
                                .HasColumnType("REAL");

                            b1.Property<double>("Longitude")
                                .HasColumnType("REAL");

                            b1.HasKey("TemperatureId");

                            b1.ToTable("Temperatures");

                            b1.WithOwner()
                                .HasForeignKey("TemperatureId");
                        });

                    b.Navigation("Coordinates");
                });
#pragma warning restore 612, 618
        }
    }
}
