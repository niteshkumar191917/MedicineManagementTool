using MedicineManagementTool.DAL.Entity;
using MedicineMAnagementTool.Common.CommonClass;

namespace MedicineManagementTool.DAL.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> CheckLoginAsync(string email, string password);
        Task<bool> FindByEmailAsync(string email);
        Task<int> FindUserIDAsync(string email);
        Task<ResponseEn<User>> GetAllAsync(int page, int recordsPerPage, int sortCount, string sortColumn);
        Task<ResponseEn<User>> SearchAsync(int page, int recordsPerPage, string data);
    }
}