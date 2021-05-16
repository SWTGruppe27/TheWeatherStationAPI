﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheWeatherStationAPI.Data;

namespace TheWeatherStationAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20210426135646_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheWeatherStationAPI.Models.Station", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeatherObservationId")
                        .HasColumnType("int");

                    b.HasKey("StationId");

                    b.HasIndex("WeatherObservationId")
                        .IsUnique();

                    b.ToTable("Station");
                });

            modelBuilder.Entity("TheWeatherStationAPI.Models.WeatherObservation", b =>
                {
                    b.Property<int>("WeatherObservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AirPressure")
                        .HasColumnType("float");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<double>("Temperature")
                        .HasColumnType("float");

                    b.HasKey("WeatherObservationId");

                    b.ToTable("WeatherObservations");
                });

            modelBuilder.Entity("TheWeatherStationAPI.Models.Station", b =>
                {
                    b.HasOne("TheWeatherStationAPI.Models.WeatherObservation", "WeatherObservation")
                        .WithOne("Station")
                        .HasForeignKey("TheWeatherStationAPI.Models.Station", "WeatherObservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherObservation");
                });

            modelBuilder.Entity("TheWeatherStationAPI.Models.WeatherObservation", b =>
                {
                    b.Navigation("Station");
                });
#pragma warning restore 612, 618
        }
    }
}