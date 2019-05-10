FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
COPY ./bin/Release/netcoreapp2.2/publish/ app/
COPY ./bin/Release/netcoreapp2.2/publish/ClientApp ClientApp/
ENTRYPOINT ["dotnet", "app/babelhealth.dll"]
