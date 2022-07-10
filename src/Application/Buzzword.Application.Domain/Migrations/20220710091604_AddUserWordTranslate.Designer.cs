﻿// <auto-generated />
using System;
using Buzzword.Application.Domain.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Buzzword.Application.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220710091604_AddUserWordTranslate")]
    partial class AddUserWordTranslate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.GroupWord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserWordId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserWordId");

                    b.ToTable("GroupWord");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.UserWord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Translate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserWord");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.Group", b =>
                {
                    b.HasOne("Buzzword.Application.Domain.Entities.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.GroupWord", b =>
                {
                    b.HasOne("Buzzword.Application.Domain.Entities.Group", "Group")
                        .WithMany("GroupWords")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Buzzword.Application.Domain.Entities.UserWord", "UserWord")
                        .WithMany()
                        .HasForeignKey("UserWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("UserWord");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.UserWord", b =>
                {
                    b.HasOne("Buzzword.Application.Domain.Entities.User", "User")
                        .WithMany("UserWords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.Group", b =>
                {
                    b.Navigation("GroupWords");
                });

            modelBuilder.Entity("Buzzword.Application.Domain.Entities.User", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("UserWords");
                });
#pragma warning restore 612, 618
        }
    }
}