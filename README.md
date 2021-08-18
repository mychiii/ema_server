## EMa Server
Technologies used:
* .NET 5

### How to build
1. Change `ConnectionString` in `appsettings.json`
2. Execute this PM `update-database`
3. Set startup project `EMa.API` and `EMa.WebApp`
4. Port used: https://localhost:5001/ for API and https://localhost:5000 for web application.

### Project references
User Logon

| # | PhoneNumber | UserName   | ChildName | Password  | Status |
|---|-------------|------------|-----------|-----------|--------|
| 1 | 0112233445  | 0112233445 | EMa Baby  | Abc123!@# | Active |