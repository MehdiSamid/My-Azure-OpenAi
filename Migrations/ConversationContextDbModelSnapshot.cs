﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenAI_UIR.Data;

#nullable disable

namespace OpenAIUIR.Migrations
{
    [DbContext(typeof(ConversationContextDb))]
    partial class ConversationContextDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OpenAI_UIR.Models.Conversation", b =>
                {
                    b.Property<int>("IdC")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdC"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.HasKey("IdC");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("OpenAI_UIR.Models.Question", b =>
                {
                    b.Property<int>("IdQ")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdQ"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdC")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQ");

                    b.HasIndex("IdC");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("OpenAI_UIR.Models.Response", b =>
                {
                    b.Property<int>("IdR")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdR"));

                    b.Property<string>("Ds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdQ")
                        .HasColumnType("int");

                    b.Property<string>("ReponseText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdR");

                    b.HasIndex("IdQ");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("OpenAI_UIR.Models.Question", b =>
                {
                    b.HasOne("OpenAI_UIR.Models.Conversation", "Conversation")
                        .WithMany()
                        .HasForeignKey("IdC")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("OpenAI_UIR.Models.Response", b =>
                {
                    b.HasOne("OpenAI_UIR.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("IdQ")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });
#pragma warning restore 612, 618
        }
    }
}
