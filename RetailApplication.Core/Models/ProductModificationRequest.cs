using RetailApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.Models
{
    public class ProductModificationRequest: Product
    {
        public required string RequestReason { get; set; }
    }
}
