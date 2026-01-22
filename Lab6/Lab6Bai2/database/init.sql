-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CSharp5Lab6Bai2')
BEGIN
    CREATE DATABASE CSharp5Lab6Bai2;
END
GO

USE CSharp5Lab6Bai2;
GO

-- Create Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Reservations')
BEGIN
    CREATE TABLE Reservations (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(250) NOT NULL,
        StartLocation NVARCHAR(250) NULL,
        EndLocation NVARCHAR(250) NULL
    );
END
GO

-- Seed Data
INSERT INTO Reservations (Name, StartLocation, EndLocation)
VALUES 
(N'Lê Văn Tám', N'Hà Nội', N'Hải Phòng'),
(N'Trần Thị Mẹt', N'Đà Nẵng', N'Huế'),
(N'Nguyễn Văn Bơ', N'TP.HCM', N'Vũng Tàu');
GO
