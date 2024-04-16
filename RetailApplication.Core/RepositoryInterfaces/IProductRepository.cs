using RetailApplication.Core.Entities;
using RetailApplication.Core.Enums;
using RetailApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> Search(ProductFilters filters);
        Task<int> Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Product product);
        Task<Product> GetProductById(int id);

        Task<bool> PushToApprovalQueue(ProductApproval approvalRequest);
        Task<List<ProductApproval>> GetApprovalList();
        Task<bool> Approve(int id);
        Task<bool> Decline(int id);
    }
}
