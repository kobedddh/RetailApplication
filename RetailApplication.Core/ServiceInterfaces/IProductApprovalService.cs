using RetailApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Core.ServiceInterfaces
{
    public interface IProductApprovalService
    {
        Task<List<ProductApproval>> GetApprovalList();
        Task<bool> Approve(int id);
        Task<bool> Decline(int id);
    }
}
