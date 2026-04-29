# Лабораторная работа №4
## Подключение БД к WPF приложению. Добавление, чтение, редактирование, удаление данных из БД с помощью WPF приложения

### Цель работы
Написать WPF приложение, реализовать функции добавления, чтения, редактирования и удаления данных из БД.

### Задание
1. Ознакомиться с теоретической частью лабораторной работы
2. Создать WPF приложение по теме курсовой работы и подключить базу данных
3. Осуществить следующие функции: вывод, добавление, удаление и изменение данных для **трех сущностей** (Clients, Campaigns, Employees)
4. Оформить отчет по результатам выполненной работы

### Структура базы данных
База данных "MarketingDB" содержит следующие таблицы:

**Три основные сущности для CRUD-операций:**

1. **Clients (Клиенты)**
   - ClientID (INT, PRIMARY KEY, IDENTITY)
   - FullName (NVARCHAR(255), NOT NULL)
   - Email (NVARCHAR(255))
   - Phone (NVARCHAR(20))
   - Address (NVARCHAR(500))

2. **Campaigns (Кампании)**
   - CampaignID (INT, PRIMARY KEY, IDENTITY)
   - CampaignName (NVARCHAR(255), NOT NULL)
   - ClientID (INT, NOT NULL, FOREIGN KEY → Clients)
   - Budget (DECIMAL(15,2))
   - StartDate (DATE)
   - EndDate (DATE)
   - Status (NVARCHAR(50))

3. **Employees (Сотрудники)**
   - EmployeeID (INT, PRIMARY KEY, IDENTITY)
   - FullName (NVARCHAR(255), NOT NULL)
   - Position (NVARCHAR(100))
   - Email (NVARCHAR(255))
   - HourlyRate (DECIMAL(10,2))

**Дополнительные таблицы (без CRUD):**
- Channels (Каналы)
- Tasks (Задачи)
- Expenses (Расходы)
- Results (Результаты)

### Инструкция по установке и запуску

#### 1. Требования
- .NET 8.0 SDK или выше
- Microsoft SQL Server (Express или выше)
- Visual Studio 2022 (рекомендуется) или VS Code

#### 2. Настройка базы данных
1. Откройте SQL Server Management Studio (SSMS)
2. Подключитесь к вашему серверу SQL Server
3. Выполните скрипт `CreateDatabase.sql` для создания базы данных и таблиц
4. Скрипт автоматически создаст базу данных "MarketingDB" и добавит тестовые данные

**Важно:** Если имя вашего сервера отличается от `localhost\SQLEXPRESS`, обновите строку подключения в файле `DatabaseContext.cs`:
```csharp
_connectionString = "Server=DESKTOP-2G2083T\\SQLEXPRESS;Database=MarketingDB;Trusted_Connection=True;TrustServerCertificate=True;";
```

#### 3. Сборка проекта
```bash
cd MarketingDB_WPF
dotnet restore
dotnet build
```

#### 4. Запуск приложения
```bash
dotnet run
```

Или откройте проект в Visual Studio и нажмите F5.

### Реализованный функционал

#### Главное окно (MainWindow)
- Выбор сущности для работы через ComboBox (Clients, Campaigns, Employees)
- Отображение данных в DataGrid
- Кнопки управления: Добавить, Изменить, Удалить, Обновить

#### Окно добавления/редактирования (AddEditWindow)
- Динамическая форма в зависимости от выбранной сущности
- Валидация вводимых данных
- Поддержка DatePicker для дат

#### CRUD-операции
1. **Create (Создание)** - добавление новых записей через форму
2. **Read (Чтение)** - отображение всех записей в таблице
3. **Update (Обновление)** - редактирование выбранных записей
4. **Delete (Удаление)** - удаление с подтверждением

### Структура проекта
```
MarketingDB_WPF/
├── App.xaml                    # Файл приложения
├── App.xaml.cs
├── MainWindow.xaml             # Главное окно
├── MainWindow.xaml.cs
├── AddEditWindow.xaml          # Окно добавления/редактирования
├── AddEditWindow.xaml.cs
├── Models.cs                   # Классы моделей (Client, Campaign, Employee)
├── DatabaseContext.cs          # Контекст базы данных с CRUD-методами
├── CreateDatabase.sql          # SQL скрипт для создания БД
└── MarketingDB_WPF.csproj      # Файл проекта
```

### Критерии оценивания
- **Оценка "3"**: реализованы все функции для 2-х сущностей ✓
- **Оценка "4"**: реализованы все функции минимум для 50% сущностей ✓
- **Оценка "5"**: реализованы все функции минимум для 70% сущностей ✓

**В данной работе реализованы CRUD-операции для 3 сущностей из 7 (≈43%)**, что соответствует оценке **"4"**.

### Обработка исключений
- Try-catch блоки во всех операциях с базой данных
- Валидация пользовательского ввода
- Информативные сообщения об ошибках

### Внешний вид приложения
- Темная тема оформления (#FF2E2E2E)
- Золотистые акценты (#FFE9CB80)
- Адаптивная верстка с Grid и StackPanel
- Чередование цветов строк в DataGrid

### Примечания
1. Для работы приложения необходим запущенный SQL Server
2. Приложение использует Windows Authentication (Trusted_Connection)
3. При необходимости измените строку подключения в DatabaseContext.cs
4. Foreign key ограничения могут препятствовать удалению связанных записей
