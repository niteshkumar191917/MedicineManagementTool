using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.IService
{
    public interface IMedicineService
    {
        Task<bool> CreateAsync(MedicineDTO newMedicine, int currentLoggesUserId);
        Task<bool> DeleteAsync(int id, int currentLoggesUserId);
        Task<bool> UpdateAsync(int id, MedicineDTO medicineDto, int currentLoggesUserId);
        Task<List<MedicineDTO>> GetAllAsync();
        Task<MedicineDTO> GetByIdAsync(int id);
        Task<bool> IsNameAndCodeUnique(string name, string code, int id = 0);
        Task<ResponseDTO<MedicineDTO>> GetAllAsync(PaginationDTO paginationDto, int sortCount, string sortColumn);
        Task<ResponseDTO<MedicineDTO>> SearchAsync(PaginationDTO paginationDto, string data);
    }
}
