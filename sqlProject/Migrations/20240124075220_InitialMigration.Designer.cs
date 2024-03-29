﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sqlProject;

#nullable disable

namespace sqlProject.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240124075220_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("AchievementLoggingAnswer", b =>
                {
                    b.Property<int>("AnswersID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LoggingsID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnswersID", "LoggingsID");

                    b.HasIndex("LoggingsID");

                    b.ToTable("AchievementLoggingAnswer");
                });

            modelBuilder.Entity("sqlProject.model.AchievementLogging", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TestID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("StudentID");

                    b.HasIndex("TestID");

                    b.ToTable("AchievementLogging");
                });

            modelBuilder.Entity("sqlProject.model.Answer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OwnerQuestionID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("OwnerQuestionID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("sqlProject.model.Logging", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("LoggingTypeID")
                        .HasColumnType("INTEGER");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("LoggingTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Logging");
                });

            modelBuilder.Entity("sqlProject.model.LoggingType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("LoggingTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            TypeName = "Вход"
                        },
                        new
                        {
                            ID = 2,
                            TypeName = "Выход"
                        });
                });

            modelBuilder.Entity("sqlProject.model.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsUsing")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OwnerTestID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("OwnerTestID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("sqlProject.model.Test", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsQuestionsOrderRandom")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("sqlProject.model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserTypeID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserTypeID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Login = "admin",
                            Password = "admin",
                            UserTypeID = 1
                        });
                });

            modelBuilder.Entity("sqlProject.model.UserType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            TypeName = "Учитель"
                        },
                        new
                        {
                            ID = 2,
                            TypeName = "Студент"
                        });
                });

            modelBuilder.Entity("AchievementLoggingAnswer", b =>
                {
                    b.HasOne("sqlProject.model.Answer", null)
                        .WithMany()
                        .HasForeignKey("AnswersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sqlProject.model.AchievementLogging", null)
                        .WithMany()
                        .HasForeignKey("LoggingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("sqlProject.model.AchievementLogging", b =>
                {
                    b.HasOne("sqlProject.model.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sqlProject.model.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("sqlProject.model.Answer", b =>
                {
                    b.HasOne("sqlProject.model.Question", "OwnerQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("OwnerQuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerQuestion");
                });

            modelBuilder.Entity("sqlProject.model.Logging", b =>
                {
                    b.HasOne("sqlProject.model.LoggingType", "LoggingType")
                        .WithMany("Loggings")
                        .HasForeignKey("LoggingTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sqlProject.model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoggingType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("sqlProject.model.Question", b =>
                {
                    b.HasOne("sqlProject.model.Test", "OwnerTest")
                        .WithMany("Questions")
                        .HasForeignKey("OwnerTestID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OwnerTest");
                });

            modelBuilder.Entity("sqlProject.model.User", b =>
                {
                    b.HasOne("sqlProject.model.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("sqlProject.model.LoggingType", b =>
                {
                    b.Navigation("Loggings");
                });

            modelBuilder.Entity("sqlProject.model.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("sqlProject.model.Test", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("sqlProject.model.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
