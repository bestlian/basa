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

            modelBuilder.Entity("BasaProject.Models.MsRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
