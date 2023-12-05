using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities_FurnitureWebsite;

public partial class FurnitureWebsiteContext : DbContext
{
    public FurnitureWebsiteContext()
    {
    }

    public FurnitureWebsiteContext(DbContextOptions<FurnitureWebsiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Argument> Arguments { get; set; }

    public virtual DbSet<Colour> Colours { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<LikeProduct> LikeProducts { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<Revenue> Revenues { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserVoucher> UserVouchers { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }
    public object LikeProductModel { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FurnitureWebsite;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Argument>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ARGUMENTS");

            entity.Property(e => e.MinPaid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RatingLower).HasDefaultValueSql("((0.0))");
            entity.Property(e => e.RatingUpper).HasDefaultValueSql("((5.0))");
            entity.Property(e => e.RegAge).HasDefaultValueSql("((18))");
        });

        modelBuilder.Entity<Colour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__COLOURS__3214EC27072A535D");

            entity.ToTable("COLOURS");

            entity.HasIndex(e => e.Barcode, "UQ__COLOURS__177800D353F7868C").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__COLOURS__737584F6FACEB3AA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Barcode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId }).HasName("PK__COMMENTS__DCC800C2FACD619D");

            entity.ToTable("COMMENTS", tb => tb.HasTrigger("trg_CheckRating"));

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Date)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMMENTS__Produc__7A672E12");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMMENTS__UserID__7B5B524B");
        });

        modelBuilder.Entity<LikeProduct>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId }).HasName("PK__LIKE_PRO__DCC800C2D8E6FEBD");

            entity.ToTable("LIKE_PRODUCT");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnType("date");

            entity.HasOne(d => d.Product).WithMany(p => p.LikeProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIKE_PROD__Produ__02FC7413");

            entity.HasOne(d => d.User).WithMany(p => p.LikeProducts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIKE_PROD__UserI__03F0984C");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PERMISSI__3214EC27AB68CE4B");

            entity.ToTable("PERMISSIONS");

            entity.HasIndex(e => e.Name, "UQ__PERMISSI__737584F6DA4D4782").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCTS__3214EC27B4F54226");

            entity.ToTable("PRODUCTS");

            entity.HasIndex(e => e.Name, "UQ__PRODUCTS__737584F6BEF10BD8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImgDirect)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Info)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Material)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasMany(d => d.Tags).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PRODUCT_T__TagID__76969D2E"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PRODUCT_T__Produ__75A278F5"),
                    j =>
                    {
                        j.HasKey("ProductId", "TagId").HasName("PK__PRODUCT___625B09491DD18F71");
                        j.ToTable("PRODUCT_TAG");
                        j.IndexerProperty<int>("ProductId").HasColumnName("ProductID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RECEIPTS__3214EC27C3791FB3");

            entity.ToTable("RECEIPTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnType("date");
            entity.Property(e => e.Paid).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValueSql("('Shopping')");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RECEIPTS__UserID__7C4F7684");

            entity.HasMany(d => d.Vouchers).WithMany(p => p.Receipts)
                .UsingEntity<Dictionary<string, object>>(
                    "ReceiptVoucher",
                    r => r.HasOne<Voucher>().WithMany()
                        .HasForeignKey("VoucherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RECEIPT_V__Vouch__7E37BEF6"),
                    l => l.HasOne<Receipt>().WithMany()
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RECEIPT_V__Recei__7D439ABD"),
                    j =>
                    {
                        j.HasKey("ReceiptId", "VoucherId").HasName("PK__RECEIPT___CFA6239CE44907E8");
                        j.ToTable("RECEIPT_VOUCHER");
                        j.IndexerProperty<int>("ReceiptId").HasColumnName("ReceiptID");
                        j.IndexerProperty<int>("VoucherId").HasColumnName("VoucherID");
                    });
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => new { e.ReceiptId, e.ProductId, e.ColourId }).HasName("PK__RECEIPT___7902254A58880E92");

            entity.ToTable("RECEIPT_DETAIL", tb => tb.HasTrigger("trg_AfterInsertUpdateReceiptDetail"));

            entity.Property(e => e.ReceiptId).HasColumnName("ReceiptID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ColourId).HasColumnName("ColourID");
            entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Colour).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ColourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECEIPT_D__Colou__797309D9");

            entity.HasOne(d => d.Product).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECEIPT_D__Produ__787EE5A0");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECEIPT_D__Recei__778AC167");
        });

        modelBuilder.Entity<Revenue>(entity =>
        {
            entity.HasKey(e => new { e.Year, e.Month }).HasName("PK__REVENUES__FB142A43C17571A6");

            entity.ToTable("REVENUES");

            entity.Property(e => e.Year).HasDefaultValueSql("(datepart(year,getdate()))");
            entity.Property(e => e.Month).HasDefaultValueSql("(datepart(month,getdate()))");
            entity.Property(e => e.Value).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ROLES__3214EC27CE832123");

            entity.ToTable("ROLES");

            entity.HasIndex(e => e.Name, "UQ__ROLES__737584F64DC49E9C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ROLE_PERM__Permi__02084FDA"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ROLE_PERM__RoleI__01142BA1"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__ROLE_PER__6400A18AE00B5EE3");
                        j.ToTable("ROLE_PERMISSION");
                        j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("PermissionID");
                    });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TAGS__3214EC274CF53395");

            entity.ToTable("TAGS");

            entity.HasIndex(e => e.Name, "UQ__TAGS__737584F61EA283BB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3214EC27A18B8102");

            entity.ToTable("USERS", tb => tb.HasTrigger("trg_CheckAge"));

            entity.HasIndex(e => e.UserName, "UQ__USERS__C9F284566631E276").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PassWord)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnType("date");
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__USER_ROLE__RoleI__00200768"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__USER_ROLE__UserI__7F2BE32F"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__USER_ROL__AF27604FE7EF232F");
                        j.ToTable("USER_ROLE");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                    });
        });

        modelBuilder.Entity<UserVoucher>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.VoucherId }).HasName("PK__USER_VOU__14262B30694CC728");

            entity.ToTable("USER_VOUCHER");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VoucherId).HasColumnName("VoucherID");
            entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.User).WithMany(p => p.UserVouchers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_VOUC__UserI__04E4BC85");

            entity.HasOne(d => d.Voucher).WithMany(p => p.UserVouchers)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_VOUC__Vouch__05D8E0BE");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VOUCHERS__3214EC27AB11043A");

            entity.ToTable("VOUCHERS");

            entity.HasIndex(e => e.Name, "UQ__VOUCHERS__737584F6AE1918F3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValueSql("('Percentage')");
            entity.Property(e => e.ValidDate).HasColumnType("date");
            entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
