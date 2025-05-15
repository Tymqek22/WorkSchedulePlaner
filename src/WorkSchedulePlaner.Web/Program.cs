using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Application.ShiftTiles.AssignShift;
using WorkSchedulePlaner.Application.ShiftTiles.DeleteShift;
using WorkSchedulePlaner.Application.ShiftTiles.UpdateShift;
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
builder.Services.AddTransient<UpdateShift>();
builder.Services.AddScoped<ICommandHandler<AddEmployeeCommand,AddEmployeeResult>,AddEmployeeCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateEmployeeCommand,UpdateEmployeeResult>,UpdateEmployeeComandHandler>();
builder.Services.AddScoped<IQueryHandler<GetFromScheduleQuery,List<Employee>>,GetFromScheduleQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetByIdFromScheduleQuery,Employee>,GetByIdFromScheduleQueryHandler>();

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
