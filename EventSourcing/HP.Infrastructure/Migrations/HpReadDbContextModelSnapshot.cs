﻿// <auto-generated />
using System;
using HP.Infrastructure.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HP.Infrastructure.Migrations
{
    [DbContext(typeof(HpReadDbContext))]
    partial class HpReadDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HP.Domain.People.Read.PersonDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("GoalType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonRole")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PersonDetails");
                });

            modelBuilder.Entity("HP.Domain.TodoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Completed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TodoDetailsId")
                        .HasColumnType("uuid");

                    b.Property<int>("TodoStatusId")
                        .HasColumnType("integer");

                    b.Property<string>("TodoType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TodoDetailsId");

                    b.HasIndex("TodoStatusId");

                    b.ToTable("TodoItem");
                });

            modelBuilder.Entity("HP.Domain.TodoStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TodoStatus");
                });

            modelBuilder.Entity("HP.Domain.Todos.Read.TodoDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Completed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.Property<string[]>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTime>("TargetEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TargetStartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TodoStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TodoType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("TodoDetails");
                });

            modelBuilder.Entity("HP.Domain.TodoItem", b =>
                {
                    b.HasOne("HP.Domain.Todos.Read.TodoDetails", null)
                        .WithMany("SubTodos")
                        .HasForeignKey("TodoDetailsId");

                    b.HasOne("HP.Domain.TodoStatus", "TodoStatus")
                        .WithMany()
                        .HasForeignKey("TodoStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TodoStatus");
                });

            modelBuilder.Entity("HP.Domain.Todos.Read.TodoDetails", b =>
                {
                    b.Navigation("SubTodos");
                });
#pragma warning restore 612, 618
        }
    }
}
