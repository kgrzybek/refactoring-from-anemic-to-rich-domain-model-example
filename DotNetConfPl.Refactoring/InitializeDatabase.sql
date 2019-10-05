CREATE SCHEMA companies AUTHORIZATION dbo
GO

CREATE TABLE companies.Companies
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Name] VARCHAR(200) NOT NULL,
    [ContactEmployeeId] UNIQUEIDENTIFIER NULL,
    [AddressId] UNIQUEIDENTIFIER NULL,
	[Source] VARCHAR(30) NOT NULL,
	CONSTRAINT [PK_companies_Companies_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE companies.Employees
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[PersonId] UNIQUEIDENTIFIER NOT NULL,
	[CompanyId] UNIQUEIDENTIFIER NOT NULL,
    [Email] NVARCHAR(255) NULL,
    [Phone] NVARCHAR(255) NULL,
	[ActiveFrom] DATETIME2 NOT NULL,
	[ActiveTo] DATETIME2 NULL,
	CONSTRAINT [PK_companies_Employees_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE companies.Persons
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[FirstName] NVARCHAR(255) NULL,
	[LastName] NVARCHAR(255) NULL,
    [FullName] NVARCHAR(255) NULL
	CONSTRAINT [PK_companies_Persons_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE VIEW companies.v_Companies
AS
SELECT
	[Company].[Id],
	[Company].[Name],
	[Company].[Source],
	[Employee].Email AS [ContactEmployeeEmail],
	[Employee].Phone AS [ContactEmployeePhone],	
	[Person].[FullName] AS [ContactEmployeeFullName]
FROM companies.Companies AS [Company]
	LEFT JOIN [companies].[Employees] AS [Employee]
		ON [Company].[ContactEmployeeId] = [Employee].[CompanyId]
	LEFT JOIN [companies].[Persons] AS [Person]
		ON [Employee].[PersonId] = [Person].[Id]
GO

CREATE VIEW companies.v_Employees
AS
SELECT
	[Employee].[Id],
	[Employee].[CompanyId],
	[Company].[Name] AS [CompanyName],
	[Person].[FullName] AS [FullName],
	[Employee].[Email],
	[Employee].[Phone],
	[Employee].[ActiveFrom],
	[Employee].[ActiveTo],
	[Employee].[PersonId],
	CASE
		WHEN [Employee].Id = [Company].[ContactEmployeeId] THEN CAST(1 AS BIT)
		ELSE CAST(0 AS BIT)
	END AS [IsContact]
FROM [companies].[Employees] AS  [Employee]
	INNER JOIN companies.Companies AS [Company] 
		ON [Company].[Id] = [Employee].[CompanyId]
	INNER JOIN [companies].[Persons] AS [Person]
		ON [Employee].[PersonId] = [Person].[Id]
GO