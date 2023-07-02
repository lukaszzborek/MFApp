FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MF/MFApp/MFApp.csproj", "MF/MFApp/"]
RUN dotnet restore "MF/MFApp/MFApp.csproj"
COPY . .
WORKDIR "/src/MF/MFApp"
RUN dotnet build "MFApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MFApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MFApp.dll"]
