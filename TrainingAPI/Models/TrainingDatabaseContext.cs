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
        => optionsBuilder.UseSqlServer("Server=database,1433;Database=TrainingDatabase;User Id=sa;Password=yourStrong(!)Password; TrustServerCertificate=True;");
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
                .HasColumnName("Wbs");

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
                new Order { OrderId = 1, Name = "Order 1", TypeId = 1, LineId = 1, Batch = "BATCH001", Quantity = 0, PlannedQuantity = 90, PlannedDate = "2024-04-10", UserId = null, Wbs = "WBS001", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 2, Name = "Order 2", TypeId = 2, LineId = 2, Batch = "BATCH002", Quantity = 0, PlannedQuantity = 140, PlannedDate = "2024-04-12", UserId = null, Wbs = "WBS002", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 3, Name = "Order 3", TypeId = 3, LineId = 3, Batch = "BATCH003", Quantity = 0, PlannedQuantity = 180, PlannedDate = "2024-04-15", UserId = null, Wbs = "WBS003", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 4, Name = "Order 4", TypeId = 4, LineId = 4, Batch = "BATCH004", Quantity = 0, PlannedQuantity = 110, PlannedDate = "2024-04-18", UserId = null, Wbs = "WBS004", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 5, Name = "Order 5", TypeId = 5, LineId = 1, Batch = "BATCH005", Quantity = 0, PlannedQuantity = 80, PlannedDate = "2024-04-20", UserId = null, Wbs = "WBS005", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 6, Name = "Order 6", TypeId = 1, LineId = 2, Batch = "BATCH006", Quantity = 0, PlannedQuantity = 170, PlannedDate = "2024-04-22", UserId = null, Wbs = "WBS006", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 7, Name = "Order 7", TypeId = 2, LineId = 3, Batch = "BATCH007", Quantity = 0, PlannedQuantity = 200, PlannedDate = "2024-04-25", UserId = null, Wbs = "WBS007", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 8, Name = "Order 8", TypeId = 3, LineId = 4, Batch = "BATCH008", Quantity = 0, PlannedQuantity = 120, PlannedDate = "2024-04-28", UserId = null, Wbs = "WBS008", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 9, Name = "Order 9", TypeId = 4, LineId = 5, Batch = "BATCH009", Quantity = 0, PlannedQuantity = 90, PlannedDate = "2024-05-01", UserId = null, Wbs = "WBS009", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 10, Name = "Order 10", TypeId = 5, LineId = 1, Batch = "BATCH010", Quantity = 0, PlannedQuantity = 140, PlannedDate = "2024-05-03", UserId = null, Wbs = "WBS010", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 11, Name = "Order 11", TypeId = 1, LineId = 2, Batch = "BATCH011", Quantity = 0, PlannedQuantity = 180, PlannedDate = "2024-05-06", UserId = null, Wbs = "WBS011", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 12, Name = "Order 12", TypeId = 2, LineId = 3, Batch = "BATCH012", Quantity = 0, PlannedQuantity = 100, PlannedDate = "2024-05-09", UserId = null, Wbs = "WBS012", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 13, Name = "Order 13", TypeId = 3, LineId = 4, Batch = "BATCH013", Quantity = 0, PlannedQuantity = 150, PlannedDate = "2024-05-12", UserId = null, Wbs = "WBS013", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 14, Name = "Order 14", TypeId = 4, LineId = 5, Batch = "BATCH014", Quantity = 0, PlannedQuantity = 180, PlannedDate = "2024-05-15", UserId = null, Wbs = "WBS014", StartingDate = null, StatusId = 3 },
                new Order { OrderId = 15, Name = "Order 15", TypeId = 5, LineId = 1, Batch = "BATCH015", Quantity = 0, PlannedQuantity = 70, PlannedDate = "2024-05-18", UserId = null, Wbs = "WBS015", StartingDate = null, StatusId = 3 }
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
