using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Yb.Api.Controllers.Base;
using Yb.Bll.Cms;
using Yb.Bll.Sys;
using Yb.Dal.Base;
using Yb.Dal.Cms;
using Yb.Dal.Sys;

var builder = WebApplication.CreateBuilder(args);

// === 添加内存缓存 ===
builder.Services.AddMemoryCache();

// Add services to the container.
builder.Services.AddControllers();

// === CORS 配置 ===
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Vue CLI 默认地址
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// === JWT 认证配置 ===
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

// === Swagger/OpenAPI 配置 ===
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // 支持 ApiResult<T>
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

    // JWT 安全方案
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT授权(数据将在请求头中进行传输) 在下方输入 Bearer {token} 即可，注意两者之间有空格",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // 全局应用 JWT 安全要求（所有接口都需要认证）
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
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
builder.Services.AddScoped<AuthBll>();

// === 构建应用 ===
var app = builder.Build();

// === 开发环境启用 Swagger ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// === 中间件管道 ===
app.UseHttpsRedirection();
app.UseRouting();           // 必须在 UseCors / UseAuthentication 前
app.UseCors("AllowVueDev");  // 启用策略
app.UseAuthentication();    // 必须在 UseAuthorization 前
app.UseAuthorization();
app.MapControllers();

// === 启动应用 ===
app.Run();