using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PrsWeb.Controllers;

namespace PrsWeb.Models;

public partial class PrsdbContext : DbContext
{
    //public PrsdbContext()
    //{
    //}

    public PrsdbContext(DbContextOptions<PrsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LineItem> LineItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }
    
    

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=DESKTOP-VDMQ3OL\\SQLEXPRESS;Database=capstone_prs;Integrated Security=True;TrustServerCertificate=True;");

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<LineItem>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__LineItem__3213E83FF39FF91F");

    //            entity.HasOne(d => d.Product).WithMany(p => p.LineItems)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__LineItem__produc__38996AB5");

    //            entity.HasOne(d => d.Request).WithMany(p => p.LineItems)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__LineItem__reques__37A5467C");
    //        });

    //        modelBuilder.Entity<Product>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__Product__3213E83F2C71DE84");

    //            entity.HasOne(d => d.Vendor).WithMany(p => p.Products)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Product__vendorI__2F10007B");
    //        });

    //        modelBuilder.Entity<Request>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__Request__3213E83F00264F45");

    //            entity.Property(e => e.Status).HasDefaultValue("New");
    //            entity.Property(e => e.SubmittedDate).HasDefaultValueSql("(getdate())");

    //            entity.HasOne(d => d.User).WithMany(p => p.Requests)
    //                .OnDelete(DeleteBehavior.ClientSetNull)
    //                .HasConstraintName("FK__Request__userId__33D4B598");
    //        });

    //        modelBuilder.Entity<User>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F1CED7019");
    //        });

    //        modelBuilder.Entity<Vendor>(entity =>
    //        {
    //            entity.HasKey(e => e.Id).HasName("PK__Vendor__3213E83F042D3377");
    //        });

    //        OnModelCreatingPartial(modelBuilder);
    //    }

    //    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
