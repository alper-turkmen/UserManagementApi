FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserManagementAPI.csproj", "./"]
RUN dotnet restore "UserManagementAPI.csproj"
COPY . .
RUN dotnet build "UserManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagementAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagementAPI.dll"]