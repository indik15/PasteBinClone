﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PasteBinClone.Persistence.Data;

#nullable disable

namespace PasteBinClone.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240805213710_removedTotalActivePastes")]
    partial class removedTotalActivePastes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PasteBinClone.Domain.Models.ApiUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ApiUsers");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PasteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PasteId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.ContentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("ContentTypes");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Paste", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BodyUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<long>("Dislikes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<DateTime>("ExpireAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("int");

                    b.Property<long>("Likes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LanguageId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Pastes", t =>
                        {
                            t.HasCheckConstraint("MinDislikesLength", "Dislikes >= 0");

                            t.HasCheckConstraint("MinLikesLength", "Likes >= 0");
                        });
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDisliked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<Guid>("PasteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PasteId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.UserPasteInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalPastes")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrivatePastes")
                        .HasColumnType("int");

                    b.Property<int>("TotalPublicPastes")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserPasteInfo");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Comment", b =>
                {
                    b.HasOne("PasteBinClone.Domain.Models.Paste", "Paste")
                        .WithMany("Comments")
                        .HasForeignKey("PasteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PasteBinClone.Domain.Models.ApiUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Paste");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Paste", b =>
                {
                    b.HasOne("PasteBinClone.Domain.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PasteBinClone.Domain.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PasteBinClone.Domain.Models.ContentType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("PasteBinClone.Domain.Models.ApiUser", "User")
                        .WithMany("Pastes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Language");

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Rating", b =>
                {
                    b.HasOne("PasteBinClone.Domain.Models.Paste", "Paste")
                        .WithMany("Ratings")
                        .HasForeignKey("PasteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PasteBinClone.Domain.Models.ApiUser", "ApiUser")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ApiUser");

                    b.Navigation("Paste");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.UserPasteInfo", b =>
                {
                    b.HasOne("PasteBinClone.Domain.Models.ApiUser", "ApiUser")
                        .WithOne("UserPasteInfo")
                        .HasForeignKey("PasteBinClone.Domain.Models.UserPasteInfo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApiUser");
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.ApiUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Pastes");

                    b.Navigation("Ratings");

                    b.Navigation("UserPasteInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("PasteBinClone.Domain.Models.Paste", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
