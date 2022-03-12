using Microsoft.EntityFrameworkCore;
using ParseLib.Models.DAO;

#nullable disable

namespace ParseLib
{
    public partial class BFSStoreContext : DbContext
    {
        public BFSStoreContext()
        {
        }

        public BFSStoreContext(DbContextOptions<BFSStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<LikeJunction> LikeJunctions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInfo> ProductInfos { get; set; }
        public virtual DbSet<ProductIngredientsTableRow> ProductIngredientsTableRows { get; set; }
        public virtual DbSet<ProductLine> ProductLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NONAME;Database=bfsStore;User Id=sa;Password=sa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.AddressId, "IX_AppUsers_AddressId");

                entity.Property(e => e.Email).HasMaxLength(40);

                entity.Property(e => e.FirstName).HasMaxLength(40);

                entity.Property(e => e.Patronymic).HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.SecondName).HasMaxLength(40);

                entity.Property(e => e.UserName).HasMaxLength(40);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.AddressId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.AuthorId, "IX_Comments_AuthorId");

                entity.HasIndex(e => e.ProductId, "IX_Comments_ProductId");

                entity.Property(e => e.Content).HasMaxLength(300);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AuthorId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Description>(entity =>
            {
                entity.HasIndex(e => e.ProductInfoId, "IX_Descriptions_ProductInfoId");

                entity.HasIndex(e => e.ProductInfoId1, "IX_Descriptions_ProductInfoId1");

                entity.HasOne(d => d.ProductInfo)
                    .WithMany(p => p.DescriptionProductInfos)
                    .HasForeignKey(d => d.ProductInfoId);

                entity.HasOne(d => d.ProductInfoId1Navigation)
                    .WithMany(p => p.DescriptionProductInfoId1Navigations)
                    .HasForeignKey(d => d.ProductInfoId1);
            });

            modelBuilder.Entity<LikeJunction>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_LikeJunctions_ProductId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.LikeJunctions)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CartId, "IX_Orders_CartId");

                entity.HasIndex(e => e.CostumerId, "IX_Orders_CostumerId");

                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CartId);

                entity.HasOne(d => d.Costumer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CostumerId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.AppUserId, "IX_Products_AppUserId");

                entity.HasIndex(e => e.NavCategoryFirstLvlId, "IX_Products_NavCategoryFirstLvlId");

                entity.HasIndex(e => e.NavCategorySecondLvlId, "IX_Products_NavCategorySecondLvlId");

                entity.Property(e => e.PriceUsd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Rating).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.AppUserId);

                entity.HasOne(d => d.NavCategoryFirstLvl)
                    .WithMany(p => p.ProductNavCategoryFirstLvls)
                    .HasForeignKey(d => d.NavCategoryFirstLvlId);

                entity.HasOne(d => d.NavCategorySecondLvl)
                    .WithMany(p => p.ProductNavCategorySecondLvls)
                    .HasForeignKey(d => d.NavCategorySecondLvlId);
            });

            modelBuilder.Entity<ProductInfo>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_ProductInfos_ProductId");

                entity.HasIndex(e => e.ShortDescriptionId, "IX_ProductInfos_ShortDescriptionId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInfos)
                    .HasForeignKey(d => d.ProductId);

                entity.HasOne(d => d.ShortDescription)
                    .WithMany(p => p.ProductInfos)
                    .HasForeignKey(d => d.ShortDescriptionId);
            });

            modelBuilder.Entity<ProductIngredientsTableRow>(entity =>
            {
                entity.HasIndex(e => e.ProductInfoId, "IX_ProductIngredientsTableRows_ProductInfoId");

                entity.HasOne(d => d.ProductInfo)
                    .WithMany(p => p.ProductIngredientsTableRows)
                    .HasForeignKey(d => d.ProductInfoId);
            });

            modelBuilder.Entity<ProductLine>(entity =>
            {
                entity.HasIndex(e => e.CartId, "IX_ProductLines_CartId");

                entity.HasIndex(e => e.ProductId, "IX_ProductLines_ProductId");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.ProductLines)
                    .HasForeignKey(d => d.CartId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductLines)
                    .HasForeignKey(d => d.ProductId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
