using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Repository;
using WorkSchedulePlaner.Application.ShiftTiles.AssignShift;
using WorkSchedulePlaner.Application.ShiftTiles.DeleteShift;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Persistence;
using WorkSchedulePlaner.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SchedulePlaner")));

builder.Services.AddScoped<IRepository<WorkSchedule>,Repository<WorkSchedule>>();
builder.Services.AddScoped<IRepository<Employee>,Repository<Employee>>();
builder.Services.AddScoped<IRepository<ShiftTile>,Repository<ShiftTile>>();
builder.Services.AddScoped<IRepository<EmployeeShift>,Repository<EmployeeShift>>();
builder.Services.AddScoped<IEmployeeShiftRepository,EmployeeShiftRepository>();
builder.Services.AddScoped<IWorkScheduleRepository,WorkScheduleRepository>();
builder.Services.AddTransient<AssignShift>();
builder.Services.AddTransient<DeleteShift>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
