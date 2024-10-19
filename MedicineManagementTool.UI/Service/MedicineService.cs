using MedicineManagementTool.UI.IService;
using MedicineMAnagementTool.Common.DTOs;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MedicineManagementTool.UI.Service
{
    public class MedicineService : IMedicineService
    {        
        private readonly HttpClient _httpClient;
        
        public MedicineService(HttpClient httpClient)
        {
            _httpClient = httpClient;          
        }

        public async Task<bool> AddNewMedicine(MedicineDTO newMedicine)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(newMedicine), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Medicine/AddNewMedicine", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var responsBody = await response.Content.ReadAsStreamAsync();
                    var newUser = await JsonSerializer.DeserializeAsync<UserDTO>(responsBody, new JsonSerializerOptions
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

        public async Task<ResponseDTO<MedicineDTO>> GetAllMedicine(int sortCount, string sortColumn, int page, int quantityPerPage)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<MedicineDTO>>
                ($"api/Medicine/GetAllMedicines?Page={page}&RecordsPerPage={quantityPerPage}&sortCount={sortCount}&sortColumn={sortColumn}");
        }

        public async Task<ResponseDTO<MedicineDTO>> SearchAsync(string data, int page, int quantityPerPage)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<MedicineDTO>>
                ($"api/Medicine/SearchMedicine?data={data}&" +
                $"Page={page}&RecordsPerPage={quantityPerPage}");
        }

        public async Task<MedicineDTO> GetMedicineById(int id)
        {
            try
            {
                var apiResponse = await _httpClient.GetStreamAsync($"api/Medicine/GetMedicine/{id}");
                var student = await JsonSerializer.DeserializeAsync<MedicineDTO>(apiResponse,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> UpdateMedicineData(MedicineDTO medicineData)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(medicineData), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Medicine/UpdateMedicine?id={medicineData.Id}", itemJson);//

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> DeleteMedicineData(int id)
        {
            try
            {                
                var response = await _httpClient.DeleteAsync($"api/Medicine/Delete/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<ResponseDTO<MedicineDTO>> GetAllAvailableMedicine()
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<MedicineDTO>>($"api/Medicine/GetAllMedicine");
        }
    }
}

