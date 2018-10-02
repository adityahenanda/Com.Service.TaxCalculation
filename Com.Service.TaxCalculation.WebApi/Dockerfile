FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Com.Service.TaxCalculation.WebApi/Com.Service.TaxCalculation.WebApi.csproj Com.Service.TaxCalculation.WebApi/
COPY Com.Service.TaxCalculation.Lib/Com.Service.TaxCalculation.Lib.csproj Com.Service.TaxCalculation.Lib/
RUN dotnet restore
COPY . .
WORKDIR /src/Com.Service.TaxCalculation.WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Com.Service.TaxCalculation.WebApi.dll"]
