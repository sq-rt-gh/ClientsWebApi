# ClientsWebApi

Простое ASP.NET Core Web API-приложение для работы с ĸлиентами (просмотр, добавление, редаĸтирование, удаление). 
Клиентами выступают юридичесĸие лица (ЮЛ) и индивидуальные предприниматели (ИП). 
ЮЛ могут иметь неĸоторое ĸоличество учредителей.

## Технологии
- ASP.NET Core
- EF Core
- подход Code First
- OpenAPI (Swagger)
- SQL Express LocalDB

## Структура проекта 
```
ClientsWebApi/
│   appsettings.json                          # Конфигурация
│   Program.cs                                # Точка входа в приложение 
│
├───Controllers/                              # API-контроллеры
│       ClientsController.cs
│       FoundersController.cs
│
├───Data/                                     # AppDbContext и конфигурация EF Core
│       AppDbContext.cs
│
├───DTO/                                      # Data Transfer Objects
│       ClientDto.cs
│       FounderDto.cs
│
├───Migrations/                               # EF Core миграции
│       20250630110540_Updated.cs
│       20250630110540_Updated.Designer.cs
│       AppDbContextModelSnapshot.cs
│
└───Models/                                   # EF Core модели сущностей
        Client.cs
        Founder.cs

```


## Быстрый запуск

**1. Клонирование репозитория:**

```
git clone https://github.com/sq-rt-gh/ClientsWebApi.git
cd ClientsWebApi
```

**2. Настройка БД:**

```
dotnet ef database update
```

**3. Запуск:**

```
dotnet run
```

**4. Открытие Swagger UI**

Откройте в браузере:
```
https://localhost:7226/swagger
```
