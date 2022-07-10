#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/UI/Clients/Buzzword.WebApp/Buzzword.WebApp.csproj", "src/UI/Clients/Buzzword.WebApp/"]
COPY ["src/Application/Buzzword.Application.Interfaces/Buzzword.Application.Interfaces.csproj", "src/Application/Buzzword.Application.Interfaces/"]
COPY ["src/Application/Buzzword.Application.Contracts/Buzzword.Application.Contracts.csproj", "src/Application/Buzzword.Application.Contracts/"]
COPY ["src/Libraries/Buzzword.Common/Buzzword.Common.csproj", "src/Libraries/Buzzword.Common/"]
COPY ["src/Application/Buzzword.Application.WebDomainServices/Buzzword.Application.WebDomainServices.csproj", "src/Application/Buzzword.Application.WebDomainServices/"]
COPY ["src/Libraries/Buzzword.HttpPolly/Buzzword.HttpPolly.csproj", "src/Libraries/Buzzword.HttpPolly/"]
COPY ["src/UI/Modules/Buzzword.Components/Buzzword.Components.csproj", "src/UI/Modules/Buzzword.Components/"]
COPY ["src/UI/Modules/Buzzword.Pages/Buzzword.Pages.csproj", "src/UI/Modules/Buzzword.Pages/"]
COPY ["src/UI/Modules/Buzzword.Shared/Buzzword.Shared.csproj", "src/UI/Modules/Buzzword.Shared/"]
RUN dotnet restore "src/UI/Clients/Buzzword.WebApp/Buzzword.WebApp.csproj"
COPY . .
WORKDIR "/src/src/UI/Clients/Buzzword.WebApp"
RUN dotnet build "Buzzword.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Buzzword.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Buzzword.WebApp.dll"]