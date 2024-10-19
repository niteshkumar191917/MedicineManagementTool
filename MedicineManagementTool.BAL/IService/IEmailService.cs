using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.IService
{
    public interface IEmailService
    {
        void SendEmail(UserDTO request);
    }
}
