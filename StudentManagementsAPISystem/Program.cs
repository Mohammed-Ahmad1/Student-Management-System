using Microsoft.EntityFrameworkCore;
using Students_Management_System.Data_Access_Layer.Data;
using StudentAPIDataAccessLayer.DataAccess;
using StudentAPIBusinessLayer.ServiceLayer;
using StudentAPIDataAccessLayer.Entities;


var builder = WebApplication.CreateBuilder(args);

#region Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#endregion

#region EF Core + DbContext (DI)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
#endregion

#region DI (DAL + BLL)
builder.Services.AddScoped<StudentsDataAccess>();
builder.Services.AddScoped<StudentsService>();

builder.Services.AddScoped<CoursesDataAccess>();
builder.Services.AddScoped<CourseService>();

builder.Services.AddScoped<EnrollmentsDataAccess>();
builder.Services.AddScoped<EnrollmentsService>();


builder.Services.AddScoped<DepartmentDataAccess>();
builder.Services.AddScoped<DepartmentService>();


builder.Services.AddScoped<InstructorsDataAccess>();
builder.Services.AddScoped<InstructorsService>();

#endregion








// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
