# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar la solución
COPY ["Catalog.slnx", "./"]

# Copiar todos los .csproj manteniendo la estructura
# Usamos comodines para evitar errores de tipografía en los nombres de carpetas
COPY ["src/Order/Domain/Catalog.Order.Domain/*.csproj", "src/Order/Domain/Catalog.Order.Domain/"]
COPY ["src/Order/Application/Catalog.Order.Application/*.csproj", "src/Order/Application/Catalog.Order.Application/"]
COPY ["src/Order/Infrastructure/Adapters/Driving/Catalog.Order.API/*.csproj", "src/Order/Infrastructure/Adapters/Driving/Catalog.Order.API/"]
COPY ["src/Order/Infrastructure/Adapters/Driven/Catalog.Order.Postgre/*.csproj", "src/Order/Infrastructure/Adapters/Driven/Catalog.Order.Postgre/"]
COPY ["src/Order/Infrastructure/Adapters/Driven/Catalog.Order.kafka.Producer/*.csproj", "src/Order/Infrastructure/Adapters/Driven/Catalog.Order.kafka.Producer/"]

# Restaurar dependencias de la solución completa
RUN dotnet restore "Catalog.slnx"

# Copiar el resto del código
COPY . .

# Compilar específicamente la API
WORKDIR "/src/src/Order/Infrastructure/Adapters/Driving/Catalog.Order.API"
RUN dotnet build "Catalog.Order.API.csproj" -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish "Catalog.Order.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
# El puerto por defecto en .NET 8/10 es 8080
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Order.API.dll"]