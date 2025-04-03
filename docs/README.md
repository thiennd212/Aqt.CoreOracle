# Aqt.CoreOracle Documentation

## Giới thiệu
Aqt.CoreOracle là một ứng dụng web được xây dựng trên ABP Framework, cung cấp các chức năng quản lý cơ bản cho hệ thống.

## Modules
1. [Quản lý Danh mục](./categories/README.md)
   - Quản lý loại danh mục
   - Quản lý danh mục phân cấp

## Công nghệ sử dụng
- ABP Framework 9.1.*
- ASP.NET Core 9.0
- Entity Framework Core 9.0
- Oracle Database
- LeptonX Lite Theme
- Bootstrap 5
- jQuery
- DataTables

## Cài đặt
1. Yêu cầu hệ thống:
   - .NET 9.0 SDK
   - Oracle Database 19c trở lên
   - Node.js 16.x trở lên

2. Cài đặt packages:
```bash
dotnet restore
npm install
```

3. Cấu hình database trong appsettings.json:
```json
{
  "ConnectionStrings": {
    "Default": "User Id=username;Password=password;Data Source=localhost:1521/XE"
  }
}
```

4. Chạy migrations:
```bash
dotnet ef database update
```

5. Chạy ứng dụng:
```bash
dotnet run
```

## Cấu trúc Solution
```
src/
├── Aqt.CoreOracle.Domain/           # Domain layer
├── Aqt.CoreOracle.Application/      # Application layer
├── Aqt.CoreOracle.EntityFrameworkCore/  # Database layer
└── Aqt.CoreOracle.Web/             # Web layer

test/
├── Aqt.CoreOracle.Domain.Tests/
├── Aqt.CoreOracle.Application.Tests/
└── Aqt.CoreOracle.Web.Tests/
```

## Development Guidelines
1. Code Style
   - Sử dụng C# Coding Conventions
   - Sử dụng async/await cho tất cả I/O operations
   - Implement proper exception handling
   - Sử dụng dependency injection

2. Database
   - Sử dụng migrations cho schema changes
   - Implement proper indexing
   - Tối ưu queries

3. Security
   - Implement proper authorization
   - Validate tất cả input
   - Sử dụng HTTPS
   - Implement proper logging

4. Testing
   - Unit tests cho business logic
   - Integration tests cho APIs
   - UI tests cho critical flows

## Deployment
1. Build ứng dụng:
```bash
dotnet publish -c Release
```

2. Cấu hình IIS:
   - Application Pool: No Managed Code
   - Enable Windows Authentication
   - Configure SSL certificate

3. Environment variables:
   - ASPNETCORE_ENVIRONMENT
   - ASPNETCORE_URLS
   - ConnectionStrings__Default

## Monitoring
1. Logging
   - Sử dụng Serilog
   - Log levels: Information, Warning, Error
   - Log destinations: File, Console, Seq

2. Performance Monitoring
   - Application Insights
   - Health checks
   - Database monitoring

## Support
- Issue Tracking: Azure DevOps
- Documentation: /docs
- Team Channel: Microsoft Teams 