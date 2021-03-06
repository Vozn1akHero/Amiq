USE [master]
GO
/****** Object:  Database [Amiq]    Script Date: 30.07.2021 18:15:11 ******/
CREATE DATABASE [Amiq]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Amiq', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Amiq.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Amiq_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Amiq_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [Amiq] SET QUERY_STORE = OFF
GO
USE [Amiq]
GO
/****** Object:  Schema [Chat]    Script Date: 30.07.2021 18:15:11 ******/
CREATE SCHEMA [Chat]
GO
/****** Object:  Schema [Core]    Script Date: 30.07.2021 18:15:11 ******/
CREATE SCHEMA [Core]
GO
/****** Object:  Schema [Friendship]    Script Date: 30.07.2021 18:15:11 ******/
CREATE SCHEMA [Friendship]
GO
/****** Object:  Schema [Group]    Script Date: 30.07.2021 18:15:11 ******/
CREATE SCHEMA [Group]
GO
/****** Object:  Schema [Post]    Script Date: 30.07.2021 18:15:11 ******/
CREATE SCHEMA [Post]
GO
/****** Object:  Schema [User]    Script Date: 30.07.2021 18:15:11 ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Chat].[Chat]    Script Date: 30.07.2021 18:15:11 ******/
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
/****** Object:  Table [Chat].[Message]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Chat].[Message](
	[MessageId] [uniqueidentifier] NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[TextContent] [nvarchar](250) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EFTest]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EFTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
 CONSTRAINT [PK_EFTest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Friendship].[Friendship]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Friendship].[Friendship](
	[FriendshipId] [uniqueidentifier] NOT NULL,
	[FirstUserId] [int] NOT NULL,
	[SecondUserId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[Group]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[Group](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupBlockedUsers]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[GroupBlockedUsers](
	[GroupBlockedUserId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[UserInt] [nchar](10) NOT NULL,
	[BanDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupBlockedUsers] PRIMARY KEY CLUSTERED 
(
	[GroupBlockedUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupParticipant]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Group].[GroupParticipant](
	[GroupParticipantId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Joined] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupParticipant] PRIMARY KEY CLUSTERED 
(
	[GroupParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[Comment]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[Comment](
	[CommentId] [uniqueidentifier] NOT NULL,
	[TextContent] [nvarchar](250) NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[CommentToComment]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[CommentToComment](
	[CommentToCommentId] [uniqueidentifier] NOT NULL,
	[ParentCommentId] [uniqueidentifier] NOT NULL,
	[ChildCommentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CommentToComment] PRIMARY KEY CLUSTERED 
(
	[CommentToCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[GroupPost]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[GroupPost](
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_GroupPost] PRIMARY KEY CLUSTERED 
(
	[GroupPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[Post]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[Post](
	[PostId] [uniqueidentifier] NOT NULL,
	[TextContent] [nvarchar](500) NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[UserPost]    Script Date: 30.07.2021 18:15:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[UserPost](
	[UserPostId] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserPost] PRIMARY KEY CLUSTERED 
(
	[UserPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [User].[BlockedUsers]    Script Date: 30.07.2021 18:15:11 ******/
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
/****** Object:  Table [User].[User]    Script Date: 30.07.2021 18:15:11 ******/
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
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EFTest] ON 

INSERT [dbo].[EFTest] ([Id], [Title]) VALUES (1, N'dsad')
INSERT [dbo].[EFTest] ([Id], [Title]) VALUES (2, N'hgfh')
INSERT [dbo].[EFTest] ([Id], [Title]) VALUES (3, N'fdgd')
SET IDENTITY_INSERT [dbo].[EFTest] OFF
GO
SET IDENTITY_INSERT [User].[User] ON 

INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex]) VALUES (1, N'chlenix', N'Dimitr', N'Ruski', N'111', CAST(N'1999-04-10' AS Date), N'M')
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex]) VALUES (2, N'derek', N'Derek', N'Jankowski', N'333', CAST(N'1998-04-11' AS Date), N'M')
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex]) VALUES (3, N'karina', N'Katarzyna', N'Brovlowska', N'888', CAST(N'1997-01-02' AS Date), N'F')
SET IDENTITY_INSERT [User].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_UserLogin]    Script Date: 30.07.2021 18:15:11 ******/
ALTER TABLE [User].[User] ADD  CONSTRAINT [UC_UserLogin] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [Chat].[Chat] ADD  CONSTRAINT [DF_Chat_ChatId]  DEFAULT (newid()) FOR [ChatId]
GO
ALTER TABLE [Chat].[Message] ADD  CONSTRAINT [DF_Message_MessageId]  DEFAULT (newid()) FOR [MessageId]
GO
ALTER TABLE [Chat].[Message] ADD  CONSTRAINT [DF_Message_Date]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Friendship].[Friendship] ADD  CONSTRAINT [DF_Friendship_FriendshipId]  DEFAULT (newid()) FOR [FriendshipId]
GO
ALTER TABLE [Group].[Group] ADD  CONSTRAINT [DF_Group_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_GroupBlockedUserId]  DEFAULT (newid()) FOR [GroupBlockedUserId]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_BanDate]  DEFAULT (getdate()) FOR [BanDate]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_GroupParticipantId]  DEFAULT (newid()) FOR [GroupParticipantId]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CommentId]  DEFAULT (newid()) FOR [CommentId]
GO
ALTER TABLE [Post].[CommentToComment] ADD  CONSTRAINT [DF_CommentToComment_CommentToCommentId]  DEFAULT (newid()) FOR [CommentToCommentId]
GO
ALTER TABLE [Post].[Post] ADD  DEFAULT (newid()) FOR [PostId]
GO
ALTER TABLE [Post].[UserPost] ADD  CONSTRAINT [DF_UserPost_PostId]  DEFAULT (newid()) FOR [PostId]
GO
ALTER TABLE [User].[BlockedUsers] ADD  CONSTRAINT [DF_BlockedUsers_BlockedUsersId]  DEFAULT (newid()) FOR [BlockedUsersId]
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
ALTER TABLE [Group].[GroupParticipant]  WITH CHECK ADD  CONSTRAINT [FK_GroupParticipant_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
GO
ALTER TABLE [Group].[GroupParticipant] CHECK CONSTRAINT [FK_GroupParticipant_Group]
GO
ALTER TABLE [Group].[GroupParticipant]  WITH CHECK ADD  CONSTRAINT [FK_GroupParticipant_User] FOREIGN KEY([UserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Group].[GroupParticipant] CHECK CONSTRAINT [FK_GroupParticipant_User]
GO
ALTER TABLE [Post].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
GO
ALTER TABLE [Post].[Comment] CHECK CONSTRAINT [FK_Comment_Post]
GO
ALTER TABLE [Post].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([AuthorId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Post].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [Post].[CommentToComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentToComment_ChildComment] FOREIGN KEY([ChildCommentId])
REFERENCES [Post].[Comment] ([CommentId])
GO
ALTER TABLE [Post].[CommentToComment] CHECK CONSTRAINT [FK_CommentToComment_ChildComment]
GO
ALTER TABLE [Post].[CommentToComment]  WITH CHECK ADD  CONSTRAINT [FK_CommentToComment_ParentComment] FOREIGN KEY([ParentCommentId])
REFERENCES [Post].[CommentToComment] ([CommentToCommentId])
GO
ALTER TABLE [Post].[CommentToComment] CHECK CONSTRAINT [FK_CommentToComment_ParentComment]
GO
ALTER TABLE [Post].[GroupPost]  WITH CHECK ADD  CONSTRAINT [FK_GroupPost_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
GO
ALTER TABLE [Post].[GroupPost] CHECK CONSTRAINT [FK_GroupPost_Post]
GO
ALTER TABLE [Post].[UserPost]  WITH CHECK ADD  CONSTRAINT [FK_UserPost_Post] FOREIGN KEY([UserPostId])
REFERENCES [Post].[Post] ([PostId])
GO
ALTER TABLE [Post].[UserPost] CHECK CONSTRAINT [FK_UserPost_Post]
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
USE [master]
GO
ALTER DATABASE [Amiq] SET  READ_WRITE 
GO
