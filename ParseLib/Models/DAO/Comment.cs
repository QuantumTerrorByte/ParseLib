#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class Comment
    {
        public long Id { get; set; }
        public string AuthorId { get; set; }
        public long ProductId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public bool IsEdited { get; set; }

        public virtual AppUser Author { get; set; }
        public virtual Product Product { get; set; }
    }
}
