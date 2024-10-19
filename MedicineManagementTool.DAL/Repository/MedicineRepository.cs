using MedicineManagementTool.DAL.DataContext;
using MedicineManagementTool.DAL.Entity;
using MedicineManagementTool.DAL.IRepository;
using MedicineMAnagementTool.Common.CommonClass;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MedicineManagementTool.DAL.Repository
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        private DbSet<Medicine> entities;

        public MedicineRepository(ApplicationDbContext db) : base(db)
        {
            entities = db.Set<Medicine>();
        }

        public async Task<bool> DeleteAsync(Medicine medicine)
        {            
            if (medicine != null)
            {                              
                entities.Update(medicine);
                return true;
            }
            return false;
        }

        public async Task<Medicine> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Medicine entity)
        {
            var medicine = await entities.FindAsync(id);
            if (medicine != null)
            {               
                entities.Update(entity);
                return true;
            }
            return false;
        }

        public async Task<bool> CheckNameAndCode(string name, string code,int id=0)
        {
            if (id != 0)
            {
                var result = await entities.FirstOrDefaultAsync(x => (x.Name == name || x.Code == code) && x.Id!=id );
                if (result != null)
                {
                    return true;
                }
                return false;
            }
            else
            {
                var result = await entities.FirstOrDefaultAsync(x => x.Name == name || x.Code == code);
                if (result != null)
                {
                    return true;
                }
                return false;
            }
        }
        public override async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await entities.Where(x=>x.IsDeleted==false && x.Quantity>0).ToListAsync();
        }
        public async Task<ResponseEn<Medicine>> SearchAsync(int page, int recordsPerPage, string data)
        {
            IQueryable<Medicine> query = entities.Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(data))
            {
                query = query.OrderByDescending(e => e.Id);
               // query = query.OrderBy(e => e.Name);
                query = query.Where(e => e.Name.ToLower().Contains(data.ToLower())
                    || e.Code.ToLower().Contains(data.ToLower()) || e.Price.ToString() == data
                    || e.Quantity.ToString() == data);
            }
            if (query != null)
            {
                return new ResponseEn<Medicine>
                {
                    Count = await query.CountAsync(),
                    ListGeneric = await query.Skip((page - 1) * recordsPerPage).
                    Take(recordsPerPage).ToListAsync(),
                };
            }
            return null;
        }
        public async Task<ResponseEn<Medicine>> GetAllAsync(int page, int recordsPerPage, int sortCount, string sortColumn)
        {
            var query = entities.Where(x=>x.IsDeleted==false).AsQueryable().OrderByDescending(x => x.Id);
            if (sortCount == 1)
            {
                query = query.OrderBy($"{sortColumn} {"ASC"}");
            }
            else
            {
                query = query.OrderBy($"{sortColumn} {"DESC"}");
            }

            return new ResponseEn<Medicine>
            {
                Count = await query.CountAsync(),
                ListGeneric = await query.Skip((page - 1) * recordsPerPage).
                Take(recordsPerPage).ToListAsync()
            };
        }
    }
}
