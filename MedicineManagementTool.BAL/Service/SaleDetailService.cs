using AutoMapper;
using MedicineManagementTool.BAL.IService;
using MedicineManagementTool.DAL.Entity;
using MedicineManagementTool.DAL.IUnitOfWork;
using MedicineMAnagementTool.Common.CommonClass;
using MedicineMAnagementTool.Common.DTOs;

namespace MedicineManagementTool.BAL.Service
{
    public class SaleDetailService : ISaleDetailService
    {
        private IWrapperRepository _wrapperRepository;
        private readonly IMapper _mapper;

        public SaleDetailService(IWrapperRepository wrapperRepository, IMapper mapper)
        {
            _wrapperRepository = wrapperRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(SaleDetailDTO newSaleDetail, int currentLoggesUserId)
        {
            try
            {
                var saleDetail = _mapper.Map<SaleDetailDTO, SaleDetails>(newSaleDetail);
                var ChechMedicine = await _wrapperRepository.MedicineRepository.GetById(newSaleDetail.MedicineId);
                if (ChechMedicine == null)
                {
                    return false;
                }
                else if (ChechMedicine.Quantity < newSaleDetail.Quantity)
                {
                    return false;
                }

                saleDetail.TotalPrice = newSaleDetail.Quantity * ChechMedicine.Price;
                await _wrapperRepository.SaleDetailRepository.CreateAsync(saleDetail);
                await _wrapperRepository.Save();
                if (await UpdateSellMedicine(ChechMedicine, newSaleDetail.Quantity, currentLoggesUserId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally { _wrapperRepository.Dispose(); }
        }

        public async Task<bool> UpdateSellMedicine(Medicine ChechMedicine, int quantity, int currentLoggesUserId)
        {
            try
            {
                ChechMedicine.Quantity -= quantity;

                var medicine = await _wrapperRepository.MedicineRepository.GetById(ChechMedicine.Id);
                var medicineDTO = _mapper.Map<Medicine, MedicineDTO>(ChechMedicine);
                var result = _mapper.Map(medicineDTO, medicine);
                result.ModifiedDate = DateTime.Now;
                result.ModifiedBy = currentLoggesUserId;
                if (await _wrapperRepository.MedicineRepository.UpdateAsync(ChechMedicine.Id, result))
                {
                    await _wrapperRepository.Save();
                    return true;
                }
                else { return false; }
            }
            finally { _wrapperRepository.Dispose(); }
        }

        public async Task<SaleDetailDTO> GetByIdAsync(int id)
        {
            var saleDetail = await _wrapperRepository.SaleDetailRepository.GetByIdAsync(id);
            var saleDetailDTO = _mapper.Map<SaleDetailDTO>(saleDetail);
            return saleDetailDTO;
        }
        public async Task<List<SaleDetailDTO>> GetAllAsync()
        {
            var saleDetail = await _wrapperRepository.SaleDetailRepository.GetAllAsync();

            var saleDetailDTO = _mapper.Map<List<SaleDetailDTO>>(saleDetail) ?? null;
            return saleDetailDTO;
        }

        public async Task<ResponseDTO<SaleDetailDTO>> SearchAsync(PaginationDTO paginationDto, string data)
        {
            var salesDetails = await _wrapperRepository.SaleDetailRepository.SearchAsync(paginationDto.Page,
            paginationDto.RecordsPerPage, data);

            var sales = _mapper.Map<ResponseDTO<SaleDetailDTO>>(salesDetails) ?? null;
            return sales;
        }

        public async Task<ResponseDTO<SaleDetailDTO>> GetAllAsync(PaginationDTO paginationDto, int userId)
        {
            var salesDetails = await _wrapperRepository.SaleDetailRepository.GetAllAsync(paginationDto.Page,
                paginationDto.RecordsPerPage, userId);

            var sales = _mapper.Map<ResponseEn<SaleDetails>, ResponseDTO<SaleDetailDTO>>(salesDetails) ?? null;
            return sales;
        }
    }
}
