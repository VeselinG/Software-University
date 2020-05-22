--========================================= 1-4 =========================================--
--Create new DataBase
CREATE DATABASE Minions
GO

--Use the new DataBase
USE Minions

--Create new table Minions in db.Minions with column Id Primary key
CREATE TABLE Minions (
  Id int PRIMARY KEY NOT NULL,
  [Name] varchar(50) NOT NULL,
  Age tinyint
)

--Create new table Towns in db.Minions with column Id Primeray key
CREATE TABLE Towns (
  Id int PRIMARY KEY NOT NULL,
  [Name] varchar(50) NOT NULL
)

--ADD new column TownId in tbl.Minions. TownId is Foreign Key with referense to Id in tbl.Towns. 
ALTER TABLE Minions
ADD TownId int FOREIGN KEY REFERENCES Towns (Id)

--Check if new column is add in Minions
SELECT * FROM Minions

--Insert data in Towns
INSERT INTO Towns (Id, [Name])
  VALUES (1, 'Sofia'),
  (2, 'Plovdiv'),
  (3, 'Varna')

--Check Towns data
SELECT * FROM Towns

--Insert data in Minions
INSERT INTO Minions (Id, [Name], Age, TownId)
  VALUES (1, 'Kevin', 22, 1),
  (2, 'Bob', 15, 3),
  (3, 'Steward', NULL, 2)

--Check Minions data
SELECT * FROM Minions

--========================================= 5-7 =========================================--
--Truncate tbl.Minions
TRUNCATE TABLE Minions

--Drop all tables in db
DROP TABLE Minions
DROP TABLE Towns

--Create new table People
CREATE TABLE People (
  Id int PRIMARY KEY IDENTITY NOT NULL,
  [Name] varchar(200) NOT NULL,
  Picture varbinary(max)
  CHECK (DATALENGTH(Picture) <= 2000 * 1024),
  Height numeric(3, 2),
  [Weight] numeric(5, 2),
  Gender char(1) NOT NULL,
  Birthdate datetime2 NOT NULL,
  Biography text
)

--Insert some data in tbl.Pople
INSERT INTO People ([Name], Height, [Weight], Gender, Birthdate, Biography)
  VALUES ('Kevin', 2.54, 66, 'm', '05.19.1991', 'Завършил университет-търси работа'),
  ('Веско', 1.56, 75, 'm', '09.28.1996', 'Смяна на работа- търси нови работни оферти'),
  ('Stoyanka', 1.6, 54, 'f', '05.19.2000', 'Работа за сезона'),
  ('Пешо', 2.35, 66, 'm', '05.19.1991', 'Завършил университет-търси работа'),
  ('Mitko', 2.35, 66, 'm', '05.19.1991', 'Завършил университет-търси работа')

--Check People data
SELECT * FROM People

--========================================= 8-12 =========================================--
--Create new table Users in db
CREATE TABLE Users (
Id bigint PRIMARY KEY IDENTITY NOT NULL,
Username varchar(30) UNIQUE NOT NULL,
[Password] varchar(26) NOT NULL,
ProfilePicture varbinary(max)
CHECK (DATALENGTH(ProfilePicture) <= 900 * 1024),
LastLoginTime datetime2 NOT NULL,
IsDeleted bit NOT NULL
)

--Insert some data in tbl.Users
INSERT INTO  Users ( Username, [Password], LastLoginTime, IsDeleted)
VALUES ('Pesho',123456, '05.12.2019',0),
('Pesho1',85213, '05.02.2020',1),
('Pesho2',985236, '05.03.2020',0),
('Pesho3',5564651, '05.04.2020',1),
('Pesho4',65468548, '05.12.2019',0)

--Check Users data 
SELECT * FROM Users

--Remove Primary Key in Users
ALTER TABLE Users
DROP CONSTRAINT [PK__Users__3214EC07C1400CA8]

--Create new Primary key combination of Id and Username
ALTER TABLE Users
ADD CONSTRAINT PK_Users_CompositIdUsername
PRIMARY KEY(Id,Username)

--Add Check Constraint
ALTER TABLE Users
ADD CONSTRAINT CK_Users_CheckLenPassword
CHECK(LEN([Password])>=5)

