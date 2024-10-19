using MedicineManagementTool.BAL.IService;
using MedicineMAnagementTool.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManagementTool.API.Controllers
{
    [Authorize(Roles ="User")]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailsController : ControllerBase
    {
        private readonly ISaleDetailService _saleDetailService;
        public SaleDetailsController(ISaleDetailService saleDetailService)
        {
            _saleDetailService = saleDetailService;
        }

        [HttpPost("AddSaleDetail")]
        public async Task<ActionResult<StatusDTO>> CreateSaleDetail(SaleDetailDTO saleDetail)
        {
            try
            {
                int currentLoggesUserId = int.Parse(User.Claims.First(c => c.Type.Contains("userdata")).Value);
                await _saleDetailService.CreateAsync(saleDetail, currentLoggesUserId);
                return new StatusDTO
                {
                    StatusCode = 1,
                    StatusMessage = "Item sell successfull"
                };
            }
            catch
            {
                return new StatusDTO();
            }
        }

        [HttpGet("GetAllSaleDetail")]
        public async Task<ActionResult<ResponseDTO<SaleDetailDTO>>> GetAllSalesDetail()
        {
            try
            {
                var saleDetail = await _saleDetailService.GetAllAsync();
                if (saleDetail == null || saleDetail.Count == 0)
                {
                    return new ResponseDTO<SaleDetailDTO>
                    {
                        StatusCode = 0,
                        StatusMessage = "DB is Empty",
                    };
                }
                return new ResponseDTO<SaleDetailDTO>
                {
                    StatusCode = 1,
                    ListGeneric = saleDetail
                };
            }
            catch
            {
                return new ResponseDTO<SaleDetailDTO>
                {
                    StatusCode = 0,
                    StatusMessage = "Server Error",
                };
            }
        }

        [HttpGet("GetSaleDetailById/{id}")]
        public async Task<ActionResult<SaleDetailDTO>> GetSaleDetailById(int id)
        {
            try
            {                
                var result = await _saleDetailService.GetByIdAsync(id);
                if (result == null)
                {
                    return BadRequest("Not Found");
                }
                else { return result; }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllSaleDetails")]
        public async Task<ActionResult<ResponseDTO<SaleDetailDTO>>> GetAllSales([FromQuery] PaginationDTO paginationDto,  int userId)
        {
            try
            {
                var saleDetails = await _saleDetailService.GetAllAsync(paginationDto,userId);
                if (saleDetails == null || saleDetails.Count == 0)
                {
                    return new ResponseDTO<SaleDetailDTO>
                    {
                        StatusCode = 0,
                        StatusMessage = ""
                    };
                }

                saleDetails.StatusCode = 1;
                return saleDetails;
            }
            catch
            {
                return new ResponseDTO<SaleDetailDTO>
                {
                    StatusCode = 0,
                    StatusMessage = "Server Error"
                };
            }
        }
    }
}
