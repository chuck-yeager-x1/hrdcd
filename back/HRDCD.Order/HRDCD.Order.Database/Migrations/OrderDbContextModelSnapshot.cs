﻿// <auto-generated />
using System;
using HRDCD.Order.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HRDCD.Order.Database.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HRDCD.Order.DataModel.Entity.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("DeleteDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("del_date");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ins_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_del");

                    b.Property<string>("OrderDescription")
                        .HasColumnType("text")
                        .HasColumnName("order_desc");

                    b.Property<string>("OrderName")
                        .HasColumnType("text")
                        .HasColumnName("order_name");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("order_num");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("upd_date");

                    b.HasKey("Id");

                    b.HasIndex("OrderNumber")
                        .IsUnique();

                    b.ToTable("order", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
