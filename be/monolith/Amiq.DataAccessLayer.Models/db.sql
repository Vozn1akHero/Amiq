USE [master]
GO
/****** Object:  Database [Amiq]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE DATABASE [Amiq]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Amiq', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Amiq.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Amiq_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Amiq.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Schema [Chat]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE SCHEMA [Chat]
GO
/****** Object:  Schema [Core]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE SCHEMA [Core]
GO
/****** Object:  Schema [Friendship]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE SCHEMA [Friendship]
GO
/****** Object:  Schema [Group]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE SCHEMA [Group]
GO
/****** Object:  Schema [Notification]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE SCHEMA [Notification]
GO
/****** Object:  Schema [Post]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE SCHEMA [Post]
GO
/****** Object:  Schema [User]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE SCHEMA [User]
GO
/****** Object:  UserDefinedTableType [Chat].[ChatPreview]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Chat].[Chat]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Chat].[Message]    Script Date: 11/16/2021 11:07:26 AM ******/
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
	[IsReadByReceiver] [bit] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Core].[TextBlock]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Friendship].[FriendRequest]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Friendship].[Friendship]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Group].[Group]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Group].[GroupBlockedUsers]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Group].[GroupDescriptionBlock]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Group].[GroupEvent]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Group].[GroupEventParticipant]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Group].[GroupParticipant]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Group].[HiddenGroup]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Notification].[Notification]    Script Date: 11/16/2021 11:07:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Notification].[Notification](
	[NotificationId] [uniqueidentifier] NOT NULL,
	[NotificationTypeId] [int] NULL,
	[ImageSrc] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](1000) NOT NULL,
	[Link] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Notification].[NotificationType]    Script Date: 11/16/2021 11:07:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Notification].[NotificationType](
	[NotificationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_NotificationType_1] PRIMARY KEY CLUSTERED 
(
	[NotificationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[ChildToParrentComment]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Post].[Comment]    Script Date: 11/16/2021 11:07:26 AM ******/
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
	[IsRemoved] [bit] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[GroupPost]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Post].[GroupPostComment]    Script Date: 11/16/2021 11:07:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[GroupPostComment](
	[GroupPostCommentId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[CommentId] [uniqueidentifier] NOT NULL,
	[AuthorVisibilityType] [varchar](3) NOT NULL,
	[MainParentId] [uniqueidentifier] NULL,
	[ParentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GroupPostComment] PRIMARY KEY CLUSTERED 
(
	[GroupPostCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[Post]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [Post].[UserPost]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [User].[BlockedUsers]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [User].[Session]    Script Date: 11/16/2021 11:07:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [User].[Session](
	[SessionId] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[StartedAt] [datetime] NOT NULL,
	[EndedAt] [datetime] NOT NULL,
	[SessionToken] [varchar](1000) NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[User]    Script Date: 11/16/2021 11:07:26 AM ******/
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
/****** Object:  Table [User].[UserDescriptionBlock]    Script Date: 11/16/2021 11:07:26 AM ******/
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
GO
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'bbd8302b-3c76-4c11-a4d2-57070866987c', 6, 11)
GO
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'74706169-ad9f-4523-9c53-7c19ffedaf45', 6, 2)
GO
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'74e06ec1-bc09-495c-a6a5-88af43375386', 6, 3)
GO
INSERT [Chat].[Chat] ([ChatId], [FirstUserId], [SecondUserId]) VALUES (N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', 1, 6)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'3e932c1c-0d0c-43e3-b86d-0a63d533b21f', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'dsfsdf', CAST(N'2021-08-28T00:27:44.963' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'1052d8be-70b9-40fc-8833-0e6e037896c2', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'hfdhdh', CAST(N'2021-08-28T00:23:31.773' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'c016cc5a-8a2e-4901-b7dd-1da6d41c09da', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'fgjgfjghj', CAST(N'2021-08-28T00:23:55.187' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'78893a6c-e955-44e5-b8a3-3776631e5b4f', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'ertertyret', CAST(N'2021-08-28T00:27:37.160' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'3b473c16-6314-438f-b208-3a7822a9c5ef', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'sdfsdf', CAST(N'2021-08-28T00:27:30.423' AS DateTime), 1, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'd575dc1f-fbfd-46dd-b55b-603f8283b6cb', N'c3ed431f-d9ef-406d-9227-9cc8da3970e0', N'rtyrhbf', CAST(N'2021-08-28T00:27:25.467' AS DateTime), 1, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'12a7b348-74eb-4eab-b156-66c9ef7a5db5', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'ghjghj', CAST(N'2021-08-28T00:26:16.963' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'd38fabf9-ec6c-4c2d-9562-6d56b0d57ae7', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'khjgrtyrt', CAST(N'2021-08-28T00:26:22.340' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'17c6e867-111b-ec11-a7f4-d83bbff1afdf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'fdgdfgfdg', CAST(N'2021-09-21T21:23:32.333' AS DateTime), 3, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'f4918f1d-ad27-ec11-a7fd-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'gdsgsg', CAST(N'2021-10-07T22:28:23.040' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'a6e72612-ae27-ec11-a7fd-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dfdasdasdsad', CAST(N'2021-10-07T22:35:13.397' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'a7e72612-ae27-ec11-a7fd-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dfdasdasdsad', CAST(N'2021-10-07T22:35:22.117' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'ecfa2c18-ae27-ec11-a7fd-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dfdasdasdsad', CAST(N'2021-10-07T22:35:23.503' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'edfa2c18-ae27-ec11-a7fd-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dfdasdasdsad', CAST(N'2021-10-07T22:35:26.560' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'eefa2c18-ae27-ec11-a7fd-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dfdasdasdsad', CAST(N'2021-10-07T22:35:30.050' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'19819597-172d-ec11-a803-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dsadsad', CAST(N'2021-10-14T19:53:10.227' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'1eb1009f-172d-ec11-a803-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'cxzczxc', CAST(N'2021-10-14T19:53:22.673' AS DateTime), 6, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'13f5f917-a345-ec11-a828-d83bbff1afdf', N'bbd8302b-3c76-4c11-a4d2-57070866987c', N'Cześć', CAST(N'2021-11-15T00:32:13.587' AS DateTime), 11, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'5654ed1e-a345-ec11-a828-d83bbff1afdf', N'bbd8302b-3c76-4c11-a4d2-57070866987c', N'hej', CAST(N'2021-11-15T00:32:25.247' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'207ea525-a345-ec11-a828-d83bbff1afdf', N'bbd8302b-3c76-4c11-a4d2-57070866987c', N'co tam?', CAST(N'2021-11-15T00:32:36.520' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'bbe79330-a345-ec11-a828-d83bbff1afdf', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'gfdgdg', CAST(N'2021-11-15T00:32:54.860' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'02879914-5746-ec11-a828-d83bbff1afdf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'dfdsfsf', CAST(N'2021-11-15T22:00:37.333' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'03879914-5746-ec11-a828-d83bbff1afdf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'dfdsfsffdsfdsfsf', CAST(N'2021-11-15T22:00:39.253' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'04879914-5746-ec11-a828-d83bbff1afdf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'dfdsfsffdsfdsfsfdwadasd', CAST(N'2021-11-15T22:00:41.120' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'f36c781b-5746-ec11-a828-d83bbff1afdf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'test', CAST(N'2021-11-15T22:00:48.860' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'f46c781b-5746-ec11-a828-d83bbff1afdf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'test2', CAST(N'2021-11-15T22:00:51.970' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'4c1468bf-5746-ec11-a828-d83bbff1afdf', N'bbd8302b-3c76-4c11-a4d2-57070866987c', N'dsadad', CAST(N'2021-11-15T22:05:23.900' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'7c7080c5-5746-ec11-a828-d83bbff1afdf', N'bbd8302b-3c76-4c11-a4d2-57070866987c', N'dsadad', CAST(N'2021-11-15T22:05:34.127' AS DateTime), 6, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'984abcde-5a46-ec11-a828-d83bbff1afdf', N'bbd8302b-3c76-4c11-a4d2-57070866987c', N'dd', CAST(N'2021-11-15T22:27:44.950' AS DateTime), 11, 0)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'8e7287e9-f77a-49d5-b620-e98199396401', N'74706169-ad9f-4523-9c53-7c19ffedaf45', N'dsadasd', CAST(N'2021-08-28T00:23:23.433' AS DateTime), 2, 1)
GO
INSERT [Chat].[Message] ([MessageId], [ChatId], [TextContent], [CreatedAt], [AuthorId], [IsReadByReceiver]) VALUES (N'93ea537f-86e3-4270-9d2c-f9d807d130cf', N'74e06ec1-bc09-495c-a6a5-88af43375386', N'tryrytryry', CAST(N'2021-08-28T00:26:31.397' AS DateTime), 3, 1)
GO
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'5d50fc15-adbe-4aef-a7fe-aa694a7d94de', N'Test 2', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
GO
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'53017af3-420b-4e2e-ac7a-c9aad1d0c20f', N'Test 3 for group id 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
GO
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'100fdf34-351c-462d-bbd1-e0d338edff24', N'Test 4 for group id 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
GO
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'b80afd29-1385-41aa-b480-fffac9eb228c', N'Test 1', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.')
GO
INSERT [Friendship].[FriendRequest] ([FriendRequestId], [IssuerId], [ReceiverId], [CreatedAt]) VALUES (N'a1840cef-327e-4284-8314-14e328711abd', 6, 14, CAST(N'2021-10-29T22:54:47.937' AS DateTime))
GO
INSERT [Friendship].[FriendRequest] ([FriendRequestId], [IssuerId], [ReceiverId], [CreatedAt]) VALUES (N'775fc049-8f35-41e5-ae7c-3a3653778045', 6, 16, CAST(N'2021-10-29T22:54:53.077' AS DateTime))
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'03b60042-0493-4f4a-be9c-241bf800a125', 6, 3)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'5e8e7a6b-2e48-4a52-8559-2cc9fbf70a8f', 38, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'b3c48d04-08c2-45db-8cc5-2d92638f4ab3', 25, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'50017717-7afa-4bff-a750-35c6573a8eb1', 6, 11)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'81a13140-2687-4393-b870-3653606742ae', 36, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'e506dc0b-8703-43fb-aa58-436f4129c8f1', 1, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'f6615d0b-1ae7-45e5-8ac8-586b73ceca6c', 26, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'3a1ac1d9-48ae-40df-a941-63d0a8c51892', 6, 21)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'fef14afd-cc05-42db-8227-69bf616d3d0c', 31, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'f1adab6c-2dd7-4744-93b3-69c23ead3721', 6, 70)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'36db3208-36b6-4073-944b-70be49b6330d', 22, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'cd1dc639-a082-42b2-a3a9-82d9a2e970ec', 32, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'b340b23e-f557-4e86-a412-8988863fb3f3', 29, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'e1c7579e-e223-4c82-b8d6-a41154678975', 6, 2)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'81f6616f-fffd-41d8-a516-b8b42d48d22f', 6, 20)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'2d09872b-717d-4eec-b089-bc1ca5f08585', 37, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'06e91f16-c04b-4183-ac38-caf7b9372e52', 39, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'2113acb6-759f-4c44-b9c9-de517aa86474', 28, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'81fa70ed-1f6f-4bfc-9f77-f0abb151a8ec', 27, 6)
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'a070c7f2-fd2c-43b1-930f-f63e96846cac', 30, 6)
GO
SET IDENTITY_INSERT [Group].[Group] ON 
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1, N'Pierwsza grupa', N'group.jpg', N'ghjg sasadfsdf', CAST(N'2021-08-01T22:26:49.717' AS DateTime), 1)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (2, N'gdf', N'group.jpg', NULL, CAST(N'2021-08-01T22:32:07.110' AS DateTime), 1)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1002, N'test3', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:02.317' AS DateTime), 6)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1003, N'fhdhd', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:39.110' AS DateTime), 6)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1004, N'sfdsfsd', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:40.150' AS DateTime), 6)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1005, N'gfertert', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:41.173' AS DateTime), 6)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1006, N'fhdhdfh', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:47.680' AS DateTime), 6)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1007, N'sfsdfsf', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:49.563' AS DateTime), 6)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1008, N'Opasdp[sakaolk', N'group.jpg', NULL, CAST(N'2021-09-30T21:18:52.733' AS DateTime), 6)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1009, N'fdsfsdf', N'group.jpg', N'ewtegfdg', CAST(N'2021-10-15T19:53:53.933' AS DateTime), 2)
GO
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt], [CreatedBy]) VALUES (1011, N'gfghfgh', N'group.jpg', N'klfdgkdl;fgkm', CAST(N'2021-10-15T19:54:02.250' AS DateTime), 2)
GO
SET IDENTITY_INSERT [Group].[Group] OFF
GO
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'28e01953-d0a7-4b67-91f3-109355f5955b', 1, 21, CAST(N'2021-11-04T20:48:51.013' AS DateTime), NULL)
GO
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'dc81ed58-685f-4d7f-820d-ad77dea3304f', 1, 21, CAST(N'2021-11-04T20:45:18.037' AS DateTime), NULL)
GO
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'9827a289-b8a1-4038-8624-dd226c99473e', 1, 21, CAST(N'2021-11-04T20:49:49.533' AS DateTime), NULL)
GO
INSERT [Group].[GroupBlockedUsers] ([GroupBlockedUserId], [GroupId], [UserId], [BannedAt], [BannedUntil]) VALUES (N'2a971cca-8b0c-40b7-8f82-e7131a7b3380', 1, 21, CAST(N'2021-11-04T20:49:04.293' AS DateTime), NULL)
GO
INSERT [Group].[GroupDescriptionBlock] ([GroupDescriptionBlockId], [GroupId], [TextBlockId]) VALUES (N'ac8d936f-5662-4041-9162-b327879e7cdf', 1, N'53017af3-420b-4e2e-ac7a-c9aad1d0c20f')
GO
INSERT [Group].[GroupDescriptionBlock] ([GroupDescriptionBlockId], [GroupId], [TextBlockId]) VALUES (N'cacaab14-d6b8-4280-b73d-bcc2398fb857', 1, N'100fdf34-351c-462d-bbd1-e0d338edff24')
GO
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'3f109df3-9e2e-ec11-a809-d83bbff1afdf', 1, N'Test1', CAST(N'2021-10-20T21:51:28.427' AS DateTime), N'user.jpg', CAST(N'2021-10-16T18:34:37.850' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 0)
GO
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'40900330-a02e-ec11-a809-d83bbff1afdf', 1, N'Test2', CAST(N'2021-10-21T21:10:00.000' AS DateTime), N'user.jpg', CAST(N'2021-10-16T18:43:28.680' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 0)
GO
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'a88f73f1-b72e-ec11-a809-d83bbff1afdf', 1, N'Test3', CAST(N'2021-10-21T21:10:00.000' AS DateTime), N'user.jpg', CAST(N'2021-10-16T18:50:00.000' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 0)
GO
INSERT [Group].[GroupEvent] ([GroupEventId], [GroupId], [Name], [Date], [AvatarSrc], [CreatedAt], [CreatedBy], [Description], [IsCancelled], [IsHidden]) VALUES (N'c9c5f500-b82e-ec11-a809-d83bbff1afdf', 1, N'Test4', CAST(N'2021-10-21T21:30:00.000' AS DateTime), N'user.jpg', CAST(N'2021-10-16T21:33:57.660' AS DateTime), 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, 1)
GO
INSERT [Group].[GroupEventParticipant] ([GroupEventParticipantId], [GroupEventId], [GroupParticipantId], [JoinedAt]) VALUES (N'fe11e13d-90c8-4a34-91c3-8fb944cb1f57', N'3f109df3-9e2e-ec11-a809-d83bbff1afdf', N'd4c11f78-2d40-400d-8ecb-77b7a56df7ef', CAST(N'2021-11-03T22:54:29.557' AS DateTime))
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'5e2c01e4-261b-40fb-8ef9-09b781169097', 1011, 6, CAST(N'2021-10-15T21:51:28.427' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'c9886445-4604-472b-8321-14d31fc5d33c', 1, 17, CAST(N'2021-11-01T16:54:47.507' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'cdefced3-c315-4ee6-b979-179ab51370c0', 1, 16, CAST(N'2021-11-01T16:54:44.770' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'e3199f56-647d-4894-9ff6-18aa37dd633b', 1011, 2, CAST(N'2021-10-15T21:47:39.873' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'b295575b-5959-4f19-8ca7-23253ca51c47', 1, 14, CAST(N'2021-11-01T16:54:37.270' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'8ce5c08a-692a-4a9f-8308-2403de41fc48', 1, 12, CAST(N'2021-11-01T16:54:13.707' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'68594dd9-0af0-4586-bfd6-27618000ca84', 1, 21, CAST(N'2021-11-01T16:54:58.467' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'bdd18498-abc9-4be2-b0ea-2f2df1c2e604', 1, 15, CAST(N'2021-11-01T16:54:40.933' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'b225c12f-4313-4484-8e50-45774b619110', 1003, 6, CAST(N'2021-09-30T21:19:07.637' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'1669c860-7264-4be4-bdb1-49ab979d056b', 1, 20, CAST(N'2021-11-01T16:54:55.810' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'bd93eab7-7368-435a-b892-49d501681a40', 1, 23, CAST(N'2021-11-01T16:55:03.780' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'a6327571-ac71-494a-851a-4b95f3a4007b', 1004, 6, CAST(N'2021-09-30T21:19:10.497' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'f1c35a73-4707-42df-97aa-5cdfa9f33ae1', 1, 19, CAST(N'2021-11-01T16:54:53.190' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'4b266ee7-97b1-407b-bc9e-63760d6875c8', 2, 6, CAST(N'2021-09-30T21:00:01.950' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'de968769-7e26-4412-93c8-6426c26f3b9a', 2, 1, CAST(N'2021-08-01T22:51:10.947' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'd0070c60-adac-48e7-a8e1-66650ae09357', 1, 22, CAST(N'2021-11-01T16:55:00.807' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'30be09a2-493d-4fb0-b97b-67a39a6e0f8b', 1, 1, CAST(N'2021-08-01T22:48:14.897' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'd4c11f78-2d40-400d-8ecb-77b7a56df7ef', 1, 6, CAST(N'2021-09-30T20:59:59.417' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'b5e070c4-25f2-4cc2-9009-780980e2cd32', 1, 11, CAST(N'2021-11-01T16:54:10.403' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'1254ff7a-4759-4648-8166-8e105f5bc934', 1009, 6, CAST(N'2021-11-07T17:05:38.437' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'9c999d3f-be3c-4ae7-8026-8e549864ce22', 1, 10, CAST(N'2021-11-01T16:54:02.510' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'a0bae610-2a08-4a76-8823-9eb8810e35b2', 1, 18, CAST(N'2021-11-01T16:54:50.390' AS DateTime), 0, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'f9c86cfa-cc89-48a8-b713-a809ea532951', 1005, 6, CAST(N'2021-09-30T21:19:12.640' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'3c80bc14-3fe6-467c-a580-d032f1357e41', 1002, 6, CAST(N'2021-09-30T21:19:05.280' AS DateTime), 1, 1)
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined], [IsAdmin], [IsParticipantVisible]) VALUES (N'ca4fefb8-1af0-4ad0-851a-fe2e10a0993d', 1, 13, CAST(N'2021-11-01T16:54:35.043' AS DateTime), 0, 1)
GO
INSERT [Group].[HiddenGroup] ([HiddenGroupId], [UserId], [GroupId]) VALUES (N'468bc4b7-0ee7-4977-9987-742ceb0e3710', 6, 1011)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'2439de37-5d1a-ec11-a7f4-d83bbff1afdf', N'dsadad', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, NULL, CAST(N'2021-09-20T23:53:51.990' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', N'hfdhfdh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, NULL, CAST(N'2021-09-20T23:54:04.573' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'040975f2-101b-ec11-a7f4-d83bbff1afdf', N'dfgfdgdfg', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', CAST(N'2021-09-21T21:20:15.280' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'c2b483fd-fe22-ec11-a7fa-d83bbff1afdf', N'fdjhdh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'040975f2-101b-ec11-a7f4-d83bbff1afdf', CAST(N'2021-10-01T23:31:52.193' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'97282111-0023-ec11-a7fa-d83bbff1afdf', N'hfdhdfh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'040975f2-101b-ec11-a7f4-d83bbff1afdf', CAST(N'2021-10-01T23:39:34.597' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'073c8497-bc23-ec11-a7fa-d83bbff1afdf', N'dsadsad', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 6, NULL, CAST(N'2021-10-02T22:09:05.447' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'365e474c-1c3b-ec11-a819-d83bbff1afdf', N'test', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, NULL, CAST(N'2021-11-01T15:02:07.650' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', N'test2', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:03:11.347' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', N'test3', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:05:59.390' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'd8b8a5dd-203b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:34:49.523' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'dab8a5dd-203b-ec11-a819-d83bbff1afdf', N'dfsfdsfsdfsdf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:34:58.470' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'5507e41b-213b-ec11-a819-d83bbff1afdf', N'fdsfsdf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:36:33.950' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'16771250-213b-ec11-a819-d83bbff1afdf', N'dsadsad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:38:01.497' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'6b06ef9a-213b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:40:07.093' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'6d06ef9a-213b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:40:16.453' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'859c42d3-213b-ec11-a819-d83bbff1afdf', N'dsadsad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:41:41.593' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'4509ab6b-223b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:45:57.293' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'01af80c3-223b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:48:24.653' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'fb0046d3-223b-ec11-a819-d83bbff1afdf', N'dsadasd', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:48:51.113' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'a84efa06-233b-ec11-a819-d83bbff1afdf', N'dsadasdsad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:50:17.860' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'fcd1434b-233b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:52:12.423' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'180efca0-233b-ec11-a819-d83bbff1afdf', N'gfdsfsdfsf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:54:36.240' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 1)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'f32b96d2-243b-ec11-a819-d83bbff1afdf', N'dsffsdfsdf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'180efca0-233b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T16:03:08.953' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'b6da8612-253b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T16:04:56.227' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'b0dd6b16-343b-ec11-a819-d83bbff1afdf', N'dsadsad', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-01T17:52:25.213' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'c0748417-aa41-ec11-a820-d83bbff1afdf', N'sfafasfa', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-09T23:12:14.640' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'a886e620-aa41-ec11-a820-d83bbff1afdf', N'dfsdsfsdf', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-09T23:12:30.383' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'96d8bde0-6942-ec11-a822-d83bbff1afdf', N'dasdasdasd', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, N'a886e620-aa41-ec11-a820-d83bbff1afdf', CAST(N'2021-11-10T22:05:06.117' AS DateTime), NULL, NULL, N'a886e620-aa41-ec11-a820-d83bbff1afdf', 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8b6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:03.177' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8c6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad2', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:05.490' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8d6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad23', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:06.803' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8e6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad23435', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:08.343' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8f6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad2343543535', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:09.660' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'906e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad23435435352313', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:11.077' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'916e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad234354353523135435435', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:12.307' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'56955603-6b42-ec11-a822-d83bbff1afdf', N'dsadsad2343543535231354354353213213', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:13.657' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'57955603-6b42-ec11-a822-d83bbff1afdf', N'dsadsad234354353523135435435321321323525325', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:15.360' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'58955603-6b42-ec11-a822-d83bbff1afdf', N'dsadsad23435433253523135435435321321323525325', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:17.010' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'59955603-6b42-ec11-a822-d83bbff1afdf', N'qw', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:19.417' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'5a955603-6b42-ec11-a822-d83bbff1afdf', N'ghjgh hfghfg', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:21.973' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'6df04f13-f097-41ec-b135-14e46c3d428f', N'cf6bc35f-6ea2-4848-839c-83a8998855cb', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'04973834-1233-442e-bb3d-15ee0817e986', N'e4baa157-6be5-4491-9323-65a04b7531f6', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'8ca64f89-63d0-44f2-85fc-198bc2fec34b', N'2fd35b83-4c33-47bf-a2a1-449771668756', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'413f7072-daad-467e-a133-22a17394d227', N'7a0f25cc-b8de-4fc3-b8fc-a6435ee8cfa8', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'dd2e1b96-7ec0-4f38-9bca-26d02eb3cd0b', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 1, 1)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'3667d851-8c60-41d9-bc3d-42bafaef9c5c', N'7863166a-3a87-4475-8735-fb1e70a0fe5d', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'19624261-86c7-4fde-a865-5b18cf1dccd6', N'837387b8-cff4-4cf5-9f1f-027b4502a6af', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'04717589-79fa-40c4-b71c-8673977c0421', N'7c4a0a9f-98fa-4798-a2f7-9f74ed6b3509', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'476e148e-9c69-469a-9754-8f0fe6ce7a5b', N'68b82c0e-f1c2-44d8-89d3-63a54f8d6959', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'98f1bace-9ae0-4d69-b620-911588f62a07', N'1e91a69e-6336-44a7-9adb-db022e1a7e7a', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'90402cbf-4a28-4c88-a054-d23cff9168e2', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'c96f1610-6285-4211-8d84-dbd9219a5610', N'888ab433-508f-4182-8592-46312cd54c2a', 1, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'e5463ae4-879a-4e5b-8904-dc47b0c3dc0e', N'383b1bcd-390b-4e2c-a2b9-d560765ea433', 1003, 6)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId]) VALUES (N'76c75159-e65c-4859-aba3-ea1b6e55ba00', N'4b89b5cc-dfde-4a5d-9329-42d9dc8b962f', 1, 6)
GO
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', 1, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', N'GA', NULL, NULL)
GO
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'e9db7826-b5dd-47bb-8dbc-c69489de0760', 1, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4')
GO
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'2749ebe5-1c3b-ec11-a819-d83bbff1afdf', 1, N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', N'U', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'e9db7826-b5dd-47bb-8dbc-c69489de0760')
GO
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'190efca0-233b-ec11-a819-d83bbff1afdf', 1, N'180efca0-233b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'e9db7826-b5dd-47bb-8dbc-c69489de0760')
GO
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'f42b96d2-243b-ec11-a819-d83bbff1afdf', 1, N'f32b96d2-243b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'190efca0-233b-ec11-a819-d83bbff1afdf')
GO
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'b7da8612-253b-ec11-a819-d83bbff1afdf', 1, N'b6da8612-253b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4')
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'0dbb48d3-9b8a-42af-81c5-00aa58aa90dc', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-11T23:14:47.800' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'837387b8-cff4-4cf5-9f1f-027b4502a6af', N'dsadsa', NULL, NULL, CAST(N'2021-10-14T21:03:42.857' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'a01ed70f-41f5-4518-b918-07fa333559cd', N'jhyityurtyurthrfhrfher', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'434a6ab8-d113-4da5-91ad-0bd217b50b99', N'fdgfdg', NULL, NULL, CAST(N'2021-10-10T20:30:14.933' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'f73955cc-91e6-4ecb-bd81-1285856c8d56', N'gfdgdfgdfg', NULL, NULL, CAST(N'2021-10-11T23:14:42.190' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'3b6d8f3e-a25a-4a4f-9bab-15caf28b0b71', N'gjhfj', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'399054ce-e9fc-49c6-92e9-1f03c4ca0a84', N'dsadad', NULL, NULL, CAST(N'2021-11-13T15:09:09.407' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'1751eba6-d7ae-4d84-9962-2260bb374a98', N'fgfg', NULL, NULL, CAST(N'2021-10-19T21:34:45.953' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7e46b1dd-d2ec-4a12-8004-23d0670be225', N'fdgdfgdfgdfg', NULL, NULL, CAST(N'2021-10-11T23:14:57.460' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'18d881ad-0dfe-49e8-a7ab-260aa1353c26', N'dadsad', NULL, NULL, CAST(N'2021-10-28T00:29:39.913' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'003753b4-22d6-423a-a231-31daa4afb2a6', N'dsadasd', NULL, NULL, CAST(N'2021-10-11T23:13:35.767' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'4b89b5cc-dfde-4a5d-9329-42d9dc8b962f', N'sdffsdfsdf', NULL, NULL, CAST(N'2021-10-19T21:33:42.103' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'2fd35b83-4c33-47bf-a2a1-449771668756', N'fdgdfgfdg', NULL, NULL, CAST(N'2021-10-02T23:01:51.307' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'888ab433-508f-4182-8592-46312cd54c2a', N'gdfsg', NULL, NULL, CAST(N'2021-10-02T23:15:33.253' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', N'loREM dsadikjdgfok ;daslda''; dsfkgjdkgjdkgjld', NULL, NULL, CAST(N'2021-09-12T22:59:50.993' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'fedff6ca-d408-4691-8f5e-54122ab7199d', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-11T23:13:06.963' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'fa8410ad-5d48-46f9-9a9e-58e30ed9e9a8', N'dawdawd', NULL, NULL, CAST(N'2021-10-11T23:16:21.327' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'e7cedcd2-3486-4ad0-980d-5904236428da', N'vnhj', NULL, NULL, CAST(N'2021-10-11T22:58:04.390' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'cc6eb0b7-436c-41c5-9cd9-5b81bba8c812', N'dsadasd', NULL, NULL, CAST(N'2021-10-11T23:16:16.277' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'68b82c0e-f1c2-44d8-89d3-63a54f8d6959', N'gdsgsdgds', NULL, NULL, CAST(N'2021-10-02T22:59:27.270' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'e4baa157-6be5-4491-9323-65a04b7531f6', N'fgdgfdg', NULL, NULL, CAST(N'2021-10-19T21:33:44.883' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'63febafa-fa1f-4e77-b906-69b61dd75aa2', N'dadsad', NULL, NULL, CAST(N'2021-10-28T00:29:41.830' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'a51d4962-4047-4fd5-8e33-6dc770433950', N'dsfdsdfdsf', NULL, NULL, CAST(N'2021-10-28T00:29:34.850' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'c1380b00-9d09-44d7-a77a-6e7dbf2f6a0b', N'gdfgfdg', NULL, NULL, CAST(N'2021-10-11T23:16:18.737' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'604c9af6-8219-44c7-a8bd-70ccf4c55f0e', N'fdsdf', NULL, NULL, CAST(N'2021-10-11T23:04:50.727' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'cf6bc35f-6ea2-4848-839c-83a8998855cb', N'dasd', NULL, NULL, CAST(N'2021-10-17T21:31:50.413' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'c77ebb27-875b-4fc0-998c-875d9df77830', N'fgdsfsdf', NULL, NULL, CAST(N'2021-10-10T20:29:39.340' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', N'sdfsfdsfsdfsd', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'76351e67-1298-4208-b708-9d4ef000ab4c', N'fdggd', NULL, NULL, CAST(N'2021-10-10T20:42:57.780' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'd6c3c87e-c00e-4b0d-90bd-9eae0296a085', N'wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfswqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd ', NULL, NULL, CAST(N'2021-10-11T23:16:55.860' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7c4a0a9f-98fa-4798-a2f7-9f74ed6b3509', N'gdfsggdf', NULL, NULL, CAST(N'2021-10-02T23:17:15.883' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7a0f25cc-b8de-4fc3-b8fc-a6435ee8cfa8', N'dsadagfdsg', NULL, NULL, CAST(N'2021-10-02T23:10:01.680' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'570f5d1d-b39b-41e5-abbb-ad5c6c2a8d37', N'dfsf', NULL, NULL, CAST(N'2021-10-10T20:59:58.063' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'132eb31c-1fe9-4d25-ae20-b0c417f886da', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-10T20:36:45.863' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'17c59f15-281d-4b57-a8ff-caae973f988f', N'dsad', NULL, NULL, CAST(N'2021-10-30T00:07:20.410' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'86c7e361-fc09-411a-b7df-d0181a09fb6f', N'dasdasd', NULL, NULL, CAST(N'2021-10-11T23:16:29.923' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'5a2dca94-ecfd-4492-94bb-d1779ae84ca3', N'hghg', NULL, NULL, CAST(N'2021-10-11T22:59:53.710' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'1cf707cf-f614-4f8d-82ef-d4295546ecac', N'fgdsfsdf', NULL, NULL, CAST(N'2021-10-10T20:29:48.260' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'd1ac09ec-b2b9-48f0-934b-d52cd78fde27', N'dasdsa', NULL, NULL, CAST(N'2021-10-10T20:39:01.760' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'383b1bcd-390b-4e2c-a2b9-d560765ea433', N'sadasd', NULL, NULL, CAST(N'2021-10-14T22:36:00.860' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'981a2813-c88c-4708-b6d6-d68757848942', N'hghg', NULL, NULL, CAST(N'2021-10-11T23:02:19.070' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'6bea3baa-829c-4d47-b9b7-d930853bf284', N'fdgfdgdfg', NULL, NULL, CAST(N'2021-10-11T23:11:52.460' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'1e91a69e-6336-44a7-9adb-db022e1a7e7a', N'fgdgfdg', NULL, NULL, CAST(N'2021-10-19T21:34:03.063' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'41204fb6-5e39-4caa-9487-dcf5e8670f1b', N'Ldsa ksdfjskd fdsf. LDA;SDLK. lsadlsdlals;dald ASDLA;DL', NULL, NULL, CAST(N'2021-09-12T23:00:25.650' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'9c5a85c3-b1bc-4f61-a179-efb4b1fdff53', N'DFD', NULL, NULL, CAST(N'2021-10-11T23:09:40.917' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'43372775-d27c-4fe2-a15f-f576e8463764', N'fsdfs', NULL, NULL, CAST(N'2021-10-11T23:04:03.063' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7863166a-3a87-4475-8735-fb1e70a0fe5d', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-02T23:17:56.580' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'523ab624-b571-461d-a996-fd74e182c5ec', N'dasda', NULL, NULL, CAST(N'2021-10-11T22:57:06.717' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'95aed42f-fefe-4c13-b07c-fec176056505', N'sgdfbdvb', NULL, NULL, CAST(N'2021-10-11T23:16:33.497' AS DateTime))
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'b7f167a1-30c4-4b74-9664-ffebf4e8aab4', N'fdgfdg', NULL, NULL, CAST(N'2021-10-10T20:33:55.460' AS DateTime))
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'ee4963f0-d4ce-4e97-ba4f-082c3017ee45', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'50126786-ee1c-4845-bbcb-096c41041faa', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'f8b5a8a7-4b4a-4e2a-9d2a-0b252b7b9d42', N'f73955cc-91e6-4ecb-bd81-1285856c8d56', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'a223f9ee-fd37-4cdd-b3c6-0f43996eaf09', N'e7cedcd2-3486-4ad0-980d-5904236428da', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'f0b38483-ac57-45cc-a0bd-20e60c1ea6f1', N'9c5a85c3-b1bc-4f61-a179-efb4b1fdff53', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'6469d0dc-24c7-48ec-97e1-3ce015d2f316', N'5a2dca94-ecfd-4492-94bb-d1779ae84ca3', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'f14e0788-ea53-4757-be78-4517e34997ce', N'fedff6ca-d408-4691-8f5e-54122ab7199d', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'c9ee6fba-356e-42e7-af34-48625983e7a5', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'32d15dc9-a9ff-4411-aba5-48a100181c44', N'523ab624-b571-461d-a996-fd74e182c5ec', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'1037df6a-5ab9-4a04-be0c-48b0211386f8', N'a51d4962-4047-4fd5-8e33-6dc770433950', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'28a63558-f241-4096-a086-522959b8abe9', N'003753b4-22d6-423a-a231-31daa4afb2a6', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'b2aeb351-6931-4c75-85d0-58aa1cd26e37', N'399054ce-e9fc-49c6-92e9-1f03c4ca0a84', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'9c7b230c-c966-4596-85d1-654fdb2198f5', N'604c9af6-8219-44c7-a8bd-70ccf4c55f0e', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'b9377c38-ef5a-42eb-b8b1-71571ea8e489', N'7e46b1dd-d2ec-4a12-8004-23d0670be225', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'37a496f3-7c80-4da6-92dd-7b411f084e93', N'981a2813-c88c-4708-b6d6-d68757848942', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'167ae0ee-fca8-4e1e-9b2e-8117f2b2cf69', N'd6c3c87e-c00e-4b0d-90bd-9eae0296a085', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'0c4847c5-6d94-48ad-b6a1-81b6ebed826f', N'570f5d1d-b39b-41e5-abbb-ad5c6c2a8d37', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'710f6a7c-19cf-4f1a-974d-82a7779a69b8', N'd1ac09ec-b2b9-48f0-934b-d52cd78fde27', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'86f09c5c-17cc-4859-9775-a6b284ffc993', N'fa8410ad-5d48-46f9-9a9e-58e30ed9e9a8', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'637494fa-d6d8-4da3-bf82-c269a80b097e', N'132eb31c-1fe9-4d25-ae20-b0c417f886da', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'd698bf74-546d-4525-97f8-c4a3eb9ec024', N'b7f167a1-30c4-4b74-9664-ffebf4e8aab4', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'9698e60e-771c-40cd-8776-c900afd8f4eb', N'76351e67-1298-4208-b708-9d4ef000ab4c', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'35de6722-e5ef-4af2-9799-ca7b019e33c7', N'cc6eb0b7-436c-41c5-9cd9-5b81bba8c812', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'a90c7aae-8b70-4173-93d1-d07285916ed6', N'6bea3baa-829c-4d47-b9b7-d930853bf284', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'4209d9a0-9cbd-4ee3-ad9f-d080449b5303', N'0dbb48d3-9b8a-42af-81c5-00aa58aa90dc', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'b6b610db-0cb6-402a-96e9-d2a19839265a', N'18d881ad-0dfe-49e8-a7ab-260aa1353c26', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'6bfe4b74-a7a4-4680-9331-d5827a8d7020', N'86c7e361-fc09-411a-b7df-d0181a09fb6f', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'6c85f9ed-6a15-41b0-9df3-d6eca3e183b3', N'41204fb6-5e39-4caa-9487-dcf5e8670f1b', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'7f194581-09af-4a37-b695-e78bdd3c1bd6', N'17c59f15-281d-4b57-a8ff-caae973f988f', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'11f13bc5-0fa3-4d8c-9ab2-f353ffd7256e', N'95aed42f-fefe-4c13-b07c-fec176056505', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'191b9b4c-ca64-4084-9f07-f6a38b23307f', N'c1380b00-9d09-44d7-a77a-6e7dbf2f6a0b', 6)
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'bdef8d57-2520-416f-8481-fdc992a86e49', N'43372775-d27c-4fe2-a15f-f576e8463764', 6)
GO
INSERT [User].[BlockedUsers] ([BlockedUsersId], [IssuerId], [DestUserId]) VALUES (N'0ccee93a-f954-4b40-b1bc-17d218d952ac', 8, 6)
GO
SET IDENTITY_INSERT [User].[User] ON 
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (1, N'chlenix', N'Dimitr', N'Ruski', N'111', CAST(N'1999-04-10' AS Date), N'M', N'd1@gmw.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (2, N'derek', N'Dimitr', N'Jankowski', N'333', CAST(N'1998-04-11' AS Date), N'M', N'gdfg.dsa2@gmw.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (3, N'karina', N'Katarzyna', N'Brovlowska', N'888', CAST(N'1997-01-02' AS Date), N'F', N'bdsad@gmw.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (6, N'string1', N'String', N'Stringowicz', N'$2a$11$BQXPjo9BoW6HzzIf6F1xIumJ27ajPwgTsrbC5gAsGqSVxkqT9D0Dm', CAST(N'2000-09-04' AS Date), N'M', N'string1@gmail.com', N'user.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent lobortis vitae nisl eget scelerisque. Aliquam nibh lectus, hendrerit vel nisl eu, volutpat tempus lorem. Duis eget nulla elit. Orci varius natoque penatibus et magnis dis parturient montes')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (8, N'dimitr1', N'Dimitr', N'Trankowski', N'531259032592', CAST(N'1999-05-11' AS Date), N'M', N'dsad@gmail.com', N'user.jpg', N'Lorem i t.d.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (10, N'slhhco2i', N'Aleksandra', N'Jabłoński', N'$2a$11$MlXY8UegwPcdSY76kJ9axesQxWmE.oojU/YjhFFe.VNcItNXUxxLa', CAST(N'1978-10-28' AS Date), N'F', N'test1@test.com', N'thispersondoesnotexistcom9.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (11, N'4ixhhmp4', N'Anna', N'Kowalczyk', N'$2a$11$l8xhsuDva0DaCVyKcpQ/7uryaBtvdjW4W6dApzRBqsmUPk.m/fLSm', CAST(N'1976-10-28' AS Date), N'F', N'test2@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (12, N'ndql1vb1', N'Mariusz', N'Kowalski', N'$2a$11$4NBKboOCz088EdkW4xsGUOE3VQqtKKZWEFc4aKOj5Qaem02MMeoVy', CAST(N'1994-10-28' AS Date), N'M', N'test3@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (13, N'iyoctjzo', N'Artur', N'Kamiński', N'$2a$11$U1UayU6v753W90ybafuUOedAuxPwrmpomMNAxtEJNRzW0tdBcqXq2', CAST(N'1981-10-28' AS Date), N'M', N'test4@test.com', N'thispersondoesnotexistcom3.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (14, N'jy1zrke4', N'Marian', N'Piotrowski', N'$2a$11$GCk92lX9xdW8e78IDtZieO84lEDn1TLmpJYoDvdBYIg8fD31bAx/G', CAST(N'1995-10-28' AS Date), N'M', N'test5@test.com', N'thispersondoesnotexistcom13.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (15, N'l4d02xhp', N'Maria', N'Zieliński', N'$2a$11$obnRuG1VzqOoW3LgrV23TO5ByBynksMpRr6nmVFfl5BHV7a2kCaXy', CAST(N'1983-10-28' AS Date), N'F', N'test1@test.com', N'thispersondoesnotexistcom19.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (16, N'riwff4lf', N'Stanisława', N'Wiśniewski', N'$2a$11$xZOdQT5ySlqwQkKDPBfcXe051CF4IrtAkOJeicUKoxV0STWi1eCqK', CAST(N'1992-10-28' AS Date), N'F', N'test73@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (17, N'ehe4j0eg', N'Wojciech', N'Michalski', N'$2a$11$NT2rG1QCX449t5sdIG3fb.Sdq/lQL6XNdrAMOaIGoEUuGCDF3GZea', CAST(N'1989-10-28' AS Date), N'M', N'test72@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (18, N'julmupim', N'Karolina', N'Lewandowski', N'$2a$11$TBBcvXrnhwpist.F9DxKweHhtuKdFfr8qpmrjKL1tUb13QNBfDMf2', CAST(N'1979-10-28' AS Date), N'F', N'test71@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (19, N'dvmq2wxh', N'Janina', N'Krawczyk', N'$2a$11$obmQkMLMgj7yO6fiPV5lx.iFNIiv3.4eztYlcZFpxeAQMXdVD08c.', CAST(N'1988-10-28' AS Date), N'F', N'test70@test.com', N'thispersondoesnotexistcom6.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (20, N'o5rdtrb1', N'Dariusz', N'Olszewski', N'$2a$11$UE5gb0pReu1yJS0fs3tq0.uv6VL3b.PuDEJq0pUVVFVXhier2wTya', CAST(N'1983-10-28' AS Date), N'M', N'test69@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (21, N'trdrlems', N'Ryszard', N'Majewski', N'$2a$11$.3R7O42XCp8wGII9UJmm9eaHygofzO9cFPw1WpZxoY9mo2u5Cpf/i', CAST(N'1988-10-28' AS Date), N'M', N'test68@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (22, N'y22ccnqi', N'Kamil', N'Wieczorek', N'$2a$11$Xfnx1yRbtTB4dOTdXBZ8t.nD9u0cmS7s01qMUt5T/bBnwuWRRbgOe', CAST(N'1998-10-28' AS Date), N'M', N'test67@test.com', N'thispersondoesnotexistcom17.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (23, N'hakj2wj4', N'Łukasz', N'Jabłoński', N'$2a$11$8fkA6zCRTKoucGniGamf5ukdYLMnrgLWJM3qp1AAScfhp/z8MOhrS', CAST(N'1988-10-28' AS Date), N'M', N'test66@test.com', N'thispersondoesnotexistcom7.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (24, N'a5zlotjw', N'Robert', N'Mazur', N'$2a$11$vfp3QOP7eblkt/5fc6D8VORRNNZrbNaF.Jppy8hGdDLbgkKSTAAlO', CAST(N'2001-10-28' AS Date), N'M', N'test65@test.com', N'thispersondoesnotexistcom7.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (25, N'4su4sux4', N'Wiesław', N'Wiśniewski', N'$2a$11$HYzYyA3vxHrwussTMCa1QuRq1fFZdavZqH40dbVLxZBJKkQSNPkIC', CAST(N'1981-10-28' AS Date), N'M', N'test64@test.com', N'thispersondoesnotexistcom12.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (26, N'q4uzzk2n', N'Teresa', N'Piotrowski', N'$2a$11$6MbHp5ByMVmtMtPyTvyGU.VV7cjxkP41e2AAPbdfsGdBlaO/I/m8C', CAST(N'1978-10-28' AS Date), N'F', N'test63@test.com', N'thispersondoesnotexistcom16.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (27, N'inn0zika', N'Jarosław', N'Zając', N'$2a$11$waVYsX/.QKtwPyYdSVTWouyIiMj4Khad7hp8PPTxs2CNEFli/vM16', CAST(N'1985-10-28' AS Date), N'M', N'test62@test.com', N'thispersondoesnotexistcom17.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (28, N'h3ezzgyl', N'Ryszard', N'Majewski', N'$2a$11$WkCW.skuoqR1HXxT.6ZT9.ieAAU46PsvnXI9JMjehJrUS/6.afOBS', CAST(N'1988-10-28' AS Date), N'M', N'test61@test.com', N'thispersondoesnotexistcom11.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (29, N'3ooaecil', N'Edward', N'Zieliński', N'$2a$11$KfvgEac07pN4hf5N04oUFOi4h6g1QfV6o7GW9dPyrrrrOuSWBPVde', CAST(N'1975-10-28' AS Date), N'M', N'test60@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (30, N'avgt2mnv', N'Mariusz', N'Kowalski', N'$2a$11$9jJqtyFjhGUaOSFM4VGCSOiRZHVdaDKGfWyynAXrAFN2Y95g1auvW', CAST(N'1994-10-28' AS Date), N'M', N'test59@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (31, N'1sj1nlxi', N'Maria', N'Zieliński', N'$2a$11$7s0esIxzLXj4hLJG8rsD0Of2i.lySoltbOEqoba/3coEncGMOPhdi', CAST(N'1990-10-28' AS Date), N'F', N'test58@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (32, N'1xswvly0', N'Irena', N'Adamczyk', N'$2a$11$3dSrLhTtmkJAd7..SLmuDeMZjd2MWgvVuuScJ1M9hhKV7V30ETslm', CAST(N'1988-10-28' AS Date), N'F', N'test57@test.com', N'thispersondoesnotexistcom20.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (33, N'nqhj4xrg', N'Ryszard', N'Majewski', N'$2a$11$.75ScJTEWXezM.7sgJx.BOp8ibKtVhATo8k9zbNEdkkM4vw1efzqK', CAST(N'1980-10-28' AS Date), N'M', N'test56@test.com', N'thispersondoesnotexistcom7.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (34, N'l1akegiu', N'Wiesław', N'Wiśniewski', N'$2a$11$wli/.r.KBkwj3TQrb9NgkeKqVGPXKLVEjFzxlWfdMMKntc9qP01oq', CAST(N'1975-10-28' AS Date), N'M', N'test55@test.com', N'thispersondoesnotexistcom9.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (35, N'hd4yqz2c', N'Marianna', N'Olszewski', N'$2a$11$wo8A.Kg8cIbcBCn80.yfiuHx/2JPba3kHTZIPGyiyyj70igfh2SsW', CAST(N'1978-10-28' AS Date), N'F', N'test54@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (36, N'kaqpwlsr', N'Joanna', N'Grabowski', N'$2a$11$FxhWWXN2U80HXt9KMQ.XEOfcLp97083JOzKOzZ3VeQGfuSw6WAGmW', CAST(N'1997-10-28' AS Date), N'F', N'test53@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (37, N'holiqaff', N'Irena', N'Adamczyk', N'$2a$11$n9WGQGv6vLSTE38Rpv2c5O9ZIqNU.mjvMRgtn5BfrYm.84lOdY1kO', CAST(N'1975-10-28' AS Date), N'F', N'test74@test.com', N'thispersondoesnotexistcom9.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (38, N'h35lplue', N'Sławomir', N'Dudek', N'$2a$11$80ioSpuTK44wGAOyPZBU5uSFfjnihamo6KzEFLTESdaOHBYyKjuo6', CAST(N'1996-10-28' AS Date), N'M', N'test52@test.com', N'thispersondoesnotexistcom20.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (39, N'q1utgavm', N'Stanisława', N'Wiśniewski', N'$2a$11$bNQxmpJ9sa01./CPoPjlOeEVNwogT6STx4zY3B1xSvOp.7gTQ3H82', CAST(N'1990-10-28' AS Date), N'F', N'test75@test.com', N'thispersondoesnotexistcom4.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (40, N'btyy5f53', N'Mirosław', N'Nowicki', N'$2a$11$CK/VsqKFDsdBRftIZ/hxgukoBvfC/IcIAvBt/pQ9Xoeuae1xJ8Iua', CAST(N'1996-10-28' AS Date), N'M', N'test77@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (41, N'jjhq4k51', N'Artur', N'Kamiński', N'$2a$11$3NS.sS.El8cxikE3/Se6VeKamG/zl8GZPV2R2jQS.bnzzf0PiKHkG', CAST(N'1981-10-28' AS Date), N'M', N'test98@test.com', N'thispersondoesnotexistcom18.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (42, N'byizfmay', N'Bożena', N'Wójcik', N'$2a$11$GkV8QfYPduZ26XZn/ru/X.UffWWjTnMQsuTdpJh8ouBJhJwUD40VK', CAST(N'1996-10-28' AS Date), N'F', N'test97@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (43, N'lng3n3m5', N'Dariusz', N'Olszewski', N'$2a$11$ztUZq2uiD/pGGTP8pAn7xugKVffOnVO.duASgeukFN0wXmAMgQ8Lq', CAST(N'1987-10-28' AS Date), N'M', N'test96@test.com', N'thispersondoesnotexistcom7.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (44, N'oxljf1ka', N'Bożena', N'Wójcik', N'$2a$11$AcoaoqRvXWlJJtXzDodPAOPsL7U5DG/LH56wxZcrK7EZLtgzWf.NS', CAST(N'1997-10-28' AS Date), N'F', N'test95@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (45, N'tegkuaeq', N'Marta', N'Król', N'$2a$11$Gbm9SfRQQmaetJf5gjvMc.ytxLKYUcy6KQ3qEbqxQoGhl0NBGr6n2', CAST(N'1998-10-28' AS Date), N'F', N'test94@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (46, N'ppagg04l', N'Mateusz', N'Krawczyk', N'$2a$11$Kq1NsCo2RMIthjFSYNvkae7Z4huF425I96nm7l8xLM55Y4GSBJ8BG', CAST(N'1994-10-28' AS Date), N'M', N'test93@test.com', N'thispersondoesnotexistcom17.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (47, N'nn5xxwzq', N'Wojciech', N'Michalski', N'$2a$11$mtw2DPAs9UkOdeWR6cnBhebXwWhPszvVHxy/mhwC.6Rza2.GCznSC', CAST(N'1974-10-28' AS Date), N'M', N'test92@test.com', N'thispersondoesnotexistcom18.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (48, N'0kxj5zsp', N'Marianna', N'Olszewski', N'$2a$11$iB4I/.E2CWPfNbFjB7FRoOTXztU/ZKIpRiOmFP4.utR0rc52CEQrW', CAST(N'1995-10-28' AS Date), N'F', N'test91@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (49, N'5m411hco', N'Aleksandra', N'Jabłoński', N'$2a$11$A25md4yzmgtyZh2yPyJKB.7PX9Qjhas8JZYnS45wqdpKpWGLcw1SC', CAST(N'1999-10-28' AS Date), N'F', N'test90@test.com', N'thispersondoesnotexistcom7.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (50, N'3ubuibcr', N'JanuszKazimierz', N'Pawłowski', N'$2a$11$OrciYGgk7tqknaHzNB7RO.Gd8jGjfeahLuHqvhysqkU.N5O2R9FTm', CAST(N'1998-10-28' AS Date), N'M', N'test89@test.com', N'thispersondoesnotexistcom8.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (51, N'trzzlvne', N'Edward', N'Zieliński', N'$2a$11$qrrC2Pt8UIqeL8TZRIEVv.7zL8yNxmZtV.ZpaN/koL5LcvDIuM582', CAST(N'1980-10-28' AS Date), N'M', N'test88@test.com', N'thispersondoesnotexistcom16.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (52, N'fdswhvck', N'Sławomir', N'Dudek', N'$2a$11$P8L65K0mGy8LdykrpswfE.ceOvBV9yyB9e06f9zUX2qHtBHcve37q', CAST(N'1980-10-28' AS Date), N'M', N'test87@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (53, N'sw25mv1y', N'Władysław', N'Lewandowski', N'$2a$11$S9M1.PQNIPYnnpIuUdkiJ.aBI3BrjU5kzjF9j6vc0lyMk5MkqBwem', CAST(N'1975-10-28' AS Date), N'M', N'test86@test.com', N'thispersondoesnotexistcom16.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (54, N'hbydxavi', N'Marian', N'Piotrowski', N'$2a$11$RtX52UypD.35E8gupAkpDOcWBWgFVobTF9w37OjNvA2gNbi7ob.Wa', CAST(N'1977-10-28' AS Date), N'M', N'test85@test.com', N'thispersondoesnotexistcom12.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (55, N'm5pjc30i', N'Kamil', N'Wieczorek', N'$2a$11$Ldyh5FSSSqLce54kLy8Dc.64h5RUONJLlg9yHqWOj2uCLoGq0j8VK', CAST(N'1995-10-28' AS Date), N'M', N'test84@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (56, N'lssp3igo', N'Jarosław', N'Zając', N'$2a$11$sf4DjXl35ugdd3B9eX3iLOtqctLdqHt9hs6AhlQi8J6lY0PwOThZW', CAST(N'1974-10-28' AS Date), N'M', N'test83@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (57, N'r0bpmi2j', N'Wojciech', N'Michalski', N'$2a$11$zc7obdYe0F6eoLFIDEWg4eJWENn2Wxj4f1QzkbnF2zFhYEAkHVX5K', CAST(N'1973-10-28' AS Date), N'M', N'test82@test.com', N'thispersondoesnotexistcom2.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (58, N'alx2lh2v', N'Urszula', N'Kamiński', N'$2a$11$ANMYjzOOWhUMtpfibBQ0G.5KxUP45I71ZcP6fx67d4D9yeciWyINy', CAST(N'1984-10-28' AS Date), N'F', N'test81@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (59, N'avw2pjb4', N'Zdzisław', N'Kowalczyk', N'$2a$11$mqqu/10qzcaP.e4/wOxCvudWtWB3xPbVn8MLTUKcN8faj08MAPdMq', CAST(N'2001-10-28' AS Date), N'M', N'test80@test.com', N'thispersondoesnotexistcom8.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (60, N'zspmmlfb', N'Janina', N'Krawczyk', N'$2a$11$gLCqe.avwOD6j2Hv0EIThuHUrPkE39gDWDnKrW4b0TSxfn5PfVcAi', CAST(N'1989-10-28' AS Date), N'F', N'test79@test.com', N'thispersondoesnotexistcom20.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (61, N'0mb3eqny', N'Mirosław', N'Nowicki', N'$2a$11$shUuPgT7X32PBT1JsqfzReh3f73Xe9VVaT3.jo7frfkc73pIgVavi', CAST(N'1996-10-28' AS Date), N'M', N'test78@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (62, N'0rytdpni', N'Władysław', N'Lewandowski', N'$2a$11$DQf./gEClEsN7b9vcOWZCOGNceQNQQ5tuLtVx8MDi92soVuBD.RkS', CAST(N'1987-10-28' AS Date), N'M', N'test76@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (63, N'bozreipp', N'Mirosław', N'Nowicki', N'$2a$11$ZYN0LRGBLftPd8WnIJA3tO16AemhMIivZJyTe8CzuMGas3Uc4HOaG', CAST(N'1991-10-28' AS Date), N'M', N'test51@test.com', N'thispersondoesnotexistcom10.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (64, N'0oqwixki', N'Mariusz', N'Kowalski', N'$2a$11$IRDn.EEN7hoFOCpqtqeReuaUbZAHWQkhB4JzY0Qp7yxI26BCm2qmS', CAST(N'1987-10-28' AS Date), N'M', N'test50@test.com', N'thispersondoesnotexistcom11.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (65, N'qkrkug0w', N'Marianna', N'Olszewski', N'$2a$11$O.ngRtO1RxNcF6SAlNk1uOXVFIT22tN2qKaKKnp.HWuha01meZp5C', CAST(N'1981-10-28' AS Date), N'F', N'test49@test.com', N'thispersondoesnotexistcom8.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (66, N'hc3o3ubc', N'Irena', N'Adamczyk', N'$2a$11$dMEmw.gUxPaJDj3KwXdy4O8yVekmTCcE3y6pK6j1l2MNAdiP5m1ce', CAST(N'1972-10-28' AS Date), N'F', N'test22@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (67, N'ljfzqe3p', N'Anna', N'Kowalczyk', N'$2a$11$BMyOFCm4FClTISVn8rzZAON4flfHJESgpBYNcwehduhYh9Wu9571S', CAST(N'1998-10-28' AS Date), N'F', N'test21@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (68, N'ssigyhsb', N'Joanna', N'Mazur', N'$2a$11$3.qCZjTxz2UNPC3DE8JU1.VPFTlyXNuS1e6b.dfWtuAxbFKlZV1da', CAST(N'1999-10-28' AS Date), N'F', N'test20@test.com', N'thispersondoesnotexistcom18.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (69, N'ecjhves0', N'Janina', N'Krawczyk', N'$2a$11$9FXMNpqdbLtQ6tKT7zXqae1iXcLILqdGWpi5gLCVi5pdUK2M7/YX6', CAST(N'1983-10-28' AS Date), N'F', N'test19@test.com', N'thispersondoesnotexistcom10.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (70, N'daw5liyk', N'Danuta', N'Nowicki', N'$2a$11$k4Ie0wAmmqC.RmYcA7/h2eSdkNskGxixa7ebyzoaVYpPn.ifTEr0e', CAST(N'1973-10-28' AS Date), N'F', N'test18@test.com', N'thispersondoesnotexistcom13.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (71, N'1lmkejm0', N'Iwona', N'Dąbrowski', N'$2a$11$w.XEyyvpwiRpKs6BPX00neMrZL35Th/2fnWqZFUQyBotZDOgQmk3a', CAST(N'2000-10-28' AS Date), N'F', N'test17@test.com', N'thispersondoesnotexistcom17.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (72, N'p11arczy', N'Mateusz', N'Krawczyk', N'$2a$11$c70I0Ow10qxBBKfwOviF3e1.LCEQ0V1uMboIT7KxLC/XnSkPj9Hqy', CAST(N'1975-10-28' AS Date), N'M', N'test16@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (73, N'xh010uem', N'Mirosław', N'Nowicki', N'$2a$11$bOpqkXmD7FpLD9FTM554AOtTZE0QdZzerS1eZWg0UuHukm8oWRqrm', CAST(N'1996-10-28' AS Date), N'M', N'test15@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (74, N'muwz4r0m', N'Rafał', N'Grabowski', N'$2a$11$FIo930IQVwH7VGcNGD45muzE7wzmh0dM5If9nzftK35cdyp4QBWUe', CAST(N'1999-10-28' AS Date), N'M', N'test14@test.com', N'thispersondoesnotexistcom17.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (75, N'3tucrg0j', N'Mateusz', N'Krawczyk', N'$2a$11$cBFhqPbFnLr5Wbg4T9gcn.nMNsHjLY/8yBh7v83zMWvvlqyogMnNO', CAST(N'1972-10-28' AS Date), N'M', N'test13@test.com', N'thispersondoesnotexistcom20.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (76, N'hajnm22p', N'Mariusz', N'Kowalski', N'$2a$11$FBt682tZlFBLwsQJCtYnF.sw/8U02s2qLmYAFI8yv4SRkvcWvY9RO', CAST(N'1975-10-28' AS Date), N'M', N'test12@test.com', N'thispersondoesnotexistcom6.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (77, N'hw5iv40b', N'Edward', N'Zieliński', N'$2a$11$lfnw1S5rUONSjqVY9/r2FuBAFUjBUq6OYN75Ls/yF3r83S2gRJKWC', CAST(N'1982-10-28' AS Date), N'M', N'test11@test.com', N'thispersondoesnotexistcom12.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (78, N'fnuv4mi4', N'Marian', N'Piotrowski', N'$2a$11$Epsi2rATQ6IW0Rc/1qScHOmiD0638OvmJtaAIV.J2kLR0qkeGro1a', CAST(N'1973-10-28' AS Date), N'M', N'test10@test.com', N'thispersondoesnotexistcom3.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (79, N'gwvaplp0', N'Kamil', N'Wieczorek', N'$2a$11$yVmKN5VZyVEhkE5r0FkZQOHVzp7plLwb/riaxZUrij1i2SCYQH5yO', CAST(N'1980-10-28' AS Date), N'M', N'test9@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (80, N'fd34bxed', N'Janina', N'Krawczyk', N'$2a$11$LYPNazrT4eMSowUTcizB7u.a.Kt/SxqItRyPCLNXYc30wRjV9r6x2', CAST(N'1984-10-28' AS Date), N'F', N'test8@test.com', N'thispersondoesnotexistcom17.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (81, N'mvkrrn3d', N'Beata', N'Wieczorek', N'$2a$11$q/.sqdrAY.Cj2.7zaRkyh.OHuZG5/peb5vpJz.W8nQk9QEQGxQ2XO', CAST(N'1991-10-28' AS Date), N'F', N'test7@test.com', N'thispersondoesnotexistcom16.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (82, N'ff1fc0k3', N'Jarosław', N'Zając', N'$2a$11$IXt8AnLJBFa4fjND4XVcPuqkkxB12zmeM5TwCO6r7AAkGojhBQ/yS', CAST(N'1977-10-28' AS Date), N'M', N'test6@test.com', N'thispersondoesnotexistcom4.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (83, N'0y3rg1qp', N'Jacek', N'Nowakowski', N'$2a$11$C76k7G8Z1elcCz8oyNLNXO/2XPBCeoDsqbmhBwbKcqbDLY0odKFom', CAST(N'1991-10-28' AS Date), N'M', N'test5@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (84, N't4b2faxd', N'Halina', N'Dudek', N'$2a$11$A7/irfRNZGw6Fcg7NNbdLepm6./GPnV8grQewx7hZ2cUtUQ9ym1w.', CAST(N'1974-10-28' AS Date), N'F', N'test4@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (85, N'aw1v5t33', N'Sławomir', N'Dudek', N'$2a$11$Y6QYXoL6f/BsPfL3g1vAxuuS7RQhRyuXSJj4hDlnj9I5zX1vQtTtO', CAST(N'1992-10-28' AS Date), N'M', N'test3@test.com', N'thispersondoesnotexistcom8.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (86, N'ijevgl2c', N'Stanisława', N'Wiśniewski', N'$2a$11$UpepKowfQSe5AGl/KFvCl.5nXX6dxrD.GvBDAw2nFQcetuFOWlQ3u', CAST(N'1995-10-28' AS Date), N'F', N'test2@test.com', N'thispersondoesnotexistcom3.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (87, N'3yqymqjt', N'Artur', N'Kamiński', N'$2a$11$0UrdC535DSf94wpuB4sUW.LzvpEfW/5OLgQ./Wv7ffdkcknlMFiay', CAST(N'1993-10-28' AS Date), N'M', N'test23@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (88, N'usapr3tc', N'Edward', N'Zieliński', N'$2a$11$/aePq7mkblJcbUKmj1YZ2eooCAhoyxPBVla44ffvqrtHENtb01Qsy', CAST(N'1974-10-28' AS Date), N'M', N'test24@test.com', N'thispersondoesnotexistcom7.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (89, N'wrld3ia2', N'Joanna', N'Grabowski', N'$2a$11$Z8u/sdRTtmx.V5loFcMyOuK/eNQMVZKUksmEBRLRU5D.xDCXa3Aqi', CAST(N'1989-10-28' AS Date), N'F', N'test25@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (90, N'gdu1ct2d', N'Marian', N'Piotrowski', N'$2a$11$//5hkuFjGpygggZ4cbPmqOmwdghAyaGy0YgwbqENyTtaidHFDUFOO', CAST(N'1981-10-28' AS Date), N'M', N'test26@test.com', N'thispersondoesnotexistcom6.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (91, N'e2srj2bx', N'Artur', N'Kamiński', N'$2a$11$nXiiWJohlbTgpEzV3FYpBeya9xQE2x5kx9UqJkHbbw0mJCoaYlf.i', CAST(N'1984-10-28' AS Date), N'M', N'test48@test.com', N'thispersondoesnotexistcom14.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (92, N'4o3hojak', N'Jadwiga', N'Michalski', N'$2a$11$ALAPoT/k3wJ0v8wWg/VyC.V1i4ZCsZS.EI7KDs8C.sGlKVYOvBvpe', CAST(N'1979-10-28' AS Date), N'F', N'test47@test.com', N'thispersondoesnotexistcom7.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (93, N'f1xcqtjm', N'Janina', N'Krawczyk', N'$2a$11$B0rBQgeuSIRJNDwkQNhyhucNWlahgCRmgmX4sRFz3IK8dyhoYs2ny', CAST(N'1978-10-28' AS Date), N'F', N'test46@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (94, N'osmoktm3', N'Jacek', N'Nowakowski', N'$2a$11$t6LMkwKBbcb3wYkhd3KNfu8VvmsBYiD0otdYTjMz5pHB9e39wvf3y', CAST(N'1983-10-28' AS Date), N'M', N'test45@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (95, N'a5pksrzb', N'Monika', N'Pawłowski', N'$2a$11$1hvGBbhO.DE0hZvTozaEquXyYMUizJAgKiMITr7nBM3XdN.RR7nc2', CAST(N'1993-10-28' AS Date), N'F', N'test44@test.com', N'thispersondoesnotexistcom13.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (96, N'5w4shk2l', N'Marian', N'Piotrowski', N'$2a$11$Hw/lE90eY/ompS6UUNBBPOdhzl5cn8Ql2DGpnNJis1POCkivr20K2', CAST(N'2001-10-28' AS Date), N'M', N'test43@test.com', N'thispersondoesnotexistcom6.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (97, N'pu1dqlqz', N'Edward', N'Zieliński', N'$2a$11$vJkrsl6OMNISS.Ll3HMuCutyqOZOeK51LcbfAqKwnJqkiDxLhPn.q', CAST(N'1993-10-28' AS Date), N'M', N'test42@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (98, N'jrci0tnn', N'Aleksandra', N'Jabłoński', N'$2a$11$OfBtuP83uEg5xXvPQPXDeuaquX.03zRkXvpSCSYpDynJ7sgTYJFpC', CAST(N'2000-10-28' AS Date), N'F', N'test41@test.com', N'thispersondoesnotexistcom13.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (99, N'iyanzeaq', N'Irena', N'Adamczyk', N'$2a$11$vtRLAecLzqeTKRYJJ5A8X.3LICL6faD3VGduLumnyqObbKRPQUrM6', CAST(N'1986-10-28' AS Date), N'F', N'test40@test.com', N'thispersondoesnotexistcom1.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (100, N'zocky3id', N'Jacek', N'Nowakowski', N'$2a$11$T10gLV1Oo6m9YIy15YMe7uKPoS7V0bBup/adUQ8g7CbiUL5UgAUea', CAST(N'1990-10-28' AS Date), N'M', N'test39@test.com', N'thispersondoesnotexistcom12.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (101, N'cwnfvksg', N'Halina', N'Dudek', N'$2a$11$Web9DrU5TPNwM5E3mlvP/.uo57Xq.x/r/DF6JbbJN5bAiDHp9U4NC', CAST(N'1979-10-28' AS Date), N'F', N'test99@test.com', N'thispersondoesnotexistcom4.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (102, N'qhad0hmg', N'Mateusz', N'Krawczyk', N'$2a$11$gbPUauOvQAjpw7.IcCdgaOJl7iM1Qvi1zBzcnQHc4gyw9KQSEWsgq', CAST(N'1996-10-28' AS Date), N'M', N'test38@test.com', N'thispersondoesnotexistcom11.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (103, N'zkpr4meb', N'Zbigniew', N'Król', N'$2a$11$vxC8Pfk4zZ3u4QzPaOt4cOG8nPYp8o.WQMW87v9X6ZsMj07t46ot6', CAST(N'1997-10-28' AS Date), N'M', N'test36@test.com', N'thispersondoesnotexistcom22.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (104, N'yymv4aaz', N'Marianna', N'Olszewski', N'$2a$11$dw3KSDSA1Qkbsy8NI8df2eIa8hWMzC5m6Jljvot3YGkglX4XmeoIu', CAST(N'1992-10-28' AS Date), N'F', N'test35@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (105, N'dtirg1oz', N'Magdalena', N'Nowakowski', N'$2a$11$OweDmv6uvJnB0dRlF9FSmuyhkmN/R7akEntJM5PnHV/r5x4fSlgcu', CAST(N'1977-10-28' AS Date), N'F', N'test34@test.com', N'thispersondoesnotexistcom18.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (106, N'rh3340ym', N'Joanna', N'Mazur', N'$2a$11$9OzgXobcxvxVm6oaA6/bOOQamQF2LMW1PP4xr3UZ/QeAiBBpjIF4m', CAST(N'1979-10-28' AS Date), N'F', N'test33@test.com', N'thispersondoesnotexistcom2.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (107, N'yfrd0us3', N'Marta', N'Król', N'$2a$11$uicx8pAYXsjmEGXyDR5xTuG2d6l1EukSCxtPrLxN4kd9YDS.bOwmu', CAST(N'1987-10-28' AS Date), N'F', N'test32@test.com', N'thispersondoesnotexistcom15.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (108, N'q1tkfxup', N'Jolanta', N'Kowalski', N'$2a$11$WtFUFiqFXX3az71zP5B.FuGJOE1FiSGBW.aNFcMzZo4WXJxFmKyla', CAST(N'1992-10-28' AS Date), N'F', N'test31@test.com', N'thispersondoesnotexistcom5.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (109, N'v3fqlkis', N'Janina', N'Krawczyk', N'$2a$11$4/c/jxSYarUJkG3F6W4.FO/Qd3IIGCCRNQAezeD3jGmUebDM5MNCe', CAST(N'1983-10-28' AS Date), N'F', N'test30@test.com', N'thispersondoesnotexistcom14.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (110, N'ztfg145a', N'Joanna', N'Mazur', N'$2a$11$zvaKgck3Z4wrcsw9w5BXO.elWovw8r6EMbqImHG9yQ94b/Gsi7/1i', CAST(N'1975-10-28' AS Date), N'F', N'test29@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (111, N'mf25oyib', N'Irena', N'Adamczyk', N'$2a$11$iQkKg6KVsbZsN0LkvHZ40O8mzaiCVD3xwHHdqeallqtu0Vgl7Vd02', CAST(N'1985-10-28' AS Date), N'F', N'test28@test.com', N'thispersondoesnotexistcom19.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (112, N'zlbpoeps', N'Jacek', N'Nowakowski', N'$2a$11$rMWvxrgENBiZTv/5uu1V5OcQ2cma4l5wHD1wojSF53jCFLStMjKIG', CAST(N'1993-10-28' AS Date), N'M', N'test27@test.com', N'thispersondoesnotexistcom2.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (113, N'4f3lele1', N'Magdalena', N'Nowakowski', N'$2a$11$Ml4/dtjOjT5AupSuzBfUo.I4fzYCq5FgfSPVjJ5bmGQN8XKuX6jzS', CAST(N'1991-10-28' AS Date), N'F', N'test37@test.com', N'thispersondoesnotexistcom21.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath], [ShortDescription]) VALUES (114, N'borjxngj', N'Marian', N'Piotrowski', N'$2a$11$B34cK6.HnoaBBuwnGCtQ4O2Gpl1aupPpGE4XQg5d968Y1QbUbCjrm', CAST(N'1982-10-28' AS Date), N'M', N'test100@test.com', N'thispersondoesnotexistcom9.jfif', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.')
GO
SET IDENTITY_INSERT [User].[User] OFF
GO
INSERT [User].[UserDescriptionBlock] ([UserDescriptionBlockId], [UserId], [TextBlockId]) VALUES (N'cd8ce9f9-b22a-4d41-a725-3f2ab9cb2638', 6, N'5d50fc15-adbe-4aef-a7fe-aa694a7d94de')
GO
INSERT [User].[UserDescriptionBlock] ([UserDescriptionBlockId], [UserId], [TextBlockId]) VALUES (N'41a26e72-2d80-4e3c-bf95-4df0a916e917', 6, N'b80afd29-1385-41aa-b480-fffac9eb228c')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Group_Name]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Group_Name] ON [Group].[Group]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Group_ViewName]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Group_ViewName] ON [Group].[Group]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_UserLogin]    Script Date: 11/16/2021 11:07:26 AM ******/
ALTER TABLE [User].[User] ADD  CONSTRAINT [UC_UserLogin] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User]    Script Date: 11/16/2021 11:07:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_User] ON [User].[User]
(
	[Name] ASC,
	[Surname] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_ViewName]    Script Date: 11/16/2021 11:07:26 AM ******/
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
ALTER TABLE [Chat].[Message] ADD  CONSTRAINT [DF_Message_IsRead]  DEFAULT ((0)) FOR [IsReadByReceiver]
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
ALTER TABLE [Notification].[Notification] ADD  CONSTRAINT [DF_Notification_NotificationId]  DEFAULT (newid()) FOR [NotificationId]
GO
ALTER TABLE [Post].[ChildToParrentComment] ADD  CONSTRAINT [DF_ChildToParrentComment_ChildToParentCommentId]  DEFAULT (newsequentialid()) FOR [ChildToParentCommentId]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CommentId]  DEFAULT (newsequentialid()) FOR [CommentId]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_IsRemoved]  DEFAULT ((0)) FOR [IsRemoved]
GO
ALTER TABLE [Post].[GroupPost] ADD  CONSTRAINT [DF_GroupPost_GroupPostId]  DEFAULT (newid()) FOR [GroupPostId]
GO
ALTER TABLE [Post].[GroupPostComment] ADD  CONSTRAINT [DF_GroupPostComment_GroupPostCommentId]  DEFAULT (newsequentialid()) FOR [GroupPostCommentId]
GO
ALTER TABLE [Post].[GroupPostComment] ADD  CONSTRAINT [DF_GroupPostComment_AuthorVisibilityType]  DEFAULT ('U') FOR [AuthorVisibilityType]
GO
ALTER TABLE [Post].[Post] ADD  CONSTRAINT [DF__Post__PostId__46E78A0C]  DEFAULT (newid()) FOR [PostId]
GO
ALTER TABLE [Post].[Post] ADD  CONSTRAINT [DF_Post_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Post].[UserPost] ADD  CONSTRAINT [DF_UserPost_UserPostId]  DEFAULT (newid()) FOR [UserPostId]
GO
ALTER TABLE [User].[BlockedUsers] ADD  CONSTRAINT [DF_BlockedUsers_BlockedUsersId]  DEFAULT (newid()) FOR [BlockedUsersId]
GO
ALTER TABLE [User].[Session] ADD  CONSTRAINT [DF_Session_SessionId]  DEFAULT (newid()) FOR [SessionId]
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
ALTER TABLE [Notification].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_NotificationType] FOREIGN KEY([NotificationTypeId])
REFERENCES [Notification].[NotificationType] ([NotificationTypeId])
GO
ALTER TABLE [Notification].[Notification] CHECK CONSTRAINT [FK_Notification_NotificationType]
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
ALTER TABLE [Post].[GroupPostComment]  WITH CHECK ADD  CONSTRAINT [FK_GroupPostComment_Comment] FOREIGN KEY([CommentId])
REFERENCES [Post].[Comment] ([CommentId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[GroupPostComment] CHECK CONSTRAINT [FK_GroupPostComment_Comment]
GO
ALTER TABLE [Post].[GroupPostComment]  WITH CHECK ADD  CONSTRAINT [FK_GroupPostComment_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[GroupPostComment] CHECK CONSTRAINT [FK_GroupPostComment_Group]
GO
ALTER TABLE [Post].[GroupPostComment]  WITH CHECK ADD  CONSTRAINT [FK_GroupPostComment_GroupPostComment] FOREIGN KEY([MainParentId])
REFERENCES [Post].[GroupPostComment] ([GroupPostCommentId])
GO
ALTER TABLE [Post].[GroupPostComment] CHECK CONSTRAINT [FK_GroupPostComment_GroupPostComment]
GO
ALTER TABLE [Post].[GroupPostComment]  WITH CHECK ADD  CONSTRAINT [FK_GroupPostComment_GroupPostComment1] FOREIGN KEY([ParentId])
REFERENCES [Post].[GroupPostComment] ([GroupPostCommentId])
GO
ALTER TABLE [Post].[GroupPostComment] CHECK CONSTRAINT [FK_GroupPostComment_GroupPostComment1]
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
ALTER TABLE [User].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [User].[Session] CHECK CONSTRAINT [FK_Session_User]
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
/****** Object:  StoredProcedure [Chat].[GetChatPreviews]    Script Date: 11/16/2021 11:07:27 AM ******/
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
