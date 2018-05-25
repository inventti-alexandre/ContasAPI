FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:64999
EXPOSE 64999

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Contas.sln ./
COPY src/Contas.Services.Api/Contas.Services.Api.csproj src/Contas.Services.Api/
COPY src/Contas.Domain/Contas.Domain.csproj src/Contas.Domain/
COPY src/Contas.Domain.Core/Contas.Domain.Core.csproj src/Contas.Domain.Core/
COPY src/Contas.Infrastructure.CrossCutting.IoC/Contas.Infrastructure.CrossCutting.IoC.csproj src/Contas.Infrastructure.CrossCutting.IoC/
COPY src/Contas.Infrastructure.CrossCutting.Identity/Contas.Infrastructure.CrossCutting.Identity.csproj src/Contas.Infrastructure.CrossCutting.Identity/
COPY src/Contas.Infrastructure.Data/Contas.Infrastructure.Data.csproj src/Contas.Infrastructure.Data/
COPY src/Contas.Infrastructure.CrossCutting.AspNetFilters/Contas.Infrastructure.CrossCutting.AspNetFilters.csproj src/Contas.Infrastructure.CrossCutting.AspNetFilters/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/src/Contas.Services.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Contas.Services.Api.dll"]
