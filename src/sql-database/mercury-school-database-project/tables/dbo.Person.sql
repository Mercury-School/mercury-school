CREATE TABLE [dbo].[dbo.Person]
(
  Id int NOT NULL CONSTRAINT PK_Person PRIMARY KEY,
  FirstName nvarchar(128) NOT NULL,
  MiddleName nvarchar(128) NULL,
  LastName nvarchar(128) NOT NULL,
)
