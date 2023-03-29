﻿// <auto-generated />
using System;
using BasaProject.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BasaProject.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BasaProject.Models.MsBasaLemes", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FirstWord")
                        .HasMaxLength(50)
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasMaxLength(1)
                        .HasColumnType("boolean");

                    b.Property<string>("SecondWord")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid?>("UserIn")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserUp")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("FirstWord");

                    b.ToTable("MsBasaLemes");
                });

            modelBuilder.Entity("BasaProject.Models.MsRole", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleID"));

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasMaxLength(1)
                        .HasColumnType("boolean");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid?>("UserIn")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserUp")
                        .HasColumnType("uuid");

                    b.HasKey("RoleID");

                    b.ToTable("MsRoles");
                });

            modelBuilder.Entity("BasaProject.Models.MsUser", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsDeleted")
                        .HasMaxLength(1)
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("RoleID")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserIn")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserUp")
                        .HasColumnType("uuid");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("MsUsers");
                });

            modelBuilder.Entity("BasaProject.Models.MsWordList", b =>
                {
                    b.Property<Guid>("WordID")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Desc")
                        .HasColumnType("text");

                    b.Property<string>("English")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Indonesian")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsDeleted")
                        .HasMaxLength(1)
                        .HasColumnType("boolean");

                    b.Property<string>("Type")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<Guid?>("UserIn")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserUp")
                        .HasColumnType("uuid");

                    b.Property<string>("Word")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("WordID");

                    b.ToTable("MsWordLists");
                });

            modelBuilder.Entity("BasaProject.Models.TrErrorLog", b =>
                {
                    b.Property<Guid>("ErrorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InnerException")
                        .HasColumnType("text");

                    b.Property<bool?>("IsDeleted")
                        .HasMaxLength(1)
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("StackTrace")
                        .HasColumnType("text");

                    b.Property<Guid>("UserIn")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserUp")
                        .HasColumnType("uuid");

                    b.HasKey("ErrorID");

                    b.HasIndex("UserIn");

                    b.ToTable("TrErrorLogs");
                });

            modelBuilder.Entity("BasaProject.Models.TrUserRefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedByIp")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateUp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeviceType")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsDeleted")
                        .HasMaxLength(1)
                        .HasColumnType("boolean");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RevokedByIp")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserAgent")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserIn")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserUp")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("TrUserRefreshTokens");
                });

            modelBuilder.Entity("BasaProject.Models.MsBasaLemes", b =>
                {
                    b.HasOne("BasaProject.Models.MsWordList", "Word")
                        .WithMany()
                        .HasForeignKey("FirstWord")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("BasaProject.Models.MsUser", b =>
                {
                    b.HasOne("BasaProject.Models.MsRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BasaProject.Models.TrErrorLog", b =>
                {
                    b.HasOne("BasaProject.Models.MsUser", "User")
                        .WithMany()
                        .HasForeignKey("UserIn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BasaProject.Models.TrUserRefreshToken", b =>
                {
                    b.HasOne("BasaProject.Models.MsUser", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BasaProject.Models.MsRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BasaProject.Models.MsUser", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
