USE [master]
GO
/****** Object:  Database [Amiq_Friendship]    Script Date: 1/6/2022 5:05:45 PM ******/
CREATE DATABASE [Amiq_Friendship]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Amiq_Friendship', FILENAME = N'/var/opt/mssql/data/Amiq_Friendship.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Amiq_Friendship_log', FILENAME = N'/var/opt/mssql/data/Amiq_Friendship_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Amiq_Friendship] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Amiq_Friendship].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Amiq_Friendship] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET ARITHABORT OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Amiq_Friendship] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Amiq_Friendship] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Amiq_Friendship] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Amiq_Friendship] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET RECOVERY FULL 
GO
ALTER DATABASE [Amiq_Friendship] SET  MULTI_USER 
GO
ALTER DATABASE [Amiq_Friendship] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Amiq_Friendship] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Amiq_Friendship] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Amiq_Friendship] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Amiq_Friendship] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Amiq_Friendship] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Amiq_Friendship', N'ON'
GO
ALTER DATABASE [Amiq_Friendship] SET QUERY_STORE = OFF
GO
USE [Amiq_Friendship]
GO
/****** Object:  Schema [Friendship]    Script Date: 1/6/2022 5:05:45 PM ******/
CREATE SCHEMA [Friendship]
GO
/****** Object:  Schema [User]    Script Date: 1/6/2022 5:05:45 PM ******/
CREATE SCHEMA [User]
GO
/****** Object:  Table [Friendship].[FriendRequest]    Script Date: 1/6/2022 5:05:45 PM ******/
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
/****** Object:  Table [Friendship].[Friendship]    Script Date: 1/6/2022 5:05:45 PM ******/
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
/****** Object:  Table [User].[User]    Script Date: 1/6/2022 5:05:45 PM ******/
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
ALTER TABLE [Friendship].[FriendRequest] ADD  CONSTRAINT [DF_FriendRequest_FriendRequestId]  DEFAULT (newid()) FOR [FriendRequestId]
GO
ALTER TABLE [Friendship].[FriendRequest] ADD  CONSTRAINT [DF_FriendRequest_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [Friendship].[Friendship] ADD  CONSTRAINT [DF_Friendship_FriendshipId]  DEFAULT (newid()) FOR [FriendshipId]
GO
ALTER TABLE [Friendship].[FriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_FriendRequest_Issuer] FOREIGN KEY([IssuerId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[FriendRequest] CHECK CONSTRAINT [FK_FriendRequest_Issuer]
GO
ALTER TABLE [Friendship].[FriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_FriendRequest_Receiver] FOREIGN KEY([ReceiverId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[FriendRequest] CHECK CONSTRAINT [FK_FriendRequest_Receiver]
GO
ALTER TABLE [Friendship].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK_Friendship_User] FOREIGN KEY([FirstUserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[Friendship] CHECK CONSTRAINT [FK_Friendship_User]
GO
ALTER TABLE [Friendship].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK_Friendship_User1] FOREIGN KEY([SecondUserId])
REFERENCES [User].[User] ([UserId])
GO
ALTER TABLE [Friendship].[Friendship] CHECK CONSTRAINT [FK_Friendship_User1]
GO
ALTER TABLE [Friendship].[Friendship]  WITH CHECK ADD  CONSTRAINT [CHK_Friendship_UserIds] CHECK  (([SecondUserId]>[FirstUserId]))
GO
ALTER TABLE [Friendship].[Friendship] CHECK CONSTRAINT [CHK_Friendship_UserIds]
GO
USE [master]
GO
ALTER DATABASE [Amiq_Friendship] SET  READ_WRITE 
GO
