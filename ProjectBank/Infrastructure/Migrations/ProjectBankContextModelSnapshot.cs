﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectBank.Infrastructure.Repository;

#nullable disable

namespace ProjectBank.Infrastructure.Migrations
{
    [DbContext(typeof(ProjectBankContext))]
    partial class ProjectBankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccountProject", b =>
                {
                    b.Property<int>("AccountsId")
                        .HasColumnType("int");

                    b.Property<int>("SavedProjectsId")
                        .HasColumnType("int");

                    b.HasKey("AccountsId", "SavedProjectsId");

                    b.HasIndex("SavedProjectsId");

                    b.ToTable("AccountProject");
                });

            modelBuilder.Entity("KeywordProject", b =>
                {
                    b.Property<int>("KeywordsId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("int");

                    b.HasKey("KeywordsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("KeywordProject");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AzureAdToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AzureAdToken")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entity.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Word")
                        .IsUnique();

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entity.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Degree")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<float?>("Ects")
                        .HasColumnType("real");

                    b.Property<string>("FileUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("AccountProject", b =>
                {
                    b.HasOne("ProjectBank.Infrastructure.Entity.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBank.Infrastructure.Entity.Project", null)
                        .WithMany()
                        .HasForeignKey("SavedProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KeywordProject", b =>
                {
                    b.HasOne("ProjectBank.Infrastructure.Entity.Keyword", null)
                        .WithMany()
                        .HasForeignKey("KeywordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectBank.Infrastructure.Entity.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entity.Project", b =>
                {
                    b.HasOne("ProjectBank.Infrastructure.Entity.Account", "Author")
                        .WithMany("AuthoredProjects")
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("ProjectBank.Infrastructure.Entity.Account", b =>
                {
                    b.Navigation("AuthoredProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
