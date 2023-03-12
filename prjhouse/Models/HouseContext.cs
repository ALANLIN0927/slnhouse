using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prjhouse.Models
{
    public partial class HouseContext : DbContext
    {
        public HouseContext()
        {
        }

        public HouseContext(DbContextOptions<HouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Business> Businesses { get; set; } = null!;
        public virtual DbSet<NormalMember> NormalMembers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=House;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Business");

                entity.Property(e => e.Businessmemberemail)
                    .HasMaxLength(50)
                    .HasColumnName("businessmemberemail");

                entity.Property(e => e.Businessmembername)
                    .HasMaxLength(50)
                    .HasColumnName("businessmembername");

                entity.Property(e => e.Businessmemberphone)
                    .HasMaxLength(50)
                    .HasColumnName("businessmemberphone");

                entity.Property(e => e.Businessmoney)
                    .HasColumnType("money")
                    .HasColumnName("businessmoney");

                entity.Property(e => e.Fid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fid");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'B')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<NormalMember>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NormalMember");

                entity.Property(e => e.AddressArea)
                    .HasMaxLength(50)
                    .HasColumnName("Address_Area");

                entity.Property(e => e.AddressCity)
                    .HasMaxLength(50)
                    .HasColumnName("Address_City");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Emailtified)
                    .HasMaxLength(50)
                    .HasColumnName("emailtified");

                entity.Property(e => e.FId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fId");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.GoogleEmail).HasMaxLength(50);

                entity.Property(e => e.LineUserid).HasMaxLength(50);

                entity.Property(e => e.MemberName).HasMaxLength(50);

                entity.Property(e => e.MemberPhotoFile).HasMaxLength(50);

                entity.Property(e => e.Membermoney)
                    .HasColumnType("money")
                    .HasColumnName("membermoney");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(N'M')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("order");

                entity.Property(e => e.Fid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fid");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Memberid).HasColumnName("memberid");

                entity.Property(e => e.Orderaddress)
                    .HasMaxLength(50)
                    .HasColumnName("orderaddress");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("date")
                    .HasColumnName("orderdate");

                entity.Property(e => e.Orderitemname)
                    .HasMaxLength(50)
                    .HasColumnName("orderitemname");

                entity.Property(e => e.Orderprice)
                    .HasMaxLength(50)
                    .HasColumnName("orderprice");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Fid);

                entity.ToTable("Product");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.HouseAddressArea)
                    .HasMaxLength(50)
                    .HasColumnName("HouseAddress_Area");

                entity.Property(e => e.HouseAddressCity)
                    .HasMaxLength(50)
                    .HasColumnName("HouseAddress_City");

                entity.Property(e => e.HouseName).HasMaxLength(50);

                entity.Property(e => e.HousePrice).HasColumnType("money");

                entity.Property(e => e.Housephoto).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
