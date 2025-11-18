包：
# 在 Yb.Dal 项目中安装
cd Yb.Dal
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.10
dotnet add package Microsoft.Extensions.Configuration.Abstractions --version 8.0.0

# 在 Yb.Api 项目中安装（用于 UserSecrets）
cd ../Yb.Api
dotnet add package Microsoft.Extensions.Configuration.UserSecrets --version 8.0.0

配置 User Secrets（连接字符串）
在 Yb.Api 项目目录下执行：

bash

dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:strConn" "server=localhost;port=3306;database=RGZNN;user=root;password=123456;AllowPublicKeyRetrieval=True;ConvertZeroDateTime=True"
✅ 替换 your_password 为你的 MySQL 密码。确保 MySQL 已启动，且允许远程连接（如本地开发，通常没问题）。
3. 验证是否成功
运行：

dotnet user-secrets list
应看到输出包含你的连接字符串。


全局安装 dotnet-ef 工具
运行以下命令进行安装（需要 .NET SDK 8.0）：

powershell
dotnet tool install --global dotnet-ef --version 8.0.10

确保项目引用了必要的包
在你的 启动项目（这里是 Yb.Api.csproj）中，必须包含以下 NuGet 包：
通过以下命令添加（在 Yb.Api 目录下）：

powers
编辑
cd ../Yb.Api
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.10


在解决方案根目录（YbSolution）下执行：

powershell
编辑
# 在 YbSolution 目录下
# 添加迁移
dotnet ef migrations add InitialCreate --project Yb.Dal/Yb.Dal.csproj --startup-project Yb.Api/Yb.Api.csproj

# 更新数据库
dotnet ef database update --project Yb.Dal/Yb.Dal.csproj --startup-project Yb.Api/Yb.Api.csproj


#添加添加Newtonsoft.Json引用
在包管理器中搜索Newtonsoft.Json并安装。仅为webApi添加即可