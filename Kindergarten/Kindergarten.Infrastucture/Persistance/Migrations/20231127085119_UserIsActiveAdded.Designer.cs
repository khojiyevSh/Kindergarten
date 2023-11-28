﻿// <auto-generated />
using System;
using Kindergarten.Infrastucture.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kindergarten.Infrastucture.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231127085119_UserIsActiveAdded")]
    partial class UserIsActiveAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kindergarten.Domain.Entities.Attendence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildernId")
                        .HasColumnType("integer");

                    b.Property<bool>("Participated")
                        .HasColumnType("boolean");

                    b.Property<int>("TrainingTimeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChildernId");

                    b.HasIndex("TrainingTimeId");

                    b.ToTable("Attendences");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Childern", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Bithdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FatherNumber")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActiveChildern")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MatherNumber")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Childerns");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.ChildernGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChildernId")
                        .HasColumnType("integer");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ChildernId");

                    b.HasIndex("GroupId");

                    b.ToTable("ChildernGroups");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndData")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MaxChildCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartData")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.GroupPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgeStatus")
                        .HasColumnType("integer");

                    b.Property<int>("CategotyGroup")
                        .HasColumnType("integer");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Monthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupPrices");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Bithdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActiveTeacher")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.TrainingTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("TrainingTimes");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsActiveUser")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<int>("Roles")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsUnicode(true)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Attendence", b =>
                {
                    b.HasOne("Kindergarten.Domain.Entities.Childern", "Childern")
                        .WithMany("Attendences")
                        .HasForeignKey("ChildernId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kindergarten.Domain.Entities.TrainingTime", "TrainingTime")
                        .WithMany("Attendences")
                        .HasForeignKey("TrainingTimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Childern");

                    b.Navigation("TrainingTime");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Childern", b =>
                {
                    b.HasOne("Kindergarten.Domain.Entities.User", "User")
                        .WithOne("Childern")
                        .HasForeignKey("Kindergarten.Domain.Entities.Childern", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.ChildernGroup", b =>
                {
                    b.HasOne("Kindergarten.Domain.Entities.Childern", "Childern")
                        .WithMany("ChildernGroups")
                        .HasForeignKey("ChildernId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kindergarten.Domain.Entities.Group", "Group")
                        .WithMany("ChildernGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Childern");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Group", b =>
                {
                    b.HasOne("Kindergarten.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Groups")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.GroupPrice", b =>
                {
                    b.HasOne("Kindergarten.Domain.Entities.Group", "Group")
                        .WithMany("GroupPrices")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Teacher", b =>
                {
                    b.HasOne("Kindergarten.Domain.Entities.User", "User")
                        .WithOne("Teacher")
                        .HasForeignKey("Kindergarten.Domain.Entities.Teacher", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.TrainingTime", b =>
                {
                    b.HasOne("Kindergarten.Domain.Entities.Group", "Group")
                        .WithMany("TrainingTimes")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Childern", b =>
                {
                    b.Navigation("Attendences");

                    b.Navigation("ChildernGroups");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Group", b =>
                {
                    b.Navigation("ChildernGroups");

                    b.Navigation("GroupPrices");

                    b.Navigation("TrainingTimes");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.TrainingTime", b =>
                {
                    b.Navigation("Attendences");
                });

            modelBuilder.Entity("Kindergarten.Domain.Entities.User", b =>
                {
                    b.Navigation("Childern");

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
