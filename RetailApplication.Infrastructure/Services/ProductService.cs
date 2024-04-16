using RetailApplication.Core.Entities;
using RetailApplication.Core.Enums;
using RetailApplication.Core.Exceptions;
using RetailApplication.Core.Models;
using RetailApplication.Core.RepositoryInterfaces;
using RetailApplication.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApplication.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private async Task<string?> RequireApproval(Product product, ApprovalRequestType requestType)
        {
            if (product.Price > 10000)
                throw new ArgumentOutOfRangeException("Product price cannot be more than $10000");

            if (product.Price > 5000)
                return "Product price exceed $5000";

            if (requestType != ApprovalRequestType.Update)
                return null;

            var existingProduct = await _productRepository.GetProductById(product.ProductId);
            if(existingProduct.Price * 1.5m < product.Price)
                return "Product price is more than 50% of its previous price.";

            return null;
        }
        public async Task<int> Create(ProductModificationRequest product)
        {
            var approvalReason = await RequireApproval(product, ApprovalRequestType.Create);
            if(string.IsNullOrEmpty(approvalReason))
                return await _productRepository.Create(product);

            var approvalRequest = new ProductApproval(null, product, ApprovalRequestType.Create);
            _ = await _productRepository.PushToApprovalQueue(approvalRequest);
            throw new ApprovalRequiredException(approvalReason);
        }

        public async Task<bool> Update(ProductModificationRequest product)
        {
            var approvalReason = await RequireApproval(product, ApprovalRequestType.Update);
            if (string.IsNullOrEmpty(approvalReason))
                return await _productRepository.Update(product);

            var approvalRequest = new ProductApproval(product.ProductId, product, ApprovalRequestType.Update);
            _ = await _productRepository.PushToApprovalQueue(approvalRequest);
            throw new ApprovalRequiredException(approvalReason);
        }

        public async Task<bool> DeleteRequest(ProductModificationRequest product)
        {
            var approvalRequest = new ProductApproval(product.ProductId, product, ApprovalRequestType.Delete);
            return await _productRepository.PushToApprovalQueue(approvalRequest);
        }

        public async Task<IEnumerable<Product>> Search(ProductFilters filters) => await _productRepository.Search(filters);


    }
}
