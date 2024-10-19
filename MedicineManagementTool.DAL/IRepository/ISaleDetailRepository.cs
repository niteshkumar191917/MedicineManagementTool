using MedicineManagementTool.DAL.Entity;
using MedicineMAnagementTool.Common.CommonClass;

namespace MedicineManagementTool.DAL.IRepository
{
    public interface ISaleDetailRepository : IGenericRepository<SaleDetails>
    {
        Task<SaleDetails> GetByIdAsync(int id);
        Task<ResponseEn<SaleDetails>> GetAllAsync(int page, int recordsPerPage, int userId);
        Task<ResponseEn<SaleDetails>> SearchAsync(int page, int recordsPerPage, string data);
    }
}
