using MedicineManagementTool.DAL.DataContext;
using MedicineManagementTool.DAL.IRepository;
using MedicineManagementTool.DAL.IUnitOfWork;
using MedicineManagementTool.DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace MedicineManagementTool.DAL.UnitOfWork
{
    public class WrapperRepository : IWrapperRepository
    {
        private ApplicationDbContext _dbContext;
        private UserRepository _userRepository;
        private MedicineRepository _medicineRepository;
        private UserRoleRepository _userRoleRepository;
        private SaleDetailRepository _saleDetailRepository;
        private bool _disposed;

        public WrapperRepository(ApplicationDbContext db)
        {
            _dbContext = db;
        }

        public IMedicineRepository MedicineRepository
        {
            get
            {
                if (_medicineRepository == null)
                {
                    _medicineRepository = new MedicineRepository(_dbContext);
                }
                return _medicineRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }

        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                if (_userRoleRepository == null)
                {
                    _userRoleRepository = new UserRoleRepository(_dbContext);
                }
                return _userRoleRepository;
            }
        }

        public ISaleDetailRepository SaleDetailRepository
        {
            get
            {
                if (_saleDetailRepository == null)
                {
                    _saleDetailRepository = new SaleDetailRepository(_dbContext);
                }
                return _saleDetailRepository;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
