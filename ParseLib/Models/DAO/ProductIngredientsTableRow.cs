#nullable disable

namespace ParseLib.Models.DAO
{
    public partial class ProductIngredientsTableRow
    {
        public long Id { get; set; }
        public string FirstColumn { get; set; }
        public string SecondColumn { get; set; }
        public string ThirdColumn { get; set; }
        public long? ProductInfoId { get; set; }

        public virtual ProductInfo ProductInfo { get; set; }
    }
}
