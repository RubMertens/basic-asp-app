﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Questioning.Persistance;

#nullable disable

namespace Questioning.Persistance.Migrations
{
    [DbContext(typeof(ExamDbContext))]
    partial class ExamDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Questioning.Contracts.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuestionResultId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuestionResultId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Questioning.Contracts.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Questioning.Contracts.ExamResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("Questioning.Contracts.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExamId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Questioning.Contracts.QuestionResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ExamResultId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExamResultId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionResults");
                });

            modelBuilder.Entity("Questioning.Contracts.Answer", b =>
                {
                    b.HasOne("Questioning.Contracts.Question", null)
                        .WithMany("PossibleAnswers")
                        .HasForeignKey("QuestionId");

                    b.HasOne("Questioning.Contracts.QuestionResult", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionResultId");
                });

            modelBuilder.Entity("Questioning.Contracts.ExamResult", b =>
                {
                    b.HasOne("Questioning.Contracts.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("Questioning.Contracts.Question", b =>
                {
                    b.HasOne("Questioning.Contracts.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("Questioning.Contracts.QuestionResult", b =>
                {
                    b.HasOne("Questioning.Contracts.ExamResult", null)
                        .WithMany("QuestionResults")
                        .HasForeignKey("ExamResultId");

                    b.HasOne("Questioning.Contracts.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Questioning.Contracts.Exam", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Questioning.Contracts.ExamResult", b =>
                {
                    b.Navigation("QuestionResults");
                });

            modelBuilder.Entity("Questioning.Contracts.Question", b =>
                {
                    b.Navigation("PossibleAnswers");
                });

            modelBuilder.Entity("Questioning.Contracts.QuestionResult", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
