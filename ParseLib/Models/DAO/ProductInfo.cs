#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class ProductInfo
    {
        public ProductInfo()
        {
            DescriptionProductInfoId1Navigations = new HashSet<Description>();
            DescriptionProductInfos = new HashSet<Description>();
            ProductIngredientsTableRows = new HashSet<ProductIngredientsTableRow>();
        }

        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Lang { get; set; }
        public int? ShortDescriptionId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Description ShortDescription { get; set; }
        public virtual ICollection<Description> DescriptionProductInfoId1Navigations { get; set; }
        public virtual ICollection<Description> DescriptionProductInfos { get; set; }
        public virtual ICollection<ProductIngredientsTableRow> ProductIngredientsTableRows { get; set; }
    }
}
