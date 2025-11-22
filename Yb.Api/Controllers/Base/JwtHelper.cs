using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yb.Model.Sys;
using Microsoft.Extensions.Configuration;

namespace Yb.Api.Controllers.Base
{
    public static class JwtHelper // 👈 建议改为 static class（原为普通类）
    {
        public static string IssueJWT(TokenModel tokenModel)
        {
            // 🔸 修复：.NET 8 要求显式指定 optional 参数
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var iss = configuration["Jwt:Issuer"];
            var aud = configuration["Jwt:Audience"];
            var secret = configuration["Jwt:SecretKey"];

            if (string.IsNullOrEmpty(iss) || string.IsNullOrEmpty(aud) || string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT 配置缺失：请检查 appsettings.json 中的 Jwt 节点");
            }

            var now = DateTime.UtcNow;
            var expires = now.AddSeconds(6000); // 6000秒 ≈ 100分钟

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Id ?? Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expires).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, iss),
                new Claim(JwtRegisteredClaimNames.Aud, aud),
                new Claim("UserCD", tokenModel.UserCD ?? ""),
                new Claim("UserNM", tokenModel.UserNM ?? ""),
                new Claim("DepartmentCD", tokenModel.DepartmentCD ?? ""),
                new Claim("DepartmentNM", tokenModel.DepartmentNM ?? ""),
                new Claim("RoleID", tokenModel.RoleID ?? ""),
                new Claim("RoleName", tokenModel.RoleName ?? ""),
                new Claim("DataAuthority", tokenModel.DataAuthority.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                audience: aud, // 👈 显式指定 audience（更规范）
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds
            );

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(jwt);
        }

        public static TokenModel SerializeJWT(string jwtStr)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(jwtStr);
            var tm = new TokenModel
            {
                Id = jwtToken.Id,
                Account = jwtToken.Claims.FirstOrDefault(c => c.Type == "Account")?.Value ?? "",
                UserNM = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserNM")?.Value ?? "",
                RoleID = jwtToken.Claims.FirstOrDefault(c => c.Type == "RoleID")?.Value ?? "",
                DataAuthority = int.TryParse(
                    jwtToken.Claims.FirstOrDefault(c => c.Type == "DataAuthority")?.Value, out var da) ? da : 0
                // 可继续扩展其他字段
            };
            return tm;
        }
    }
}