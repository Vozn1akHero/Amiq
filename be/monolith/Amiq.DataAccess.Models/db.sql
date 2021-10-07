USE [master]
GO
/****** Object:  Database [Amiq]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE DATABASE [Amiq]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AmicaPlus', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AmicaPlus.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AmicaPlus_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AmicaPlus_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Amiq] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Amiq].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Amiq] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Amiq] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Amiq] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Amiq] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Amiq] SET ARITHABORT OFF 
GO
ALTER DATABASE [Amiq] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Amiq] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Amiq] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Amiq] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Amiq] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Amiq] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Amiq] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Amiq] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Amiq] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Amiq] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Amiq] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Amiq] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Amiq] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Amiq] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Amiq] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Amiq] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Amiq] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Amiq] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Amiq] SET  MULTI_USER 
GO
ALTER DATABASE [Amiq] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Amiq] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Amiq] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Amiq] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Amiq] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Amiq] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Amiq', N'ON'
GO
ALTER DATABASE [Amiq] SET QUERY_STORE = OFF
GO
USE [Amiq]
GO
/****** Object:  Schema [Chat]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE SCHEMA [Chat]
GO
/****** Object:  Schema [Core]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE SCHEMA [Core]
GO
/****** Object:  Schema [Friendship]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE SCHEMA [Friendship]
GO
/****** Object:  Schema [Group]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE SCHEMA [Group]
GO
/****** Object:  Schema [Post]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE SCHEMA [Post]
GO
/****** Object:  Schema [User]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE SCHEMA [User]
GO
/****** Object:  UserDefinedTableType [Chat].[ChatPreview]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE TYPE [Chat].[ChatPreview] AS TABLE(
	[ChatId] [uniqueidentifier] NULL,
	[MessageAuthorId] [int] NULL,
	[MessageId] [uniqueidentifier] NULL,
	[AuthorAvatarPath] [nvarchar](max) NULL,
	[AuthorName] [nvarchar](300) NULL,
	[AuthorSurname] [nvarchar](300) NULL,
	[Message] [nvarchar](1000) NULL
)
GO
/****** Object:  Table [Chat].[Chat]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[Chat](
	[ChatId] [uniqueidentifier] NOT NULL,
	[FirstUserId] [int] NOT NULL,
	[SecondUserId] [int] NOT NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[ChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Chat].[Message]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[Message](
	[MessageId] [uniqueidentifier] NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[TextContent] [nvarchar](250) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Core].[TextBlock]    Script Date: 10/7/2021 9:27:51 AM ******/
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
/****** Object:  Table [Friendship].[FriendRequest]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Friendship].[FriendRequest](
	[FriendRequestId] [uniqueidentifier] NOT NULL,
	[IssuerId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_FriendRequest] PRIMARY KEY CLUSTERED 
(
	[FriendRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Friendship].[Friendship]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Friendship].[Friendship](
	[FriendshipId] [uniqueidentifier] NOT NULL,
	[FirstUserId] [int] NOT NULL,
	[SecondUserId] [int] NOT NULL,
 CONSTRAINT [PK_Friendship] PRIMARY KEY CLUSTERED 
(
	[FriendshipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[Group]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[Group](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[AvatarSrc] [varchar](max) NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupBlockedUsers]    Script Date: 10/7/2021 9:27:51 AM ******/
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
/****** Object:  Table [Group].[GroupDescriptionBlock]    Script Date: 10/7/2021 9:27:51 AM ******/
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
/****** Object:  Table [Group].[GroupParticipant]    Script Date: 10/7/2021 9:27:51 AM ******/
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
 CONSTRAINT [PK_GroupParticipant] PRIMARY KEY CLUSTERED 
