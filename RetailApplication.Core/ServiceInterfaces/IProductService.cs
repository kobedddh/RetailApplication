using RetailApplication.Core.Entities;
using RetailApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.ServiceInterfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Search(ProductFilters filters);
        Task<int> Create(ProductModificationRequest product);
        Task<bool> Update(ProductModificationRequest product);
        Task<bool> DeleteRequest(ProductModificationRequest product);
    }
}
