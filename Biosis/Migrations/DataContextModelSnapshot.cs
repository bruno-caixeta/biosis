﻿// <auto-generated />
using System;
using Biosis.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Biosis.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("CORE")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Biosis.Model.Research", b =>
                {
                    b.Property<Guid>("ResearchId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Compound");

                    b.Property<Guid>("ControlId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<Guid>("UserId");

                    b.HasKey("ResearchId");

                    b.HasIndex("UserId");

                    b.ToTable("Research");
                });

            modelBuilder.Entity("Biosis.Model.TransData", b =>
                {
                    b.Property<Guid>("TransDataId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Breed");

                    b.Property<int>("Class1");

                    b.Property<int>("Class10");

                    b.Property<int>("Class2");

                    b.Property<int>("Class3");

                    b.Property<int>("Class4");

                    b.Property<int>("Class5");

                    b.Property<int>("Class6");

                    b.Property<int>("Class7");

                    b.Property<int>("Class8");

                    b.Property<int>("Class9");

                    b.Property<string>("Compound");

                    b.Property<string>("Dose");

                    b.Property<bool>("IsControl");

                    b.Property<int>("MG");

                    b.Property<int>("MSG");

                    b.Property<int>("MSP");

                    b.Property<int>("PopulationNumber");

                    b.Property<Guid?>("ResearchId");

                    b.Property<string>("StatisticDiagnose");

                    b.Property<int>("TaintTotal");

                    b.HasKey("TransDataId");

                    b.HasIndex("ResearchId");

                    b.ToTable("TransData");
                });

            modelBuilder.Entity("Biosis.Model.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Biosis.Model.Research", b =>
                {
                    b.HasOne("Biosis.Model.User", "User")
                        .WithMany("Researches")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Biosis.Model.TransData", b =>
                {
                    b.HasOne("Biosis.Model.Research", "Research")
                        .WithMany("TransData")
                        .HasForeignKey("ResearchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