(
	[GroupParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[ChildToParrentComment]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[ChildToParrentComment](
	[ChildToParentCommentId] [uniqueidentifier] NOT NULL,
	[MainParentId] [uniqueidentifier] NULL,
	[ParentId] [uniqueidentifier] NULL,
	[CommentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ChildToParrentComment] PRIMARY KEY CLUSTERED 
(
	[ChildToParentCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[Comment]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[Comment](
	[CommentId] [uniqueidentifier] NOT NULL,
	[TextContent] [nvarchar](250) NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[EditedBy] [int] NULL,
	[EditedAt] [datetime] NULL,
	[MainParentId] [uniqueidentifier] NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[GroupPost]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[GroupPost](
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_GroupPost] PRIMARY KEY CLUSTERED 
(
	[GroupPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[Post]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[Post](
	[PostId] [uniqueidentifier] NOT NULL,
	[TextContent] [nvarchar](500) NULL,
	[EditedBy] [int] NULL,
	[EditedAt] [datetime] NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[UserPost]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[UserPost](
	[UserPostId] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserPost] PRIMARY KEY CLUSTERED 
(
	[UserPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[BlockedUsers]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[BlockedUsers](
	[BlockedUsersId] [uniqueidentifier] NOT NULL,
	[IssuerId] [int] NOT NULL,
	[DestUserId] [int] NOT NULL,
 CONSTRAINT [PK_BlockedUsers] PRIMARY KEY CLUSTERED 
(
	[BlockedUsersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[User]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](150) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Surname] [varchar](150) NOT NULL,
	[Password] [varchar](150) NOT NULL,
	[Birthdate] [date] NOT NULL,
	[Sex] [char](1) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[AvatarPath] [varchar](max) NULL,
	[ShortDescription] [nvarchar](400) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [User].[UserDescriptionBlock]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[UserDescriptionBlock](
	[UserDescriptionBlockId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[TextBlockId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserDescriptionBlock] PRIMARY KEY CLUSTERED 
(
	[UserDescriptionBlockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'893af238-e969-4c34-a54b-406223956509', 6, 1)
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'74706169-ad9f-4523-9c53-7c19ffedaf45', 6, 2)
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'74e06ec1-bc09-495c-a6a5-88af43375386', 6, 3)
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', 1, 6)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'3e932c1c-0d0c-43e3-b86d-0a63d533b21f', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'dsfsdf', CAST(N'2021-08-28T00:27:44.963' AS DateTime), 6)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'1052d8be-70b9-40fc-8833-0e6e037896c2', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'hfdhdh', CAST(N'2021-08-28T00:23:31.773' AS DateTime), 6)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'c016cc5a-8a2e-4901-b7dd-1da6d41c09da', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'fgjgfjghj', CAST(N'2021-08-28T00:23:55.187' AS DateTime), 6)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'78893a6c-e955-44e5-b8a3-3776631e5b4f', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'ertertyret', CAST(N'2021-08-28T00:27:37.160' AS DateTime), 6)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'3b473c16-6314-438f-b208-3a7822a9c5ef', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'sdfsdf', CAST(N'2021-08-28T00:27:30.423' AS DateTime), 1)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'd575dc1f-fbfd-46dd-b55b-603f8283b6cb', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'rtyrhbf', CAST(N'2021-08-28T00:27:25.467' AS DateTime), 1)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'12a7b348-74eb-4eab-b156-66c9ef7a5db5', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'ghjghj', CAST(N'2021-08-28T00:26:16.963' AS DateTime), 6)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'd38fabf9-ec6c-4c2d-9562-6d56b0d57ae7', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'khjgrtyrt', CAST(N'2021-08-28T00:26:22.340' AS DateTime), 6)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'17c6e867-111b-ec11-a7f4-d83bbff1afdf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'fdgdfgfdg', CAST(N'2021-09-21T21:23:32.333' AS DateTime), 3)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'8e7287e9-f77a-49d5-b620-e98199396401', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dsadasd', CAST(N'2021-08-28T00:23:23.433' AS DateTime), 2)
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId]) VALUES (N'93ea537f-86e3-4270-9d2c-f9d807d130cf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'tryrytryry', CAST(N'2021-08-28T00:26:31.397' AS DateTime), 3)
GO
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'5d50fc15-adbe-4aef-a7fe-aa694a7d94de', N'Test 2', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'53017af3-420b-4e2e-ac7a-c9aad1d0c20f', N'Test 3 for group id 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'100fdf34-351c-462d-bbd1-e0d338edff24', N'Test 4 for group id 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'b80afd29-1385-41aa-b480-fffac9eb228c', N'Test 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
GO
INSERT [Friendship].[FriendRequest] ([FriendRequestId], [IssuerId], [ReceiverId], [CreatedAt]) VALUES (N'b10caa17-41da-4d53-b6e8-4ef65cf8222e', 3, 6, CAST(N'2021-10-03T23:42:45.713' AS DateTime))
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'e506dc0b-8703-43fb-aa58-436f4129c8f1', 1, 6)
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'e1c7579e-e223-4c82-b8d6-a41154678975', 6, 2)
GO
SET IDENTITY_INSERT [Group].[Group] ON 

INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1, N'Pierwsza grupa', N'group.jpg', N'ghjg sasadfsdf', CAST(N'2021-08-01T22:26:49.717' AS DateTime), 1)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (2, N'gdf', N'group.jpg', NULL, CAST(N'2021-08-01T22:32:07.110' AS DateTime), 1)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1002, N'test3', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:02.317' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1003, N'fhdhd', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:39.110' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1004, N'sfdsfsd', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:40.150' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1005, N'gfertert', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:41.173' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1006, N'fhdhdfh', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:47.680' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1007, N'sfsdfsf', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:49.563' AS DateTime), 6)
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1008, N'Opasdp[sakaolk', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:52.733' AS DateTime), 6)
SET IDENTITY_INSERT [Group].[Group] OFF
GO
INSERT [Group].[GroupDescriptionBlock] ([GroupDescriptionBlockId], [GroupId], [TextBlockId]) VALUES (N'ac8d936f-5662-4041-9162-b327879e7cdf', 1, N'53017af3-420b-4e2e-ac7a-c9aad1d0c20f')
INSERT [Group].[GroupDescriptionBlock] ([GroupDescriptionBlockId], [GroupId], [TextBlockId]) VALUES (N'cacaab14-d6b8-4280-b73d-bcc2398fb857', 1, N'100fdf34-351c-462d-bbd1-e0d338edff24')
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'b225c12f-4313-4484-8e50-45774b619110', 1003, 6, CAST(N'2021-09-30T21:19:07.637' AS DateTime), 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'a6327571-ac71-494a-851a-4b95f3a4007b', 1004, 6, CAST(N'2021-09-30T21:19:10.497' AS DateTime), 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'4b266ee7-97b1-407b-bc9e-63760d6875c8', 2, 6, CAST(N'2021-09-30T21:00:01.950' AS DateTime), 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'de968769-7e26-4412-93c8-6426c26f3b9a', 2, 1, CAST(N'2021-08-01T22:51:10.947' AS DateTime), 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'30be09a2-493d-4fb0-b97b-67a39a6e0f8b', 1, 1, CAST(N'2021-08-01T22:48:14.897' AS DateTime), 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'd4c11f78-2d40-400d-8ecb-77b7a56df7ef', 1, 6, CAST(N'2021-09-30T20:59:59.417' AS DateTime), 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'f9c86cfa-cc89-48a8-b713-a809ea532951', 1005, 6, CAST(N'2021-09-30T21:19:12.640' AS DateTime), 1)
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin]) VALUES (N'3c80bc14-3fe6-467c-a580-d032f1357e41', 1002, 6, CAST(N'2021-09-30T21:19:05.280' AS DateTime), 1)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'2239de37-5d1a-ec11-a7f4-d83bbff1afdf', N'sad', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 1, NULL, CAST(N'2021-09-20T23:53:42.320' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'2439de37-5d1a-ec11-a7f4-d83bbff1afdf', N'dsadad', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, NULL, CAST(N'2021-09-20T23:53:51.990' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', N'hfdhfdh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, NULL, CAST(N'2021-09-20T23:54:04.573' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'040975f2-101b-ec11-a7f4-d83bbff1afdf', N'dfgfdgdfg', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', CAST(N'2021-09-21T21:20:15.280' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', NULL)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'c2b483fd-fe22-ec11-a7fa-d83bbff1afdf', N'fdjhdh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'040975f2-101b-ec11-a7f4-d83bbff1afdf', CAST(N'2021-10-01T23:31:52.193' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', NULL)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'97282111-0023-ec11-a7fa-d83bbff1afdf', N'hfdhdfh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'040975f2-101b-ec11-a7f4-d83bbff1afdf', CAST(N'2021-10-01T23:39:34.597' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', NULL)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'073c8497-bc23-ec11-a7fa-d83bbff1afdf', N'dsadsad', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 6, NULL, CAST(N'2021-10-02T22:09:05.447' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [GroupId]) VALUES (N'3b4d6cb1-bc23-ec11-a7fa-d83bbff1afdf', N'ghkghkgkghkghk', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 6, N'073c8497-bc23-ec11-a7fa-d83bbff1afdf', CAST(N'2021-10-02T22:09:48.910' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'8ca64f89-63d0-44f2-85fc-198bc2fec34b', N'2fd35b83-4c33-47bf-a2a1-449771668756', 1, 6)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'413f7072-daad-467e-a133-22a17394d227', N'7a0f25cc-b8de-4fc3-b8fc-a6435ee8cfa8', 1, 6)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'dd2e1b96-7ec0-4f38-9bca-26d02eb3cd0b', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 1, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'3667d851-8c60-41d9-bc3d-42bafaef9c5c', N'7863166a-3a87-4475-8735-fb1e70a0fe5d', 1, 6)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'04717589-79fa-40c4-b71c-8673977c0421', N'7c4a0a9f-98fa-4798-a2f7-9f74ed6b3509', 1, 6)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'476e148e-9c69-469a-9754-8f0fe6ce7a5b', N'68b82c0e-f1c2-44d8-89d3-63a54f8d6959', 1, 6)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'c96f1610-6285-4211-8d84-dbd9219a5610', N'888ab433-508f-4182-8592-46312cd54c2a', 1, 6)
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'a01ed70f-41f5-4518-b918-07fa333559cd', N'jhyityurtyurthrfhrfher', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'3b6d8f3e-a25a-4a4f-9bab-15caf28b0b71', N'gjhfj', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'2fd35b83-4c33-47bf-a2a1-449771668756', N'fdgdfgfdg', NULL, NULL, CAST(N'2021-10-02T23:01:51.307' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'888ab433-508f-4182-8592-46312cd54c2a', N'gdfsg', NULL, NULL, CAST(N'2021-10-02T23:15:33.253' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', N'loREM dsadikjdgfok ;daslda''; dsfkgjdkgjdkgjld', NULL, NULL, CAST(N'2021-09-12T22:59:50.993' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'68b82c0e-f1c2-44d8-89d3-63a54f8d6959', N'gdsgsdgds', NULL, NULL, CAST(N'2021-10-02T22:59:27.270' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', N'sdfsfdsfsdfsd', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7c4a0a9f-98fa-4798-a2f7-9f74ed6b3509', N'gdfsggdf', NULL, NULL, CAST(N'2021-10-02T23:17:15.883' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7a0f25cc-b8de-4fc3-b8fc-a6435ee8cfa8', N'dsadagfdsg', NULL, NULL, CAST(N'2021-10-02T23:10:01.680' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'41204fb6-5e39-4caa-9487-dcf5e8670f1b', N'Ldsa ksdfjskd fdsf. LDA;SDLK. lsadlsdlals;dald ASDLA;DL', NULL, NULL, CAST(N'2021-09-12T23:00:25.650' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7863166a-3a87-4475-8735-fb1e70a0fe5d', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-02T23:17:56.580' AS DateTime))
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'ee4963f0-d4ce-4e97-ba4f-082c3017ee45', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'c9ee6fba-356e-42e7-af34-48625983e7a5', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'6c85f9ed-6a15-41b0-9df3-d6eca3e183b3', N'41204fb6-5e39-4caa-9487-dcf5e8670f1b', 6)
GO
INSERT [User].[BlockedUsers] ([BlockedUsersId], [IssuerId], [DestUserId]) VALUES (N'0ccee93a-f954-4b40-b1bc-17d218d952ac', 8, 6)
GO
SET IDENTITY_INSERT [User].[User] ON 

INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (1, N'chlenix', N'Dimitr', N'Ruski', N'111', CAST(N'1999-04-10' AS Date), N'M', N'd1@gmw.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (2, N'derek', N'Dimitr', N'Jankowski', N'333', CAST(N'1998-04-11' AS Date), N'M', N'gdfg.dsa2@gmw.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (3, N'karina', N'Katarzyna', N'Brovlowska', N'888', CAST(N'1997-01-02' AS Date), N'F', N'bdsad@gmw.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (6, N'string1', N'String', N'Stringowicz', N'$2a$11$BQXPjo9BoW6HzzIf6F1xIumJ27ajPwgTsrbC5gAsGqSVxkqT9D0Dm', CAST(N'2000-09-04' AS Date), N'M', N'string1@gmail.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (8, N'dimitr1', N'Dimitr', N'Trankowski', N'531259032592', CAST(N'1999-05-11' AS Date), N'M', N'dsad@gmail.com', N'user.jpg', N'Lorem i t.d.')
SET IDENTITY_INSERT [User].[User] OFF
GO
INSERT [User].[UserDescriptionBlock] ([UserDescriptionBlockId], [UserId], [TextBlockId]) VALUES (N'cd8ce9f9-b22a-4d41-a725-3f2ab9cb2638', 6, N'5d50fc15-adbe-4aef-a7fe-aa694a7d94de')
INSERT [User].[UserDescriptionBlock] ([UserDescriptionBlockId], [UserId], [TextBlockId]) VALUES (N'41a26e72-2d80-4e3c-bf95-4df0a916e917', 6, N'b80afd29-1385-41aa-b480-fffac9eb228c')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Group_Name]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_Group_Name] ON [Group].[Group]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Group_ViewName]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_Group_ViewName] ON [Group].[Group]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_UserLogin]    Script Date: 10/7/2021 9:27:51 AM ******/
ALTER TABLE [User].[User] ADD  CONSTRAINT [UC_UserLogin] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_User] ON [User].[User]
(
	[Name] ASC,
	[Surname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_ViewName]    Script Date: 10/7/2021 9:27:51 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_ViewName] ON [User].[User]
