using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Produ_project.Secure.Data;

namespace Produ_project.Enitity
{
    public partial class Produ_projectContext : DbContext
    {
        public Produ_projectContext()
        {
        }

        public Produ_projectContext(DbContextOptions<Produ_projectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArtWork> ArtWorks { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<MainProduct> MainProducts { get; set; } = null!;
        public virtual DbSet<Quality> Qualities { get; set; } = null!;
        public virtual DbSet<SupplierInFo> SupplierInFos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SR6CI3Q\\SQLEXPRESS;Initial Catalog=Produ_project;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtWork>(entity =>
            {
                entity.HasKey(e => e.Awid)
                    .HasName("PK__ArtWork__07CE6A4C6351BBC9");

                entity.ToTable("ArtWork");

                entity.Property(e => e.Awid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AWID");

                entity.Property(e => e.ImgagesUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MainProductId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MainProductID");

                entity.Property(e => e.NameAw)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NameAW");

                entity.HasOne(d => d.MainProduct)
                    .WithMany(p => p.ArtWorks)
                    .HasForeignKey(d => d.MainProductId)
                    .HasConstraintName("FK__ArtWork__MainPro__3C69FB99");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoriesId)
                    .HasName("PK__Categori__EFF907B040C2A572");

                entity.Property(e => e.CategoriesId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CategoriesID");

                entity.Property(e => e.NameCategories)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MainProduct>(entity =>
            {
                entity.ToTable("MainProduct");

                entity.Property(e => e.MainProductId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MainProductID");

                entity.Property(e => e.CategoriesId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CategoriesID");

                entity.Property(e => e.NameMp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NameMP");

                entity.HasOne(d => d.Categories)
                    .WithMany(p => p.MainProducts)
                    .HasForeignKey(d => d.CategoriesId)
                    .HasConstraintName("FK__MainProdu__Categ__398D8EEE");
            });

            modelBuilder.Entity<Quality>(entity =>
            {
                entity.HasKey(e => e.Awid)
                    .HasName("PK__Quality__07CE6A4CD218F140");

                entity.ToTable("Quality");

                entity.Property(e => e.Awid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AWID");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.Pcscustomer).HasColumnName("PCScustomer");

                entity.Property(e => e.Size)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("size");

                entity.HasOne(d => d.Aw)
                    .WithOne(p => p.Quality)
                    .HasForeignKey<Quality>(d => d.Awid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quality__AWID__3F466844");
            });

            modelBuilder.Entity<SupplierInFo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SupplierInFo");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Address_");

                entity.Property(e => e.CategoriesId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CategoriesID");

                entity.Property(e => e.Certificate)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Certificate_");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Customized)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateQa)
                    .HasColumnType("datetime")
                    .HasColumnName("DateQA");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EstablishedYear)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExportUs).HasColumnName("ExportUS");

                entity.Property(e => e.Leadtime)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MainProductId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MainProductID");

                entity.Property(e => e.Moq)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MOQ");

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReviewQa).HasColumnName("ReviewQA");

                entity.Property(e => e.SampleProcess)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SlId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SlID");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.Websitelink)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Categories)
                    .WithMany()
                    .HasForeignKey(d => d.CategoriesId)
                    .HasConstraintName("FK__SupplierI__Categ__4316F928");

                entity.HasOne(d => d.MainProduct)
                    .WithMany()
                    .HasForeignKey(d => d.MainProductId)
                    .HasConstraintName("FK__SupplierI__MainP__440B1D61");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__SupplierI__UserI__44FF419A");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.NameUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Password_");

                entity.Property(e => e.Role).HasColumnName("Role_");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
