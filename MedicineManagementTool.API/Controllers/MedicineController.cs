using MedicineManagementTool.BAL.IService;
using MedicineMAnagementTool.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManagementTool.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        
        [HttpPost("AddNewMedicine")]
        public async Task<ActionResult<StatusDTO>> CreateMedicine(MedicineDTO newMedicine)
        {
            try
            {
                int currentLoggesUserId = int.Parse(User.Claims.First(c => c.Type.Contains("userdata")).Value);
                bool check = await _medicineService.IsNameAndCodeUnique(newMedicine.Name, newMedicine.Code);
                if (check)
                {
                    return BadRequest("Medicine name and code must be unique");
                }
                await _medicineService.CreateAsync(newMedicine, currentLoggesUserId);
                return new StatusDTO
                {
                    StatusCode = 1,
                    StatusMessage = "Medicine Added Successful"
                };
            }
            catch
            {
                return new StatusDTO();
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<StatusDTO>> DeleteMedicine(int id)
        {
            try
            {
                int currentLoggesUserId = int.Parse(User.Claims.First(c => c.Type.Contains("userdata")).Value);
                await _medicineService.DeleteAsync(id, currentLoggesUserId);
                return new StatusDTO
                {
                    StatusCode = 1,
                    StatusMessage = "Deleted Successful"
                };
            }
            catch
            {
                return new StatusDTO();
            }
        }

        [HttpGet("GetAllMedicine")]
        public async Task<ActionResult<ResponseDTO<MedicineDTO>>> GetAllMedicine()
        {
            try
            {
                var medicine = await _medicineService.GetAllAsync();
                if (medicine == null || medicine.Count == 0)
                {
                    return new ResponseDTO<MedicineDTO>
                    {
                        StatusCode = 0,
                        StatusMessage = "DB is Empty",
                    };
                }
                return new ResponseDTO<MedicineDTO>
                {
                    StatusCode = 1,
                    ListGeneric = medicine
                };
            }
            catch
            {
                return new ResponseDTO<MedicineDTO>
                {
                    StatusCode = 0,
                    StatusMessage = "Server Error",
                };
            }

        }

        [HttpPut("UpdateMedicine")]
        public async Task<ActionResult<StatusDTO>> UpdateMedicine(int id, MedicineDTO medicineDto)
        {
            try
            {
                int currentLoggesUserId = int.Parse(User.Claims.First(c => c.Type.Contains("userdata")).Value);
                bool check = await _medicineService.IsNameAndCodeUnique(medicineDto.Name, medicineDto.Code, id);
                if (check)
                {
                    return BadRequest("Medicine Name And Code Must be unique");
                }
                if (await _medicineService.UpdateAsync(id, medicineDto, currentLoggesUserId))
                {
                    return new StatusDTO
                    {
                        StatusCode = 1,
                        StatusMessage = "Medicine Successfully Updated"
                    };
                }
                else
                {
                    return new StatusDTO
                    {
                        StatusCode = 0,
                        StatusMessage = "error occur"
                    };
                }

            }
            catch
            {
                return new StatusDTO();
            }
        }

        [HttpGet("GetMedicine/{id}")]
        public async Task<ActionResult<MedicineDTO>> GetMedicine(int id)
        {
            try
            {
                var result = await _medicineService.GetByIdAsync(id);
                if (result == null)
                {
                    return BadRequest("Not Found");
                }
                else { return result; }
            }
            catch
            {
                return BadRequest("Not found");
            }
        }

        [HttpGet("GetAllMedicines")]
        public async Task<ActionResult<ResponseDTO<MedicineDTO>>> GetAllMedicine([FromQuery] PaginationDTO paginationDto, int sortCount, string sortColumn)
        {
            try
            {
                var medicineData = await _medicineService.GetAllAsync(paginationDto, sortCount, sortColumn);
                if (medicineData == null || medicineData.Count == 0)
                {
                    return new ResponseDTO<MedicineDTO>
                    {
                        StatusCode = 0,
                        StatusMessage = ""
                    };
                }

                medicineData.StatusCode = 1;
                return medicineData;
            }
            catch
            {
                return new ResponseDTO<MedicineDTO>
                {
                    StatusCode = 0,
                    StatusMessage = "Server Error"
                };
            }
        }

        [HttpGet("SearchMedicine")]
        public async Task<ActionResult<ResponseDTO<MedicineDTO>>> SearchMedicine([FromQuery] PaginationDTO paginationDto, string data)
        {
            try
            {
                var medicineData = await _medicineService.SearchAsync(paginationDto, data);
                if (medicineData == null || medicineData.Count == 0)
                {
                    return new ResponseDTO<MedicineDTO>
                    {
                        StatusCode = 0,
                        StatusMessage = "Not Exist"
                    };
                }

                medicineData.StatusCode = 1;
                return medicineData;

            }
            catch
            {
                return new ResponseDTO<MedicineDTO>
                {
                    StatusCode = 0,
                    StatusMessage = "Server Error"
                };
            }
        }

    }
}