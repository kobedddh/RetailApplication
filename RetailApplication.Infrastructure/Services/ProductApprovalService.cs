using RetailApplication.Core.Entities;
using RetailApplication.Core.RepositoryInterfaces;
using RetailApplication.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Infrastructure.Services
{
    public class ProductApprovalService : IProductApprovalService
    {
        private readonly IProductRepository _productRepository;
        public ProductApprovalService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Approve(int id) => await _productRepository.Approve(id);

        public async Task<bool> Decline(int id) => await _productRepository.Decline(id);

        public async Task<List<ProductApproval>> GetApprovalList() => await _productRepository.GetApprovalList();
    }
}
