#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class Cart
    {
        public Cart()
        {
            Orders = new HashSet<Order>();
            ProductLines = new HashSet<ProductLine>();
        }

        public long Id { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductLine> ProductLines { get; set; }
    }
}
