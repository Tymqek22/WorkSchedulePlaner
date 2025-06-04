using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSchedulePlaner.Application.Abstractions.Messaging;
using WorkSchedulePlaner.Application.Abstractions.Repository;
using WorkSchedulePlaner.Application.DTOs;
using WorkSchedulePlaner.Application.Features.Employees.Commands.AddEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.DeleteEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Commands.UpdateEmployee;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetByIdFromSchedule;
using WorkSchedulePlaner.Application.Features.Employees.Queries.GetFromSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.CreateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.DeleteSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Commands.UpdateSchedule;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleById;
using WorkSchedulePlaner.Application.Features.Schedules.Queries.GetScheduleDetailsFromPeriod;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.AssignShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.DeleteShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Commands.UpdateShift;
using WorkSchedulePlaner.Application.Features.ShiftTiles.Queries.GetTileById;
using WorkSchedulePlaner.Domain.Entities;
using WorkSchedulePlaner.Infrastructure.Dispatching;
using WorkSchedulePlaner.Infrastructure.Identity.DbInitializer;
using WorkSchedulePlaner.Infrastructure.Identity.Models;
using WorkSchedulePlaner.Infrastructure.Persistence;
using WorkSchedulePlaner.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SchedulePlaner")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
	.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IRepository<Employee>,Repository<Employee>>();
builder.Services.AddScoped<IRepository<ShiftTile>,Repository<ShiftTile>>();
builder.Services.AddScoped<IRepository<EmployeeShift>,Repository<EmployeeShift>>();
builder.Services.AddScoped<IRepository<ScheduleUser>,Repository<ScheduleUser>>();
builder.Services.AddScoped<IWorkScheduleRepository,WorkScheduleRepository>();
builder.Services.AddScoped<IShiftTileRepository,ShiftTileRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<ICommandHandler<AddEmployeeCommand,AddEmployeeResult>,AddEmployeeCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateEmployeeCommand,UpdateEmployeeResult>,UpdateEmployeeCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteEmployeeCommand,DeleteEmployeeResult>,DeleteEmployeeCommandHandler>();
builder.Services.AddScoped<ICommandHandler<AssignShiftCommand,AssignShiftResult>,AssignShiftCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteShiftCommand,DeleteShiftResult>,DeleteShiftCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateShiftCommand,UpdateShiftResult>,UpdateShiftCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateScheduleCommand,CreateScheduleResult>,CreateScheduleCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateScheduleCommand,UpdateScheduleResult>,UpdateScheduleCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeleteScheduleCommand,DeleteScheduleResult>,DeleteScheduleCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetFromScheduleQuery,List<EmployeeDto>>,GetFromScheduleQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetByIdFromScheduleQuery,EmployeeDto>,GetByIdFromScheduleQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetScheduleByIdQuery,WorkScheduleDto>,GetScheduleByIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetScheduleDetailsFromPeriodQuery,WorkScheduleDto>,GetScheduleDetailsFromPeriodQueryHandler>();
builder.Services.AddScoped<IQueryHandler<GetTileByIdQuery,ShiftTileDto>,GetTileByIdQueryHandler>();
builder.Services.AddScoped<ICommandDispatcher,CommandDispatcher>();
builder.Services.AddScoped<IQueryDispatcher,QueryDispatcher>();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
	var services = scope.ServiceProvider;
	await DbInitializer.SeedRoles(services);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
