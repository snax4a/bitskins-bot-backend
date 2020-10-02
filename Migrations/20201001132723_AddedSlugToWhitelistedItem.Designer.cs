﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Helpers;

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201001132723_AddedSlugToWhitelistedItem")]
    partial class AddedSlugToWhitelistedItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebApi.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("bit");

                    b.Property<int>("BotStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PasswordReset")
                        .HasColumnType("datetime");

                    b.Property<string>("ResetToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WebApi.Entities.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("AveragePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("HighestPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ItemName")
                        .HasColumnType("varchar(767)");

                    b.Property<decimal>("LowestPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("MedianPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Sold")
                        .HasColumnType("int");

                    b.Property<decimal>("StandardDeviation")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ItemName")
                        .IsUnique();

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("WebApi.Entities.PurchasedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("AppId")
                        .HasColumnType("text");

                    b.Property<string>("AssetId")
                        .HasColumnType("text");

                    b.Property<string>("ClassId")
                        .HasColumnType("text");

                    b.Property<string>("ContextId")
                        .HasColumnType("text");

                    b.Property<string>("InstanceId")
                        .HasColumnType("text");

                    b.Property<string>("ItemId")
                        .HasColumnType("text");

                    b.Property<string>("MarketHashName")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("PurchasedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("WithdrawableAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("PurchasedItems");
                });

            modelBuilder.Entity("WebApi.Entities.WhitelistedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<int>("MaxQuantity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("PriceMultiplier")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("PriceUpdatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Slug")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("WhitelistedItems");
                });

            modelBuilder.Entity("WebApi.Entities.Account", b =>
                {
                    b.OwnsMany("WebApi.Entities.RefreshToken", "RefreshTokens", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<int>("AccountId")
                                .HasColumnType("int");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime");

                            b1.Property<string>("CreatedByIp")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Expires")
                                .HasColumnType("datetime");

                            b1.Property<string>("ReplacedByToken")
                                .HasColumnType("text");

                            b1.Property<DateTime?>("Revoked")
                                .HasColumnType("datetime");

                            b1.Property<string>("RevokedByIp")
                                .HasColumnType("text");

                            b1.Property<string>("Token")
                                .HasColumnType("text");

                            b1.HasKey("Id");

                            b1.HasIndex("AccountId");

                            b1.ToTable("RefreshToken");

                            b1.WithOwner("Account")
                                .HasForeignKey("AccountId");
                        });
                });

            modelBuilder.Entity("WebApi.Entities.PurchasedItem", b =>
                {
                    b.HasOne("WebApi.Entities.Account", "Account")
                        .WithMany("PurchasedItems")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Entities.WhitelistedItem", b =>
                {
                    b.HasOne("WebApi.Entities.Account", "Account")
                        .WithMany("WhitelistedItems")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
