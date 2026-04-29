-- Marketing Database Setup Script
-- Run this script on your SQL Server to create the database and tables

USE master;
GO

-- Create database if not exists
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MarketingDB')
BEGIN
    CREATE DATABASE MarketingDB;
END
GO

USE MarketingDB;
GO

-- Create Clients table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Clients')
BEGIN
    CREATE TABLE Clients (
        ClientID INT PRIMARY KEY IDENTITY(1,1),
        FullName NVARCHAR(255) NOT NULL,
        Email NVARCHAR(255),
        Phone NVARCHAR(20),
        Address NVARCHAR(500)
    );
END
GO

-- Create Employees table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE Employees (
        EmployeeID INT PRIMARY KEY IDENTITY(1,1),
        FullName NVARCHAR(255) NOT NULL,
        Position NVARCHAR(100),
        Email NVARCHAR(255),
        HourlyRate DECIMAL(10,2)
    );
END
GO

-- Create Campaigns table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Campaigns')
BEGIN
    CREATE TABLE Campaigns (
        CampaignID INT PRIMARY KEY IDENTITY(1,1),
        CampaignName NVARCHAR(255) NOT NULL,
        ClientID INT NOT NULL,
        Budget DECIMAL(15,2),
        StartDate DATE,
        EndDate DATE,
        Status NVARCHAR(50),
        CONSTRAINT FK_Campaigns_Clients FOREIGN KEY (ClientID)
            REFERENCES Clients(ClientID)
    );
END
GO

-- Create Channels table (for reference, not used in CRUD)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Channels')
BEGIN
    CREATE TABLE Channels (
        ChannelID INT PRIMARY KEY IDENTITY(1,1),
        ChannelName NVARCHAR(255) NOT NULL,
        ChannelType NVARCHAR(100),
        PlacementCost DECIMAL(10,2)
    );
END
GO

-- Create Tasks table (for reference, not used in CRUD)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tasks')
BEGIN
    CREATE TABLE Tasks (
        TaskID INT PRIMARY KEY IDENTITY(1,1),
        CampaignID INT NOT NULL,
        Description NVARCHAR(500),
        ExecutorID INT NOT NULL,
        DueDate DATE,
        Status NVARCHAR(50),
        CONSTRAINT FK_Tasks_Campaigns FOREIGN KEY (CampaignID)
            REFERENCES Campaigns(CampaignID),
        CONSTRAINT FK_Tasks_Employees FOREIGN KEY (ExecutorID)
            REFERENCES Employees(EmployeeID)
    );
END
GO

-- Create Expenses table (for reference, not used in CRUD)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Expenses')
BEGIN
    CREATE TABLE Expenses (
        ExpenseID INT PRIMARY KEY IDENTITY(1,1),
        CampaignID INT NOT NULL,
        ExpenseItem NVARCHAR(255),
        Amount DECIMAL(15,2),
        ExpenseDate DATE,
        CONSTRAINT FK_Expenses_Campaigns FOREIGN KEY (CampaignID)
            REFERENCES Campaigns(CampaignID)
    );
END
GO

-- Create Results table (for reference, not used in CRUD)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Results')
BEGIN
    CREATE TABLE Results (
        ResultID INT PRIMARY KEY IDENTITY(1,1),
        CampaignID INT NOT NULL,
        ChannelID INT NOT NULL,
        Impressions INT,
        Clicks INT,
        ConversionRate DECIMAL(5,2),
        ReportDate DATE,
        CONSTRAINT FK_Results_Campaigns FOREIGN KEY (CampaignID)
            REFERENCES Campaigns(CampaignID),
        CONSTRAINT FK_Results_Channels FOREIGN KEY (ChannelID)
            REFERENCES Channels(ChannelID)
    );
END
GO

-- Insert sample data for testing
INSERT INTO Clients (FullName, Email, Phone, Address) VALUES
(N'Иванов Иван Иванович', N'ivanov@example.com', N'+7-900-123-4567', N'г. Москва, ул. Ленина, д. 1'),
(N'Петров Петр Петрович', N'petrov@example.com', N'+7-900-234-5678', N'г. Санкт-Петербург, пр. Невский, д. 10');

INSERT INTO Employees (FullName, Position, Email, HourlyRate) VALUES
(N'Сидоров Сидор Сидорович', N'Менеджер', N'sidorov@company.com', 500.00),
(N'Кузнецов Кузьма Кузьмич', N'Дизайнер', N'kuznetsov@company.com', 450.00);

INSERT INTO Campaigns (CampaignName, ClientID, Budget, StartDate, EndDate, Status) VALUES
(N'Новогодняя реклама', 1, 100000.00, '2024-12-01', '2024-12-31', N'Active'),
(N'Весенняя распродажа', 2, 75000.00, '2025-03-01', '2025-03-31', N'Planned');

PRINT 'Database setup completed successfully!';
GO
