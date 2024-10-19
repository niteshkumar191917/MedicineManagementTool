using MedicineManagementTool.UI.IService;
using MedicineMAnagementTool.Common.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MedicineManagementTool.UI.Pages.UserComponent
{
    public partial class AddUser
    {
        private bool IsProcessing { get; set; } = false;
        protected UserDTO newUser { get; set; } = new UserDTO();
       
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private IUserService UserService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected async void HandleInvalidSubmit()
        {
            await JSRuntime.InvokeVoidAsync("ShowToastr", "error", "enter some valid input !!");
        }

        protected async void HandleValidRequest()
        {
            IsProcessing = true;
            var result = await UserService.AddUser(newUser);
            if (result)
            {
                NavigationManager.NavigateTo("/allUsers");
                await JSRuntime.InvokeVoidAsync("ShowToastr", "added", "new user added successfully!!");
            }
            else
            {
                await JSRuntime.InvokeVoidAsync("ShowToastr", "error", "email already in use");                
            }
            IsProcessing = false;
            StateHasChanged();
        }
    }
}