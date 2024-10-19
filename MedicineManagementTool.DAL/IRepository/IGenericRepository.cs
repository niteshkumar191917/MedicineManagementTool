namespace MedicineManagementTool.DAL.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T enitity);        
        Task<IEnumerable<T>> GetAllAsync();      
    }
}
