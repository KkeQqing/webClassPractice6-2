using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Yb.Dal.Base;
using Yb.Dal.Cms;
using Yb.Dal.Sys;
using Yb.Bll.Cms;
using Yb.Bll.Sys;
using Yb.Api.Controllers.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// === 配置 JWT 认证 ===
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? string.Empty)
            )
        };
    });

// === Swagger/OpenAPI 配置（含 ApiResult<T> 支持）===
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // 告诉 Swagger 如何处理泛型包装类 ApiResult<T>
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

    // 可选：避免其他泛型警告
    options.MapType<ApiResult<string>>(() => new OpenApiSchema { Type = "object" });
    options.MapType<ApiResult<int>>(() => new OpenApiSchema { Type = "object" });

    // 文档信息
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Yb.Api",
        Version = "v1"
    });
});

// === 数据库上下文 ===
builder.Services.AddDbContext<SqlDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("strConn"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("strConn"))
    );
});

// === 注册 DAL ===
builder.Services.AddScoped<YbUserDal>();
builder.Services.AddScoped<NewsDal>();

// === 注册 BLL ===
builder.Services.AddScoped<YbUserBll>();
builder.Services.AddScoped<NewsBll>();

var app = builder.Build();

// === HTTP 请求管道 ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // ⚠️ 必须在 UseAuthorization 之前
app.UseAuthorization();
app.MapControllers();

app.Run();