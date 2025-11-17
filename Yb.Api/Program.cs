using Microsoft.EntityFrameworkCore;
using Yb.Dal.Base;
using Yb.Dal.Cms;
using Yb.Dal.Sys;
using Yb.Bll.Cms;
using Yb.Bll.Sys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Context
builder.Services.AddDbContext<SqlDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("strConn"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("strConn"))
    );
});

// Register DAL
builder.Services.AddScoped<YbUserDal>();
builder.Services.AddScoped<NewsDal>();

// Register BLL
builder.Services.AddScoped<YbUserBll>();
builder.Services.AddScoped<NewsBll>();

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