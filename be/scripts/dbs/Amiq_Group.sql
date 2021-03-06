USE [master]
GO
/****** Object:  Database [Amiq_Group]    Script Date: 1/6/2022 5:16:22 PM ******/
CREATE DATABASE [Amiq_Group]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Amiq_Group', FILENAME = N'/var/opt/mssql/data/Amiq_Group.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Amiq_Group_log', FILENAME = N'/var/opt/mssql/data/Amiq_Group_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Amiq_Group] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Amiq_Group].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Amiq_Group] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Amiq_Group] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Amiq_Group] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Amiq_Group] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Amiq_Group] SET ARITHABORT OFF 
GO
ALTER DATABASE [Amiq_Group] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Amiq_Group] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Amiq_Group] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Amiq_Group] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Amiq_Group] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Amiq_Group] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Amiq_Group] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Amiq_Group] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Amiq_Group] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Amiq_Group] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Amiq_Group] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Amiq_Group] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Amiq_Group] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Amiq_Group] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Amiq_Group] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Amiq_Group] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Amiq_Group] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Amiq_Group] SET RECOVERY FULL 
GO
ALTER DATABASE [Amiq_Group] SET  MULTI_USER 
GO
ALTER DATABASE [Amiq_Group] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Amiq_Group] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Amiq_Group] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Amiq_Group] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Amiq_Group] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Amiq_Group] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Amiq_Group', N'ON'
GO
ALTER DATABASE [Amiq_Group] SET QUERY_STORE = OFF
GO
USE [Amiq_Group]
GO
/****** Object:  Schema [Activity]    Script Date: 1/6/2022 5:16:22 PM ******/
CREATE SCHEMA [Activity]
GO
/****** Object:  Schema [Core]    Script Date: 1/6/2022 5:16:22 PM ******/
CREATE SCHEMA [Core]
GO
/****** Object:  Schema [Group]    Script Date: 1/6/2022 5:16:22 PM ******/
CREATE SCHEMA [Group]
GO
/****** Object:  Schema [User]    Script Date: 1/6/2022 5:16:22 PM ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Core].[TextBlock]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[TextBlock](
	[TextBlockId] [uniqueidentifier] NOT NULL,
	[Header] [nvarchar](150) NOT NULL,
	[Content] [nvarchar](350) NULL,
 CONSTRAINT [PK_TextBlock] PRIMARY KEY CLUSTERED 
(
	[TextBlockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[Group]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[Group](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[AvatarSrc] [varchar](max) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupBlockedUsers]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[GroupBlockedUsers](
	[GroupBlockedUserId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[BannedAt] [datetime] NOT NULL,
	[BannedUntil] [datetime] NULL,
 CONSTRAINT [PK_GroupBlockedUsers] PRIMARY KEY CLUSTERED 
(
	[GroupBlockedUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupDescriptionBlock]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[GroupDescriptionBlock](
	[GroupDescriptionBlockId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[TextBlockId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GroupDescriptionBlock] PRIMARY KEY CLUSTERED 
(
	[GroupDescriptionBlockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupEvent]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[GroupEvent](
	[GroupEventId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Date] [datetime] NOT NULL,
	[AvatarSrc] [varchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsCancelled] [bit] NOT NULL,
	[IsHidden] [bit] NOT NULL,
 CONSTRAINT [PK_GroupEvent] PRIMARY KEY CLUSTERED 
(
	[GroupEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupEventParticipant]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[GroupEventParticipant](
	[GroupEventParticipantId] [uniqueidentifier] NOT NULL,
	[GroupEventId] [uniqueidentifier] NOT NULL,
	[GroupParticipantId] [uniqueidentifier] NOT NULL,
	[JoinedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupEventParticipant] PRIMARY KEY CLUSTERED 
(
	[GroupEventParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupParticipant]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[GroupParticipant](
	[GroupParticipantId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Joined] [datetime] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsParticipantVisible] [bit] NOT NULL,
 CONSTRAINT [PK_GroupParticipant] PRIMARY KEY CLUSTERED 
(
	[GroupParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[HiddenGroup]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[HiddenGroup](
	[HiddenGroupId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_HiddenGroup] PRIMARY KEY CLUSTERED 
(
	[HiddenGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[User]    Script Date: 1/6/2022 5:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[User](
	[UserId] [int] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Surname] [varchar](150) NOT NULL,
	[AvatarPath] [varchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'3d5b4599-bfdc-4a20-9aa7-2eab55b247e1', N'test2', N'dasd')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'5d50fc15-adbe-4aef-a7fe-aa694a7d94de', N'Test 2', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'53017af3-420b-4e2e-ac7a-c9aad1d0c20f', N'Test 3 for group id 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'100fdf34-351c-462d-bbd1-e0d338edff24', N'Test 4 for group id 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'b80afd29-1385-41aa-b480-fffac9eb228c', N'Test 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
GO
SET IDENTITY_INSERT [Group].[Group] ON 

INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1, N'Pierwsza grupa1', N'comment_16325119745a3IksAyNO4ljpojHs4KwM.jpg', N'ghjg sasadfsdf', CAST(N'2021-08-01T22:26:49.717' AS DateTime), 1)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (2, N'gdf', N'group.jpg', NULL, CAST(N'2021-08-01T22:32:07.110' AS DateTime), 1)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1002, N'test3', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:02.317' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1003, N'fhdhd', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:39.110' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1004, N'sfdsfsd', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:40.150' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1005, N'gfertert', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:41.173' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1006, N'fhdhdfh', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:47.680' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1007, N'sfsdfsf', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:49.563' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1008, N'Opasdp[sakaolk', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:52.733' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1009, N'fdsfsdf', N'group.jpg', N'ewtegfdg', CAST(N'2021-10-15T19:53:53.933' AS DateTime), 2)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1011, N'gfghfgh', N'group.jpg', N'klfdgkdl;fgkm', CAST(N'2021-10-15T19:54:02.250' AS DateTime), 2)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1012, N'fdgd', N'group.jpg', N'', CAST(N'2021-11-17T23:53:46.563' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1013, N'test4', N'group.jpg', N'dsg', CAST(N'2021-11-17T23:55:55.003' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1014, N'test5', N'group.jpg', N'fdgdfg', CAST(N'2021-11-17T23:55:58.457' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1015, N'test323', N'group.jpg', N'7', CAST(N'2021-11-17T23:56:23.693' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1016, N'testCr', N'group.jpg', N'dd', CAST(N'2021-12-05T23:49:57.250' AS DateTime), 6)
SET IDENTITY_INSERT [Group].[Group] OFF
GO
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'28e01953-d0a7-4b67-91f3-109355f5955b', 1, 21, CAST(N'2021-11-04T20:48:51.013' AS DateTime), NULL)
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'dc81ed58-685f-4d7f-820d-ad77dea3304f', 1, 21, CAST(N'2021-11-04T20:45:18.037' AS DateTime), NULL)
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'9827a289-b8a1-4038-8624-dd226c99473e', 1, 21, CAST(N'2021-11-04T20:49:49.533' AS DateTime), NULL)
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'2a971cca-8b0c-40b7-8f82-e7131a7b3380', 1, 21, CAST(N'2021-11-04T20:49:04.293' AS DateTime), NULL)
GO
INSERT [Group].[GroupDescriptionBlock] ([GroupDescriptionBlockId], [GroupId], [TextBlockId]) VALUES (N'ac8d936f-5662-4041-9162-b327879e7cdf', 1, N'53017af3-420b-4e2e-ac7a-c9aad1d0c20f')
INSERT [Group].[GroupDescriptionBlock] ([GroupDescriptionBlockId], [GroupId], [TextBlockId]) VALUES (N'cacaab14-d6b8-4280-b73d-bcc2398fb857', 1, N'100fdf34-351c-462d-bbd1-e0d338edff24')
INSERT [Group].[GroupDescriptionBlock] ([GroupDescriptionBlockId], [GroupId], [TextBlockId]) VALUES (N'234350e9-a743-4295-91d6-fa73e7641250', 1, N'3d5b4599-bfdc-4a20-9aa7-2eab55b247e1')
GO
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'3f109df3-9e2e-ec11-a809-d83bbff1afdf', 1, N'Test1', CAST(N'2021-10-20T21:51:28.427' AS DateTime), N'user.jpg', CAST(N'2021-10-16T18:34:37.850' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 0)
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'40900330-a02e-ec11-a809-d83bbff1afdf', 1, N'Test2', CAST(N'2021-10-21T21:10:00.000' AS DateTime), N'user.jpg', CAST(N'2021-10-16T18:43:28.680' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 0)
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'a88f73f1-b72e-ec11-a809-d83bbff1afdf', 1, N'Test3', CAST(N'2021-10-21T21:10:00.000' AS DateTime), N'user.jpg', CAST(N'2021-10-16T18:50:00.000' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 0)
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'c9c5f500-b82e-ec11-a809-d83bbff1afdf', 1, N'Test4', CAST(N'2021-10-21T21:30:00.000' AS DateTime), N'user.jpg', CAST(N'2021-10-16T21:33:57.660' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 1)
GO
INSERT [Group].[GroupEventParticipant] ([GroupEventParticipantId], [GroupEventId], [GroupParticipantId], [JoinedAt]) VALUES (N'fe11e13d-90c8-4a34-91c3-8fb944cb1f57', N'3f109df3-9e2e-ec11-a809-d83bbff1afdf', N'd4c11f78-2d40-400d-8ecb-77b7a56df7ef', CAST(N'2021-11-03T22:54:29.557' AS DateTime))
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'38394d7f-3c91-4053-b379-01b15ca509a2', 1013, 6, CAST(N'2021-11-17T23:55:55.013' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'5e2c01e4-261b-40fb-8ef9-09b781169097', 1011, 6, CAST(N'2021-10-15T21:51:28.427' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'c9886445-4604-472b-8321-14d31fc5d33c', 1, 17, CAST(N'2021-11-01T16:54:47.507' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'cdefced3-c315-4ee6-b979-179ab51370c0', 1, 16, CAST(N'2021-11-01T16:54:44.770' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'e3199f56-647d-4894-9ff6-18aa37dd633b', 1011, 2, CAST(N'2021-10-15T21:47:39.873' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'b295575b-5959-4f19-8ca7-23253ca51c47', 1, 14, CAST(N'2021-11-01T16:54:37.270' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'8ce5c08a-692a-4a9f-8308-2403de41fc48', 1, 12, CAST(N'2021-11-01T16:54:13.707' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'68594dd9-0af0-4586-bfd6-27618000ca84', 1, 21, CAST(N'2021-11-01T16:54:58.467' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'bdd18498-abc9-4be2-b0ea-2f2df1c2e604', 1, 15, CAST(N'2021-11-01T16:54:40.933' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'b225c12f-4313-4484-8e50-45774b619110', 1003, 6, CAST(N'2021-09-30T21:19:07.637' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'1669c860-7264-4be4-bdb1-49ab979d056b', 1, 20, CAST(N'2021-11-01T16:54:55.810' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'bd93eab7-7368-435a-b892-49d501681a40', 1, 23, CAST(N'2021-11-01T16:55:03.780' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'a6327571-ac71-494a-851a-4b95f3a4007b', 1004, 6, CAST(N'2021-09-30T21:19:10.497' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'f1c35a73-4707-42df-97aa-5cdfa9f33ae1', 1, 19, CAST(N'2021-11-01T16:54:53.190' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'4b266ee7-97b1-407b-bc9e-63760d6875c8', 2, 6, CAST(N'2021-09-30T21:00:01.950' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'de968769-7e26-4412-93c8-6426c26f3b9a', 2, 1, CAST(N'2021-08-01T22:51:10.947' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'd0070c60-adac-48e7-a8e1-66650ae09357', 1, 22, CAST(N'2021-11-01T16:55:00.807' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'30be09a2-493d-4fb0-b97b-67a39a6e0f8b', 1, 1, CAST(N'2021-08-01T22:48:14.897' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'e1cce80d-705e-4390-836f-6a669e826013', 1012, 6, CAST(N'2021-11-17T23:53:46.570' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'd4c11f78-2d40-400d-8ecb-77b7a56df7ef', 1, 6, CAST(N'2021-09-30T20:59:59.417' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'b5e070c4-25f2-4cc2-9009-780980e2cd32', 1, 11, CAST(N'2021-11-01T16:54:10.403' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'1254ff7a-4759-4648-8166-8e105f5bc934', 1009, 6, CAST(N'2021-11-07T17:05:38.437' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'9c999d3f-be3c-4ae7-8026-8e549864ce22', 1, 10, CAST(N'2021-11-01T16:54:02.510' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'8b49b93b-2250-4bf4-815a-976b723f1232', 1014, 6, CAST(N'2021-11-17T23:55:58.460' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'a0bae610-2a08-4a76-8823-9eb8810e35b2', 1, 18, CAST(N'2021-11-01T16:54:50.390' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'f9c86cfa-cc89-48a8-b713-a809ea532951', 1005, 6, CAST(N'2021-09-30T21:19:12.640' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'3c80bc14-3fe6-467c-a580-d032f1357e41', 1002, 6, CAST(N'2021-09-30T21:19:05.280' AS DateTime), 1, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'3ed5ff7e-cab5-4f56-80cc-fc8a33c33abb', 1015, 6, CAST(N'2021-11-17T23:56:23.703' AS DateTime), 0, 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'ca4fefb8-1af0-4ad0-851a-fe2e10a0993d', 1, 13, CAST(N'2021-11-01T16:54:35.043' AS DateTime), 0, 1)
GO
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (1, N'Dimitr', N'Ruski', N'user.jpg')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (2, N'Dimitr', N'Jankowski', N'user.jpg')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (3, N'Katarzyna', N'Brovlowska', N'user.jpg')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (6, N'Dmytro', N'Vozniachuk', N'user.jpg')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (8, N'Dimitr', N'Trankowski', N'user.jpg')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (10, N'Aleksandra', N'Jablonski', N'thispersondoesnotexistcom9.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (11, N'Anna', N'Kowalczyk', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (12, N'Mariusz', N'Kowalski', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (13, N'Artur', N'Kaminski', N'thispersondoesnotexistcom3.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (14, N'Marian', N'Piotrowski', N'thispersondoesnotexistcom13.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (15, N'Maria', N'Zielinski', N'thispersondoesnotexistcom19.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (16, N'Stanislawa', N'Wisniewski', N'thispersondoesnotexistcom15.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (17, N'Wojciech', N'Michalski', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (18, N'Karolina', N'Lewandowski', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (19, N'Janina', N'Krawczyk', N'thispersondoesnotexistcom6.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (20, N'Dariusz', N'Olszewski', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (21, N'Ryszard', N'Majewski', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (22, N'Kamil', N'Wieczorek', N'thispersondoesnotexistcom17.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (23, N'Lukasz', N'Jablonski', N'thispersondoesnotexistcom7.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (24, N'Robert', N'Mazur', N'thispersondoesnotexistcom7.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (25, N'Wieslaw', N'Wisniewski', N'thispersondoesnotexistcom12.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (26, N'Teresa', N'Piotrowski', N'thispersondoesnotexistcom16.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (27, N'Jaroslaw', N'Zajac', N'thispersondoesnotexistcom17.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (28, N'Ryszard', N'Majewski', N'thispersondoesnotexistcom11.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (29, N'Edward', N'Zielinski', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (30, N'Mariusz', N'Kowalski', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (31, N'Maria', N'Zielinski', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (32, N'Irena', N'Adamczyk', N'thispersondoesnotexistcom20.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (33, N'Ryszard', N'Majewski', N'thispersondoesnotexistcom7.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (34, N'Wieslaw', N'Wisniewski', N'thispersondoesnotexistcom9.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (35, N'Marianna', N'Olszewski', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (36, N'Joanna', N'Grabowski', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (37, N'Irena', N'Adamczyk', N'thispersondoesnotexistcom9.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (38, N'Slawomir', N'Dudek', N'thispersondoesnotexistcom20.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (39, N'Stanislawa', N'Wisniewski', N'thispersondoesnotexistcom4.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (40, N'Miroslaw', N'Nowicki', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (41, N'Artur', N'Kaminski', N'thispersondoesnotexistcom18.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (42, N'Bozena', N'Wójcik', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (43, N'Dariusz', N'Olszewski', N'thispersondoesnotexistcom7.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (44, N'Bozena', N'Wójcik', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (45, N'Marta', N'Król', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (46, N'Mateusz', N'Krawczyk', N'thispersondoesnotexistcom17.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (47, N'Wojciech', N'Michalski', N'thispersondoesnotexistcom18.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (48, N'Marianna', N'Olszewski', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (49, N'Aleksandra', N'Jablonski', N'thispersondoesnotexistcom7.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (50, N'JanuszKazimierz', N'Pawlowski', N'thispersondoesnotexistcom8.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (51, N'Edward', N'Zielinski', N'thispersondoesnotexistcom16.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (52, N'Slawomir', N'Dudek', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (53, N'Wladyslaw', N'Lewandowski', N'thispersondoesnotexistcom16.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (54, N'Marian', N'Piotrowski', N'thispersondoesnotexistcom12.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (55, N'Kamil', N'Wieczorek', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (56, N'Jaroslaw', N'Zajac', N'thispersondoesnotexistcom15.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (57, N'Wojciech', N'Michalski', N'thispersondoesnotexistcom2.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (58, N'Urszula', N'Kaminski', N'thispersondoesnotexistcom15.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (59, N'Zdzislaw', N'Kowalczyk', N'thispersondoesnotexistcom8.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (60, N'Janina', N'Krawczyk', N'thispersondoesnotexistcom20.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (61, N'Miroslaw', N'Nowicki', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (62, N'Wladyslaw', N'Lewandowski', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (63, N'Miroslaw', N'Nowicki', N'thispersondoesnotexistcom10.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (64, N'Mariusz', N'Kowalski', N'thispersondoesnotexistcom11.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (65, N'Marianna', N'Olszewski', N'thispersondoesnotexistcom8.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (66, N'Irena', N'Adamczyk', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (67, N'Anna', N'Kowalczyk', N'thispersondoesnotexistcom15.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (68, N'Joanna', N'Mazur', N'thispersondoesnotexistcom18.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (69, N'Janina', N'Krawczyk', N'thispersondoesnotexistcom10.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (70, N'Danuta', N'Nowicki', N'thispersondoesnotexistcom13.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (71, N'Iwona', N'Dabrowski', N'thispersondoesnotexistcom17.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (72, N'Mateusz', N'Krawczyk', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (73, N'Miroslaw', N'Nowicki', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (74, N'Rafal', N'Grabowski', N'thispersondoesnotexistcom17.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (75, N'Mateusz', N'Krawczyk', N'thispersondoesnotexistcom20.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (76, N'Mariusz', N'Kowalski', N'thispersondoesnotexistcom6.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (77, N'Edward', N'Zielinski', N'thispersondoesnotexistcom12.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (78, N'Marian', N'Piotrowski', N'thispersondoesnotexistcom3.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (79, N'Kamil', N'Wieczorek', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (80, N'Janina', N'Krawczyk', N'thispersondoesnotexistcom17.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (81, N'Beata', N'Wieczorek', N'thispersondoesnotexistcom16.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (82, N'Jaroslaw', N'Zajac', N'thispersondoesnotexistcom4.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (83, N'Jacek', N'Nowakowski', N'thispersondoesnotexistcom15.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (84, N'Halina', N'Dudek', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (85, N'Slawomir', N'Dudek', N'thispersondoesnotexistcom8.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (86, N'Stanislawa', N'Wisniewski', N'thispersondoesnotexistcom3.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (87, N'Artur', N'Kaminski', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (88, N'Edward', N'Zielinski', N'thispersondoesnotexistcom7.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (89, N'Joanna', N'Grabowski', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (90, N'Marian', N'Piotrowski', N'thispersondoesnotexistcom6.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (91, N'Artur', N'Kaminski', N'thispersondoesnotexistcom14.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (92, N'Jadwiga', N'Michalski', N'thispersondoesnotexistcom7.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (93, N'Janina', N'Krawczyk', N'thispersondoesnotexistcom15.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (94, N'Jacek', N'Nowakowski', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (95, N'Monika', N'Pawlowski', N'thispersondoesnotexistcom13.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (96, N'Marian', N'Piotrowski', N'thispersondoesnotexistcom6.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (97, N'Edward', N'Zielinski', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (98, N'Aleksandra', N'Jablonski', N'thispersondoesnotexistcom13.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (99, N'Irena', N'Adamczyk', N'thispersondoesnotexistcom1.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (100, N'Jacek', N'Nowakowski', N'thispersondoesnotexistcom12.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (101, N'Halina', N'Dudek', N'thispersondoesnotexistcom4.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (102, N'Mateusz', N'Krawczyk', N'thispersondoesnotexistcom11.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (103, N'Zbigniew', N'Król', N'thispersondoesnotexistcom22.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (104, N'Marianna', N'Olszewski', N'thispersondoesnotexistcom15.jfif')
GO
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (105, N'Magdalena', N'Nowakowski', N'thispersondoesnotexistcom18.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (106, N'Joanna', N'Mazur', N'thispersondoesnotexistcom2.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (107, N'Marta', N'Król', N'thispersondoesnotexistcom15.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (108, N'Jolanta', N'Kowalski', N'thispersondoesnotexistcom5.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (109, N'Janina', N'Krawczyk', N'thispersondoesnotexistcom14.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (110, N'Joanna', N'Mazur', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (111, N'Irena', N'Adamczyk', N'thispersondoesnotexistcom19.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (112, N'Jacek', N'Nowakowski', N'thispersondoesnotexistcom2.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (113, N'Magdalena', N'Nowakowski', N'thispersondoesnotexistcom21.jfif')
INSERT [User].[User] ([UserId], [Name], [Surname], [AvatarPath]) VALUES (114, N'Marian', N'Piotrowski', N'thispersondoesnotexistcom9.jfif')
GO
ALTER TABLE [Core].[TextBlock] ADD  CONSTRAINT [DF_TextBlock_TextBlockId]  DEFAULT (newid()) FOR [TextBlockId]
GO
ALTER TABLE [Group].[Group] ADD  CONSTRAINT [DF_Group_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_GroupBlockedUserId]  DEFAULT (newid()) FOR [GroupBlockedUserId]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_BanDate]  DEFAULT (getdate()) FOR [BannedAt]
GO
ALTER TABLE [Group].[GroupDescriptionBlock] ADD  CONSTRAINT [DF_GroupDescriptionBlock_GroupDescriptionBlockId]  DEFAULT (newid()) FOR [GroupDescriptionBlockId]
GO
ALTER TABLE [Group].[GroupEvent] ADD  CONSTRAINT [DF_GroupEvent_GroupEventId]  DEFAULT (newsequentialid()) FOR [GroupEventId]
GO
ALTER TABLE [Group].[GroupEvent] ADD  CONSTRAINT [DF_GroupEvent_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Group].[GroupEvent] ADD  CONSTRAINT [DF_GroupEvent_IsCancelled]  DEFAULT ((0)) FOR [IsCancelled]
GO
ALTER TABLE [Group].[GroupEvent] ADD  CONSTRAINT [DF_GroupEvent_IsHidden]  DEFAULT ((0)) FOR [IsHidden]
GO
ALTER TABLE [Group].[GroupEventParticipant] ADD  CONSTRAINT [DF_GroupEventParticipant_GroupEventParticipantId]  DEFAULT (newid()) FOR [GroupEventParticipantId]
GO
ALTER TABLE [Group].[GroupEventParticipant] ADD  CONSTRAINT [DF_GroupEventParticipant_JoinedAt]  DEFAULT (getdate()) FOR [JoinedAt]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_GroupParticipantId]  DEFAULT (newid()) FOR [GroupParticipantId]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_Joined]  DEFAULT (getdate()) FOR [Joined]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_IsParticipantVisible]  DEFAULT ((1)) FOR [IsParticipantVisible]
GO
ALTER TABLE [Group].[HiddenGroup] ADD  CONSTRAINT [DF_HiddenGroup_HiddenGroupId]  DEFAULT (newid()) FOR [HiddenGroupId]
GO
ALTER TABLE [Group].[Group]  WITH CHECK ADD  CONSTRAINT [FK_GroupCreator_User] FOREIGN KEY([CreatedBy])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Group].[Group] CHECK CONSTRAINT [FK_GroupCreator_User]
GO
ALTER TABLE [Group].[GroupBlockedUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupBlockedUsers_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
GO
ALTER TABLE [Group].[GroupBlockedUsers] CHECK CONSTRAINT [FK_GroupBlockedUsers_Group]
GO
ALTER TABLE [Group].[GroupBlockedUsers]  WITH CHECK ADD  CONSTRAINT [FK_GroupBlockedUsers_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Group].[GroupBlockedUsers] CHECK CONSTRAINT [FK_GroupBlockedUsers_User]
GO
ALTER TABLE [Group].[GroupDescriptionBlock]  WITH CHECK ADD  CONSTRAINT [FK_GroupDescriptionBlock_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
ON DELETE CASCADE
GO
ALTER TABLE [Group].[GroupDescriptionBlock] CHECK CONSTRAINT [FK_GroupDescriptionBlock_Group]
GO
ALTER TABLE [Group].[GroupDescriptionBlock]  WITH CHECK ADD  CONSTRAINT [FK_GroupDescriptionBlock_TextBlock] FOREIGN KEY([TextBlockId])
REFERENCES [Core].[TextBlock] ([TextBlockId])
GO
ALTER TABLE [Group].[GroupDescriptionBlock] CHECK CONSTRAINT [FK_GroupDescriptionBlock_TextBlock]
GO
ALTER TABLE [Group].[GroupEvent]  WITH CHECK ADD  CONSTRAINT [FK_GroupEvent_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
GO
ALTER TABLE [Group].[GroupEvent] CHECK CONSTRAINT [FK_GroupEvent_Group]
GO
ALTER TABLE [Group].[GroupEvent]  WITH CHECK ADD  CONSTRAINT [FK_GroupEvent_User] FOREIGN KEY([CreatedBy])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Group].[GroupEvent] CHECK CONSTRAINT [FK_GroupEvent_User]
GO
ALTER TABLE [Group].[GroupEventParticipant]  WITH CHECK ADD  CONSTRAINT [FK_GroupEventParticipant_GroupEvent] FOREIGN KEY([GroupEventId])
REFERENCES [Group].[GroupEvent] ([GroupEventId])
ON DELETE CASCADE
GO
ALTER TABLE [Group].[GroupEventParticipant] CHECK CONSTRAINT [FK_GroupEventParticipant_GroupEvent]
GO
ALTER TABLE [Group].[GroupEventParticipant]  WITH CHECK ADD  CONSTRAINT [FK_GroupEventParticipant_GroupParticipant] FOREIGN KEY([GroupParticipantId])
REFERENCES [Group].[GroupParticipant] ([GroupParticipantId])
ON DELETE CASCADE
GO
ALTER TABLE [Group].[GroupEventParticipant] CHECK CONSTRAINT [FK_GroupEventParticipant_GroupParticipant]
GO
ALTER TABLE [Group].[GroupParticipant]  WITH CHECK ADD  CONSTRAINT [FK_GroupParticipant_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
ON DELETE CASCADE
GO
ALTER TABLE [Group].[GroupParticipant] CHECK CONSTRAINT [FK_GroupParticipant_Group]
GO
ALTER TABLE [Group].[GroupParticipant]  WITH CHECK ADD  CONSTRAINT [FK_GroupParticipant_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Group].[GroupParticipant] CHECK CONSTRAINT [FK_GroupParticipant_User]
GO
ALTER TABLE [Group].[HiddenGroup]  WITH CHECK ADD  CONSTRAINT [FK_HiddenGroup_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
GO
ALTER TABLE [Group].[HiddenGroup] CHECK CONSTRAINT [FK_HiddenGroup_Group]
GO
ALTER TABLE [Group].[HiddenGroup]  WITH CHECK ADD  CONSTRAINT [FK_HiddenGroup_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Group].[HiddenGroup] CHECK CONSTRAINT [FK_HiddenGroup_User]
GO
USE [master]
GO
ALTER DATABASE [Amiq_Group] SET  READ_WRITE 
GO
