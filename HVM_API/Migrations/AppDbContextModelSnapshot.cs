﻿// <auto-generated />
using System;
using HVM_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HVMAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HVM_API.Models.Audit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AffectedColumns")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NewValues")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OldValues")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PrimaryKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("HVM_API.Models.AuthObjects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AuthObjDesc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AuthObjName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("AuthObjects");
                });

            modelBuilder.Entity("HVM_API.Models.RoleAuths", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AuthObjId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("RoleId", "AuthObjId");

                    b.HasIndex("AuthObjId");

                    b.ToTable("RoleAuths");
                });

            modelBuilder.Entity("HVM_API.Models.RoleUsers", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnOrder(0);

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("UserName", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleUsers");
                });

            modelBuilder.Entity("HVM_API.Models.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HVM_API.Models.Units", b =>
                {
                    b.Property<string>("UnitCode")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UnitCode");

                    b.ToTable("Units");

                    b.HasData(
                        new
                        {
                            UnitCode = "01",
                            Active = false,
                            UnitName = "Samaghogha/Pragpur"
                        },
                        new
                        {
                            UnitCode = "02",
                            Active = false,
                            UnitName = "Nana Kapaya"
                        });
                });

            modelBuilder.Entity("HVM_API.Models.UserUnits", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnOrder(0);

                    b.Property<string>("UnitCode")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnOrder(1);

                    b.HasKey("UserName", "UnitCode");

                    b.HasIndex("UnitCode");

                    b.ToTable("UserUnits");
                });

            modelBuilder.Entity("HVM_API.Models.Users", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("UserName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HVM_API.Models.RoleAuths", b =>
                {
                    b.HasOne("HVM_API.Models.AuthObjects", "AuthObject")
                        .WithMany()
                        .HasForeignKey("AuthObjId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HVM_API.Models.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AuthObject");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("HVM_API.Models.RoleUsers", b =>
                {
                    b.HasOne("HVM_API.Models.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HVM_API.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HVM_API.Models.UserUnits", b =>
                {
                    b.HasOne("HVM_API.Models.Units", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HVM_API.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Unit");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
