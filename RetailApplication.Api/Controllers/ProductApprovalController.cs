using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailApplication.Core.ServiceInterfaces;

namespace RetailApplication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApprovalController : ControllerBase
    {
        private readonly IProductApprovalService _productApprovalService;
        public ProductApprovalController(IProductApprovalService productApprovalService)
        {
            _productApprovalService = productApprovalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productApprovalService.GetApprovalList();
            return Ok(result);
        }

        [HttpPut]
        [Route("approve/{id:int}")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _productApprovalService.Approve(id);
            return result ? Ok(result) : BadRequest();
        }

        [HttpPut]
        [Route("decline/{id:int}")]
        public async Task<IActionResult> Decline(int id)
        {
            var result = await _productApprovalService.Decline(id);
            return result ? Ok(result) : BadRequest();
        }
    }
}
