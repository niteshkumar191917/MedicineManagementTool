using MedicineManagementTool.DAL.Entity;
using MedicineMAnagementTool.Common.CommonClass;

namespace MedicineManagementTool.DAL.IRepository
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        Task<bool> DeleteAsync(Medicine medicine);
        Task<bool> UpdateAsync(int id,Medicine entity);
        Task<Medicine> GetById(int id);
        Task<bool> CheckNameAndCode(string name, string code, int id = 0);
        Task<ResponseEn<Medicine>> GetAllAsync(int page, int recordsPerPage, int sortCount, string sortColumn);
        Task<ResponseEn<Medicine>> SearchAsync(int page, int recordsPerPage, string data);
    }
}
