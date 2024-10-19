using MedicineManagementTool.DAL.DataContext;
using MedicineManagementTool.DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MedicineManagementTool.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _db;
        internal DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public virtual async Task CreateAsync(T enitity)
        {
            await _dbSet.AddAsync(enitity);
        }
           
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
