using MedicineManagementTool.BAL.IAuthenticate;
using MedicineManagementTool.DAL.DataContext;
using MedicineMAnagementTool.Common.CommonClass;
using MedicineMAnagementTool.Common.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicineManagementTool.BAL.Authentication
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _db;
        private IConfiguration _configuration;
        public AuthenticateService(IOptions<AppSettings> appSettings, ApplicationDbContext db, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _db = db;
            _configuration = configuration;
        }

        public AuthenticationResponseDTO Authenticate(string username, string password)
        {
            var responseDTO = new AuthenticationResponseDTO();
            var user = _db.User.FirstOrDefault(x => x.Email.ToLower() == username.ToLower() && x.Password == password);

            if (user == null)
            {
                responseDTO.IsAuthSuccessful = false;
                responseDTO.UserName = username;
                
                responseDTO.Token = "";
                return responseDTO;
            }

            string role = _db.Role.FirstOrDefault(x => x.RoleId == (_db.UserRole.FirstOrDefault(x => x.UserId == user.Id)).RoleId).RoleName;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email,user.Email),                    
                    new Claim(ClaimTypes.UserData,user.Id.ToString()),                    
                                      
                    new Claim(ClaimTypes.Role,role)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            responseDTO.Token = result;
            responseDTO.UserName = user.Name;
            responseDTO.IsAuthSuccessful = true;
            return responseDTO;
        }
    }
}
