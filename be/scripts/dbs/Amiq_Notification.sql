USE [master]
GO
/****** Object:  Database [Amiq_Notification]    Script Date: 1/6/2022 5:16:57 PM ******/
CREATE DATABASE [Amiq_Notification]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Amiq_Notification', FILENAME = N'/var/opt/mssql/data/Amiq_Notification.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Amiq_Notification_log', FILENAME = N'/var/opt/mssql/data/Amiq_Notification_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Amiq_Notification] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Amiq_Notification].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Amiq_Notification] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Amiq_Notification] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Amiq_Notification] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Amiq_Notification] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Amiq_Notification] SET ARITHABORT OFF 
GO
ALTER DATABASE [Amiq_Notification] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Amiq_Notification] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Amiq_Notification] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Amiq_Notification] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Amiq_Notification] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Amiq_Notification] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Amiq_Notification] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Amiq_Notification] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Amiq_Notification] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Amiq_Notification] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Amiq_Notification] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Amiq_Notification] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Amiq_Notification] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Amiq_Notification] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Amiq_Notification] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Amiq_Notification] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Amiq_Notification] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Amiq_Notification] SET RECOVERY FULL 
GO
ALTER DATABASE [Amiq_Notification] SET  MULTI_USER 
GO
ALTER DATABASE [Amiq_Notification] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Amiq_Notification] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Amiq_Notification] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Amiq_Notification] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Amiq_Notification] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Amiq_Notification] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Amiq_Notification', N'ON'
GO
ALTER DATABASE [Amiq_Notification] SET QUERY_STORE = OFF
GO
USE [Amiq_Notification]
GO
/****** Object:  Schema [Activity]    Script Date: 1/6/2022 5:16:57 PM ******/
CREATE SCHEMA [Activity]
GO
/****** Object:  Schema [Notification]    Script Date: 1/6/2022 5:16:57 PM ******/
CREATE SCHEMA [Notification]
GO
/****** Object:  Schema [User]    Script Date: 1/6/2022 5:16:57 PM ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Activity].[GroupVisitation]    Script Date: 1/6/2022 5:16:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Activity].[GroupVisitation](
	[GroupVisitationId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[LastVisited] [datetime] NOT NULL,
	[VisitationTotalTime] [bigint] NOT NULL,
 CONSTRAINT [PK_GroupVisitation] PRIMARY KEY CLUSTERED 
(
	[GroupVisitationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Activity].[ProfileVisitation]    Script Date: 1/6/2022 5:16:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Activity].[ProfileVisitation](
	[ProfileVisitationId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[VisitedUserId] [int] NOT NULL,
	[LastVisited] [datetime] NOT NULL,
	[VisitationTotalTime] [bigint] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Notification].[Notification]    Script Date: 1/6/2022 5:16:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Notification].[Notification](
	[NotificationId] [uniqueidentifier] NOT NULL,
	[ImageSrc] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](1000) NOT NULL,
	[Link] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
	[NotificationType] [varchar](3) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Notification].[NotificationQueue]    Script Date: 1/6/2022 5:16:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Notification].[NotificationQueue](
	[NotificationQueueId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_NotificationQueue] PRIMARY KEY CLUSTERED 
(
	[NotificationQueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Notification].[NotificationRequest]    Script Date: 1/6/2022 5:16:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Notification].[NotificationRequest](
	[NotificationRequestId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[VisitedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_NotificationRequest] PRIMARY KEY CLUSTERED 
(
	[NotificationRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[User]    Script Date: 1/6/2022 5:16:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[User](
	[UserId] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Surname] [nvarchar](150) NOT NULL,
	[AvatarPath] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [Activity].[GroupVisitation] ADD  CONSTRAINT [DF_GroupVisitation_GroupVisitationId]  DEFAULT (newid()) FOR [GroupVisitationId]
GO
ALTER TABLE [Activity].[ProfileVisitation] ADD  CONSTRAINT [DF_ProfileVisitation_ProfileVisitationId]  DEFAULT (newid()) FOR [ProfileVisitationId]
GO
ALTER TABLE [Notification].[Notification] ADD  CONSTRAINT [DF_Notification_NotificationId]  DEFAULT (newid()) FOR [NotificationId]
GO
ALTER TABLE [Notification].[NotificationQueue] ADD  CONSTRAINT [DF_NotificationQueue_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Notification].[NotificationRequest] ADD  CONSTRAINT [DF_NotificationRequest_NotificationRequestId]  DEFAULT (newid()) FOR [NotificationRequestId]
GO
ALTER TABLE [Notification].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Notification].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
ALTER TABLE [Notification].[NotificationQueue]  WITH CHECK ADD  CONSTRAINT [FK_NotificationQueue_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [Notification].[NotificationQueue] CHECK CONSTRAINT [FK_NotificationQueue_User]
GO
ALTER TABLE [Notification].[NotificationRequest]  WITH CHECK ADD  CONSTRAINT [FK_NotificationRequest_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Notification].[NotificationRequest] CHECK CONSTRAINT [FK_NotificationRequest_User]
GO
USE [master]
GO
ALTER DATABASE [Amiq_Notification] SET  READ_WRITE 
GO