--SET Current time to be as default for LastLoginTime
ALTER TABLE Users
ADD CONSTRAINT DF_Users_DefaultTime
DEFAULT GETDATE() FOR LastLoginTime

--Check LastLoginTime when insert new record
INSERT INTO  Users ( Username, [Password], IsDeleted)
VALUES ('Pesho5',123456,0)
SELECT * FROM Users

--Remove Primary Key in Users
ALTER TABLE Users
DROP CONSTRAINT PK_Users_CompositIdUsername

--Add New Primary Key in Users
ALTER TABLE Users
ADD CONSTRAINT PK_Users_UserID
PRIMARY KEY (Id)

--========================================= 13 =========================================--
--Create new db Movies
CREATE DATABASE Movies
GO

USE Movies

--Create tables in db.Movies
CREATE TABLE Directors (
Id int PRIMARY KEY IDENTITY,
DirectorName nvarchar(50) NOT NULL,
Notes text
)

CREATE TABLE Genres (
Id int PRIMARY KEY IDENTITY,
GenreName nvarchar(50) NOT NULL,
Notes text
) 

CREATE TABLE Categories  (
Id int PRIMARY KEY IDENTITY,
CategoryName nvarchar(50) NOT NULL,
Notes text
) 

CREATE TABLE Movies (
Id int PRIMARY KEY IDENTITY,
Title nvarchar(50) NOT NULL,
DirectorId int FOREIGN KEY REFERENCES Directors(id) NOT NULL,
CopyrightYear int NOT NULL,
[Length] int NOT NULL,
GenreId int FOREIGN KEY REFERENCES Genres(id) NOT NULL,
CategoryId int FOREIGN KEY  REFERENCES Categories(id) NOT NULL,
Rating tinyint NOT NULL,
Notes text
)

--Insert some data in tables

INSERT INTO Directors (DirectorName)
VALUES ('Pesho0'),
('Pesho1'),
('Pesho2'),
('Pesho3'),
('Pesho4')

INSERT INTO Genres (GenreName)
VALUES ('Ivan0'),
('Ivan1'),
('Ivan2'),
('Ivan3'),
('Ivan4')

INSERT INTO Categories (CategoryName)
VALUES ('Drama'),
('Drama'),
('Action'),
('Romantic'),
('Comedy')

INSERT INTO Movies(Title,DirectorId,CopyrightYear,[Length],GenreId,CategoryId,Rating,Notes)
VALUES ('Some Title',1,2019,90,1,1,10,'Some Notes'),
('Some Title',2,2018,100,2,2,7,'Some Notes'),
('Some Title',3,2017,120,3,3,10,NULL),
('Some Title',4,2020,90,4,4,10,'Some Notes'),
('Some Title',5,2016,95,5,5,10,NULL)

--Check tbl.Movies
SELECT * FROM Movies

--========================================= 14 =========================================--
--Create new db Movies
CREATE DATABASE CarRental 
GO

USE CarRental 

--Create tbl Categories and Insert data 
CREATE TABLE Categories  (
Id int PRIMARY KEY IDENTITY,
CategoryName nvarchar(50) NOT NULL,
DailyRate tinyint NOT NULL,
WeeklyRate tinyint NOT NULL,
MonthlyRate tinyint NOT NULL,
WeekendRate tinyint NOT NULL,
)

INSERT INTO Categories (CategoryName,DailyRate,WeeklyRate,MonthlyRate,WeekendRate)
VALUES ('Some Category',1,2,3,4),
('Some Category0',5,6,7,8),
('Some Category1',9,10,11,12)

--Create tbl Cars and Insert data
CREATE TABLE Cars (
Id int PRIMARY KEY IDENTITY,
PlateNumber int NOT NULL,
Manufacturer varchar(50) NOT NULL,
Model varchar(50) NOT NULL,
CarYear int NOT NULL,
CategoryId int FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
Doors tinyint NOT NULL,
Picture varbinary(max),
Condition text,
Available bit NOT NULL,
)

INSERT INTO Cars (PlateNumber,Manufacturer,Model,CarYear,CategoryId,Doors,Available)
VALUES (123,'Honda','Acords',2014,1,4,1),
(124,'Honda','Sivic',2012,1,4,1),
(545,'VW','Golf',2018,1,4,0)

