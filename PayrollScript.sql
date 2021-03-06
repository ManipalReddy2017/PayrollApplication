USE [master]
GO
/****** Object:  Database [Payroll]    Script Date: 2/13/2022 8:52:38 PM ******/
CREATE DATABASE [Payroll]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Payroll', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Payroll.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Payroll_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Payroll_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Payroll] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Payroll].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Payroll] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Payroll] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Payroll] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Payroll] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Payroll] SET ARITHABORT OFF 
GO
ALTER DATABASE [Payroll] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Payroll] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Payroll] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Payroll] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Payroll] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Payroll] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Payroll] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Payroll] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Payroll] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Payroll] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Payroll] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Payroll] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Payroll] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Payroll] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Payroll] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Payroll] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Payroll] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Payroll] SET RECOVERY FULL 
GO
ALTER DATABASE [Payroll] SET  MULTI_USER 
GO
ALTER DATABASE [Payroll] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Payroll] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Payroll] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Payroll] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Payroll] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Payroll] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Payroll', N'ON'
GO
ALTER DATABASE [Payroll] SET QUERY_STORE = OFF
GO
USE [Payroll]
GO
/****** Object:  Table [dbo].[Dependent]    Script Date: 2/13/2022 8:52:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dependent](
	[FirstName] [nchar](10) NOT NULL,
	[LastName] [nchar](10) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[DependentId] [int] IDENTITY(1,1) NOT NULL,
	[Relation] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[DependentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2/13/2022 8:52:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [nchar](10) NULL,
	[PayCheckAfterDeductions] [decimal](18, 2) NULL,
	[PayCheckBeforeDeductions] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Dependent]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertEmployee]    Script Date: 2/13/2022 8:52:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertEmployee]
@firstName VARCHAR(50),
@lastName VARCHAR(50),
@email VARCHAR(50),
@payCheckBeforeDeductions DECIMAL(18,2),
@payCheckAfterDeductions DECIMAL(18,2),
@spouseFirstName VARCHAR(50),
@spouseLastName VARCHAR(50),
@childFirstName VARCHAR(50),
@childLastName VARCHAR(50)
AS
Begin

    Declare @id int;
	INSERT INTO [dbo].[Employee]
           ([FirstName]
           ,[LastName]
		   ,[Email]
		   ,[PayCheckBeforeDeductions]
		   ,[PayCheckAfterDeductions])
     VALUES
           (@firstName,@lastName,@email,@payCheckBeforeDeductions,@payCheckAfterDeductions)
	SET @id=SCOPE_IDENTITY()

	Print @spouseFirstName
	Print @id
	if(COALESCE(@spouseFirstName, '') <> '')
	BEGIN
		INSERT INTO [dbo].[Dependent]
			   ([FirstName]
			   ,[LastName]
			   ,[EmployeeId]
			   ,[Relation])
		 VALUES
			   (@spouseFirstName
			   ,@spouseLastName
			   ,@id
			   ,'Spouse')
	END
	END
	if(COALESCE(@childFirstName, '') <> '')
	BEGIN
		 INSERT INTO [dbo].[Dependent]
				   ([FirstName]
				   ,[LastName]
				   ,[EmployeeId]
				   ,[Relation])
			 VALUES
				   (@childFirstName
				   ,@childLastName
				   ,@id
				   ,'Child')
     END

GO
/****** Object:  StoredProcedure [dbo].[sp_SelectPayrollEmployee]    Script Date: 2/13/2022 8:52:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SelectPayrollEmployee]
@employeeId int = 0
AS

IF(@employeeId > 0)
BEGIN
	SELECT 
	*
		FROM [Payroll].[dbo].[Employee] E
	where 
		E.EmployeeId = @employeeId	
	SELECT 
		*
	FROM 
		[Payroll].[dbo].[Dependent] 
	where 
		EmployeeId = @employeeId
END
ELSE
BEGIN
	SELECT *
	FROM [Payroll].[dbo].[Employee] 

	SELECT *
	FROM [Payroll].[dbo].[Dependent] 
END
GO
USE [master]
GO
ALTER DATABASE [Payroll] SET  READ_WRITE 
GO
