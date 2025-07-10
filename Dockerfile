# Usa la imagen oficial de .NET SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia y restaura los paquetes
COPY *.csproj ./
RUN dotnet restore

# Copia todo el código y compílalo
COPY . ./
RUN dotnet publish -c Release -o out

# Usa imagen runtime para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Expone el puerto (ajústalo si cambiaste)
EXPOSE 5075

# Comando para iniciar la app
ENTRYPOINT ["dotnet", "LecturasAPI.dll"]