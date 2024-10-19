using MedicineManagementTool.DAL.DataContext;
using MedicineManagementTool.DAL.Entity;
using MedicineManagementTool.DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MedicineManagementTool.DAL.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
      
        public UserRoleRepository(ApplicationDbContext db) : base(db)
        {
            
        }
    }
}
