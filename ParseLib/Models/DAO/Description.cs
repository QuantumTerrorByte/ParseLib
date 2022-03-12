#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class Description
    {
        public Description()
        {
            ProductInfos = new HashSet<ProductInfo>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public long? ProductInfoId { get; set; }
        public long? ProductInfoId1 { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
        public virtual ProductInfo ProductInfoId1Navigation { get; set; }
        public virtual ICollection<ProductInfo> ProductInfos { get; set; }
    }
}
