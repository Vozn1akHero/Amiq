USE [Amiq]
GO
/****** Object:  Schema [Chat]    Script Date: 8/27/2021 10:55:25 PM ******/
CREATE SCHEMA [Chat]
GO
/****** Object:  Schema [Core]    Script Date: 8/27/2021 10:55:25 PM ******/
CREATE SCHEMA [Core]
GO
/****** Object:  Schema [Friendship]    Script Date: 8/27/2021 10:55:25 PM ******/
CREATE SCHEMA [Friendship]
GO
/****** Object:  Schema [Group]    Script Date: 8/27/2021 10:55:25 PM ******/
CREATE SCHEMA [Group]
GO
/****** Object:  Schema [Post]    Script Date: 8/27/2021 10:55:25 PM ******/
CREATE SCHEMA [Post]
GO
/****** Object:  Schema [User]    Script Date: 8/27/2021 10:55:25 PM ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Chat].[Chat]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Chat].[Message]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Core].[TextBlock]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Friendship].[Friendship]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Group].[Group]    Script Date: 8/27/2021 10:55:25 PM ******/
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
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Group].[GroupBlockedUsers]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Group].[GroupDescriptionBlock]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Group].[GroupParticipant]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Post].[Comment]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Post].[CommentToComment]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Post].[GroupPost]    Script Date: 8/27/2021 10:55:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Post].[GroupPost](
	[GroupPostId] [uniqueidentifier] NOT NULL,
	[PostId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_GroupPost] PRIMARY KEY CLUSTERED 
(
	[GroupPostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Post].[Post]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [Post].[UserPost]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [User].[BlockedUsers]    Script Date: 8/27/2021 10:55:25 PM ******/
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
/****** Object:  Table [User].[User]    Script Date: 8/27/2021 10:55:25 PM ******/
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
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [User].[UserDescriptionBlock]    Script Date: 8/27/2021 10:55:25 PM ******/
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
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'5d50fc15-adbe-4aef-a7fe-aa694a7d94de', N'Test 2', N'DSADA')
INSERT [Core].[TextBlock] ([TextBlockId], [Header], [Content]) VALUES (N'b80afd29-1385-41aa-b480-fffac9eb228c', N'Test 1', N'DSADdfsghdfgdfg dasda DSFGDSG')
GO
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'e506dc0b-8703-43fb-aa58-436f4129c8f1', 1, 2)
INSERT [Friendship].[Friendship] ([FriendshipId], [FirstUserId], [SecondUserId]) VALUES (N'e1c7579e-e223-4c82-b8d6-a41154678975', 3, 1)
GO
SET IDENTITY_INSERT [Group].[Group] ON 

INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt]) VALUES (1, N'SAD', NULL, N'ghjg sasadfsdf', CAST(N'2021-08-01T22:26:49.717' AS DateTime))
INSERT [Group].[Group] ([GroupId], [Name], [AvatarSrc], [Description], [CreatedAt]) VALUES (2, N'gdf', NULL, NULL, CAST(N'2021-08-01T22:32:07.110' AS DateTime))
SET IDENTITY_INSERT [Group].[Group] OFF
GO
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined]) VALUES (N'de968769-7e26-4412-93c8-6426c26f3b9a', 2, 1, CAST(N'2021-08-01T22:51:10.947' AS DateTime))
INSERT [Group].[GroupParticipant] ([GroupParticipantId], [GroupId], [UserId], [Joined]) VALUES (N'30be09a2-493d-4fb0-b97b-67a39a6e0f8b', 1, 1, CAST(N'2021-08-01T22:48:14.897' AS DateTime))
GO
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId]) VALUES (N'dd2e1b96-7ec0-4f38-9bca-26d02eb3cd0b', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 1)
INSERT [Post].[GroupPost] ([GroupPostId], [PostId], [GroupId]) VALUES (N'71bc1fee-3f4d-48e6-af0b-a4535aa42b7f', N'a323d4e0-d734-462a-836f-4e0ea939d8a8', 1)
GO
INSERT [Post].[Post] ([PostId], [TextContent]) VALUES (N'a01ed70f-41f5-4518-b918-07fa333559cd', N'jhyityurtyurthrfhrfher')
INSERT [Post].[Post] ([PostId], [TextContent]) VALUES (N'3b6d8f3e-a25a-4a4f-9bab-15caf28b0b71', N'gjhfj')
INSERT [Post].[Post] ([PostId], [TextContent]) VALUES (N'a323d4e0-d734-462a-836f-4e0ea939d8a8', N'gfhfghfgh')
INSERT [Post].[Post] ([PostId], [TextContent]) VALUES (N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', N'sdfsfdsfsdfsd')
GO
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'5a402472-d7ea-4d1e-9903-26e98601d291', N'a323d4e0-d734-462a-836f-4e0ea939d8a8', 1)
INSERT [Post].[UserPost] ([UserPostId], [PostId], [UserId]) VALUES (N'c9ee6fba-356e-42e7-af34-48625983e7a5', N'09b5c6be-f8bc-4b65-82ba-9c4b803aa8bd', 1)
GO
SET IDENTITY_INSERT [User].[User] ON 

INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath]) VALUES (1, N'chlenix', N'Dimitr', N'Ruski', N'111', CAST(N'1999-04-10' AS Date), N'M', N'd1@gmw.com', NULL)
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath]) VALUES (2, N'derek', N'Derek', N'Jankowski', N'333', CAST(N'1998-04-11' AS Date), N'M', N'gdfg.dsa2@gmw.com', NULL)
INSERT [User].[User] ([UserId], [Login], [Name], [Surname], [Password], [Birthdate], [Sex], [Email], [AvatarPath]) VALUES (3, N'karina', N'Katarzyna', N'Brovlowska', N'888', CAST(N'1997-01-02' AS Date), N'F', N'bdsad@gmw.com', NULL)
SET IDENTITY_INSERT [User].[User] OFF
GO
INSERT [User].[UserDescriptionBlock] ([UserDescriptionBlockId], [UserId], [TextBlockId]) VALUES (N'cd8ce9f9-b22a-4d41-a725-3f2ab9cb2638', 1, N'5d50fc15-adbe-4aef-a7fe-aa694a7d94de')
INSERT [User].[UserDescriptionBlock] ([UserDescriptionBlockId], [UserId], [TextBlockId]) VALUES (N'41a26e72-2d80-4e3c-bf95-4df0a916e917', 1, N'b80afd29-1385-41aa-b480-fffac9eb228c')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_UserLogin]    Script Date: 8/27/2021 10:55:25 PM ******/
ALTER TABLE [User].[User] ADD  CONSTRAINT [UC_UserLogin] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [Chat].[Chat] ADD  CONSTRAINT [DF_Chat_ChatId]  DEFAULT (newid()) FOR [ChatId]
GO
ALTER TABLE [Chat].[Message] ADD  CONSTRAINT [DF_Message_MessageId]  DEFAULT (newid()) FOR [MessageId]
GO
ALTER TABLE [Chat].[Message] ADD  CONSTRAINT [DF_Message_Date]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Core].[TextBlock] ADD  CONSTRAINT [DF_TextBlock_TextBlockId]  DEFAULT (newid()) FOR [TextBlockId]
GO
ALTER TABLE [Friendship].[Friendship] ADD  CONSTRAINT [DF_Friendship_FriendshipId]  DEFAULT (newid()) FOR [FriendshipId]
GO
ALTER TABLE [Group].[Group] ADD  CONSTRAINT [DF_Group_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_GroupBlockedUserId]  DEFAULT (newid()) FOR [GroupBlockedUserId]
GO
ALTER TABLE [Group].[GroupBlockedUsers] ADD  CONSTRAINT [DF_GroupBlockedUsers_BanDate]  DEFAULT (getdate()) FOR [BanDate]
GO
ALTER TABLE [Group].[GroupDescriptionBlock] ADD  CONSTRAINT [DF_GroupDescriptionBlock_GroupDescriptionBlockId]  DEFAULT (newid()) FOR [GroupDescriptionBlockId]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_GroupParticipantId]  DEFAULT (newid()) FOR [GroupParticipantId]
GO
ALTER TABLE [Group].[GroupParticipant] ADD  CONSTRAINT [DF_GroupParticipant_Joined]  DEFAULT (getdate()) FOR [Joined]
GO
ALTER TABLE [Post].[Comment] ADD  CONSTRAINT [DF_Comment_CommentId]  DEFAULT (newid()) FOR [CommentId]
GO
ALTER TABLE [Post].[CommentToComment] ADD  CONSTRAINT [DF_CommentToComment_CommentToCommentId]  DEFAULT (newid()) FOR [CommentToCommentId]
GO
ALTER TABLE [Post].[GroupPost] ADD  CONSTRAINT [DF_GroupPost_GroupPostId]  DEFAULT (newid()) FOR [GroupPostId]
GO
ALTER TABLE [Post].[Post] ADD  DEFAULT (newid()) FOR [PostId]
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
ALTER TABLE [Post].[GroupPost]  WITH CHECK ADD  CONSTRAINT [FK_GroupPost_Group] FOREIGN KEY([GroupId])
REFERENCES [Group].[Group] ([GroupId])
ON DELETE CASCADE
GO
ALTER TABLE [Post].[GroupPost] CHECK CONSTRAINT [FK_GroupPost_Group]
GO
ALTER TABLE [Post].[GroupPost]  WITH CHECK ADD  CONSTRAINT [FK_GroupPost_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
GO
ALTER TABLE [Post].[GroupPost] CHECK CONSTRAINT [FK_GroupPost_Post]
GO
ALTER TABLE [Post].[UserPost]  WITH CHECK ADD  CONSTRAINT [FK_UserPost_Post] FOREIGN KEY([PostId])
REFERENCES [Post].[Post] ([PostId])
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
