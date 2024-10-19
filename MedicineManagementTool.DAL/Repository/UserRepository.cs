using MedicineManagementTool.DAL.DataContext;
using MedicineManagementTool.DAL.Entity;
using MedicineManagementTool.DAL.IRepository;
using MedicineMAnagementTool.Common.CommonClass;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MedicineManagementTool.DAL.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private DbSet<User> entities;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            entities = db.Set<User>();
        }

        public async Task<int> FindUserIDAsync(string email)
        {
            var result = await entities.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if(result == null)
            {
                return 0;
            }
            else
            {
                return result.Id;
            }
        }

        public async Task<bool> FindByEmailAsync(string email)
        {
            var result = await entities.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> CheckLoginAsync(string email, string password)
        {
            bool reult = await entities.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == password).AnyAsync();
            if (reult)
            {
                return true;
            }
            return false;
        }

        public async Task<ResponseEn<User>> SearchAsync(int page, int recordsPerPage, string data)
        {
            IQueryable<User> query = entities;

            if (!string.IsNullOrEmpty(data))
            {
                query = query.OrderByDescending(e => e.Id);
                //query = query.OrderBy(e => e.Name);
                query = query.Where(e => e.Name.ToLower().Contains(data.ToLower())
                    || e.Email.ToLower().Contains(data.ToLower()) );
            }
            if (query != null)
            {
                return new ResponseEn<User>
                {
                    Count = await query.CountAsync(),
                    ListGeneric = await query.Skip((page - 1) * recordsPerPage).
                    Take(recordsPerPage).ToListAsync(),
                };
            }
            return null;
        }
        public async Task<ResponseEn<User>> GetAllAsync(int page, int recordsPerPage, int sortCount, string sortColumn)
        {
            var query = entities.AsQueryable().OrderByDescending(e=>e.Id);
            if (sortCount == 1)
            {
                query = query.OrderBy($"{sortColumn} {"ASC"}");
            }
            else
            {
                query = query.OrderBy($"{sortColumn} {"DESC"}");
            }
            
            return new ResponseEn<User>
            {
                Count = await query.CountAsync(),
                ListGeneric = await query.Skip((page - 1) * recordsPerPage).
                Take(recordsPerPage).ToListAsync()
            };
        }
    }
}