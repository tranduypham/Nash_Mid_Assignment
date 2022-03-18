﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mid_Assignment_Project.Models;

#nullable disable

namespace Mid_Assignment_Project.Migrations
{
    [DbContext(typeof(LibaryDbContext))]
    partial class LibaryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mid_Assignment_Project.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("BookName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("no name")
                        .HasColumnName("name");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(new DateTime(2022, 3, 18, 9, 6, 30, 521, DateTimeKind.Local).AddTicks(5407))
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("BookId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            BookName = "Campell Biology",
                            CategoryId = 1,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6050)
                        },
                        new
                        {
                            BookId = 2,
                            BookName = "Time and space",
                            CategoryId = 1,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6055)
                        },
                        new
                        {
                            BookId = 3,
                            BookName = "WW1",
                            CategoryId = 2,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6057)
                        },
                        new
                        {
                            BookId = 4,
                            BookName = "Vietnam and the communitism",
                            CategoryId = 2,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6057)
                        },
                        new
                        {
                            BookId = 5,
                            BookName = "The phantom of the opera",
                            CategoryId = 3,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6058)
                        },
                        new
                        {
                            BookId = 6,
                            BookName = "Kieu",
                            CategoryId = 3,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6059)
                        });
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.BookBorrowingRequest", b =>
                {
                    b.Property<int>("BookBorrowingRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookBorrowingRequestId"), 1L, 1);

                    b.Property<int>("AuthorizeBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AuthorizeDate")
                        .HasColumnType("smalldatetime")
                        .HasColumnName("authprized_at");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(new DateTime(2022, 3, 18, 9, 6, 30, 522, DateTimeKind.Local).AddTicks(1699))
                        .HasColumnName("created_at");

                    b.Property<byte>("State")
                        .HasColumnType("tinyint");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookBorrowingRequestId");

                    b.ToTable("BookBorrowingRequests");

                    b.HasData(
                        new
                        {
                            BookBorrowingRequestId = 1,
                            AuthorizeBy = 0,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6080),
                            State = (byte)0,
                            UserId = 2,
                            Username = "user1"
                        },
                        new
                        {
                            BookBorrowingRequestId = 2,
                            AuthorizeBy = 0,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6082),
                            State = (byte)0,
                            UserId = 3,
                            Username = "user2"
                        });
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.BookBorrowingRequestDetail", b =>
                {
                    b.Property<int>("BookBorrowingRequestDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookBorrowingRequestDetailId"), 1L, 1);

                    b.Property<int>("BookBorrowingRequestId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("BookName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("no-name")
                        .HasColumnName("name");

                    b.HasKey("BookBorrowingRequestDetailId");

                    b.HasIndex("BookBorrowingRequestId");

                    b.HasIndex("BookId");

                    b.ToTable("BookBorrowingRequestDetails");

                    b.HasData(
                        new
                        {
                            BookBorrowingRequestDetailId = 1,
                            BookBorrowingRequestId = 1,
                            BookId = 1,
                            BookName = "Campell Biology Edit"
                        },
                        new
                        {
                            BookBorrowingRequestDetailId = 2,
                            BookBorrowingRequestId = 1,
                            BookId = 2,
                            BookName = "Time and space"
                        },
                        new
                        {
                            BookBorrowingRequestDetailId = 3,
                            BookBorrowingRequestId = 2,
                            BookId = 2,
                            BookName = "Time and space"
                        },
                        new
                        {
                            BookBorrowingRequestDetailId = 4,
                            BookBorrowingRequestId = 2,
                            BookId = 3,
                            BookName = "WW1"
                        },
                        new
                        {
                            BookBorrowingRequestDetailId = 5,
                            BookBorrowingRequestId = 2,
                            BookId = 4,
                            BookName = "Vietnam and the communitism"
                        },
                        new
                        {
                            BookBorrowingRequestDetailId = 6,
                            BookBorrowingRequestId = 2,
                            BookId = 5,
                            BookName = "The phantom of the opera"
                        });
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValue("no name")
                        .HasColumnName("name");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(new DateTime(2022, 3, 18, 9, 6, 30, 521, DateTimeKind.Local).AddTicks(2992))
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Science",
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6024)
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "History",
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6030)
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Literature",
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(6031)
                        });
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(973))
                        .HasColumnName("created_at");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("payload");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("TokenId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smalldatetime")
                        .HasDefaultValue(new DateTime(2022, 3, 18, 9, 6, 30, 521, DateTimeKind.Local).AddTicks(82))
                        .HasColumnName("created_at");

                    b.Property<bool>("IsSuper")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("isAdmin");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(5818),
                            IsSuper = true,
                            Password = "123456789",
                            Username = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(5826),
                            IsSuper = false,
                            Password = "123456789",
                            Username = "user1"
                        },
                        new
                        {
                            UserId = 3,
                            CreatedAt = new DateTime(2022, 3, 18, 9, 6, 30, 523, DateTimeKind.Local).AddTicks(5828),
                            IsSuper = false,
                            Password = "123456789",
                            Username = "user2"
                        });
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.Book", b =>
                {
                    b.HasOne("Mid_Assignment_Project.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.BookBorrowingRequestDetail", b =>
                {
                    b.HasOne("Mid_Assignment_Project.Models.BookBorrowingRequest", null)
                        .WithMany()
                        .HasForeignKey("BookBorrowingRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mid_Assignment_Project.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mid_Assignment_Project.Models.Token", b =>
                {
                    b.HasOne("Mid_Assignment_Project.Models.User", null)
                        .WithOne()
                        .HasForeignKey("Mid_Assignment_Project.Models.Token", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}