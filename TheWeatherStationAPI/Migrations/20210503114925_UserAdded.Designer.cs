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
    [Migration("20210503114925_UserAdded")]
    partial class UserAdded
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

            modelBuilder.Entity("TheWeatherStationAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("LastName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PwHash")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("UserId");

                    b.ToTable("User");
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
