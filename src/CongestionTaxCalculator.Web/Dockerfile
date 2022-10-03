#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./CongestionTaxCalculator.Web/CongestionTaxCalculator.Web.csproj", "CongestionTaxCalculator.Web/"]
COPY ["./CongestionTaxCalculator.Application/CongestionTaxCalculator.Application.csproj", "CongestionTaxCalculator.Application/"]
COPY ["./CongestionTaxCalculator.Domain/CongestionTaxCalculator.Domain.csproj", "CongestionTaxCalculator.Domain/"]
COPY ["./CongestionTaxCalculator.Infrastructure/CongestionTaxCalculator.Infrastructure.csproj", "CongestionTaxCalculator.Infrastructure/"]
RUN dotnet restore "./CongestionTaxCalculator.Web/CongestionTaxCalculator.Web.csproj"
COPY . .
WORKDIR "/src/CongestionTaxCalculator.Web"
RUN dotnet build "CongestionTaxCalculator.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CongestionTaxCalculator.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CongestionTaxCalculator.Web.dll"]