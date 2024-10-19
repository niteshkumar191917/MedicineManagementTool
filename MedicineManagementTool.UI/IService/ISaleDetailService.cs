using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.UI.IService
{
    public interface ISaleDetailService
    {
        Task<bool> CreateSaleDetail(SaleDetailDTO newSaleDetail);
        Task<ResponseDTO<SaleDetailDTO>> GetAllSaleDetail(int page, int quantityPerPage, int userId);
    }
}
