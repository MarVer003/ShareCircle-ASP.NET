﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShareCircle.Data;

#nullable disable

namespace ShareCircle.Migrations
{
    [DbContext(typeof(ShareCircleDbContext))]
    [Migration("20250107160657_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ShareCircle.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumPrijave")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Priimek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ShareCircle.Models.ClanSkupine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumPridruzitve")
                        .HasColumnType("datetime2");

                    b.Property<int>("SkupinaID")
                        .HasColumnType("int");

                    b.Property<decimal>("Stanje")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UporabnikID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("SkupinaID");

                    b.HasIndex("UporabnikID");

                    b.ToTable("ClanSkupine");
                });

            modelBuilder.Entity("ShareCircle.Models.RazdelitevStroska", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ID_dolznika")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ID_stroska")
                        .HasColumnType("int");

                    b.Property<decimal>("Znesek")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("ID_dolznika");

                    b.HasIndex("ID_stroska");

                    b.ToTable("RazdelitevStroska");
                });

            modelBuilder.Entity("ShareCircle.Models.Skupina", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatumNastanka")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImeSkupine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Skupina");
                });

            modelBuilder.Entity("ShareCircle.Models.Strosek", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<decimal>("CelotniZnesek")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DatumPlacila")
                        .HasColumnType("datetime2");

                    b.Property<string>("ID_placnika")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ID_skupine")
                        .HasColumnType("int");

                    b.Property<string>("Naslov")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StevilkaStroska")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ID_placnika");

                    b.HasIndex("ID_skupine");

                    b.ToTable("Strosek");
                });

            modelBuilder.Entity("ShareCircle.Models.Vracilo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatumVracila")
                        .HasColumnType("datetime2");

                    b.Property<string>("ID_dolznika")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ID_skupine")
                        .HasColumnType("int");

                    b.Property<string>("ID_upnika")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("ZnesekVracila")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ID_dolznika");

                    b.HasIndex("ID_skupine");

                    b.HasIndex("ID_upnika");

                    b.ToTable("Vracilo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ShareCircle.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ShareCircle.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShareCircle.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ShareCircle.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShareCircle.Models.ClanSkupine", b =>
                {
                    b.HasOne("ShareCircle.Models.Skupina", "Skupina")
                        .WithMany("ClanSkupine")
                        .HasForeignKey("SkupinaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShareCircle.Models.ApplicationUser", "Uporabnik")
                        .WithMany("ClanSkupine")
                        .HasForeignKey("UporabnikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skupina");

                    b.Navigation("Uporabnik");
                });

            modelBuilder.Entity("ShareCircle.Models.RazdelitevStroska", b =>
                {
                    b.HasOne("ShareCircle.Models.ApplicationUser", "Dolznik")
                        .WithMany("RazdelitveStroskov")
                        .HasForeignKey("ID_dolznika")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShareCircle.Models.Strosek", "Strosek")
                        .WithMany("RazdelitveStroskov")
                        .HasForeignKey("ID_stroska")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dolznik");

                    b.Navigation("Strosek");
                });

            modelBuilder.Entity("ShareCircle.Models.Skupina", b =>
                {
                    b.HasOne("ShareCircle.Models.ApplicationUser", null)
                        .WithMany("Skupine")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("ShareCircle.Models.Strosek", b =>
                {
                    b.HasOne("ShareCircle.Models.ApplicationUser", "Placnik")
                        .WithMany("Stroski")
                        .HasForeignKey("ID_placnika")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShareCircle.Models.Skupina", "Skupina")
                        .WithMany("Stroski")
                        .HasForeignKey("ID_skupine")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Placnik");

                    b.Navigation("Skupina");
                });

            modelBuilder.Entity("ShareCircle.Models.Vracilo", b =>
                {
                    b.HasOne("ShareCircle.Models.ApplicationUser", null)
                        .WithMany("Vracila")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("ShareCircle.Models.ApplicationUser", "Dolžnik")
                        .WithMany()
                        .HasForeignKey("ID_dolznika")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShareCircle.Models.Skupina", "Skupina")
                        .WithMany("Vracila")
                        .HasForeignKey("ID_skupine")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShareCircle.Models.ApplicationUser", "Upnik")
                        .WithMany()
                        .HasForeignKey("ID_upnika")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Dolžnik");

                    b.Navigation("Skupina");

                    b.Navigation("Upnik");
                });

            modelBuilder.Entity("ShareCircle.Models.ApplicationUser", b =>
                {
                    b.Navigation("ClanSkupine");

                    b.Navigation("RazdelitveStroskov");

                    b.Navigation("Skupine");

                    b.Navigation("Stroski");

                    b.Navigation("Vracila");
                });

            modelBuilder.Entity("ShareCircle.Models.Skupina", b =>
                {
                    b.Navigation("ClanSkupine");

                    b.Navigation("Stroski");

                    b.Navigation("Vracila");
                });

            modelBuilder.Entity("ShareCircle.Models.Strosek", b =>
                {
                    b.Navigation("RazdelitveStroskov");
                });
#pragma warning restore 612, 618
        }
    }
}
