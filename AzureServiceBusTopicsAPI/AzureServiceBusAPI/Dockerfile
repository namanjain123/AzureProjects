#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AzureServiceBusAPI/AzureServiceBusAPI.csproj", "AzureServiceBusAPI/"]
RUN dotnet restore "AzureServiceBusAPI/AzureServiceBusAPI.csproj"
COPY . .
WORKDIR "/src/AzureServiceBusAPI"
RUN dotnet build "AzureServiceBusAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzureServiceBusAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzureServiceBusAPI.dll"]