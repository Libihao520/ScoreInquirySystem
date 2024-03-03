using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ScoreInquirySystem.Data;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classinfo> Classinfos { get; set; }

    public virtual DbSet<Examination> Examinations { get; set; }

    public virtual DbSet<Score> Scores { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studentclass> Studentclasses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=119.3.218.15;uid=root;pwd=1qazZAQ!;database=test", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf16_general_ci")
            .HasCharSet("utf16");

        modelBuilder.Entity<Classinfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("CLASSINFO")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Grade).HasColumnName("GRADE");
            entity.Property(e => e.Name)
                .HasMaxLength(5)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Examination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("EXAMINATION")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.HasIndex(e => e.Createdatetime, "INDEX_EXAMINATION");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Createdatetime)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDATETIME");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Score>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("SCORES")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.HasIndex(e => new { e.Subject, e.Examinationid, e.Studentid }, "INDEX_SCORES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Examinationid).HasColumnName("EXAMINATIONID");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
            entity.Property(e => e.Subject).HasColumnName("SUBJECT");
            entity.Property(e => e.Value)
                .HasPrecision(4, 1)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("STUDENT")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.HasIndex(e => new { e.Status, e.Jointime }, "INDEX_STUDENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Jointime)
                .HasColumnType("datetime")
                .HasColumnName("JOINTIME");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Sex).HasColumnName("SEX");
            entity.Property(e => e.Status).HasColumnName("STATUS");
        });

        modelBuilder.Entity<Studentclass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("STUDENTCLASS")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.HasIndex(e => new { e.Jointime, e.Classid, e.Studentid }, "INDEX_STUDENTCLASS");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Classid).HasColumnName("CLASSID");
            entity.Property(e => e.Jointime)
                .HasColumnType("datetime")
                .HasColumnName("JOINTIME");
            entity.Property(e => e.Studentid).HasColumnName("STUDENTID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("USERS")
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
