using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Yb.Dal.Base;
using Yb.Dal.Cms;
using Yb.Dal.Sys;
using Yb.Bll.Cms;
using Yb.Bll.Sys;
using Yb.Api.Controllers.Base; // 👈 新增：用于 MapType

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // === 核心：告诉 Swagger 如何处理 ApiResult<T> ===
    options.MapType<ApiResult<object>>(() => new OpenApiSchema
    {
        Type = "object",
        Properties = new Dictionary<string, OpenApiSchema>
        {
            ["success"] = new OpenApiSchema { Type = "boolean" },
            ["code"] = new OpenApiSchema { Type = "integer", Format = "int32" },
            ["result"] = new OpenApiSchema { Type = "object", Nullable = true },
            ["error"] = new OpenApiSchema { Type = "string", Nullable = true },
            ["msg"] = new OpenApiSchema { Type = "string", Nullable = true },
            ["modelErrors"] = new OpenApiSchema
            {
                Type = "array",
                Items = new OpenApiSchema { Type = "object" },
                Nullable = true
            },
            ["extra"] = new OpenApiSchema { Type = "object", Nullable = true }
        }
    });

    // 可选：支持常见泛型（避免警告）
    options.MapType<ApiResult<string>>(() => new OpenApiSchema { Type = "object" });
    options.MapType<ApiResult<int>>(() => new OpenApiSchema { Type = "object" });

    // 文档信息
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Yb.Api",
        Version = "v1"
    });
});

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