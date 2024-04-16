using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RetailApplication.Core.Entities;
using RetailApplication.Core.Exceptions;
using RetailApplication.Core.Models;
using RetailApplication.Core.ServiceInterfaces;

namespace RetailApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //[HttpPost]
        //public Task<IActionResult> Search(ProductFilter filter)
        //{

        //}

        [HttpPost]
        public async Task<IActionResult> Create(ProductModificationRequest product)
        {
            try
            {
                var result = await _productService.Create(product);
                return Ok($"Product created with id:{result}");
            }
            catch(ApprovalRequiredException aex)
            {
                return Ok($"Product creation request has been sent but requires approval. reason: {aex}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductModificationRequest product)
        {
            try
            {
                var result = await _productService.Update(product);
                return Ok($"Product updated successfully");
            }
            catch (ApprovalRequiredException aex)
            {
                return Ok($"Product update request has been sent but requires approval. reason: {aex}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ProductModificationRequest product)
        {
            try
            {
                var result = await _productService.DeleteRequest(product);
                return result ? 
                    Ok($"Product delete request has been sent successfully") :
                    BadRequest("Product deletion failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
