using AutoMapper;
using RetailApplication.Core.Entities;
using RetailApplication.Core.Enums;
using RetailApplication.Core.IDBHelpers;
using RetailApplication.Core.Models;
using RetailApplication.Core.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RetailApplication.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(IDBHelper dBHelper, IMapper mapper):base(dBHelper) 
        {
            _mapper = mapper;
        }
        public async Task<int> Create(Product product)
        {
            return await DBHelper.InsertAsync(product);
        }

        public async Task<bool> Delete(Product product)
        {
            return (await DBHelper.DeleteAsync(product)) > 0;
        }

        public async Task<bool> Update(Product product)
        {
            return (await DBHelper.UpdateAsync(product)) > 0;
        }

        public async Task<IEnumerable<Product>> Search(ProductFilters filters)
        {
            var whereClause = filters.HasNoFilter ? "" :
                    $@"where 1=1 
                        {(string.IsNullOrEmpty(filters.ProductName) ? "" : " and Name like @productName")}
                        {(!filters.MinPrice.HasValue ? "" : " and Price < @minPrice")}
                        {(!filters.MaxPrice.HasValue ? "" : " and Price > @maxPrice")}
                        {(!filters.PostedDateFrom.HasValue ? "" : " and CreatedOn > @postedDateFrom")}
                        {(!filters.PostedDateTo.HasValue ? "" : " and CreatedOn < @postedDateTo")}
                        ";
            var sql = $@"select * from product
                         {whereClause}
                         order by 1 desc;";
            var param = new
            {
                productName = '%' + filters.ProductName + '%',
                minPrice = filters.MinPrice,
                maxPrice = filters.MaxPrice,
                postedDateFrom = filters.PostedDateFrom,
                postedDateTo = filters.PostedDateTo
            };
            return await DBHelper.ListAsync<Product>(sql, param);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await DBHelper.GetAsync<Product>(id);
        }

        public async Task<bool> PushToApprovalQueue(ProductApproval approvalRequest)
        {
            var approvalId = await DBHelper.InsertAsync(approvalRequest);
            return approvalId > 0;
        }

        public async Task<List<ProductApproval>> GetApprovalList() => await DBHelper.GetAllAsync<ProductApproval>();

        public async Task<bool> Approve(int id)
        {
            var approvalRequest = await GetProductApprovalById(id);
            if (approvalRequest == null) return false;

            bool modifyResult = false;
            var product = _mapper.Map<ProductApproval, Product>(approvalRequest);
            using (TransactionScope trxScope = new TransactionScope())
            {
                modifyResult = approvalRequest.RequestType switch
                {
                    ApprovalRequestType.Create => (await Create(product)) > 0,
                    ApprovalRequestType.Update => await Update(product),
                    ApprovalRequestType.Delete => await Delete(product),
                    _ => throw new NotImplementedException()
                };
                if(modifyResult)
                    await DBHelper.DeleteAsync(approvalRequest);

                trxScope.Complete();
            }

            return modifyResult;
        }

        public async Task<bool> Decline(int id)
        {
            var approvalRequest = await GetProductApprovalById(id);
            if (approvalRequest == null) return false;
            return (await DBHelper.DeleteAsync(approvalRequest)) > 0;
        }

        private async Task<ProductApproval> GetProductApprovalById(int id) => await DBHelper.GetAsync<ProductApproval>(id);
    }
}
