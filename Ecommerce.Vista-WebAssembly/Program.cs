using Ecommerce.Vista_WebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Ecommerce.Vista.Servicios.Contrato;
using Ecommerce.Vista.Servicios.Implementacion;

using Blazored.LocalStorage;
using Blazored.Toast;

using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7283/api/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

builder.Services.AddScoped<ICarritoServicio,CarritoServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IDashboardServicio, DashboardServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