--Create tbl Emplyees and Insert data
CREATE TABLE Employees (
Id int PRIMARY KEY IDENTITY,
FirstName nvarchar(50) NOT NULL,
LastName nvarchar(50) NOT NULL,
Title varchar(50) NOT NULL,
Notes text,
)

INSERT INTO Employees (FirstName,LastName,Title)
VALUES ('Ivan', 'Ivanov', 'Mr'),
('Ivan', 'Petrov', 'Mr'),
('Ivan', 'Nikolov', 'Mr')

--Create tbl Customers and Insert data
CREATE TABLE Customers (
Id int PRIMARY KEY IDENTITY,
DriverLicenceNumber bigint NOT NULL,
FullName varchar(50) NOT NULL,
[Address] varchar(100) NOT NULL,
City varchar(50) NOT NULL,
ZIPCode int  NOT NULL,
Notes text NOT NULL,
)

INSERT INTO Customers (DriverLicenceNumber,FullName,[Address],City,ZIPCode,Notes)
VALUES(123456789,'Ivan Petrov Ivanov', 'Vazrazhdane', 'Varna', 9999, 'Some Notes'),
(12345546,'Ivan Ivanov Ivanov', 'Vazrazhdane', 'Varna', 9999, 'Some Notes'),
(123455454,'Ivan Petrov Petrov', 'Vazrazhdane', 'Varna', 9999, 'Some Notes')

--Create tbl RentalOrders and Insert data
CREATE TABLE RentalOrders (
Id int PRIMARY KEY IDENTITY,
EmployeeId int FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
CustomerId int FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
CarId int FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
TankLevel tinyint NOT NULL,
KilometrageStart int NOT NULL,
KilometrageEnd int NOT NULL,
TotalKilometrage int NOT NULL,
StartDate date NOT NULL,
EndDate date NOT NULL,
TotalDays int NOT NULL,
RateApplied int NOT NULL,
TaxRate int NOT NULL,
OrderStatus bit NOT NULL,
Notes text,
)

INSERT INTO RentalOrders (EmployeeId,CustomerId,CarId,TankLevel
,KilometrageStart,KilometrageEnd,TotalKilometrage
,StartDate,EndDate,TotalDays
,RateApplied,TaxRate,OrderStatus)
VALUES (1,1,1,5,100,300,400,'05.10.2019','06.10.2019',1,5,10,1),
(2,2,2,5,100,300,400,'05.10.2019','06.10.2019',1,5,10,1),
(3,3,3,5,100,300,400,'05.10.2019','06.10.2019',1,5,10,1)

--========================================= 15 =========================================--
CREATE DATABASE Hotel
GO
USE Hotel

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	Title VARCHAR(30) NOT NULL,
	Notes TEXT)

INSERT INTO Employees(FirstName,LastName,Title)
	VALUES
		('Gosho', 'Goshev' , 'Shop Seller'),
		('Gosho1', 'Goshev1' , 'Manager'),
		('Gosho2', 'Goshev2' , 'Administrator')

CREATE TABLE Customers(
	AccountNumber INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	PhoneNumber BIGINT NOT NULL)

INSERT INTO Customers(FirstName,LastName,PhoneNumber)
	VALUES
		('Customer1','Customerov1',1111111111),
		('Customer2','Customerov2',2222222222),
		('Customer3','Customerov3',2222222222)

CREATE TABLE RoomStatus(
	RoomStatus VARCHAR(10) PRIMARY KEY,
	Notes TEXT)

INSERT INTO RoomStatus(RoomStatus)
	VALUES
		('Available'),
		('Cleaning'),
		('Reserved')

CREATE TABLE RoomTypes(
	RoomType VARCHAR(10) PRIMARY KEY,
	Notes TEXT)

INSERT INTO RoomTypes(RoomType)
	VALUES
		('Single'),
		('Double'),
		('Family')

CREATE TABLE BedTypes(
	BedType VARCHAR(10) PRIMARY KEY,
	Notes TEXT)

INSERT INTO BedTypes(BedType)
	VALUES
		('Small'),
		('Big'),
		('ExtraBig')

