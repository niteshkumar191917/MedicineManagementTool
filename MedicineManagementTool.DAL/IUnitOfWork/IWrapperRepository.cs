using MedicineManagementTool.DAL.IRepository;

namespace MedicineManagementTool.DAL.IUnitOfWork
{
    public interface IWrapperRepository : IDisposable
    {
        IMedicineRepository MedicineRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        ISaleDetailRepository SaleDetailRepository { get; }  
        Task Save();
    }
}
