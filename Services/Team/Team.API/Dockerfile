FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["Services/Team/Team.API/Team.API.csproj", "Services/Team/Team.API/"]
COPY ["Services/Team/Team.Application/Team.Application.csproj", "Services/Team/Team.Application/"]
COPY ["Services/Team/Team.Domain/Team.Domain.csproj", "Services/Team/Team.Domain/"]
COPY ["Services/Team/Team.Persistence/Team.Persistence.csproj", "Services/Team/Team.Persistence/"]
RUN dotnet restore "Services/Team/Team.API/Team.API.csproj"
COPY . .
WORKDIR "/src/Services/Team/Team.API"
RUN dotnet build "Team.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Team.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Team.API.dll"]