(
	[Name] ASC,
	[Surname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [Chat].[Chat] ADD  CONSTRAINT [DF_Chat_ChatId]  DEFAULT (newid()) FOR [ChatId]
GO
ALTER TABLE [Chat].[Message] ADD  CONSTRAINT [DF_Message_MessageId]  DEFAULT (newsequentialid()) FOR [MessageId]
GO
ALTER TABLE [Chat].[Message] ADD  CONSTRAINT [DF_Message_Date]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Core].[TextBlock] ADD  CONSTRAINT [DF_TextBlock_TextBlockId]  DEFAULT (newid()) FOR [TextBlockId]
GO
ALTER TABLE [Friendship].[FriendRequest] ADD  CONSTRAINT [DF_FriendRequest_FriendRequestId]  DEFAULT (newid()) FOR [FriendRequestId]
GO
ALTER TABLE [Friendship].[FriendRequest] ADD  CONSTRAINT [DF_FriendRequest_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Friendship].[Friendship] ADD  CONSTRAINT [DF_Friendship_FriendshipId]  DEFAULT (newid()) FOR [FriendshipId]
GO
ALTER TABLE [Group].[Group] ADD  CONSTRAINT [DF_Group_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_GroupBlockedUserId]  DEFAULT (newid()) FOR [GroupBlockedUserId]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_BanDate]  DEFAULT (getdate()) FOR [BannedAt]
GO
ALTER TABLE [Group].[GroupDescriptionBlock] ADD  CONSTRAINT [DF_GroupDescriptionBlock_GroupDescriptionBlockId]  DEFAULT (newid()) FOR [GroupDescriptionBlockId]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_GroupParticipantId]  DEFAULT (newid()) FOR [GroupParticipantId]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_Joined]  DEFAULT (getdate()) FOR [Joined]
GO
ALTER TABLE [Post].[ChildToParrentComment] ADD  CONSTRAINT [DF_ChildToParrentComment_ChildToParentCommentId]  DEFAULT (newsequentialid()) FOR [ChildToParentCommentId]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CommentId]  DEFAULT (newsequentialid()) FOR [CommentId]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Post].[GroupPost] ADD  CONSTRAINT [DF_GroupPost_GroupPostId]  DEFAULT (newid()) FOR [GroupPostId]
GO
ALTER TABLE [Post].[Post] ADD  CONSTRAINT [DF__Post__PostId__46E78A0C]  DEFAULT (newid()) FOR [PostId]
GO
ALTER TABLE [Post].[Post] ADD  CONSTRAINT [DF_Post_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Post].[UserPost] ADD  CONSTRAINT [DF_UserPost_UserPostId]  DEFAULT (newid()) FOR [UserPostId]
GO
ALTER TABLE [User].[BlockedUsers] ADD  CONSTRAINT [DF_BlockedUsers_BlockedUsersId]  DEFAULT (newid()) FOR [BlockedUsersId]
GO
ALTER TABLE [User].[UserDescriptionBlock] ADD  CONSTRAINT [DF_UserDescriptionBlock_UserDescriptionBlockId]  DEFAULT (newid()) FOR [UserDescriptionBlockId]
GO
ALTER TABLE [Chat].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_User] FOREIGN KEY([FirstUserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Chat].[Chat] CHECK CONSTRAINT [FK_Chat_User]
GO
ALTER TABLE [Chat].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_User1] FOREIGN KEY([SecondUserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Chat].[Chat] CHECK CONSTRAINT [FK_Chat_User1]
GO
ALTER TABLE [Chat].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Chat] FOREIGN KEY([ChatId])
REFERENCES [Chat].[Chat] ([ChatId])
GO
ALTER TABLE [Chat].[Message] CHECK CONSTRAINT [FK_Message_Chat]
GO
ALTER TABLE [Chat].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User] FOREIGN KEY([AuthorId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Chat].[Message] CHECK CONSTRAINT [FK_Message_User]
GO
ALTER TABLE [Friendship].[FriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_FriendRequest_UserIssuer] FOREIGN KEY([IssuerId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[FriendRequest] CHECK CONSTRAINT [FK_FriendRequest_UserIssuer]
GO
ALTER TABLE [Friendship].[FriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_FriendRequest_UserReceiver] FOREIGN KEY([ReceiverId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[FriendRequest] CHECK CONSTRAINT [FK_FriendRequest_UserReceiver]
GO
ALTER TABLE [Friendship].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK_Friendship_FirstUser] FOREIGN KEY([FirstUserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[Friendship] CHECK CONSTRAINT [FK_Friendship_FirstUser]
GO
ALTER TABLE [Friendship].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK_Friendship_SecondUser] FOREIGN KEY([SecondUserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[Friendship] CHECK CONSTRAINT [FK_Friendship_SecondUser]
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
ALTER TABLE [Post].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Post].[Comment] CHECK CONSTRAINT [FK_Comment_Group]
GO
ALTER TABLE [Post].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_MainParentComment] FOREIGN KEY([MainParentId])
REFERENCES [Post].[Comment] ([CommentId])
GO
ALTER TABLE [Post].[Comment] CHECK CONSTRAINT [FK_Comment_MainParentComment]
GO
ALTER TABLE [Post].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_ParentComment] FOREIGN KEY([ParentId])
REFERENCES [Post].[Comment] ([CommentId])
GO
ALTER TABLE [Post].[Comment] CHECK CONSTRAINT [FK_Comment_ParentComment]
GO
ALTER TABLE [Post].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[Comment] CHECK CONSTRAINT [FK_Comment_Post]
GO
ALTER TABLE [Post].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([AuthorId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Post].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [Post].[GroupPost]  WITH CHECK ADD  CONSTRAINT [FK_GroupPost_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
GO
ALTER TABLE [Post].[GroupPost] CHECK CONSTRAINT [FK_GroupPost_Group]
GO
ALTER TABLE [Post].[GroupPost]  WITH CHECK ADD  CONSTRAINT [FK_GroupPost_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[GroupPost] CHECK CONSTRAINT [FK_GroupPost_Post]
GO
ALTER TABLE [Post].[GroupPost]  WITH CHECK ADD  CONSTRAINT [FK_GroupPost_User] FOREIGN KEY([AuthorId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Post].[GroupPost] CHECK CONSTRAINT [FK_GroupPost_User]
GO
ALTER TABLE [Post].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([EditedBy])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Post].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
ALTER TABLE [Post].[UserPost]  WITH CHECK ADD  CONSTRAINT [FK_UserPost_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[UserPost] CHECK CONSTRAINT [FK_UserPost_Post]
GO
ALTER TABLE [Post].[UserPost]  WITH CHECK ADD  CONSTRAINT [FK_UserPost_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Post].[UserPost] CHECK CONSTRAINT [FK_UserPost_User]
GO
ALTER TABLE [User].[BlockedUsers]  WITH CHECK ADD  CONSTRAINT [FK_BlockedUsers_DestUser] FOREIGN KEY([DestUserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [User].[BlockedUsers] CHECK CONSTRAINT [FK_BlockedUsers_DestUser]
GO
ALTER TABLE [User].[BlockedUsers]  WITH CHECK ADD  CONSTRAINT [FK_BlockedUsers_IssuerUser] FOREIGN KEY([IssuerId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [User].[BlockedUsers] CHECK CONSTRAINT [FK_BlockedUsers_IssuerUser]
GO
ALTER TABLE [User].[UserDescriptionBlock]  WITH CHECK ADD  CONSTRAINT [FK_UserDescriptionBlock_TextBlock] FOREIGN KEY([TextBlockId])
REFERENCES [Core].[TextBlock] ([TextBlockId])
GO
ALTER TABLE [User].[UserDescriptionBlock] CHECK CONSTRAINT [FK_UserDescriptionBlock_TextBlock]
GO
ALTER TABLE [User].[UserDescriptionBlock]  WITH CHECK ADD  CONSTRAINT [FK_UserDescriptionBlock_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [User].[UserDescriptionBlock] CHECK CONSTRAINT [FK_UserDescriptionBlock_User]
GO
/****** Object:  StoredProcedure [Chat].[GetChatPreviews]    Script Date: 10/7/2021 9:27:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [Chat].[GetChatPreviews] (@userId int, @length int, @skip int)
as
begin

with cte_CP as (
	select c.ChatId, u.UserId as MessageAuthorId, m.MessageId, m.CreatedAt, u.AvatarPath as AuthorAvatarPath, u.Name as AuthorName, u.Surname as AuthorSurname, m.TextContent as Message,
	ROW_NUMBER() OVER(PARTITION BY m.ChatId ORDER BY m.CreatedAt DESC) AS rank
	from Chat.Chat c left join Chat.Message m on c.ChatId = m.ChatId 
	left join [User].[User] u on m.AuthorId = u.UserId 
	where c.FirstUserId = @userId or c.SecondUserId = @userId)
select cte_CP.ChatId, cte_CP.MessageAuthorId, cte_CP.MessageId, cte_CP.AuthorAvatarPath, cte_CP.AuthorName, cte_CP.AuthorSurname, cte_CP.Message from cte_CP 
where rank = 1
order by cte_CP.ChatId, cte_CP.CreatedAt
offset @skip rows
fetch next @length rows only

end

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id grupy, jeśli komentarz został stworzony przez administratora grupy' , @level0type=N'SCHEMA',@level0name=N'Post', @level1type=N'TABLE',@level1name=N'Comment'
GO
USE [master]
GO
ALTER DATABASE [Amiq] SET  READ_WRITE 
GO
