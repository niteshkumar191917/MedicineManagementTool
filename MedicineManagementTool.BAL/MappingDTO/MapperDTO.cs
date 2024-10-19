using AutoMapper;
using MedicineManagementTool.DAL.Entity;
using MedicineMAnagementTool.Common.CommonClass;
using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.MappingDTO
{
    public class MapperDTO : Profile
    {
        public MapperDTO()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<MedicineDTO, Medicine>().ReverseMap();
            CreateMap<SaleDetailDTO, SaleDetails>().ReverseMap();
            CreateMap<ResponseDTO<User>, ResponseDTO<UserDTO>>().ReverseMap();
            CreateMap<ResponseDTO<Medicine>, ResponseDTO<MedicineDTO>>().ReverseMap();
            CreateMap<ResponseDTO<SaleDetails>, ResponseDTO<SaleDetailDTO>>().ReverseMap();
            CreateMap<ResponseEn<User>, ResponseDTO<UserDTO>>().ReverseMap();
            CreateMap<ResponseEn<Medicine>, ResponseDTO<MedicineDTO>>().ReverseMap();
            CreateMap<ResponseEn<SaleDetails>, ResponseDTO<SaleDetailDTO>>().ReverseMap();
        }
    }
}