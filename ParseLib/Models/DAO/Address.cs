#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class Address
    {
        public Address()
        {
            AppUsers = new HashSet<AppUser>();
        }

        public long Id { get; set; }
        public string PostalOffice { get; set; }
        public string City { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
