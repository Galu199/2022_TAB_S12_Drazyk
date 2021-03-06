USE [master]
GO
/****** Object:  Database [TAB_DataBase]    Script Date: 27.05.2022 18:53:33 ******/
CREATE DATABASE [TAB_DataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TAB_DataBase', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TAB_DataBase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TAB_DataBase_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TAB_DataBase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TAB_DataBase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TAB_DataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TAB_DataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TAB_DataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TAB_DataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TAB_DataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TAB_DataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [TAB_DataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TAB_DataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TAB_DataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TAB_DataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TAB_DataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TAB_DataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TAB_DataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TAB_DataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TAB_DataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TAB_DataBase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TAB_DataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TAB_DataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TAB_DataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TAB_DataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TAB_DataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TAB_DataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TAB_DataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TAB_DataBase] SET RECOVERY FULL 
GO
ALTER DATABASE [TAB_DataBase] SET  MULTI_USER 
GO
ALTER DATABASE [TAB_DataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TAB_DataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TAB_DataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TAB_DataBase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TAB_DataBase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TAB_DataBase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TAB_DataBase', N'ON'
GO
ALTER DATABASE [TAB_DataBase] SET QUERY_STORE = OFF
GO
USE [TAB_DataBase]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [text] NOT NULL,
	[DATE] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Users_Id] [varchar](450) NULL,
	[Posts_Id] [int] NULL,
	[ModedBy] [varchar](450) NULL,
 CONSTRAINT [Comments_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [text] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Users_Id] [varchar](450) NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [text] NOT NULL,
	[Text] [text] NOT NULL,
	[Rating] [int] NOT NULL,
	[DATE] [datetime] NOT NULL,
	[Pinned] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Users_Id] [varchar](450) NULL,
	[ModedBy] [varchar](450) NULL,
 CONSTRAINT [Posts_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
	[Users_Id] [varchar](450) NOT NULL,
	[Posts_Id] [int] NOT NULL,
	[value] [int] NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[Users_Id] ASC,
	[Posts_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [text] NOT NULL,
	[Posts_Id] [int] NULL,
 CONSTRAINT [Tags_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 27.05.2022 18:53:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [varchar](450) NOT NULL,
	[Email] [text] NOT NULL,
	[UserName] [text] NOT NULL,
	[PhoneNumber] [text] NOT NULL,
	[Ban] [bit] NOT NULL,
	[ModedBy] [varchar](450) NULL,
 CONSTRAINT [Users_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'5.0.16')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220509170357_updateIdentity', N'5.0.16')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'15e525e3-8c56-4b9f-9b0a-f43d1817e438', N'Moderator', N'MODERATOR', N'ac6a9b52-c715-4093-9a55-24e050e9c017')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'e8627bcf-73f3-4f0e-83f4-8c14d26f394c', N'Administrator', N'ADMINISTRATOR', N'62bb96a6-c6ae-41b2-bf41-78e3ba7695b4')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ebed1012-67f3-4a5d-a664-1778ab49ca16', N'User', N'USER', N'bf7954ef-0e68-4dd6-9996-7727376990b8')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', N'15e525e3-8c56-4b9f-9b0a-f43d1817e438')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'88ae4962-10ec-4651-b077-39d4a0791001', N'e8627bcf-73f3-4f0e-83f4-8c14d26f394c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'55ea261d-e941-4734-8dd5-33cb703142d5', N'ebed1012-67f3-4a5d-a664-1778ab49ca16')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5ccab313-0e01-4c16-8373-5fff23d7adf6', N'ebed1012-67f3-4a5d-a664-1778ab49ca16')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9954b7f7-bc1c-4090-8a8a-cc3d38fa3583', N'ebed1012-67f3-4a5d-a664-1778ab49ca16')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', N'ebed1012-67f3-4a5d-a664-1778ab49ca16')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'10ee88e4-d3ab-4637-bb4a-9a0a8e877ae6', N'test12@g.l', N'TEST12@G.L', N'test12@g.l', N'TEST12@G.L', 0, N'AQAAAAEAACcQAAAAEJQKpTWHfS+v+Zcm+QbBW8pBev8P4FPHbC9zbNzrp4TghZ1Xt8aiZ/gdtC2hAxAmRw==', N'OKXQJ27SYUN4ABR6ZONJN7G55SS6MNMP', N'2a056bf3-4665-410e-bf4a-7c9893a722ed', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'12409729-57d5-4bf7-9c5a-1630fd85a5c6', N'a@a.pl', N'A@A.PL', N'a@a.pl', N'A@A.PL', 1, N'AQAAAAEAACcQAAAAECcJLC6e0hBmb770irptJXcoI+4GqwaUF/xqsSygPjgW3nquz6pZr/PjliMsr1txKQ==', N'JUVZKRQ5JKO4S4DMCTDO5TAOZX24MFVJ', N'40e50fc8-86b4-485d-9e7c-fa2fa7745f25', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'184c44d5-6c48-4d42-adf0-a732e65a6589', N'test2@gmail.com', N'TEST2@GMAIL.COM', N'test2@gmail.com', N'TEST2@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEFVVmbVGvfu67b1VK6TJKvaQ1A+Rg5PC7JB3GNQGMISSA63tfuMxAz90JRFRc4KGxg==', N'DTC5LM6NKSHLENM3LNJ2O7IVGKOIF6ZG', N'aa45ea52-3deb-4c83-af14-c5864dab2d1b', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1f364ba1-b287-4af8-98ff-50992d2b845b', N'test9@g.l', N'TEST9@G.L', N'test9@g.l', N'TEST9@G.L', 0, N'AQAAAAEAACcQAAAAECXCAosx+8rlqhA57ZbuncBFHBmY1BfCqeeGN/Wk6TRB6hRb5tcDwEzaOSpgyydspQ==', N'6YW5HYM4UIQYNDCBJRQ6LL724RV6IAIJ', N'6c955771-f4a8-4b90-bd27-e29026540d8a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'55ea261d-e941-4734-8dd5-33cb703142d5', N'test14@gmail.com', N'TEST14@GMAIL.COM', N'test14@gmail.com', N'TEST14@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAELtEVLAVV/0pB7Qdr0Y79W6Tulk1GDPjGlCuNEWimf0LEX6nOeWHD4Lop2NG/AAokQ==', N'A4ELCRTSDIZQX2WQMW2O52RKQXLXNHAT', N'861d3a73-5eb1-4227-87cc-96b357d29767', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5ccab313-0e01-4c16-8373-5fff23d7adf6', N'test0@gmail.com', N'TEST0@GMAIL.COM', N'test0@gmail.com', N'TEST0@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEAyy93rec2HxN1iaTE+Uv5JRBv+m9sLrAh6g3VgvihTRQJJeu1A76NUAUP+X0e+ROA==', N'UMEW3XCCOD7WMMOEDELKPSNT4OJLWBRZ', N'a0543ff1-55e3-42fb-b839-bcc832f2e70c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8378f74c-3336-43e3-b202-8b150848f238', N'test6@g.l', N'TEST6@G.L', N'test6@g.l', N'TEST6@G.L', 0, N'AQAAAAEAACcQAAAAEGtnpecUdCNUmpApJxiXbOrM97CB9IQbaQvDShtZpYd/qeB64WAGXFYEh5hJz/+0TA==', N'E73JB76NXFJBHRNUHXRXY64WG6JSHZY3', N'920fe8cb-b68e-4991-b12b-a026b2792e67', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'854681eb-0b2d-4742-9991-4e6673eb463b', N'test8@g.l', N'TEST8@G.L', N'test8@g.l', N'TEST8@G.L', 0, N'AQAAAAEAACcQAAAAEEYWvAEuZ4w+FFRqnuz8xScPN6LaxC8nRLz/ZPge/R74J2Eu99qtIGIQbYJgy9i56w==', N'FHD56G4I56VTCUMEPEDACDCVSG6YIGMD', N'bfcdd38d-06cd-44aa-b2e3-ff9770aa91d5', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'88ae4962-10ec-4651-b077-39d4a0791001', N'test13@gmail.com', N'TEST13@GMAIL.COM', N'test13@gmail.com', N'TEST13@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEGzHjbwrHeVQUP7jGC/9arm9QsAEWZUsEujC/UK1Cgr/QI9tNtVCqf/shur+r3tDcA==', N'LTIL73AHNAU5AJP53II45ZK4BVO64OJU', N'3d77d9c0-7572-4c20-990e-dd12398aec19', N'123 123 123', 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'900fc736-c6b7-40df-851e-8671a162a408', N'test1@gmail.com', N'TEST1@GMAIL.COM', N'test1@gmail.com', N'TEST1@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAECxSqRyik7khOMNMoZxXnh4zINHc4ZAwjYq2zPbWoL1h64PQSkEuKNasDtFMh75AaQ==', N'OJSHBSEEHSCTU3XRR5HD7JHPMGBXPQRZ', N'd99b9b4c-2a81-44bc-9990-7f908f21ca9d', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9954b7f7-bc1c-4090-8a8a-cc3d38fa3583', N'test15@gmail.com', N'TEST15@GMAIL.COM', N'test15@gmail.com', N'TEST15@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAECv7JLcFshSy7kybVGyl3CZWrIwv0rdrEPH+Hb4Bd/YL/vxdGrGRzg2sAbcY25/41g==', N'ZWLVKSLRKMYWNJDPUCC5JXHM42H7QTEG', N'602545bf-4431-4736-a123-9e7d405ef79e', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', N'Admin@2137.pl', N'ADMIN@2137.PL', N'Admin@2137.pl', N'ADMIN@2137.PL', 1, N'AQAAAAEAACcQAAAAEGblO8p0ZHhzrJzaFXvvObtR195dETgeNaDxAqP38ZphLCQsBTFr3D9AIDtDgM03vw==', N'OPRRUGCHSQXSJ3GFAMJATCEDWJ3X5JVV', N'7984f78c-53d4-401a-9ba3-210d6c6d2d4a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c746b22d-33fe-4a23-ab10-0931925477be', N'test4@gmail.com', N'TEST4@GMAIL.COM', N'test4@gmail.com', N'TEST4@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEFkUKZjJMDqLeJWRUPSDreh2nCgoPvdA0RQ4d+rMM3GavQo1lBSB2OMTg5RinuST/g==', N'Z7UZOY3RZSES7SWKBDABNRSWED3FFXCX', N'1b2f1007-dd27-41b7-97fa-32f09103955e', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c9251b41-615f-4d72-ac75-804c9c4648ec', N'test10@g.l', N'TEST10@G.L', N'test10@g.l', N'TEST10@G.L', 0, N'AQAAAAEAACcQAAAAENCsQ+Twb2FZriAXY90WgGaRqyq/VT5pagYVq2HLNYW6H+HC1VlRb0ZkD8GVubGrJA==', N'QW5DF65VHAEGTF6LKZ75FXMISLSYJUFG', N'fbaf048e-dc77-4bfc-8a2a-a688f0c1c643', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f9956b29-5f02-415a-a773-cd0dfc0f0e1a', N'test5@g.l', N'TEST5@G.L', N'test5@g.l', N'TEST5@G.L', 0, N'AQAAAAEAACcQAAAAEHsemo37CzP+G/2jAJQzBRX4fCEkwGq8DQvHChhlua0gJl9Lhu4jsptIFszTk/8Rdg==', N'C7Q66JD63EVETGY2BFQD2XMNTB5NWGFK', N'a0cb3f66-8b28-4009-98cf-7ac5ac904c28', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fbc3e470-91c3-4f85-a24b-bf96f74be618', N'test11@g.l', N'TEST11@G.L', N'test11@g.l', N'TEST11@G.L', 0, N'AQAAAAEAACcQAAAAEM48L9iY4eF/DKNcMcM/DDU0hq70ivw7qlZW11Xetkw62CA/lu7Z7I3iyul7SEuO8A==', N'A5CSLSAOE5ZT3RWWCYEDL3X7JGAX7RFO', N'531687d9-a93e-4c7b-a0c2-be38a477e1d8', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fc8d5538-f73e-4201-827a-6f544085f6b5', N'test7@g.l', N'TEST7@G.L', N'test7@g.l', N'TEST7@G.L', 0, N'AQAAAAEAACcQAAAAENQWVGog229o4bzzVq8tLvOeGjo1+rr5fboxyfPnPWZdanG+rKKW04cflBJPOw3Bug==', N'WPZVUYYMMV3JVSSV27CROZIG6CYTWJVE', N'2932fb2c-0bd7-4d06-bc58-afeaf9011c76', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fe03dad5-08d3-46f7-8bf7-7e41421a7f1d', N'test3@gmail.com', N'TEST3@GMAIL.COM', N'test3@gmail.com', N'TEST3@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEGlqLBlQRPv8zAozbrizuaMJmBxQXcifHuSSWRqmoxsH9VoDpOOXY/OoqThjFQYnFQ==', N'RBPF7LHCSL65DEDEVSQPQNU3Z7GNJIZW', N'131a2281-85c2-4492-bd0f-e24601f4ae9c', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (1, N'a', CAST(N'2022-01-01T00:00:00.000' AS DateTime), 1, N'a', 2, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (3, N'komentarz', CAST(N'2022-02-01T00:00:00.000' AS DateTime), 1, N'a', 2, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (4, N'test', CAST(N'2022-03-01T00:00:00.000' AS DateTime), 1, N'a', 2, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (5, N'LOREM IMPSUM', CAST(N'2022-05-19T16:06:39.277' AS DateTime), 0, NULL, NULL, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (7, N'LOREM IMPSUM', CAST(N'2022-05-19T16:06:59.877' AS DateTime), 0, NULL, NULL, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (9, N'LOREM IMPSUM', CAST(N'2022-05-19T16:09:11.453' AS DateTime), 0, NULL, NULL, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (10, N'LOREM IMPSUM', CAST(N'2022-05-19T16:14:01.163' AS DateTime), 0, NULL, NULL, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (11, N'test', CAST(N'2022-05-19T16:17:35.053' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (12, N'test 2', CAST(N'2022-05-19T16:18:40.410' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (13, N'LOREM IMPSUM', CAST(N'2022-05-21T12:25:45.630' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (15, N'test', CAST(N'2022-05-21T13:28:00.010' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 2, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (16, N'asd', CAST(N'2022-05-22T17:01:33.433' AS DateTime), 0, N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', 1013, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (17, N'HEHE', CAST(N'2022-05-24T17:21:05.510' AS DateTime), 0, N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (18, N'asdasd', CAST(N'2022-05-24T17:26:09.197' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (19, N'test comment edit1', CAST(N'2022-05-26T11:34:53.223' AS DateTime), 1, N'88ae4962-10ec-4651-b077-39d4a0791001', 1013, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (20, N'bardzo fajne', CAST(N'2022-05-26T13:43:27.750' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (21, N'przyda mi się <3', CAST(N'2022-05-26T13:43:51.887' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (22, N'a', CAST(N'2022-05-26T16:18:31.727' AS DateTime), 0, N'88ae4962-10ec-4651-b077-39d4a0791001', 12, NULL)
INSERT [dbo].[Comments] ([Id], [Text], [DATE], [Deleted], [Users_Id], [Posts_Id], [ModedBy]) VALUES (23, N'GO BREAK THE TOS!', CAST(N'2022-05-27T18:28:40.307' AS DateTime), 1, N'88ae4962-10ec-4651-b077-39d4a0791001', 8, N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0')
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([Id], [Text], [Date], [Users_Id]) VALUES (1, N'TEST', CAST(N'2022-01-01T00:00:00.000' AS DateTime), N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Notifications] ([Id], [Text], [Date], [Users_Id]) VALUES (6, N'User: <a href=''/User/Details/88ae4962-10ec-4651-b077-39d4a0791001''>test13@gmail.com</a> reported Comment: <a href=''/Comment/Delete/23''>GO BREAK THE TOS!</a> Please check it out.', CAST(N'2022-05-27T18:36:18.223' AS DateTime), N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0')
INSERT [dbo].[Notifications] ([Id], [Text], [Date], [Users_Id]) VALUES (7, N'User: <a href=''/User/Details/88ae4962-10ec-4651-b077-39d4a0791001''>Administrator Mojo jojo</a> reported Post: <a href=''/Post/Details/8''>b2</a> Please check it out.', CAST(N'2022-05-27T18:47:09.393' AS DateTime), N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0')
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (2, N'a1 EDITED', N'TEST POST AAA', 0, CAST(N'2022-01-01T00:00:00.000' AS DateTime), 0, 1, N'a', N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (4, N'b1', N'b', 0, CAST(N'2022-01-01T01:01:00.000' AS DateTime), 0, 1, N'b', N'a')
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (7, N'a2', N'a', 0, CAST(N'2022-01-03T00:00:00.000' AS DateTime), 0, 1, N'a', N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (8, N'b2', N'b', 0, CAST(N'2022-01-04T00:00:00.000' AS DateTime), 0, 1, N'b', N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0')
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (9, N'test1', N'jakis tekst', 0, CAST(N'2022-01-01T00:00:00.000' AS DateTime), 0, 1, N'a', N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (10, N'Test1', N'jakis tekst testowy 1', 0, CAST(N'2022-05-12T19:35:17.437' AS DateTime), 0, 0, N'88ae4962-10ec-4651-b077-39d4a0791001', NULL)
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (11, N'test 2', N'bardzo fajny text testowy, można sobie coś pisać może polskie znaki będą działać ?', 0, CAST(N'2022-05-12T19:38:41.827' AS DateTime), 0, 0, N'88ae4962-10ec-4651-b077-39d4a0791001', NULL)
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (12, N'asdasd', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec congue massa. Aenean dictum ultrices vehicula. Pellentesque eget sagittis ex, gravida euismod ipsum. Suspendisse potenti. Donec sed odio lorem. Cras nec vestibulum nisi. Vivamus in metus ullamcorper, aliquet orci eu, tempus risus.  Aenean quam ex, rhoncus et est imperdiet, ornare cursus metus. Etiam consectetur vestibulum metus eget vulputate. Nam id lacus eget massa volutpat malesuada. Quisque id purus pharetra, scelerisque nisi a, consequat odio. Curabitur arcu tellus, venenatis et molestie eget, molestie sed lorem. Proin maximus magna ac metus consequat porta. Suspendisse eget egestas nunc. Sed vel posuere ipsum. Quisque lacinia purus quis rhoncus mollis.', 0, CAST(N'2022-05-12T20:41:58.563' AS DateTime), 1, 0, N'88ae4962-10ec-4651-b077-39d4a0791001', NULL)
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (1012, N'Nowe tesowe', N'asa', 0, CAST(N'2022-05-18T18:37:26.660' AS DateTime), 0, 0, N'88ae4962-10ec-4651-b077-39d4a0791001', NULL)
INSERT [dbo].[Posts] ([Id], [Title], [Text], [Rating], [DATE], [Pinned], [Deleted], [Users_Id], [ModedBy]) VALUES (1013, N'fajny post', N'SKŁADNIKI  10 SZTUK, PO 108 KCAL 1 szklanka mąki pszennej 2 jajka 1 szklanka mleka 3/4 szklanki wody (najlepiej gazowanej) szczypta soli 3 łyżki masła lub oleju roślinnego DODAJ NOTATKĘ PRZYGOTOWANIE Mąkę wsypać do miski, dodać jajka, mleko, wodę i sól. Zmiksować na gładkie ciasto. Dodać roztopione masło lub olej roślinny i razem zmiksować (lub wykorzystać tłuszcz do smarowania patelni przed smażeniem każdego naleśnika). Naleśniki smażyć na dobrze rozgrzanej patelni z cienkim dnem np. naleśnikowej. Przewrócić na drugą stronę gdy spód naleśnika będzie już ładnie zrumieniony i ścięty. WSKAZÓWKI Do naleśników deserowych można dodać 1 łyżkę cukru.', 0, CAST(N'2022-05-22T16:34:13.903' AS DateTime), 0, 0, N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', NULL)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
INSERT [dbo].[Ratings] ([Users_Id], [Posts_Id], [value]) VALUES (N'88ae4962-10ec-4651-b077-39d4a0791001', 7, 1)
INSERT [dbo].[Ratings] ([Users_Id], [Posts_Id], [value]) VALUES (N'88ae4962-10ec-4651-b077-39d4a0791001', 9, 5)
INSERT [dbo].[Ratings] ([Users_Id], [Posts_Id], [value]) VALUES (N'88ae4962-10ec-4651-b077-39d4a0791001', 12, 5)
INSERT [dbo].[Ratings] ([Users_Id], [Posts_Id], [value]) VALUES (N'88ae4962-10ec-4651-b077-39d4a0791001', 1013, 5)
INSERT [dbo].[Ratings] ([Users_Id], [Posts_Id], [value]) VALUES (N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', 9, 1)
INSERT [dbo].[Ratings] ([Users_Id], [Posts_Id], [value]) VALUES (N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', 12, 3)
INSERT [dbo].[Ratings] ([Users_Id], [Posts_Id], [value]) VALUES (N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', 1013, 5)
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (1, N'Test', 2)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (3, N'hashtagJanPawelDrugiZyjeWNaszychSercach', 2)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (14, N'Test3', 11)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (16, N'Fajny Post', 12)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (1002, N'Test1', 2)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (1003, N'asd', 12)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (1005, N'asdasd', 10)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (1006, N':O', 2)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (1008, N'Przepis na ciasto', 1013)
INSERT [dbo].[Tags] ([Id], [Text], [Posts_Id]) VALUES (1009, N'LOREM IMPSUM', 12)
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'12409729-57d5-4bf7-9c5a-1630fd85a5c6', N'a@a.pl', N'a@a.pl', N'brak', 1, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'55ea261d-e941-4734-8dd5-33cb703142d5', N'test14@gmail.com', N'test14@gmail.com', N'brak', 0, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'5ccab313-0e01-4c16-8373-5fff23d7adf6', N'test0@gmail.com', N'test0@gmail.com', N'brak', 0, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'88ae4962-10ec-4651-b077-39d4a0791001', N'test13@gmail.com', N'Administrator Mojo jojo', N'420 213 769', 0, NULL)
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'9954b7f7-bc1c-4090-8a8a-cc3d38fa3583', N'test15@gmail.com', N'test15@gmail.com', N'brak', 0, NULL)
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'a', N'a@a.a', N'a', N'a', 1, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'b', N'b@b.b', N'b', N'b', 0, NULL)
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'b0a11298-7dd2-42a5-8a27-45e7cc8978b0', N'Admin@2137.pl', N'Mod1', N'123 123 124', 0, N'88ae4962-10ec-4651-b077-39d4a0791001')
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'c', N'c@c.c', N'c', N'c', 1, N'a')
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'fe03dad5-08d3-46f7-8bf7-7e41421a7f1d', N'test8@g.l', N'test8@g.l', N'brak', 0, NULL)
INSERT [dbo].[Users] ([Id], [Email], [UserName], [PhoneNumber], [Ban], [ModedBy]) VALUES (N'test', N'testSQL', N'testSql', N'brak', 0, NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 27.05.2022 18:53:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 27.05.2022 18:53:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Comments__IDX]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [Comments__IDX] ON [dbo].[Comments]
(
	[ModedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Posts__IDX]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [Posts__IDX] ON [dbo].[Posts]
(
	[ModedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Users__IDX]    Script Date: 27.05.2022 18:53:34 ******/
CREATE NONCLUSTERED INDEX [Users__IDX] ON [dbo].[Users]
(
	[ModedBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [Comments_Posts_FK] FOREIGN KEY([Posts_Id])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [Comments_Posts_FK]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [Comments_Users_FK] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [Comments_Users_FK]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [Comments_Users_FKv2] FOREIGN KEY([ModedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [Comments_Users_FKv2]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Users] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Users]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [Posts_Users_FK] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [Posts_Users_FK]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [Posts_Users_FKv2] FOREIGN KEY([ModedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [Posts_Users_FKv2]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Posts] FOREIGN KEY([Posts_Id])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Rating_Posts]
GO
ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Users] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Rating_Users]
GO
ALTER TABLE [dbo].[Tags]  WITH CHECK ADD  CONSTRAINT [Tags_Posts_FK] FOREIGN KEY([Posts_Id])
REFERENCES [dbo].[Posts] ([Id])
GO
ALTER TABLE [dbo].[Tags] CHECK CONSTRAINT [Tags_Posts_FK]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [Users_Users_FK] FOREIGN KEY([ModedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [Users_Users_FK]
GO
USE [master]
GO
ALTER DATABASE [TAB_DataBase] SET  READ_WRITE 
GO
