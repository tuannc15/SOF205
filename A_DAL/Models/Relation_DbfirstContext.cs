using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace A_DAL.Models
{
    public partial class Relation_DbfirstContext : DbContext
    {
        public Relation_DbfirstContext()
        {
        }
        public Relation_DbfirstContext(DbContextOptions<Relation_DbfirstContext> options)
            : base(options)
        {
        }
        // Dbset đại diện cho 1 đối tượng thực thể trong db / các bảng
        // DbSet còn chứa các phương thức hỗ trợ chúng ta tương tác với dữ liệu trong 
        // bảng đó (C-Create, R-Read, U-Update, D-Delete => CRUD)
        public virtual DbSet<Child> Children { get; set; } = null!;
        public virtual DbSet<Parent> Parents { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SHANGHAIK;Database=Relation_Dbfirst;Trusted_Connection=True;");
            }
        }
        // Connection String: Là 1 chuỗi chứa các thông tin cụ thể cần thiết để tạo kết nối với DB

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Child>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Children)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Child");
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.ToTable("Parent");

                entity.Property(e => e.ParentId).HasColumnName("parentId");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
