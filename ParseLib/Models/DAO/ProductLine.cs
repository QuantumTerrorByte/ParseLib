#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class ProductLine
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public long Amount { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
