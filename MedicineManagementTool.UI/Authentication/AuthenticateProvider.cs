using Blazored.LocalStorage;
using MedicineManagementTool.UI.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MedicineManagementTool.UI.Authentication
{
    public class AuthenticateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        public AuthenticateProvider(HttpClient http, ILocalStorageService localStorage)
        {
            _httpClient = http;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("accessToken");
                if (String.IsNullOrEmpty(token))
                {
                    return _anonymous;
                }
                IEnumerable<Claim> claims = JwtParser.ParseClaimsFromJwt(token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(
                    JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
            }
            catch
            { return _anonymous; }
        }
        public async Task NotifyUserAuthentication(string token)
        {
            var authUser = new ClaimsPrincipal(new ClaimsIdentity(
                JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authUser));
            NotifyAuthenticationStateChanged(authState);
            await GetAuthenticationStateAsync();
        }


        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
