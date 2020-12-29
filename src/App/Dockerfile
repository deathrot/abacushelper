FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine as Runtime

WORKDIR /app

COPY . .

ENTRYPOINT ["dotnet", "App.dll"]