using MedicineManagementTool.UI.IService;
using MedicineMAnagementTool.Common.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace MedicineManagementTool.UI.Pages.LoginLogOutScreen
{
    public partial class Login
    {
        private bool IsProcessing { get; set; } = false;
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUserService UserService { get; set; }
        private LoginCredentialDTO UserCredentials = new LoginCredentialDTO();
        [CascadingParameter] public Task<AuthenticationState> authStateTask { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var user = (await authStateTask).User;
            if (user.Identity.IsAuthenticated)                
            {
                if (user.IsInRole("Admin"))
                { 
                    
                    NavigationManager.NavigateTo("/allMedicine");
                }
                else
                {
                    NavigationManager.NavigateTo("/allMedicineUser");
                }
                
            }
        }
        private async Task HandleLoginCredentials()
        {
            IsProcessing = true;
            try
            {
                var result = await UserService.CheckLogin(UserCredentials);
                if (result.IsAuthSuccessful == true)
                {
                    var user = (await authStateTask).User;
                    if (user.IsInRole("Admin"))
                    {
                        NavigationManager.NavigateTo("/allMedicine", forceLoad: true);
                    }
                    else
                    {
                        NavigationManager.NavigateTo("/allMedicineUser", forceLoad: true);
                    }                    
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("ShowToastr", "error", "Wrong Credentials!!");
                }
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Error: {ex.Message}");
            }
            IsProcessing = false;
            StateHasChanged();
        }
        protected async void HandleInvalidSubmit()
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error", "enter some valid input !!");
        }
    }
}