#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class Order
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public int OrderStatus { get; set; }
        public string CostumerId { get; set; }
        public string Comment { get; set; }
        public DateTime DateTime { get; set; }
        public bool GiftWrap { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual AppUser Costumer { get; set; }
    }
}
