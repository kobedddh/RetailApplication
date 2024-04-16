using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.Entities
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