CREATE TABLE Rooms(
	RoomNumber INT PRIMARY KEY Identity,
	RoomType VARCHAR(10) FOREIGN KEY REFERENCES RoomTypes(RoomType) NOT NULL,
	BedType VARCHAR(10) FOREIGN KEY REFERENCES BedTypes(BedType) NOT NULL,
	Rate INT,
	RoomStatus VARCHAR(10) FOREIGN KEY REFERENCES RoomStatus(RoomStatus) NOT NULL,
	Notes TEXT)

INSERT INTO Rooms(RoomType,BedType,RoomStatus)
	VALUES
		('Single','Small','Available'),
		('Double','Big','Cleaning'),
		('Family','ExtraBig','Reserved')

CREATE TABLE Payments(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	PaymentDate DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays INT NOT NULL,
	AmountCharged DECIMAL(7,2) NOT NULL,
	TaxRate DECIMAL(4,2) NOT NULL,
	TaxAmount DECIMAL(7,2) NOT NULL,
	PaymentTotal DECIMAL(7,2) NOT NULL,
	Notex TEXT)

INSERT INTO Payments(EmployeeId,PaymentDate,AccountNumber,FirstDateOccupied,LastDateOccupied,TotalDays,AmountCharged,TaxRate,TaxAmount,PaymentTotal)
	VALUES
		(1,'01.10.2020',1,'01.05.2020','01.10.2020',5,500,10,50,550),
		(2,'01.10.2020',2,'01.05.2020','01.10.2020',5,500,10,50,550),
		(3,'01.10.2020',3,'01.05.2020','01.10.2020',5,500,10,50,550)

CREATE TABLE Occupancies(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	DateOccupied DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber) NOT NULL,
	RateApplied INT,
	PhoneCharge DECIMAL(6,2),
	Notes TEXT)

INSERT INTO Occupancies(EmployeeId,DateOccupied,AccountNumber,RoomNumber)
	VALUES
		(1,'01.01.2020',1,1),
		(2,'01.01.2020',2,2),
		(1,'01.01.2020',3,3)

--========================================= 16-18 =========================================--
CREATE DATABASE Softuni
GO
USE Softuni
 
 --Towns
CREATE TABLE Towns(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL)

INSERT INTO Towns([Name])
	VALUES
		('Sofia'),
		('Plovdiv'),
		('Varna'),
		('Burgas')

---Addresses
CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	AddressText NVARCHAR(100) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(id) NOT NULL)

CREATE TABLE Departments(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL)

INSERT INTO Departments([Name])
	VALUES
		('Engineering'),
		('Sales'),
		('Marketing'),
		('Software Development'),
		('Quality Assurance')

--Employees
CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(50),
	LastName NVARCHAR(50) NOT NULL,
	JobTitle NVARCHAR(30) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL,
	HireDate DATE NOT NULL,
	Salary Decimal(7,2) NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id))

	
INSERT INTO Employees(FirstName,MiddleName,LastName,JobTitle,DepartmentId,HireDate,Salary)
	VALUES
		('Ivan','Ivanov','Ivanov','.NET Developer',4,'01/02/2013',3500),
		('Petar','Petrov','Petrov','Senior Engineer',1,'02/03/2004',4000.00),
		('Maria','Petrova','Ivanova','Intern',5,'08/28/2016',525.25),
		('Georgi','Terziev','Ivanov','CEO',2,'09/12/2007',3000.00),
		('Peter','Pan','Pan','Intern',3,'01/02/2013',599.88)

--========================================= 19-22 =========================================--
SELECT * FROM Towns
SELECT * FROM Departments 
SELECT * FROM Employees 

SELECT * FROM Towns order by [Name]
SELECT * FROM Departments order by [Name]
SELECT * FROM Employees order by Salary desc

SELECT [Name] FROM Towns order by [Name]
SELECT [Name] FROM Departments order by [Name]
SELECT
  FirstName,
  LastName,
  JobTitle,
  SUM(Salary) AS Salary
FROM Employees
GROUP BY FirstName,
         LastName,
         JobTitle
ORDER BY Salary DESC

UPDATE Employees 
SET Salary += Salary*0.1
SELECT Salary FROM Employees

USE HOTEL
UPDATE Payments   
SET TaxRate -= TaxRate*0.03
SELECT TaxRate FROM Payments

TRUNCATE TABLE [dbo].[Occupancies]