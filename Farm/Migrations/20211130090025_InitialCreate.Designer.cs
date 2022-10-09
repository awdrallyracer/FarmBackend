﻿// <auto-generated />
using System;
using Farm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Farm.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211130090025_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Farm.Models.DbModels.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("BarnId")
                        .HasColumnType("int");

                    b.Property<bool>("Injection")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BarnId");

                    b.HasIndex("UserId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Barn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Conditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Barns");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Care", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnimalId")
                        .HasColumnType("int");

                    b.Property<int?>("BarnId")
                        .HasColumnType("int");

                    b.Property<int?>("FoodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.HasIndex("BarnId");

                    b.HasIndex("FoodId");

                    b.ToTable("Cares");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AnimalId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Farm.Models.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Animal", b =>
                {
                    b.HasOne("Farm.Models.DbModels.Barn", "Barn")
                        .WithMany("Animals")
                        .HasForeignKey("BarnId");

                    b.HasOne("Farm.Models.DbModels.User", "User")
                        .WithMany("Animals")
                        .HasForeignKey("UserId");

                    b.Navigation("Barn");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Care", b =>
                {
                    b.HasOne("Farm.Models.DbModels.Animal", "Animal")
                        .WithMany("Cares")
                        .HasForeignKey("AnimalId");

                    b.HasOne("Farm.Models.DbModels.Barn", "Barn")
                        .WithMany("Cares")
                        .HasForeignKey("BarnId");

                    b.HasOne("Farm.Models.DbModels.Food", "Food")
                        .WithMany("Cares")
                        .HasForeignKey("FoodId");

                    b.Navigation("Animal");

                    b.Navigation("Barn");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Food", b =>
                {
                    b.HasOne("Farm.Models.DbModels.Animal", "Animal")
                        .WithMany("Foods")
                        .HasForeignKey("AnimalId");

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Animal", b =>
                {
                    b.Navigation("Cares");

                    b.Navigation("Foods");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Barn", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Cares");
                });

            modelBuilder.Entity("Farm.Models.DbModels.Food", b =>
                {
                    b.Navigation("Cares");
                });

            modelBuilder.Entity("Farm.Models.DbModels.User", b =>
                {
                    b.Navigation("Animals");
                });
#pragma warning restore 612, 618
        }
    }
}
