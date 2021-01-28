﻿// <auto-generated />
using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    [Migration("20210119133448_V2")]
    partial class V2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Backend.Models.Cart", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.HasKey("ID");

                    b.ToTable("_Cart");
                });

            modelBuilder.Entity("Backend.Models.CartItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<double>("BasePrice")
                        .HasColumnType("float")
                        .HasColumnName("BasePrice");

                    b.Property<int?>("CartID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Name");

                    b.Property<int>("Quantity")
                        .HasMaxLength(10)
                        .HasColumnType("int")
                        .HasColumnName("Qunatity");

                    b.HasKey("ID");

                    b.HasIndex("CartID");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("Backend.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Name");

                    b.Property<double>("Price")
                        .HasColumnType("float")
                        .HasColumnName("Price");

                    b.Property<int?>("restaurantID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("restaurantID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Backend.Models.Restaurant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Address");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Name");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PhoneNumber");

                    b.Property<int?>("_CartID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("_CartID");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Backend.Models.CartItem", b =>
                {
                    b.HasOne("Backend.Models.Cart", null)
                        .WithMany("Items")
                        .HasForeignKey("CartID");
                });

            modelBuilder.Entity("Backend.Models.Product", b =>
                {
                    b.HasOne("Backend.Models.Restaurant", "restaurant")
                        .WithMany("Products")
                        .HasForeignKey("restaurantID");

                    b.Navigation("restaurant");
                });

            modelBuilder.Entity("Backend.Models.Restaurant", b =>
                {
                    b.HasOne("Backend.Models.Cart", "_Cart")
                        .WithMany()
                        .HasForeignKey("_CartID");

                    b.Navigation("_Cart");
                });

            modelBuilder.Entity("Backend.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Backend.Models.Restaurant", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
