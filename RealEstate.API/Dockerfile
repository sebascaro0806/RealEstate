#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RealEstate.API/RealEstate.API.csproj", "RealEstate.API/"]
COPY ["RealEstate.Application/RealEstate.Application.csproj", "RealEstate.Application/"]
COPY ["RealEstate.Domain/RealEstate.Domain.csproj", "RealEstate.Domain/"]
COPY ["RealEstate.Infrastructure/RealEstate.Infrastructure.csproj", "RealEstate.Infrastructure/"]
RUN dotnet restore "RealEstate.API/RealEstate.API.csproj"
COPY . .
WORKDIR "/src/RealEstate.API"
RUN dotnet build "RealEstate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RealEstate.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealEstate.API.dll"]