#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Application/Buzzword.Application.API/Buzzword.Application.API.csproj", "src/Application/Buzzword.Application.API/"]
COPY ["src/Application/Buzzword.Application.Contracts/Buzzword.Application.Contracts.csproj", "src/Application/Buzzword.Application.Contracts/"]
RUN dotnet restore "src/Application/Buzzword.Application.API/Buzzword.Application.API.csproj"
COPY . .
WORKDIR "/src/src/Application/Buzzword.Application.API"
RUN dotnet build "Buzzword.Application.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Buzzword.Application.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Buzzword.Application.API.dll"]