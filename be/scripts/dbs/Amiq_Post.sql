USE [master]
GO
/****** Object:  Database [Amiq_Post]    Script Date: 1/6/2022 5:17:24 PM ******/
CREATE DATABASE [Amiq_Post]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Amiq_Post', FILENAME = N'/var/opt/mssql/data/Amiq_Post.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Amiq_Post_log', FILENAME = N'/var/opt/mssql/data/Amiq_Post_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Amiq_Post] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Amiq_Post].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Amiq_Post] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Amiq_Post] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Amiq_Post] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Amiq_Post] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Amiq_Post] SET ARITHABORT OFF 
GO
ALTER DATABASE [Amiq_Post] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Amiq_Post] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Amiq_Post] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Amiq_Post] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Amiq_Post] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Amiq_Post] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Amiq_Post] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Amiq_Post] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Amiq_Post] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Amiq_Post] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Amiq_Post] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Amiq_Post] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Amiq_Post] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Amiq_Post] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Amiq_Post] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Amiq_Post] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Amiq_Post] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Amiq_Post] SET RECOVERY FULL 
GO
ALTER DATABASE [Amiq_Post] SET  MULTI_USER 
GO
ALTER DATABASE [Amiq_Post] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Amiq_Post] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Amiq_Post] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Amiq_Post] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Amiq_Post] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Amiq_Post] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Amiq_Post', N'ON'
GO
ALTER DATABASE [Amiq_Post] SET QUERY_STORE = OFF
GO
USE [Amiq_Post]
GO
/****** Object:  Schema [Group]    Script Date: 1/6/2022 5:17:24 PM ******/
CREATE SCHEMA [Group]
GO
/****** Object:  Schema [Post]    Script Date: 1/6/2022 5:17:24 PM ******/
CREATE SCHEMA [Post]
GO
/****** Object:  Schema [User]    Script Date: 1/6/2022 5:17:24 PM ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Group].[Group]    Script Date: 1/6/2022 5:17:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[Group](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[AvatarSrc] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Post].[Comment]    Script Date: 1/6/2022 5:17:24 PM ******/
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
/****** Object:  Table [Post].[GroupPost]    Script Date: 1/6/2022 5:17:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[GroupPost](
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[VisibleAsCreatedByAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_GroupPost] PRIMARY KEY CLUSTERED 
(
	[GroupPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[GroupPostComment]    Script Date: 1/6/2022 5:17:24 PM ******/
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
/****** Object:  Table [Post].[Post]    Script Date: 1/6/2022 5:17:24 PM ******/
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
/****** Object:  Table [Post].[UserPost]    Script Date: 1/6/2022 5:17:24 PM ******/
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
/****** Object:  Table [User].[User]    Script Date: 1/6/2022 5:17:24 PM ******/
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
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'2439de37-5d1a-ec11-a7f4-d83bbff1afdf', N'dsadad', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, NULL, CAST(N'2021-09-20T23:53:51.990' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', N'hfdhfdh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, NULL, CAST(N'2021-09-20T23:54:04.573' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'040975f2-101b-ec11-a7f4-d83bbff1afdf', N'dfgfdgdfg', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', CAST(N'2021-09-21T21:20:15.280' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'c2b483fd-fe22-ec11-a7fa-d83bbff1afdf', N'fdjhdh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'040975f2-101b-ec11-a7f4-d83bbff1afdf', CAST(N'2021-10-01T23:31:52.193' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'97282111-0023-ec11-a7fa-d83bbff1afdf', N'hfdhdfh', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6, N'040975f2-101b-ec11-a7f4-d83bbff1afdf', CAST(N'2021-10-01T23:39:34.597' AS DateTime), NULL, NULL, N'31ad2145-5d1a-ec11-a7f4-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'073c8497-bc23-ec11-a7fa-d83bbff1afdf', N'dsadsad', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 6, NULL, CAST(N'2021-10-02T22:09:05.447' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'365e474c-1c3b-ec11-a819-d83bbff1afdf', N'test', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, NULL, CAST(N'2021-11-01T15:02:07.650' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', N'test2', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:03:11.347' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', N'test3', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:05:59.390' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'd8b8a5dd-203b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:34:49.523' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'dab8a5dd-203b-ec11-a819-d83bbff1afdf', N'dfsfdsfsdfsdf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:34:58.470' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'5507e41b-213b-ec11-a819-d83bbff1afdf', N'fdsfsdf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:36:33.950' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'16771250-213b-ec11-a819-d83bbff1afdf', N'dsadsad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:38:01.497' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'6b06ef9a-213b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:40:07.093' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'6d06ef9a-213b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:40:16.453' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'859c42d3-213b-ec11-a819-d83bbff1afdf', N'dsadsad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:41:41.593' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'4509ab6b-223b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:45:57.293' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'01af80c3-223b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:48:24.653' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'fb0046d3-223b-ec11-a819-d83bbff1afdf', N'dsadasd', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:48:51.113' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'a84efa06-233b-ec11-a819-d83bbff1afdf', N'dsadasdsad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:50:17.860' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'fcd1434b-233b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:52:12.423' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'180efca0-233b-ec11-a819-d83bbff1afdf', N'gfdsfsdfsf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T15:54:36.240' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'f32b96d2-243b-ec11-a819-d83bbff1afdf', N'dsffsdfsdf', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'180efca0-233b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T16:03:08.953' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'b6da8612-253b-ec11-a819-d83bbff1afdf', N'dsadad', N'1751eba6-d7ae-4d84-9962-2260bb374a98', 6, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', CAST(N'2021-11-01T16:04:56.227' AS DateTime), NULL, NULL, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'b0dd6b16-343b-ec11-a819-d83bbff1afdf', N'dsadsad', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-01T17:52:25.213' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'c0748417-aa41-ec11-a820-d83bbff1afdf', N'sfafasfa', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-09T23:12:14.640' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'a886e620-aa41-ec11-a820-d83bbff1afdf', N'dfsdsfsdf', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-09T23:12:30.383' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'96d8bde0-6942-ec11-a822-d83bbff1afdf', N'dasdasdasd', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, N'a886e620-aa41-ec11-a820-d83bbff1afdf', CAST(N'2021-11-10T22:05:06.117' AS DateTime), NULL, NULL, N'a886e620-aa41-ec11-a820-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8b6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:03.177' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8c6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad2', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:05.490' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8d6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad23', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:06.803' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8e6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad23435', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:08.343' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'8f6e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad2343543535', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:09.660' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'906e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad23435435352313', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:11.077' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'916e17fd-6a42-ec11-a822-d83bbff1afdf', N'dsadsad234354353523135435435', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:12.307' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'56955603-6b42-ec11-a822-d83bbff1afdf', N'dsadsad2343543535231354354353213213', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:13.657' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'57955603-6b42-ec11-a822-d83bbff1afdf', N'dsadsad234354353523135435435321321323525325', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:15.360' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'58955603-6b42-ec11-a822-d83bbff1afdf', N'dsadsad23435433253523135435435321321323525325', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:17.010' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'59955603-6b42-ec11-a822-d83bbff1afdf', N'qw', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:19.417' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'5a955603-6b42-ec11-a822-d83bbff1afdf', N'ghjgh hfghfg', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-10T22:13:21.973' AS DateTime), NULL, NULL, NULL, 1)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'5dbc522d-4950-ec11-a82e-d83bbff1afdf', N'ok d', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, N'906e17fd-6a42-ec11-a822-d83bbff1afdf', CAST(N'2021-11-28T13:46:17.487' AS DateTime), NULL, NULL, N'906e17fd-6a42-ec11-a822-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'87fe3d7a-5550-ec11-a82e-d83bbff1afdf', N'dsadsa', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, N'59955603-6b42-ec11-a822-d83bbff1afdf', CAST(N'2021-11-28T15:14:20.497' AS DateTime), NULL, NULL, N'59955603-6b42-ec11-a822-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'b13e8a2a-5a50-ec11-a82e-d83bbff1afdf', N'dasdadasd', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-28T15:47:54.260' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'b23e8a2a-5a50-ec11-a82e-d83bbff1afdf', N'dasdadasdfdgdfg', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-28T15:47:56.033' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'b33e8a2a-5a50-ec11-a82e-d83bbff1afdf', N'wdasd', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, NULL, CAST(N'2021-11-28T15:47:58.320' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'a3438347-5a50-ec11-a82e-d83bbff1afdf', N'dsfgsdgdsgsdg', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6, N'b13e8a2a-5a50-ec11-a82e-d83bbff1afdf', CAST(N'2021-11-28T15:48:42.867' AS DateTime), NULL, NULL, N'b13e8a2a-5a50-ec11-a82e-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'9e87d9c1-6651-ec11-a832-d83bbff1afdf', N'dsadasd', N'399b40ad-7082-4d07-a735-f39077e21941', 6, NULL, CAST(N'2021-11-29T23:50:33.240' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'14bc6a23-bc55-ec11-a839-d83bbff1afdf', N'fggfh', N'55f31503-b55d-498a-8053-478475185b31', 6, NULL, CAST(N'2021-12-05T12:11:48.803' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', N'fdgdfg', N'55f31503-b55d-498a-8053-478475185b31', 6, NULL, CAST(N'2021-12-05T12:16:27.027' AS DateTime), NULL, NULL, NULL, 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'fe3163db-be55-ec11-a839-d83bbff1afdf', N'gfdgdfgdfgdfg', N'55f31503-b55d-498a-8053-478475185b31', 6, N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', CAST(N'2021-12-05T12:31:16.447' AS DateTime), NULL, NULL, N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'2a3aa8ed-be55-ec11-a839-d83bbff1afdf', N'fdgdfg', N'55f31503-b55d-498a-8053-478475185b31', 6, N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', CAST(N'2021-12-05T12:31:47.100' AS DateTime), NULL, NULL, N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', 0)
INSERT [Post].[Comment] ([CommentId], [TextContent], [PostId], [AuthorId], [ParentId], [CreatedAt], [EditedBy], [EditedAt], [MainParentId], [IsRemoved]) VALUES (N'09aa5055-c055-ec11-a839-d83bbff1afdf', N'sdfdsfsdf', N'55f31503-b55d-498a-8053-478475185b31', 6, N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', CAST(N'2021-12-05T12:41:50.507' AS DateTime), NULL, NULL, N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', 0)
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'0c3e01d7-dca7-480e-a7a3-07aefd56350c', N'168ab6ee-2200-4e3c-870b-b7f570ef5a5c', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'9756c5b5-4af7-4430-95dd-0a86cec1f004', N'2b960c19-1bef-46a9-8590-e568eae5031d', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'bdb83ca4-19f1-483d-8fc2-117c1874c6d6', N'acc78c66-cbe8-459e-924a-b7f312afd20c', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'0bc23f0a-2a25-4240-8380-31a912b672aa', N'351cd153-442d-4e9d-a913-840dc61df04a', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'497f0191-1972-41d1-9f63-4245733e04a2', N'5ef31886-3c4b-407f-a8ef-ca3b2a64ca41', 1, 6, 0)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'84681cdf-a8fb-4d66-9f32-436eae316611', N'b4e707b8-d7c9-4e5c-a416-fc100b88a116', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'27638bd4-cd75-47a2-92a7-55127d0011b3', N'71404c60-dd6f-47be-8d49-bcb982aa8b37', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'd8eff04b-fb73-4c48-8be7-6b8cc444fbee', N'274d96a4-5949-4fa8-920a-15a550a25ac6', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'5fb99e9e-abfc-4924-bd5b-74718449cd34', N'80e66d97-eeba-4e90-8e38-edd4ea69847c', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'c83ea51e-48d2-4f3b-9b9b-74b64d5c8d68', N'ced1a6a0-4954-4cb2-af39-c7bd3380d748', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'c387956d-d3fa-40c9-8d3d-a335a6c221c0', N'79ff36ee-8636-4971-a6aa-532787028afd', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'95dbb7ae-5868-4f94-a384-a42aa8af561a', N'0b29c90a-24bf-4744-8232-b741e54bbd87', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'1c5653d0-f0c7-4344-b1f7-ab946730d2d7', N'cbd4fd5f-9fa0-491c-b045-c5dcf1e69595', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'bf52ff78-0141-48f1-b091-b49bfbbec1f7', N'2b5ae0b2-767a-40cc-b7d9-60f344107798', 1, 6, 0)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'b7bd0cbb-7f65-4311-b3f5-cca6d3745764', N'55f31503-b55d-498a-8053-478475185b31', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'fcae20ae-7c0b-4d88-b085-d1655dc69ab1', N'acd4ca8a-3b04-41e4-b9be-b8380b2b1353', 1, 6, 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId], [AuthorId], [VisibleAsCreatedByAdmin]) VALUES (N'cdba352a-3822-4438-8d37-e5af63947a27', N'1ff81750-b3fb-4569-a464-5b895010b566', 1, 6, 1)
GO
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', 1, N'365e474c-1c3b-ec11-a819-d83bbff1afdf', N'GA', NULL, NULL)
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'e9db7826-b5dd-47bb-8dbc-c69489de0760', 1, N'25ed3e72-1c3b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4')
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'2749ebe5-1c3b-ec11-a819-d83bbff1afdf', 1, N'ba3168d6-1c3b-ec11-a819-d83bbff1afdf', N'U', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'e9db7826-b5dd-47bb-8dbc-c69489de0760')
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'190efca0-233b-ec11-a819-d83bbff1afdf', 1, N'180efca0-233b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'e9db7826-b5dd-47bb-8dbc-c69489de0760')
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'f42b96d2-243b-ec11-a819-d83bbff1afdf', 1, N'f32b96d2-243b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'190efca0-233b-ec11-a819-d83bbff1afdf')
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'b7da8612-253b-ec11-a819-d83bbff1afdf', 1, N'b6da8612-253b-ec11-a819-d83bbff1afdf', N'GA', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4', N'3a6a2f0b-3367-44ad-b97e-817f1d64aaf4')
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'9f87d9c1-6651-ec11-a832-d83bbff1afdf', 1, N'9e87d9c1-6651-ec11-a832-d83bbff1afdf', N'U', NULL, NULL)
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'15bc6a23-bc55-ec11-a839-d83bbff1afdf', 1, N'14bc6a23-bc55-ec11-a839-d83bbff1afdf', N'GA', NULL, NULL)
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'3bf53fc9-bc55-ec11-a839-d83bbff1afdf', 1, N'3af53fc9-bc55-ec11-a839-d83bbff1afdf', N'U', NULL, NULL)
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'ff3163db-be55-ec11-a839-d83bbff1afdf', 1, N'fe3163db-be55-ec11-a839-d83bbff1afdf', N'U', N'3bf53fc9-bc55-ec11-a839-d83bbff1afdf', N'3bf53fc9-bc55-ec11-a839-d83bbff1afdf')
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'2b3aa8ed-be55-ec11-a839-d83bbff1afdf', 1, N'2a3aa8ed-be55-ec11-a839-d83bbff1afdf', N'GA', N'3bf53fc9-bc55-ec11-a839-d83bbff1afdf', N'3bf53fc9-bc55-ec11-a839-d83bbff1afdf')
INSERT [Post].[GroupPostComment] ([GroupPostCommentId], [GroupId], [CommentId], [AuthorVisibilityType], [MainParentId], [ParentId]) VALUES (N'0aaa5055-c055-ec11-a839-d83bbff1afdf', 1, N'09aa5055-c055-ec11-a839-d83bbff1afdf', N'GA', N'3bf53fc9-bc55-ec11-a839-d83bbff1afdf', N'3bf53fc9-bc55-ec11-a839-d83bbff1afdf')
GO
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'0dbb48d3-9b8a-42af-81c5-00aa58aa90dc', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-11T23:14:47.800' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'837387b8-cff4-4cf5-9f1f-027b4502a6af', N'dsadsa', NULL, NULL, CAST(N'2021-10-14T21:03:42.857' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'a01ed70f-41f5-4518-b918-07fa333559cd', N'jhyityurtyurthrfhrfher', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'434a6ab8-d113-4da5-91ad-0bd217b50b99', N'fdgfdg', NULL, NULL, CAST(N'2021-10-10T20:30:14.933' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'f73955cc-91e6-4ecb-bd81-1285856c8d56', N'gfdgdfgdfg', NULL, NULL, CAST(N'2021-10-11T23:14:42.190' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'274d96a4-5949-4fa8-920a-15a550a25ac6', N'fghfghgfhjgfjhgsdfsdf', NULL, NULL, CAST(N'2021-12-05T12:11:41.700' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'3b6d8f3e-a25a-4a4f-9bab-15caf28b0b71', N'gjhfj', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'63d0004f-45a6-43fe-b1a9-15e13004c28a', N'cdvdasda asd sa', NULL, NULL, CAST(N'2021-11-28T11:56:38.723' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'399054ce-e9fc-49c6-92e9-1f03c4ca0a84', N'dsadad', NULL, NULL, CAST(N'2021-11-13T15:09:09.407' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'1751eba6-d7ae-4d84-9962-2260bb374a98', N'fgfg', NULL, NULL, CAST(N'2021-10-19T21:34:45.953' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7e46b1dd-d2ec-4a12-8004-23d0670be225', N'fdgdfgdfgdfg', NULL, NULL, CAST(N'2021-10-11T23:14:57.460' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'18d881ad-0dfe-49e8-a7ab-260aa1353c26', N'dadsad', NULL, NULL, CAST(N'2021-10-28T00:29:39.913' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'6c65ce8f-aa4a-4d61-94f6-2b2895014405', N'hej', NULL, NULL, CAST(N'2021-11-16T22:26:49.277' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'003753b4-22d6-423a-a231-31daa4afb2a6', N'dsadasd', NULL, NULL, CAST(N'2021-10-11T23:13:35.767' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'4b89b5cc-dfde-4a5d-9329-42d9dc8b962f', N'sdffsdfsdf', NULL, NULL, CAST(N'2021-10-19T21:33:42.103' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'2fd35b83-4c33-47bf-a2a1-449771668756', N'fdgdfgfdg', NULL, NULL, CAST(N'2021-10-02T23:01:51.307' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'888ab433-508f-4182-8592-46312cd54c2a', N'gdfsg', NULL, NULL, CAST(N'2021-10-02T23:15:33.253' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'55f31503-b55d-498a-8053-478475185b31', N'fghfghgfhjgfjhgsdfsdffghfgh', NULL, NULL, CAST(N'2021-12-05T12:11:42.717' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', N'loREM dsadikjdgfok ;daslda''; dsfkgjdkgjdkgjld', NULL, NULL, CAST(N'2021-09-12T22:59:50.993' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'79ff36ee-8636-4971-a6aa-532787028afd', N'fghfgh', NULL, NULL, CAST(N'2021-12-05T12:11:39.710' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'fedff6ca-d408-4691-8f5e-54122ab7199d', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-11T23:13:06.963' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'fa8410ad-5d48-46f9-9a9e-58e30ed9e9a8', N'dawdawd', NULL, NULL, CAST(N'2021-10-11T23:16:21.327' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'e7cedcd2-3486-4ad0-980d-5904236428da', N'vnhj', NULL, NULL, CAST(N'2021-10-11T22:58:04.390' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'cc6eb0b7-436c-41c5-9cd9-5b81bba8c812', N'dsadasd', NULL, NULL, CAST(N'2021-10-11T23:16:16.277' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'1ff81750-b3fb-4569-a464-5b895010b566', N'sdadsadgfhfgh', NULL, NULL, CAST(N'2021-12-05T12:11:32.457' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'2b5ae0b2-767a-40cc-b7d9-60f344107798', N'sdadsadgfhfgh', NULL, NULL, CAST(N'2021-12-05T12:11:31.600' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'68b82c0e-f1c2-44d8-89d3-63a54f8d6959', N'gdsgsdgds', NULL, NULL, CAST(N'2021-10-02T22:59:27.270' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'e4baa157-6be5-4491-9323-65a04b7531f6', N'fgdgfdg', NULL, NULL, CAST(N'2021-10-19T21:33:44.883' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'63febafa-fa1f-4e77-b906-69b61dd75aa2', N'dadsad', NULL, NULL, CAST(N'2021-10-28T00:29:41.830' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'a51d4962-4047-4fd5-8e33-6dc770433950', N'dsfdsdfdsf', NULL, NULL, CAST(N'2021-10-28T00:29:34.850' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'c1380b00-9d09-44d7-a77a-6e7dbf2f6a0b', N'gdfgfdg', NULL, NULL, CAST(N'2021-10-11T23:16:18.737' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'd224c8aa-dd28-48d8-8f44-6e9cb5030aeb', N'dsad', NULL, NULL, CAST(N'2021-11-27T15:06:08.963' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'604c9af6-8219-44c7-a8bd-70ccf4c55f0e', N'fdsdf', NULL, NULL, CAST(N'2021-10-11T23:04:50.727' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'cf6bc35f-6ea2-4848-839c-83a8998855cb', N'dasd', NULL, NULL, CAST(N'2021-10-17T21:31:50.413' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'351cd153-442d-4e9d-a913-840dc61df04a', N'fdgfdgfhfghgfhfg', NULL, NULL, CAST(N'2021-12-05T12:11:20.513' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'c77ebb27-875b-4fc0-998c-875d9df77830', N'fgdsfsdf', NULL, NULL, CAST(N'2021-10-10T20:29:39.340' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'cb342767-c1aa-45f6-bfec-8e8bb2508ab4', N'dsad', NULL, NULL, CAST(N'2021-11-27T15:08:05.307' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', N'sdfsfdsfsdfsd', NULL, NULL, CAST(N'2021-09-09T00:00:00.000' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'76351e67-1298-4208-b708-9d4ef000ab4c', N'fdggd', NULL, NULL, CAST(N'2021-10-10T20:42:57.780' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'd6c3c87e-c00e-4b0d-90bd-9eae0296a085', N'wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfswqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd wqasfsgfdsgdsgd ', NULL, NULL, CAST(N'2021-10-11T23:16:55.860' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7c4a0a9f-98fa-4798-a2f7-9f74ed6b3509', N'gdfsggdf', NULL, NULL, CAST(N'2021-10-02T23:17:15.883' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7a0f25cc-b8de-4fc3-b8fc-a6435ee8cfa8', N'dsadagfdsg', NULL, NULL, CAST(N'2021-10-02T23:10:01.680' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'958b6004-5432-441a-9648-ac19e173ab36', N'sdadasgfds', NULL, NULL, CAST(N'2021-11-28T11:56:20.660' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'570f5d1d-b39b-41e5-abbb-ad5c6c2a8d37', N'dfsf', NULL, NULL, CAST(N'2021-10-10T20:59:58.063' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'132eb31c-1fe9-4d25-ae20-b0c417f886da', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-10T20:36:45.863' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'0b29c90a-24bf-4744-8232-b741e54bbd87', N'fdgfdgfhfghgfhfgwdasda', NULL, NULL, CAST(N'2021-12-05T12:11:21.540' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'acc78c66-cbe8-459e-924a-b7f312afd20c', N'fghfghgfhjgfjhg', NULL, NULL, CAST(N'2021-12-05T12:11:40.740' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'168ab6ee-2200-4e3c-870b-b7f570ef5a5c', N'dfgdfgfdgdfg', NULL, NULL, CAST(N'2021-12-05T12:09:43.120' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'acd4ca8a-3b04-41e4-b9be-b8380b2b1353', N'adasd', NULL, NULL, CAST(N'2021-12-05T12:11:26.620' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'71404c60-dd6f-47be-8d49-bcb982aa8b37', N'sdadsadgfhfghgfhgfh', NULL, NULL, CAST(N'2021-12-05T12:11:34.583' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'cbd4fd5f-9fa0-491c-b045-c5dcf1e69595', N'fdgfdgfhfghgfhfgwdasdgfnbgna', NULL, NULL, CAST(N'2021-12-05T12:11:22.803' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'ced1a6a0-4954-4cb2-af39-c7bd3380d748', N'fghfghfg', NULL, NULL, CAST(N'2021-12-05T12:11:28.060' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'5ef31886-3c4b-407f-a8ef-ca3b2a64ca41', N'sdadsad', NULL, NULL, CAST(N'2021-12-05T12:11:29.943' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'17c59f15-281d-4b57-a8ff-caae973f988f', N'dsad', NULL, NULL, CAST(N'2021-10-30T00:07:20.410' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'86c7e361-fc09-411a-b7df-d0181a09fb6f', N'dasdasd', NULL, NULL, CAST(N'2021-10-11T23:16:29.923' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'5a2dca94-ecfd-4492-94bb-d1779ae84ca3', N'hghg', NULL, NULL, CAST(N'2021-10-11T22:59:53.710' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'1cf707cf-f614-4f8d-82ef-d4295546ecac', N'fgdsfsdf', NULL, NULL, CAST(N'2021-10-10T20:29:48.260' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'd1ac09ec-b2b9-48f0-934b-d52cd78fde27', N'dasdsa', NULL, NULL, CAST(N'2021-10-10T20:39:01.760' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'383b1bcd-390b-4e2c-a2b9-d560765ea433', N'sadasd', NULL, NULL, CAST(N'2021-10-14T22:36:00.860' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'981a2813-c88c-4708-b6d6-d68757848942', N'hghg', NULL, NULL, CAST(N'2021-10-11T23:02:19.070' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'6bea3baa-829c-4d47-b9b7-d930853bf284', N'fdgfdgdfg', NULL, NULL, CAST(N'2021-10-11T23:11:52.460' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'1e91a69e-6336-44a7-9adb-db022e1a7e7a', N'fgdgfdg', NULL, NULL, CAST(N'2021-10-19T21:34:03.063' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'41204fb6-5e39-4caa-9487-dcf5e8670f1b', N'Ldsa ksdfjskd fdsf. LDA;SDLK. lsadlsdlals;dald ASDLA;DL', NULL, NULL, CAST(N'2021-09-12T23:00:25.650' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7a5dbb3f-0fb4-4dea-abac-e2d790d80f62', N'fsdfsfdsf', NULL, NULL, CAST(N'2021-11-28T12:37:50.893' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'2b960c19-1bef-46a9-8590-e568eae5031d', N'fdgfdgfhfgh', NULL, NULL, CAST(N'2021-12-05T12:11:19.430' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'80e66d97-eeba-4e90-8e38-edd4ea69847c', N'gfdgfdg', NULL, NULL, CAST(N'2021-12-05T12:11:25.640' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'9c5a85c3-b1bc-4f61-a179-efb4b1fdff53', N'DFD', NULL, NULL, CAST(N'2021-10-11T23:09:40.917' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'399b40ad-7082-4d07-a735-f39077e21941', N'gfdgdfgfdg', NULL, NULL, CAST(N'2021-11-28T12:37:53.653' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'43372775-d27c-4fe2-a15f-f576e8463764', N'fsdfs', NULL, NULL, CAST(N'2021-10-11T23:04:03.063' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'7863166a-3a87-4475-8735-fb1e70a0fe5d', N'gfdgdfg', NULL, NULL, CAST(N'2021-10-02T23:17:56.580' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'b4e707b8-d7c9-4e5c-a416-fc100b88a116', N'sdadsadgfhfgh', NULL, NULL, CAST(N'2021-12-05T12:11:33.087' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'523ab624-b571-461d-a996-fd74e182c5ec', N'dasda', NULL, NULL, CAST(N'2021-10-11T22:57:06.717' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'95aed42f-fefe-4c13-b07c-fec176056505', N'sgdfbdvb', NULL, NULL, CAST(N'2021-10-11T23:16:33.497' AS DateTime))
INSERT [Post].[Post] ([PostId], [TextContent], [EditedBy], [EditedAt], [CreatedAt]) VALUES (N'b7f167a1-30c4-4b74-9664-ffebf4e8aab4', N'fdgfdg', NULL, NULL, CAST(N'2021-10-10T20:33:55.460' AS DateTime))
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'ee4963f0-d4ce-4e97-ba4f-082c3017ee45', N'c6ce92e9-b3c8-4a49-956c-530e4dcdb6ad', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'50126786-ee1c-4845-bbcb-096c41041faa', N'63febafa-fa1f-4e77-b906-69b61dd75aa2', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'f8b5a8a7-4b4a-4e2a-9d2a-0b252b7b9d42', N'f73955cc-91e6-4ecb-bd81-1285856c8d56', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'a223f9ee-fd37-4cdd-b3c6-0f43996eaf09', N'e7cedcd2-3486-4ad0-980d-5904236428da', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'f0b38483-ac57-45cc-a0bd-20e60c1ea6f1', N'9c5a85c3-b1bc-4f61-a179-efb4b1fdff53', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'6469d0dc-24c7-48ec-97e1-3ce015d2f316', N'5a2dca94-ecfd-4492-94bb-d1779ae84ca3', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'f14e0788-ea53-4757-be78-4517e34997ce', N'fedff6ca-d408-4691-8f5e-54122ab7199d', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'c9ee6fba-356e-42e7-af34-48625983e7a5', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'32d15dc9-a9ff-4411-aba5-48a100181c44', N'523ab624-b571-461d-a996-fd74e182c5ec', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'1037df6a-5ab9-4a04-be0c-48b0211386f8', N'a51d4962-4047-4fd5-8e33-6dc770433950', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'28a63558-f241-4096-a086-522959b8abe9', N'003753b4-22d6-423a-a231-31daa4afb2a6', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'b2aeb351-6931-4c75-85d0-58aa1cd26e37', N'399054ce-e9fc-49c6-92e9-1f03c4ca0a84', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'9c7b230c-c966-4596-85d1-654fdb2198f5', N'604c9af6-8219-44c7-a8bd-70ccf4c55f0e', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'b9377c38-ef5a-42eb-b8b1-71571ea8e489', N'7e46b1dd-d2ec-4a12-8004-23d0670be225', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'37a496f3-7c80-4da6-92dd-7b411f084e93', N'981a2813-c88c-4708-b6d6-d68757848942', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'167ae0ee-fca8-4e1e-9b2e-8117f2b2cf69', N'd6c3c87e-c00e-4b0d-90bd-9eae0296a085', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'0c4847c5-6d94-48ad-b6a1-81b6ebed826f', N'570f5d1d-b39b-41e5-abbb-ad5c6c2a8d37', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'710f6a7c-19cf-4f1a-974d-82a7779a69b8', N'd1ac09ec-b2b9-48f0-934b-d52cd78fde27', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'86f09c5c-17cc-4859-9775-a6b284ffc993', N'fa8410ad-5d48-46f9-9a9e-58e30ed9e9a8', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'637494fa-d6d8-4da3-bf82-c269a80b097e', N'132eb31c-1fe9-4d25-ae20-b0c417f886da', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'd698bf74-546d-4525-97f8-c4a3eb9ec024', N'b7f167a1-30c4-4b74-9664-ffebf4e8aab4', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'9698e60e-771c-40cd-8776-c900afd8f4eb', N'76351e67-1298-4208-b708-9d4ef000ab4c', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'35de6722-e5ef-4af2-9799-ca7b019e33c7', N'cc6eb0b7-436c-41c5-9cd9-5b81bba8c812', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'a90c7aae-8b70-4173-93d1-d07285916ed6', N'6bea3baa-829c-4d47-b9b7-d930853bf284', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'4209d9a0-9cbd-4ee3-ad9f-d080449b5303', N'0dbb48d3-9b8a-42af-81c5-00aa58aa90dc', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'b6b610db-0cb6-402a-96e9-d2a19839265a', N'18d881ad-0dfe-49e8-a7ab-260aa1353c26', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'6bfe4b74-a7a4-4680-9331-d5827a8d7020', N'86c7e361-fc09-411a-b7df-d0181a09fb6f', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'6c85f9ed-6a15-41b0-9df3-d6eca3e183b3', N'41204fb6-5e39-4caa-9487-dcf5e8670f1b', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'7f194581-09af-4a37-b695-e78bdd3c1bd6', N'17c59f15-281d-4b57-a8ff-caae973f988f', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'11f13bc5-0fa3-4d8c-9ab2-f353ffd7256e', N'95aed42f-fefe-4c13-b07c-fec176056505', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'191b9b4c-ca64-4084-9f07-f6a38b23307f', N'c1380b00-9d09-44d7-a77a-6e7dbf2f6a0b', 6)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'bdef8d57-2520-416f-8481-fdc992a86e49', N'43372775-d27c-4fe2-a15f-f576e8463764', 6)
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
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CommentId]  DEFAULT (newsequentialid()) FOR [CommentId]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_IsRemoved]  DEFAULT ((0)) FOR [IsRemoved]
GO
ALTER TABLE [Post].[GroupPost] ADD  CONSTRAINT [DF_GroupPost_GroupPostId]  DEFAULT (newid()) FOR [GroupPostId]
GO
ALTER TABLE [Post].[GroupPost] ADD  CONSTRAINT [DF_GroupPost_VisibleAsCreatedByAdmin]  DEFAULT ((1)) FOR [VisibleAsCreatedByAdmin]
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
ALTER TABLE [Post].[GroupPost]  WITH CHECK ADD  CONSTRAINT [FK_GroupPost_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[GroupPost] CHECK CONSTRAINT [FK_GroupPost_Post]
GO
ALTER TABLE [Post].[GroupPostComment]  WITH CHECK ADD  CONSTRAINT [FK_GroupPostComment_Comment] FOREIGN KEY([CommentId])
REFERENCES [Post].[Comment] ([CommentId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[GroupPostComment] CHECK CONSTRAINT [FK_GroupPostComment_Comment]
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id grupy, jeśli komentarz został stworzony przez administratora grupy' , @level0type=N'SCHEMA',@level0name=N'Post', @level1type=N'TABLE',@level1name=N'Comment'
GO
USE [master]
GO
ALTER DATABASE [Amiq_Post] SET  READ_WRITE 
GO
