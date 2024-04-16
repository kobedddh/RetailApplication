using Dapper;

namespace RetailApplication.Core.Entities
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime CreatedOn { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime? ModifiedOn { get; set; }
    }
}
