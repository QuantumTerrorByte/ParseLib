using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class Product
    {
        [NotMapped]
        public string UrlFormParser { get; set; }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Amount { get; set; }
        public string Brand { get; set; }
        public decimal PriceUsd { get; set; }
        public string ImgUrl { get; set; }
        public decimal Rating { get; set; }
        public long Popularity { get; set; }
        public string AppUserId { get; set; }
        
        public virtual AppUser AppUser { get; set; }
        public virtual Category NavCategoryFirstLvl { get; set; }
        public virtual Category NavCategorySecondLvl { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ProductInfo> ProductInfos { get; set; }
        
        #region UnusedProps

        public virtual ICollection<LikeJunction> LikeJunctions { get; set; }

        public virtual ICollection<ProductLine> ProductLines { get; set; }

        public int? NavCategoryFirstLvlId { get; set; }

        public int? NavCategorySecondLvlId { get; set; }

        public Product()
        {
            Comments = new HashSet<Comment>();
            LikeJunctions = new HashSet<LikeJunction>();
            ProductInfos = new HashSet<ProductInfo>();
            ProductLines = new HashSet<ProductLine>();
        }
        
        #endregion

    }
}