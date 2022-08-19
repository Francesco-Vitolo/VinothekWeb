﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VinothekManagerWeb.Data;

#nullable disable

namespace VinothekManagerWeb.Migrations
{
    [DbContext(typeof(VinothekDbContext))]
    partial class VinothekDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VinothekManagerWeb.Models.EventModel", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.EventProductModel", b =>
                {
                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("EventID", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("EventProduct");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.ImageModel", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"), 1L, 1);

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.ProducerModel", b =>
                {
                    b.Property<int>("ProducerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProducerId"), 1L, 1);

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Telefon")
                        .HasColumnType("int");

                    b.HasKey("ProducerId");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.ProductModel", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<bool>("Aktiv")
                        .HasColumnType("bit");

                    b.Property<double?>("Alkoholgehalt")
                        .HasColumnType("float");

                    b.Property<string>("Art")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Beschreibung")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Geschmack")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<int?>("Jahrgang")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Preis")
                        .HasColumnType("float");

                    b.Property<int?>("ProducerId")
                        .HasColumnType("int");

                    b.Property<string>("Qualitätssiegel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rebsorten")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("ImageId")
                        .IsUnique()
                        .HasFilter("[ImageId] IS NOT NULL");

                    b.HasIndex("ProducerId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.EventProductModel", b =>
                {
                    b.HasOne("VinothekManagerWeb.Models.EventModel", "Event")
                        .WithMany("EventProducts")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VinothekManagerWeb.Models.ProductModel", "Product")
                        .WithMany("EventProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.ProductModel", b =>
                {
                    b.HasOne("VinothekManagerWeb.Models.ImageModel", "Image")
                        .WithOne("Product")
                        .HasForeignKey("VinothekManagerWeb.Models.ProductModel", "ImageId");

                    b.HasOne("VinothekManagerWeb.Models.ProducerModel", "Producer")
                        .WithMany("Products")
                        .HasForeignKey("ProducerId");

                    b.Navigation("Image");

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.EventModel", b =>
                {
                    b.Navigation("EventProducts");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.ImageModel", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.ProducerModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VinothekManagerWeb.Models.ProductModel", b =>
                {
                    b.Navigation("EventProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
