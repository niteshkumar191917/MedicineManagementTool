using MedicineManagementTool.UI.IService;
using MedicineMAnagementTool.Common.DTOs;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MedicineManagementTool.UI.Service
{
    public class SaleDetailService : ISaleDetailService
    {       
        private readonly HttpClient _httpClient;

        public SaleDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateSaleDetail(SaleDetailDTO newSaleDetail)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(newSaleDetail), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/SaleDetails/AddSaleDetail", itemJson);                
                if (response.IsSuccessStatusCode)
                {
                    var responsBody = await response.Content.ReadAsStreamAsync();
                    var newUser = await JsonSerializer.DeserializeAsync<SaleDetailDTO>(responsBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ResponseDTO<SaleDetailDTO>> GetAllSaleDetail( int page, int quantityPerPage,int userId)//int sortCount, string sortColumn,
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<SaleDetailDTO>>
                ($"api/SaleDetails/GetAllSaleDetails?Page={page}&RecordsPerPage={quantityPerPage}&userId={userId}");
        }
    }
}
