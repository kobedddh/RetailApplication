using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RetailApplication.Core.Models
{
    public class ProductFilters
    {
        public string? ProductName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? PostedDateFrom { get; set; }
        public DateTime? PostedDateTo { get; set; }
        [JsonIgnore]
        public bool HasNoFilter => string.IsNullOrEmpty(ProductName) && !MinPrice.HasValue && !MaxPrice.HasValue 
            && !PostedDateFrom.HasValue && !PostedDateTo.HasValue;
    }
}
