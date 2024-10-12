# Usa una imagen base oficial de .NET 8 SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establece el directorio de trabajo en el contenedor
WORKDIR /src

# Copia el archivo de solución y los archivos del proyecto a la imagen
COPY *.sln .
COPY DateShowerApplication/DateShowerApplication.csproj DateShowerApplication/

# Restaura las dependencias necesarias (usa los proyectos .csproj)
RUN dotnet restore

# Copia el resto de los archivos del proyecto a la imagen
COPY DateShowerApplication/. DateShowerApplication/

# Cambia el directorio de trabajo al proyecto
WORKDIR /src/DateShowerApplication

# Compila la aplicación en modo Release (más óptimo para producción)
RUN dotnet publish -c Release -o /app

# Usa una imagen base ligera para ejecutar la aplicación (runtime de .NET)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Establece el directorio de trabajo en el contenedor para ejecutar la app
WORKDIR /app

# Copia los archivos compilados desde la etapa de build
COPY --from=build /app .

# Expone los puertos HTTP (5175) y HTTPS (7273) especificados en launchsettings.json
EXPOSE 5175
EXPOSE 7273

# Establece las variables de entorno para el entorno de Desarrollo
ENV ASPNETCORE_ENVIRONMENT=Development

# Comando para ejecutar la aplicación (usará tanto HTTP como HTTPS)
ENTRYPOINT ["dotnet", "DateShowerApplication.dll"]
