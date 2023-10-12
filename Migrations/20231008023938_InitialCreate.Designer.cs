﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace QA_Feedback.Migrations
{
    [DbContext(typeof(QuizContext))]
    [Migration("20231008023938_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("QA_Feedback.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Source")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("QA_Feedback.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Accuracy_Stars")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Difficulty_Stars")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Pyramidality_Stars")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Question")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("QA_Feedback.Models.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Question")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stars")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Source");
                });
#pragma warning restore 612, 618
        }
    }
}
