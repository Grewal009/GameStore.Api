# Game Store API

## Starting SQL Server

```powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2019-latest

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest


docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=password' -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name azuresqledge mcr.microsoft.com/azure-sql-edge

docker run -d --name sql_server_test -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=password" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest

docker run -d --name sql_server -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=passwordA1." -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest

docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Password1." -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```
