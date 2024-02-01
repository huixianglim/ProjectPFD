
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

if exists (select * from sysobjects 
  where id = object_id('dbo.Crosscheck') and sysstat & 0xf = 3)
  drop table dbo.Crosscheck;
GO



CREATE TABLE dbo.Users 
(
  Name          varchar(20)     NOT NULL,
  Email         varchar(50)     NOT NULL UNIQUE,
  UserID        INT IDENTITY(1,1),
  Password      varchar(30)     NOT NULL,
  Money         Decimal,
  LastLoggedIn  DATETIME NOT NULL,
  PRIMARY KEY(UserID)
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
  DateOfTransaction DATETIME NOT NULL,
  Location varchar(80) NOT NULL,
  Primary KEY(TransactionID), 
  FOREIGN KEY (UserID) REFERENCES Users(UserID)

);
GO

CREATE TABLE dbo.Crosscheck 
(
	check_id varchar(500) NOT NULL,
	user_id int NOT NULL
    Primary KEY(check_id),
);
GO
INSERT INTO [dbo].[Crosscheck] ([check_id] ,[user_id]) VALUES ('HuiXiang', 2);
INSERT INTO [dbo].[Crosscheck] ([check_id], [user_id]) VALUES ('Wesley', 1);
INSERT INTO [dbo].[Crosscheck] ([check_id], [user_id]) VALUES ('Kenan', 3);


SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([Name], [Password], [Money], [LastLoggedIn], [UserID], [Email]) VALUES 
('Wesley Khalifa', 'pass123', 1234, '2024-01-28T12:00:00', 1, '123@gmail.com');
INSERT [dbo].[Users] ([Name], [Password], [Money], [LastLoggedIn], [UserID], [Email]) VALUES 
('Hui Xiang', 'pass124', 2345, '2024-01-27T08:30:00', 2, '124@gmail.com');
INSERT [dbo].[Users] ([Name], [Password], [Money], [LastLoggedIn], [UserID], [Email]) VALUES 
('Kenan', 'pass125', 234, '2024-01-26T18:45:00', 3, '125@gmail.com');
INSERT [dbo].[Users] ([Name], [Password], [Money], [LastLoggedIn], [UserID], [Email]) VALUES 
('Garthic Phone', 'pass125', 0, '2024-01-25T10:15:00', 4, '126@gmail.com');
INSERT [dbo].[Users] ([Name], [Password], [Money], [LastLoggedIn], [UserID], [Email]) VALUES 
('Jaygen', 'pass124', 346, '2024-01-24T15:20:00', 5, '127@gmail.com');
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

INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',20,1,'2022-06-11T08:30:00','Bishan Interchange');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',40,1,'2022-05-11T14:45:00','Ang Mo Kio Interchange');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',50,1,'2022-12-11T10:00:00','Starhub');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',30,1,'2022-11-11T18:30:00','Starbucks');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',12,1,'2022-01-11T12:15:00','Macdonalds');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',21,1,'2022-02-11T09:45:00','Macdonalds');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',20,2,'2022-03-11T14:00:00','KFC');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',40,2,'2022-04-11T08:30:00','NTUC');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',50,2,'2022-12-11T11:15:00','Jollibee');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',30,2,'2022-07-11T15:45:00','Macdonalds');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',12,2,'2022-08-11T13:30:00','KFC');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',21,2,'2022-11-11T16:20:00','NTUC');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',20,3,'2022-12-11T10:45:00','Burger King');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',40,3,'2022-11-11T14:00:00','Karthik');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',50,3,'2022-12-11T09:30:00','Wesley');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',30,3,'2022-11-11T17:10:00','Jollibee');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',12,3,'2022-12-11T12:45:00','Macdonalds');
INSERT [dbo].[Transactions] ([Type],[Amount],[UserID],[DateOfTransaction],[Location]) 
VALUES('Debit Transfer',21,3,'2022-11-11T14:30:00','KFC');


