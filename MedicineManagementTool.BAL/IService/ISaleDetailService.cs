using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.IService
{
    public interface ISaleDetailService
    {
        Task<bool> CreateAsync(SaleDetailDTO newSaleDetail, int currentLoggesUserId);
        Task<List<SaleDetailDTO>> GetAllAsync();
        Task<SaleDetailDTO> GetByIdAsync(int id);
        Task<ResponseDTO<SaleDetailDTO>> GetAllAsync(PaginationDTO paginationDto,  int userId);
        Task<ResponseDTO<SaleDetailDTO>> SearchAsync(PaginationDTO paginationDto, string data);
    }
}
