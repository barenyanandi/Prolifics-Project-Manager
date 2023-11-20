--creating database
CREATE DATABASE PPM

--using database
USE PPM


--Project table
CREATE TABLE Project
(
  ProjectId INT PRIMARY KEY,
  ProjectName VARCHAR(100),
  StartDate DATETIME,
  EndDate DATETIME,
  -- Timestamp tracking
  CreatedOn DATETIME DEFAULT CURRENT_TIMESTAMP,
  ModifiedOn DATETIME DEFAULT GETDATE()
);

SELECT * FROM Project


--Employee table
CREATE TABLE Employee
(
  EmployeeId INT PRIMARY KEY,
  FirstName VARCHAR(100),
  LastName VARCHAR(100),
  Email VARCHAR(100),
  Mobile VARCHAR(100),
  [Address] VARCHAR(100),
  RoleId INT                  --- Role id as foreign key
  CONSTRAINT FK_RoleId FOREIGN KEY (RoleId)
  REFERENCES [Role](RoleId)
  ON DELETE CASCADE
  ON UPDATE CASCADE,
  ProjectId INT                  --- Project id as foreign key
  CONSTRAINT FK_ProjectId FOREIGN KEY (ProjectId)
  REFERENCES Project(ProjectId)
  ON DELETE CASCADE
  ON UPDATE CASCADE,
  --- Timestamp tracking
  CreatedOn DATETIME DEFAULT CURRENT_TIMESTAMP,
  ModifiedOn DATETIME DEFAULT GETDATE()
);

SELECT * FROM Employee 


--[Role] table
CREATE TABLE [Role]
(
  RoleId INT PRIMARY KEY,
  RoleName VARCHAR(100),
  --- Timestamp tracking
  CreatedOn DATETIME DEFAULT CURRENT_TIMESTAMP,
  ModifiedOn DATETIME DEFAULT GETDATE()
);

SELECT * FROM [Role]





-- creating stored procedures

-- SP to Add Project
CREATE PROCEDURE AddProject
	@ProjectId INT,
	@ProjectName VARCHAR(100),
	@StartDate DATETIME,
	@EndDate DATETIME
AS
BEGIN
	INSERT INTO Project(ProjectId, ProjectName, StartDate, EndDate)