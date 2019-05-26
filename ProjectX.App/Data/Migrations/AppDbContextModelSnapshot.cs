﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectX.App.Data;

namespace ProjectX.App.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjectX.App.Models.DataProtectionKey", b =>
                {
                    b.Property<string>("FriendlyName")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("XmlData");

                    b.HasKey("FriendlyName");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("ProjectX.App.Models.DeleteRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IP");

                    b.Property<int>("ThoughtId");

                    b.HasKey("Id");

                    b.HasIndex("ThoughtId");

                    b.ToTable("DeleteRequests");
                });

            modelBuilder.Entity("ProjectX.App.Models.Thought", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Tags")
                        .IsRequired();

                    b.Property<string>("TagsDelimiter")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<long>("Views");

                    b.HasKey("Id");

                    b.ToTable("Thoughts");
                });

            modelBuilder.Entity("ProjectX.App.Models.Viewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IP");

                    b.Property<int>("ThoughtId");

                    b.HasKey("Id");

                    b.HasIndex("ThoughtId");

                    b.ToTable("Viewers");
                });

            modelBuilder.Entity("ProjectX.App.Models.DeleteRequest", b =>
                {
                    b.HasOne("ProjectX.App.Models.Thought", "Thoughts")
                        .WithMany()
                        .HasForeignKey("ThoughtId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectX.App.Models.Viewer", b =>
                {
                    b.HasOne("ProjectX.App.Models.Thought", "Thoughts")
                        .WithMany()
                        .HasForeignKey("ThoughtId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
