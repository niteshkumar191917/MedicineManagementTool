using Blazored.LocalStorage;
using MedicineManagementTool.UI.Authentication;
using MedicineManagementTool.UI.IService;
using MedicineMAnagementTool.Common.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MedicineManagementTool.UI.Service
{
    public class UserService : IUserService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
   
        public UserService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _jsRuntime = jSRuntime;            
        }
        public async Task<bool> AddUser(UserDTO userCredential)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(userCredential), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/User/register", itemJson);
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
        public async Task<AuthenticationResponseDTO?> CheckLogin(LoginCredentialDTO userCredential)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(userCredential), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/User/LoginUser", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var responsBody = await response.Content.ReadAsStreamAsync();
                    var authResponse = await JsonSerializer.DeserializeAsync<AuthenticationResponseDTO>(responsBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });                                      
                    var apiResponse = await _httpClient.GetStreamAsync($"api/User/GetUserId?email={userCredential.Email}");
                    var userId = await JsonSerializer.DeserializeAsync<int>(apiResponse,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });                   

                    await _localStorage.SetItemAsync("accessToken", authResponse.Token);
                    
                    ((AuthenticateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authResponse.Token);
                    return authResponse;
                }
                await _jsRuntime.InvokeVoidAsync("ShowToastr", "error", "Wrong credentials !!");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await _jsRuntime.InvokeVoidAsync("ShowToastr", "error", "server error !!");
                return null;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("accessToken");            

            ((AuthenticateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;

        }

        public async Task<ResponseDTO<UserDTO>> GetAllUser(int sortCount, string sortColumn, int page, int quantityPerPage)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<UserDTO>>
                ($"api/User/GetAllUsers?Page={page}&RecordsPerPage={quantityPerPage}&sortCount={sortCount}&sortColumn={sortColumn}");
        }

        public async Task<ResponseDTO<UserDTO>> SearchAsync(string data, int page, int quantityPerPage)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<UserDTO>>
                ($"api/User/searchUser?data={data}&" +
                $"Page={page}&RecordsPerPage={quantityPerPage}");
        }

        public async Task<int> GetUserId(string emailAddress)
        {           
            return await _httpClient.GetFromJsonAsync<int>
               ($"api/User/GetUserId?email={emailAddress}");
        }
    }
}