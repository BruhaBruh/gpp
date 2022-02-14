#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV LOGGING__CONSOLE__FORMATTERNAME=Systemd

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GPPlanetGQL.csproj", "."]
RUN dotnet restore "./GPPlanetGQL.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GPPlanetGQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GPPlanetGQL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GPPlanetGQL.dll", "--urls", "http://*:5000"]