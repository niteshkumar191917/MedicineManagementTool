using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.UI.IService
{
    public interface IMedicineService
    {
        Task<bool> AddNewMedicine(MedicineDTO newMedicine);
        Task<bool> UpdateMedicineData(MedicineDTO updatedMedicine);
        Task<bool> DeleteMedicineData(int id);
        Task<MedicineDTO> GetMedicineById(int id);
        Task<ResponseDTO<MedicineDTO>> GetAllAvailableMedicine();
        Task<ResponseDTO<MedicineDTO>> GetAllMedicine(int sortCount, string sortColumn, int page, int quantityPerPage);
        Task<ResponseDTO<MedicineDTO>> SearchAsync(string data, int page, int quantityPerPage);
    }
}
