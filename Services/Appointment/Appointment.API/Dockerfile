FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["Services/Appointment/Appointment.API/Appointment.API.csproj", "Services/Appointment/Appointment.API/"]
COPY ["Services/Appointment/Appointment.Application/Appointment.Application.csproj", "Services/Appointment/Appointment.Application/"]
COPY ["Services/Appointment/Appointment.Domain/Appointment.Domain.csproj", "Services/Appointment/Appointment.Domain/"]
COPY ["Services/Appointment/Appointment.Persistence/Appointment.Persistence.csproj", "Services/Appointment/Appointment.Persistence/"]
RUN dotnet restore "./Services/Appointment/Appointment.API/./Appointment.API.csproj"
COPY . .
WORKDIR "/src/Services/Appointment/Appointment.API"
RUN dotnet build "Appointment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Appointment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointment.API.dll"]