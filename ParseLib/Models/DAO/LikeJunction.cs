#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class LikeJunction
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
