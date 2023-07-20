﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(MyDB_Context))]
    [Migration("20230719191343_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Data.DataModel.Account", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("account_id");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("avatar");

                    b.Property<int>("CountError")
                        .HasColumnType("int")
                        .HasColumnName("countError");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("birtday");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<bool>("IsLock")
                        .HasColumnType("bit")
                        .HasColumnName("islock");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("phone");

                    b.Property<bool>("Sate")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<DateTime?>("TimeLock")
                        .HasColumnType("datetime2")
                        .HasColumnName("timelock");

                    b.Property<string>("TolenChangePassword")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tokenChange_password");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Data.DataModel.AddressAccount", b =>
                {
                    b.Property<Guid?>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("address_id");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("accountId");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.HasKey("AddressId");

                    b.HasIndex("AccountId");

                    b.ToTable("AddressAccount");
                });

            modelBuilder.Entity("Data.DataModel.Category", b =>
                {
                    b.Property<Guid?>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("category_id");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("categoryName");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Data.DataModel.Comment", b =>
                {
                    b.Property<Guid?>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("commentId");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("accountId");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("answer");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("productId");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("question");

                    b.HasKey("CommentId");

                    b.HasIndex("AccountId");

                    b.HasIndex("ProductId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Data.DataModel.Order", b =>
                {
                    b.Property<Guid?>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("orderId");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("accountId");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("CancellationReason")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cancellation_reason");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_time");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("Feedback")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("feedback");

                    b.Property<Guid?>("PaymentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("paymentId");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<int?>("State")
                        .HasColumnType("int")
                        .HasColumnName("state");

                    b.Property<float?>("TotalPrice")
                        .HasColumnType("real")
                        .HasColumnName("totalPrice");

                    b.HasKey("OrderId");

                    b.HasIndex("AccountId");

                    b.HasIndex("PaymentId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Data.DataModel.OrderDetail", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("orderId");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("productId");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<float?>("price")
                        .HasColumnType("real")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("Data.DataModel.Payments", b =>
                {
                    b.Property<Guid?>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("paymentId");

                    b.Property<float?>("Amount")
                        .HasColumnType("real")
                        .HasColumnName("amount");

                    b.Property<string>("Bank")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("bank");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_time");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Type");

                    b.HasKey("PaymentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Data.DataModel.Product", b =>
                {
                    b.Property<Guid?>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("productId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("categoryId");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<float?>("Price")
                        .HasColumnType("real")
                        .HasColumnName("price");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("productName");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<float?>("SalePrice")
                        .HasColumnType("real")
                        .HasColumnName("salePrice");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Data.DataModel.Roles", b =>
                {
                    b.Property<int?>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("roleId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("roleName");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.DataModel.Users", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("avatar");

                    b.Property<int>("CountError")
                        .HasColumnType("int")
                        .HasColumnName("countError");

                    b.Property<DateTime?>("DateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("birtday");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<bool>("IsLock")
                        .HasColumnType("bit")
                        .HasColumnName("islock");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("phone");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("roleId");

                    b.Property<bool>("Sate")
                        .HasColumnType("bit")
                        .HasColumnName("state");

                    b.Property<DateTime?>("TimeLock")
                        .HasColumnType("datetime2")
                        .HasColumnName("timelock");

                    b.Property<string>("TolenChangePassword")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tokenChange_password");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("userName");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.DataModel.AddressAccount", b =>
                {
                    b.HasOne("Data.DataModel.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Data.DataModel.Comment", b =>
                {
                    b.HasOne("Data.DataModel.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("Data.DataModel.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.DataModel.Order", b =>
                {
                    b.HasOne("Data.DataModel.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("Data.DataModel.Payments", "Payments")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.Navigation("Account");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Data.DataModel.OrderDetail", b =>
                {
                    b.HasOne("Data.DataModel.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("Data.DataModel.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.DataModel.Product", b =>
                {
                    b.HasOne("Data.DataModel.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Data.DataModel.Users", b =>
                {
                    b.HasOne("Data.DataModel.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
