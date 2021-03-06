using ExampleDapper.Data;
using ExampleDapper.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
// builder.Services.AddScoped<ICompanyRepository, CompanyRepositoryEntityFramework>();
// builder.Services.AddScoped<ICompanyRepository, CompanyRepositoryDapper>();
// builder.Services.AddScoped<ICompanyRepository, CompanyRepositoryDapperStoredProcedure>();
builder.Services.AddScoped<ICustomRepository, CustomRepository>();
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepositoryDapperContrib>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositoryDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
