using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.IService
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserDTO newUser);   
        Task<int> UserIdByEmial(string emialId);        
        Task<bool> CheckLoginAsync(LoginCredentialDTO loginCredentialDTO);

        Task<ResponseDTO<UserDTO>> GetAllAsync(PaginationDTO paginationDto, int sortCount, string sortColumn);
        Task<ResponseDTO<UserDTO>> SearchAsync(PaginationDTO paginationDto, string data);
    }
}
