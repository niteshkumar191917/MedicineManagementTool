using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.UI.IService
{
    public interface IUserService
    {
        Task<int> GetUserId(string emailAddress);
        Task<bool> AddUser(UserDTO userCredential);
        Task<AuthenticationResponseDTO?> CheckLogin(LoginCredentialDTO userCredential);
        Task Logout();
        Task<ResponseDTO<UserDTO>> GetAllUser(int sortCount, string sortColumn, int page, int quantityPerPage);
        Task<ResponseDTO<UserDTO>> SearchAsync(string data, int page, int quantityPerPage);
    }
}
