using Blazored.LocalStorage;
using MedicineManagementTool.UI;
using MedicineManagementTool.UI.Authentication;
using MedicineManagementTool.UI.Helper;
using MedicineManagementTool.UI.IService;
using MedicineManagementTool.UI.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7064/") });
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<ISaleDetailService, SaleDetailService>();
builder.Services.AddScoped<AuthenticateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
