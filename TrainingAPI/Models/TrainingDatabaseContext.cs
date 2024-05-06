using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainingProject.Models;

public partial class TrainingDatabaseContext : DbContext
{
    public TrainingDatabaseContext()
    {
    }

    public TrainingDatabaseContext(DbContextOptions<TrainingDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Line> Lines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=database;Database=TrainingDatabase;User Id=sa;Password=yourStrong(!)Password; TrustServerCertificate=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Line>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK__Lines__32489DA535D4B69E");

            entity.Property(e => e.LineId)
                .ValueGeneratedNever()
                .HasColumnName("lineId");
            entity.Property(e => e.LineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lineName");
            entity.HasData(
                new Line() { LineId = 1, LineName = "Packing" },
                new Line() { LineId = 2, LineName = "Machining" },
                new Line() { LineId = 3, LineName = "Assembly" },
                new Line() { LineId = 4, LineName = "Fabrication" },
                new Line() { LineId = 5, LineName = "Production" }
            );
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__0809335DECC5D2B3");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("orderId");
            entity.Property(e => e.Batch)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("batch");
            entity.Property(e => e.LineId).HasColumnName("lineId");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PlannedDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("plannedDate");
            entity.Property(e => e.PlannedQuantity).HasColumnName("plannedQuantity");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StartingDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("startingDate");
            entity.Property(e => e.StatusId).HasColumnName("statusId");
            entity.Property(e => e.TypeId).HasColumnName("typeId");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Wbs)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WBS");

            entity.HasOne(d => d.Line).WithMany(p => p.Orders)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK__Orders__lineId__5070F446");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Orders__statusId__52593CB8");

            entity.HasOne(d => d.Type).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Orders__typeId__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__userId__5165187F");
            entity.HasData(
                new Order() { OrderId = 1, Name = "Order 1", TypeId = 1, Batch = "Batch 1", LineId = 1, StatusId = 3 },
                new Order() { OrderId = 2, Name = "Order 2", TypeId = 2, Batch = "Batch 2", LineId = 2, StatusId = 3 },
                new Order() { OrderId = 3, Name = "Order 3", TypeId = 3, Batch = "Batch 3", LineId = 3, StatusId = 3 }
            );
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__OrderSta__36257A18CCD15231");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("statusId");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("statusName");
            entity.HasData(
                new OrderStatus() { StatusId = 1, StatusName = "Open" },
                new OrderStatus() { StatusId = 2, StatusName = "Closed" },
                new OrderStatus() { StatusId = 3, StatusName = "Planned" },
                new OrderStatus() { StatusId = 4, StatusName = "Paused" }
            );
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Types__F04DF13AE3DA98D7");

            entity.Property(e => e.TypeId)
                .ValueGeneratedNever()
                .HasColumnName("typeId");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("typeName");
            entity.HasData(
                new Type() { TypeId = 1, TypeName = "PR001" },
                new Type() { TypeId = 2, TypeName = "PR002" },
                new Type() { TypeId = 3, TypeName = "PR003" },
                new Type() { TypeId = 4, TypeName = "PR004" },
                new Type() { TypeId = 5, TypeName = "PR005" }
            );
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__CB9A1CFF58412903");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userName");
            entity.HasData(
                new User() { UserId = 1, UserName = "User1" },
                new User() { UserId = 2, UserName = "User2" },
                new User() { UserId = 3, UserName = "User3" }
            );
        });

        OnModelCreatingPartial(modelBuilder);

    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
