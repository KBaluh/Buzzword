#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Application/Buzzword.Application.API/Buzzword.Application.API.csproj", "src/Application/Buzzword.Application.API/"]
COPY ["src/Application/Buzzword.Applicaiton.DomainServices/Buzzword.Applicaiton.DomainServices.csproj", "src/Application/Buzzword.Applicaiton.DomainServices/"]
COPY ["src/Application/Buzzword.Application.Domain/Buzzword.Application.Domain.csproj", "src/Application/Buzzword.Application.Domain/"]
COPY ["src/Application/Buzzword.Application.Interfaces/Buzzword.Application.Interfaces.csproj", "src/Application/Buzzword.Application.Interfaces/"]
COPY ["src/Application/Buzzword.Application.Contracts/Buzzword.Application.Contracts.csproj", "src/Application/Buzzword.Application.Contracts/"]
COPY ["src/Libraries/Buzzword.Common/Buzzword.Common.csproj", "src/Libraries/Buzzword.Common/"]
RUN dotnet restore "src/Application/Buzzword.Application.API/Buzzword.Application.API.csproj"
COPY . .
WORKDIR "/src/src/Application/Buzzword.Application.API"
RUN dotnet build "Buzzword.Application.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Buzzword.Application.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Buzzword.Application.API.dll"]