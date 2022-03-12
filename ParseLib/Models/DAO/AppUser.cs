#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class AppUser
    {
        public AppUser()
        {
            Comments = new HashSet<Comment>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public long AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
