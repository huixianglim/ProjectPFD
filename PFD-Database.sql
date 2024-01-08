
--Create Database PFD
--GO Uncomment this when running it for the first time


USE PFD
GO

if exists (select * from sysobjects 
  where id = object_id('dbo.Transactions') and sysstat & 0xf = 3)
  drop table dbo.Transactions;
GO

if exists (select * from sysobjects 
  where id = object_id('dbo.Contacts') and sysstat & 0xf = 3)
  drop table dbo.Contacts;
GO

if exists (select * from sysobjects 
  where id = object_id('dbo.Users') and sysstat & 0xf = 3)
  drop table dbo.Users;
GO





CREATE TABLE dbo.Users 
(
  Name 		varchar (20) 	NOT NULL,
  Email     varchar(50)     NOT NULL UNIQUE,
  UserID        INT IDENTITY(1,1),
  Password		varchar (30)	NOT NULL,
  Money      Decimal,
  Primary KEY(UserID),
);
GO


CREATE TABLE dbo.Contacts 
(
  Name 		varchar (20) 	NOT NULL,
  ContactID        INT IDENTITY(1,1),
  Number			varchar(20) NOT NULL,
  UserID INT,
  Primary KEY(ContactID), 
  FOREIGN KEY (UserID) REFERENCES Users(UserID)

);
GO

CREATE TABLE dbo.Transactions 
(
  TransactionID 		INT IDENTITY(1,1),
  Type       varchar(20) NOT NULL,
  Amount			Decimal NOT NULL,
  UserID INT,
  DateOfTransaction DATE NOT NULL,
  Location varchar(80) NOT NULL,
  Primary KEY(TransactionID), 
  FOREIGN KEY (UserID) REFERENCES Users(UserID)

);
GO


SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Name], [Password], [Money],[UserID],[Email]) VALUES ('Wesley Khalifa', 'pass123', 1234,1,'123@gmail.com')
INSERT [dbo].[Users] ([Name], [Password], [Money],[UserID],[Email]) VALUES ('Johnny', 'pass124', 2345,2,'124@gmail.com')
INSERT [dbo].[Users] ([Name], [Password], [Money],[UserID],[Email]) VALUES ('Cannon', 'pass125', 234,3,'125@gmail.com')
INSERT [dbo].[Users] ([Name], [Password], [Money],[UserID],[Email]) VALUES ('Garthic Phone', 'pass125', 0,4,'126@gmail.com')
INSERT [dbo].[Users] ([Name], [Password], [Money],[UserID],[Email]) VALUES ('Jaygen', 'pass124', 346,5,'127@gmail.com')
SET IDENTITY_INSERT [dbo].[Users] OFF

INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Wesley', '+6512345678',1)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Karthik', '+6513456778',1)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Jaden', '+6523345658',1)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Festive', '+6534345678',1)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('James', '+6512555678',1)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Gondon', '+6512345678',1)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Bukie', '+6513445678',2)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Wesley', '+6512345678',2)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Karthik', '+6513456778',2)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Jaden', '+6523345658',2)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Festive', '+6534345678',2)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('James', '+6512555678',3)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Gondon', '+6512345678',3)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Wesley', '+6512345678',3)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Karthik', '+6513456778',3)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Jaden', '+6523345658',4)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Festive', '+6534345678',4)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('James', '+6512555678',4)
INSERT [dbo].[Contacts] ([Name], [Number],[UserID]) VALUES ('Gondon', '+6512345678',4)

INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction], [Location]) VALUES('Debit Transfer',20,1,'2022-06-11','Bishan Interchange')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',40,1,'2022-05-11','Ang Mo Kio Interchange')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',50,1,'2022-12-11','Starhub')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',30,1,'2022-11-11','Starbucks')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',12,1,'2022-01-11','Macdonalds')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',21,1,'2022-02-11','Macdonalds')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',20,2,'2022-03-11','KFC')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',40,2,'2022-04-11','NTUC')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',50,2,'2022-12-11','Jollibee')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',30,2,'2022-07-11','Macdonalds')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',12,2,'2022-08-11','KFC')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',21,2,'2022-11-11','NTUC')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',20,3,'2022-12-11','Burger King')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',40,3,'2022-11-11','Karthik')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',50,3,'2022-12-11','Wesley')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',30,3,'2022-11-11','Jollibee')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',12,3,'2022-12-11','Macdonalds')
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) VALUES('Debit Transfer',21,3,'2022-11-11','KFC')

