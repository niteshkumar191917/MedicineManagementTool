using MedicineManagementTool.DAL.DataContext;
using MedicineManagementTool.DAL.Entity;
using MedicineManagementTool.DAL.IRepository;
using MedicineMAnagementTool.Common.CommonClass;
using Microsoft.EntityFrameworkCore;

namespace MedicineManagementTool.DAL.Repository
{
    public class SaleDetailRepository : GenericRepository<SaleDetails>,ISaleDetailRepository
    {
        private DbSet<SaleDetails> entities;
        public SaleDetailRepository(ApplicationDbContext db) : base(db)
        {
            entities = db.Set<SaleDetails>();
        }
        
        public async Task<SaleDetails> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }
        public async Task<ResponseEn<SaleDetails>> SearchAsync(int page, int recordsPerPage, string data)
        {
            IQueryable<SaleDetails> query = entities;

            if (!string.IsNullOrEmpty(data))
            {
                query = query.OrderByDescending(e => e.SaleId);
                query = query.Where(e => e.SaleId.ToString() == data
                    || e.UserId.ToString() == data || e.MedicineId.ToString() == data
                    || e.Quantity.ToString() == data || e.TotalPrice.ToString()==data);
            }
            if (query != null)
            {
                return new ResponseEn<SaleDetails>
                {
                    Count = await query.CountAsync(),
                    ListGeneric = await query.Skip((page - 1) * recordsPerPage).
                    Take(recordsPerPage).ToListAsync(),
                };
            }
            return null;
        }
        public async Task<ResponseEn<SaleDetails>> GetAllAsync(int page, int recordsPerPage, int userId)
        {
            var query = entities.AsQueryable().OrderByDescending(x=>x.SaleId).Where(x=>x.UserId==userId);
          
            return new ResponseEn<SaleDetails>
            {
                Count = await query.CountAsync(),
                ListGeneric = await query.Skip((page - 1) * recordsPerPage).
                Take(recordsPerPage).ToListAsync()
            };
        }
    }
}