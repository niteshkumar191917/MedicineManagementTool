using AutoMapper;
using MedicineManagementTool.BAL.Encyption;
using MedicineManagementTool.BAL.IService;
using MedicineManagementTool.DAL.Entity;
using MedicineManagementTool.DAL.IUnitOfWork;
using MedicineMAnagementTool.Common.CommonClass;
using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.Service
{
    public class UserService : IUserService
    {
        private IWrapperRepository _wrapperRepository;
        private readonly IMapper _mapper;

        public UserService(IWrapperRepository wrapperRepository, IMapper mapper)
        {
            _wrapperRepository = wrapperRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(UserDTO newUser)
        {
            try
            {
                var register = _mapper.Map<UserDTO, User>(newUser);
                bool result = await _wrapperRepository.UserRepository.FindByEmailAsync(register.Email);
                if (!result)
                {
                    register.Password = AesOperation.EncryptString(newUser.Password);
                    await _wrapperRepository.UserRepository.CreateAsync(register);
                    await _wrapperRepository.Save();
                    if (await CreateUserRole(register.Email))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else return false;
            }
            finally { _wrapperRepository.Dispose(); }
        }

        public async Task<bool> CreateUserRole(string email)
        {
            try
            {
                var registerId = _wrapperRepository.UserRepository.FindUserIDAsync(email);
                UserRole newEntry = new UserRole()
                {
                    RoleId = 2,
                    UserId = registerId.Result
                };
                await _wrapperRepository.UserRoleRepository.CreateAsync(newEntry);
                await _wrapperRepository.Save();
                return true;
            }
            finally { _wrapperRepository.Dispose(); }
        }

        public async Task<bool> CheckLoginAsync(LoginCredentialDTO loginCredentialDTO)
        {
            loginCredentialDTO.Password = AesOperation.EncryptString(loginCredentialDTO.Password);
            if (await _wrapperRepository.UserRepository.CheckLoginAsync(loginCredentialDTO.Email, loginCredentialDTO.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ResponseDTO<UserDTO>> SearchAsync(PaginationDTO paginationDto, string data)
        {
            var userData = await _wrapperRepository.UserRepository.SearchAsync(paginationDto.Page,
                paginationDto.RecordsPerPage, data);

            var users = _mapper.Map<ResponseDTO<UserDTO>>(userData) ?? null;
            return users;
        }

        public async Task<ResponseDTO<UserDTO>> GetAllAsync(PaginationDTO paginationDto, int sortCount, string sortColumn)
        {
            var users = await _wrapperRepository.UserRepository.GetAllAsync(paginationDto.Page,
                paginationDto.RecordsPerPage, sortCount, sortColumn);

            var userData = _mapper.Map<ResponseEn<User>, ResponseDTO<UserDTO>>(users) ?? null;
            return userData;
        }

        public async Task<int> UserIdByEmial(string emialId)
        {
            var result = await _wrapperRepository.UserRepository.FindUserIDAsync(emialId);
            return result;
        }
    }
}