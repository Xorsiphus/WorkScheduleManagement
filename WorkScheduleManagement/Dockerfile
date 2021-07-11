FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WorkScheduleManagement/WorkScheduleManagement.csproj", "WorkScheduleManagement/"]
RUN dotnet restore "WorkScheduleManagement/WorkScheduleManagement.csproj"
COPY . .
WORKDIR "/src/WorkScheduleManagement"
RUN dotnet build "WorkScheduleManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkScheduleManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkScheduleManagement.dll"]
