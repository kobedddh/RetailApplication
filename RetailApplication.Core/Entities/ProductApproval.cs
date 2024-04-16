using RetailApplication.Core.Enums;
using RetailApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.Entities
{
    [Table("productapproval")]
    public class ProductApproval
    {
        [Key]
        public int ProductApprovalId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string RequestReason { get; set; }
        public DateTime RequestDate { get; set; }
        public ApprovalRequestType RequestType { get; set; }

        public ProductApproval()
        {
            
        }

        public ProductApproval(int? productId, ProductModificationRequest product, ApprovalRequestType requestType)
        {
            ProductId = productId;
            ProductName = product.Name;
            ProductPrice = product.Price;
            RequestReason = product.RequestReason;
            RequestDate = DateTime.UtcNow;
            RequestType = requestType;
        }
    }
}
