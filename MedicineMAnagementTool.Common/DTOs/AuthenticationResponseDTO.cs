namespace MedicineMAnagementTool.Common.DTOs
{
    public class AuthenticationResponseDTO
    {
        public bool IsAuthSuccessful { get; set; }
        public string UserName { get; set; }             
        public string Token { get; set; }
    }
}
