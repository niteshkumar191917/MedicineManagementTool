using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.IAuthenticate
{
    public interface IAuthenticateService
    {
        AuthenticationResponseDTO Authenticate(string username, string password);
    }
}
