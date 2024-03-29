USE [master]
GO
/****** Object:  Database [DI.Reminder.DataBase]    Script Date: 25/06/2018 11:19:29 ******/
CREATE DATABASE [DI.Reminder.DataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DI.Reminder.DataBase', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DI.Reminder.DataBase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DI.Reminder.DataBase_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\DI.Reminder.DataBase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DI.Reminder.DataBase] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DI.Reminder.DataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DI.Reminder.DataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET  MULTI_USER 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DI.Reminder.DataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DI.Reminder.DataBase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [DI.Reminder.DataBase]
GO
/****** Object:  User [IIS APPPOOL\Reminder]    Script Date: 25/06/2018 11:19:30 ******/
CREATE USER [IIS APPPOOL\Reminder] FOR LOGIN [IIS APPPOOL\Reminder] WITH DEFAULT_SCHEMA=[IIS APPPOOL\Reminder]
GO
/****** Object:  DatabaseRole [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]    Script Date: 25/06/2018 11:19:30 ******/
CREATE ROLE [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]
GO
/****** Object:  Schema [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]    Script Date: 25/06/2018 11:19:30 ******/
CREATE SCHEMA [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]
GO
/****** Object:  Schema [IIS APPPOOL\Reminder]    Script Date: 25/06/2018 11:19:30 ******/
CREATE SCHEMA [IIS APPPOOL\Reminder]
GO
/****** Object:  Table [dbo].[Advertising]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advertising](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nchar](50) NOT NULL,
	[Url] [ntext] NOT NULL,
	[Image] [ntext] NULL,
 CONSTRAINT [PK_Advertising] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](16) NOT NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PromptActions]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromptActions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [ntext] NOT NULL,
	[PromptID] [int] NOT NULL,
 CONSTRAINT [PK_PromptActions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prompts]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prompts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[TimeOfPrompt] [time](7) NOT NULL,
	[DateOfCreating] [date] NOT NULL,
	[Description] [ntext] NULL,
	[Image] [ntext] NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Prompts_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nchar](50) NOT NULL,
	[Password] [nchar](50) NOT NULL,
	[Email] [nchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users_Roles]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users_Roles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_Users_Roles] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PromptActions]  WITH CHECK ADD  CONSTRAINT [FK_PromptActions_Prompts1] FOREIGN KEY([PromptID])
REFERENCES [dbo].[Prompts] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PromptActions] CHECK CONSTRAINT [FK_PromptActions_Prompts1]
GO
ALTER TABLE [dbo].[Prompts]  WITH CHECK ADD  CONSTRAINT [FK_Prompts_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([ID])
GO
ALTER TABLE [dbo].[Prompts] CHECK CONSTRAINT [FK_Prompts_Categories]
GO
ALTER TABLE [dbo].[Users_Roles]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Users_Roles] CHECK CONSTRAINT [FK_Users_Roles_Roles]
GO
ALTER TABLE [dbo].[Users_Roles]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users_Roles] CHECK CONSTRAINT [FK_Users_Roles_Users]
GO
/****** Object:  StoredProcedure [dbo].[AddAction]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddAction]
@PromptID int, 
@Name ntext 
AS
INSERT PromptActions(ActionName, PromptID) VALUES(@Name, @PromptID)
GO
/****** Object:  StoredProcedure [dbo].[AddCategory]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddCategory]
@Name nchar(50),
@ParentID int = NULL
AS
INSERT INTO Categories(Name, ParentID) VALUES (@Name, @ParentID)
GO
/****** Object:  StoredProcedure [dbo].[AddConnection]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddConnection]
@userid int,
@roleid int	
AS
INSERT INTO Users_Roles(RoleID, UserID) VALUES(@roleid,@userid)
GO
/****** Object:  StoredProcedure [dbo].[AddPrompt]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddPrompt]
@userID int,
@name nchar(50),
@categoryid int,
@timeofprompt time(7),
@dataofcreating date,
@description ntext=NULL,
@Image ntext = NULL 
AS
INSERT INTO Prompts(Name, CategoryID, TimeOfPrompt, DateOfCreating, Description, Image, UserID) VALUES(@name, @categoryid,@timeofprompt,@dataofcreating,@description,@Image, @userID)
SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[AddRole]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddRole]
@name nvarchar(50)
AS
INSERT INTO Roles(Name) VALUES(@name)
GO
/****** Object:  StoredProcedure [dbo].[AddRolesForUser]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddRolesForUser] 
@name nchar(50),
@userid int
AS
INSERT INTO Users_Roles(UserID, RoleID) VALUES (@userid, (SELECT ID FROM Roles WHERE Name=@name))
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddUser]
@login nvarchar(50),
@password nvarchar(50),
@email nchar(50)
AS
INSERT INTO Users (Login, Password, Email) VALUES(@login, @password, @email)
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteCategory]
@id int
AS
DELETE FROM Categories WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[DeletePrompt]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeletePrompt]
@userId int,
@id int
AS
DELETE FROM Prompts WHERE ID=@id AND UserID=@userId
GO
/****** Object:  StoredProcedure [dbo].[DeleteRole]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRole]
@id int
AS
DELETE FROM Roles WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteUser]
@id int
AS
DELETE FROM Users WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[DeleteUserRole]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteUserRole]
@userID int,
@id int
AS
DELETE FROM Users_Roles WHERE UserID=@userID AND RoleID=@id
GO
/****** Object:  StoredProcedure [dbo].[GetActions]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetActions] 
@id int
AS
SELECT act.ID, act.ActionName FROM PromptActions act JOIN Prompts p ON p.ID=act.PromptID WHERE p.ID=@id 
GO
/****** Object:  StoredProcedure [dbo].[GetAdvertisings]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAdvertisings]
AS
SELECT *  FROM Advertising
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCategories]
AS
SELECT * FROM Categories
GO
/****** Object:  StoredProcedure [dbo].[GetAllIDOfPromptActions]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllIDOfPromptActions]
@id int
AS
SELECT ID FROM PromptActions WHERE PromptID=@id
GO
/****** Object:  StoredProcedure [dbo].[GetAllPrompts]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPrompts]
@userID int
AS
SELECT ID, Name, CategoryID FROM Prompts  WHERE UserID=@userID
GO
/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllRoles]
AS
SELECT * FROM Roles
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
SELECT * FROM Users
GO
/****** Object:  StoredProcedure [dbo].[GetCategories]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCategories]
AS
SELECT * FROM Categories WHERE ParentID IS NULL
GO
/****** Object:  StoredProcedure [dbo].[GetCategory]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCategory]
@id int
AS
SELECT * FROM Categories WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryID]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetCategoryID] 
@Name nchar(16)
AS
SELECT * FROM Categories WHERE Name=@Name
GO
/****** Object:  StoredProcedure [dbo].[GetPrompt]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPrompt]
@id int,
@userID int
AS
SELECT ID, Name, CategoryID , DateOfCreating,TimeOfPrompt, Description, Image FROM Prompts WHERE ID = @id AND UserID = @userID
GO
/****** Object:  StoredProcedure [dbo].[GetPromptsByID]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPromptsByID]
@userID int,
@id int
AS
SELECT ID, Name, CategoryID FROM Prompts WHERE CategoryID = @id AND UserID=@userID
GO
/****** Object:  StoredProcedure [dbo].[GetRole]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRole] 
@roleName nchar(50)
AS
SELECT * FROM Roles WHERE Name=@roleName
GO
/****** Object:  StoredProcedure [dbo].[GetUnderCategories]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUnderCategories]
@id int
AS
SELECT * FROM Categories WHERE ParentID=@id
GO
/****** Object:  StoredProcedure [dbo].[GetUserByID]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByID]
@id INT
AS
SELECT * FROM Users WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[GetUserByLogin]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByLogin] 
@login nvarchar(50)
AS
SELECT * FROM Users WHERE Login=@login
GO
/****** Object:  StoredProcedure [dbo].[GetUserID]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserID]
@login nvarchar(50)	
AS
SELECT ID FROM Users WHERE Login=@login
GO
/****** Object:  StoredProcedure [dbo].[GetUserRoles]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserRoles]
@id int
AS
SELECT r.ID, r.Name FROM Roles r JOIN Users_Roles ur ON r.ID=ur.RoleID WHERE ur.UserID=@id
GO
/****** Object:  StoredProcedure [dbo].[RemoveAction]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RemoveAction]
@id int
AS
DELETE FROM PromptActions WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[SearchByCategory]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchByCategory] 
@userId int,
@id int
AS
SELECT * FROM Prompts WHERE CategoryID=@id AND UserID=@userId
GO
/****** Object:  StoredProcedure [dbo].[SearchByDate]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchByDate]
@userId int,
@date date 
AS
SELECT * FROM Prompts WHERE DateOfCreating=@date AND UserID=@userId
GO
/****** Object:  StoredProcedure [dbo].[SearchByPrompt]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchByPrompt]
@userId int,
@name nvarchar(50)
AS
SELECT * from Prompts where Name like '%'+@name+'%' AND UserID=@userId
GO
/****** Object:  StoredProcedure [dbo].[UpdatingAction]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatingAction] 
@id int, 
@name ntext
AS
UPDATE PromptActions SET ActionName=@name WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[UpdatingCategory]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatingCategory] 
@id int,
@name nchar(16),
@parentID int
AS
UPDATE Categories SET Name=@name, ParentID=@parentID WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[UpdatingCategoryWithNull]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatingCategoryWithNull]
@id int,
@name nchar(16)
AS
UPDATE Categories SET Name=@name WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[UpdatingPrompt]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatingPrompt] 
@id int,
@name nchar(50),
@categoryid int,
@timeofprompt time(7),
@dateofcreating date,
@description ntext,
@image ntext
AS
UPDATE Prompts SET Name=@name, CategoryID=@categoryid, TimeOfPrompt=@timeofprompt, DateOfCreating=@dateofcreating, Description=@description, Image=@image WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[UpdatingUser]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatingUser]
@id int,
@login nchar(50),
@password nchar(50),
@email nchar(50) = null
AS
UPDATE Users SET Login=@login, Password=@password, Email=@email WHERE ID=@id
GO
/****** Object:  StoredProcedure [dbo].[UpdatingUserRole]    Script Date: 25/06/2018 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatingUserRole] 
@roleid int, 
@userid int
AS
UPDATE Users_Roles SET RoleID=@roleid WHERE UserID=@userid
GO
USE [master]
GO
ALTER DATABASE [DI.Reminder.DataBase] SET  READ_WRITE 
GO
