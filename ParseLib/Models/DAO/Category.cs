#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class Category
    {
        public Category()
        {
            ProductNavCategoryFirstLvls = new HashSet<Product>();
            ProductNavCategorySecondLvls = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string ValueEn { get; set; }
        public string ValueRu { get; set; }
        public string ValueUk { get; set; }

        public virtual ICollection<Product> ProductNavCategoryFirstLvls { get; set; }
        public virtual ICollection<Product> ProductNavCategorySecondLvls { get; set; }
    }
}
