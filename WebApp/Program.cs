using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Endpoint=https://azureskysconfig.azconfig.io;Id=1rTO;Secret=b7DzbxURpuCGV/dSNmkg35q/sNsAdcV+CtvGSk2I/a4=";

builder.Host.ConfigureAppConfiguration(builder => builder.AddAzureAppConfiguration(connectionString));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
