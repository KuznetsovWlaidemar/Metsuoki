# SDK с поддержкой .NET 9 (предварительная версия)
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Копируем файлы решения и проекты
COPY ["Metsuoki.sln", "./"]
COPY ["src/Metsuoki.API/Metsuoki.API.csproj", "src/Metsuoki.API/"]
COPY ["src/Metsuoki.Application/Metsuoki.Application.csproj", "src/Metsuoki.Application/"]
COPY ["src/Metsuoki.Domain/Metsuoki.Domain.csproj", "src/Metsuoki.Domain/"]
COPY ["src/Metsuoki.Infrastructure/Metsuoki.Infrastructure.csproj", "src/Metsuoki.Infrastructure/"]
COPY ["src/Metsuoki.Shared/Metsuoki.Shared.csproj", "src/Metsuoki.Shared/"]

# Выполняем восстановление зависимостей
RUN dotnet restore "./Metsuoki.sln"

# Копируем все файлы исходного кода
COPY . .

# Устанавливаем рабочую директорию и выполняем публикацию для конфигурации Debug
WORKDIR "/src/src/Metsuoki.API"
RUN dotnet publish -c Debug -o /app/publish

# Установить среду на Development
ENV ASPNETCORE_ENVIRONMENT=Development

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS final
WORKDIR /app

# Копируем опубликованные файлы из стадии сборки
COPY --from=build /app/publish .

# Точка входа для запуска приложения
ENTRYPOINT ["dotnet", "Metsuoki.API.dll"]
