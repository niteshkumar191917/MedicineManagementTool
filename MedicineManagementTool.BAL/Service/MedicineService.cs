using AutoMapper;
using MedicineManagementTool.BAL.IService;
using MedicineManagementTool.DAL.Entity;
using MedicineManagementTool.DAL.IUnitOfWork;
using MedicineMAnagementTool.Common.CommonClass;
using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.Service
{
    public class MedicineService : IMedicineService
    {
        private IWrapperRepository _wrapperRepository;
        private readonly IMapper _mapper;

        public MedicineService(IWrapperRepository wrapperRepository, IMapper mapper)
        {
            _wrapperRepository = wrapperRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(MedicineDTO newMedicine, int currentLoggesUserId)
        {
            try
            {
                var medicine = _mapper.Map<MedicineDTO, Medicine>(newMedicine);
                medicine.CreatedBy = currentLoggesUserId;
                medicine.CreatedDate = DateTime.Now;
                await _wrapperRepository.MedicineRepository.CreateAsync(medicine);
                await _wrapperRepository.Save();
                return true;
            }
            finally { _wrapperRepository.Dispose(); }
        }

        public async Task<bool> DeleteAsync(int id, int currentLoggesUserId)
        {
            try
            {
                var medicine = await _wrapperRepository.MedicineRepository.GetById(id);
                medicine.DeletedBy = currentLoggesUserId;
                medicine.DeletedDate = DateTime.Now;
                medicine.IsDeleted = true;
                medicine.ModifiedDate = DateTime.Now;
                medicine.ModifiedBy = currentLoggesUserId;

                if (await _wrapperRepository.MedicineRepository.DeleteAsync(medicine))
                {
                    await _wrapperRepository.Save();
                    return true;
                }
                return false;
            }
            catch { return false; }

            finally { _wrapperRepository.Dispose(); }
        }

        public async Task<MedicineDTO> GetByIdAsync(int id)
        {
            var medicine = await _wrapperRepository.MedicineRepository.GetById(id);
            var medicineDTO = _mapper.Map<MedicineDTO>(medicine);
            return medicineDTO;
        }
        public async Task<List<MedicineDTO>> GetAllAsync()
        {
            var medicine = await _wrapperRepository.MedicineRepository.GetAllAsync();

            var medicineDto = _mapper.Map<List<MedicineDTO>>(medicine) ?? null;
            return medicineDto;
        }
        public async Task<bool> UpdateAsync(int id, MedicineDTO medicineDto, int currentLoggesUserId)
        {
            try
            {
                var medicine = await _wrapperRepository.MedicineRepository.GetById(id);
                var result = _mapper.Map(medicineDto, medicine);
                result.ModifiedDate = DateTime.Now;
                result.ModifiedBy = currentLoggesUserId;
                if (await _wrapperRepository.MedicineRepository.UpdateAsync(id, result))
                {
                    await _wrapperRepository.Save();
                    return true;
                }
                return false;
            }
            catch { return false; }

            finally { _wrapperRepository.Dispose(); }
        }

        public async Task<bool> IsNameAndCodeUnique(string name, string code, int id = 0)
        {
            var result = await _wrapperRepository.MedicineRepository.CheckNameAndCode(name, code, id);
            if (result)
            {
                return true;//if not unique
            }
            else { return false; }
        }
        public async Task<ResponseDTO<MedicineDTO>> SearchAsync(PaginationDTO paginationDto, string data)
        {
            var medicineData = await _wrapperRepository.MedicineRepository.SearchAsync(paginationDto.Page,
            paginationDto.RecordsPerPage, data);

            var medicines = _mapper.Map<ResponseDTO<MedicineDTO>>(medicineData) ?? null;
            return medicines;
        }
        public async Task<ResponseDTO<MedicineDTO>> GetAllAsync(PaginationDTO paginationDto, int sortCount, string sortColumn)
        {
            var medicineData = await _wrapperRepository.MedicineRepository.GetAllAsync(paginationDto.Page,
                paginationDto.RecordsPerPage, sortCount, sortColumn);


            var medicines = _mapper.Map<ResponseEn<Medicine>, ResponseDTO<MedicineDTO>>(medicineData) ?? null;
            return medicines;
        }
    }
}
