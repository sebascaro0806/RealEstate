﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstate.Infrastructure.Context;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    [DbContext(typeof(RealEstateDBContext))]
    partial class RealEstateDBConextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealEstate.Domain.Models.BuildingProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeInternal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("BuildingProperties");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.BuildingPropertyImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingPropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingPropertyId");

                    b.ToTable("BuildingPropertiesImages");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.BuildingPropertyTrace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingPropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateSale")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingPropertyId");

                    b.ToTable("BuildingPropertiesTraces");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.BuildingProperty", b =>
                {
                    b.HasOne("RealEstate.Domain.Models.Owner", "Owner")
                        .WithMany("BuildingProperties")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.BuildingPropertyImage", b =>
                {
                    b.HasOne("RealEstate.Domain.Models.BuildingProperty", "BuildingProperty")
                        .WithMany("BuildingPropertiesImages")
                        .HasForeignKey("BuildingPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildingProperty");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.BuildingPropertyTrace", b =>
                {
                    b.HasOne("RealEstate.Domain.Models.BuildingProperty", "BuildingProperty")
                        .WithMany()
                        .HasForeignKey("BuildingPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildingProperty");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.BuildingProperty", b =>
                {
                    b.Navigation("BuildingPropertiesImages");
                });

            modelBuilder.Entity("RealEstate.Domain.Models.Owner", b =>
                {
                    b.Navigation("BuildingProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
