USE [master]
GO
/****** Object:  Database [FPTBookStore]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE DATABASE [FPTBookStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FPTBookStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FPTBookStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FPTBookStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FPTBookStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FPTBookStore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FPTBookStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FPTBookStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FPTBookStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FPTBookStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FPTBookStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FPTBookStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [FPTBookStore] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FPTBookStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FPTBookStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FPTBookStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FPTBookStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FPTBookStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FPTBookStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FPTBookStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FPTBookStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FPTBookStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FPTBookStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FPTBookStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FPTBookStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FPTBookStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FPTBookStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FPTBookStore] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FPTBookStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FPTBookStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FPTBookStore] SET  MULTI_USER 
GO
ALTER DATABASE [FPTBookStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FPTBookStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FPTBookStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FPTBookStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FPTBookStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FPTBookStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FPTBookStore] SET QUERY_STORE = OFF
GO
USE [FPTBookStore]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/6/2021 10:39:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 11/6/2021 10:39:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/6/2021 10:39:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/6/2021 10:39:48 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/6/2021 10:39:48 PM ******/
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
	[City] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PostalCode] [nvarchar](max) NULL,
	[StreetAddress] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationUserId] [nvarchar](450) NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[ShippingDate] [datetime2](7) NOT NULL,
	[OrderTotal] [float] NOT NULL,
	[TrackingNumber] [nvarchar](max) NULL,
	[Carrier] [nvarchar](max) NULL,
	[OrderStatus] [nvarchar](max) NULL,
	[PaymentStatus] [nvarchar](max) NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[PaymentDueDate] [datetime2](7) NOT NULL,
	[TransactionId] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[StreetAddress] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ISBN] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Price50] [float] NOT NULL,
	[Price100] [float] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210917144308_AddDefaultIdentityMigration', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210918073459_AddCategoryToDataBase', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210922111208_RenameCategoryIdColumn', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210922113021_AddCoverTypeToDatabase', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210922121842_AddStoredProcedureOfCoverType', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210923141420_AddProductToDatabase', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210923141708_AddRequiredForTitlevsISBNvsAuthor', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210926140223_AddUserProperties', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210926140509_UpdateValidateForApplicationUser', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210926141359_AddBookStoreToDatabase', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210926141706_DeleteAllTableAndAddNew', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210926142037_AddBookStoreIdToUser', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210927082003_AddStateToApplicationUser', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210927084645_RenameBookStoreToCompany', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210930030921_RenameIsAuthorized', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211001031258_AddNewModelShoppingCartOrderDetailOrderHeader', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211003015933_AddRequiredForDescriptionProduct', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211004080207_remove cover', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211004090506_remove product list price', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211014014857_RemoveCompanyInUser', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211014042428_AddQuantityProduct', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211014042548_DeleteCoverTypeIdInProduct', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211022122918_RefactorRangeOfQuantityProduct', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211026025602_RemoveShoppingCart', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211103132847_Add Product created date', N'5.0.10')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'7bbe5119-68b7-46ca-90f7-4d13880daf91', N'Individual Customer', N'INDIVIDUAL CUSTOMER', N'd13613ca-b398-486f-9d43-da8fcf48c803')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a8b0769b-c98c-442d-94c5-957f9a222589', N'Admin', N'ADMIN', N'35fa3663-3697-47cd-9a42-734e85078a50')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'15790df5-16f6-47a7-acd5-29d72c9fbb85', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'30eee504-cb69-4deb-8a0a-28744224a8d9', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4e4e866f-6418-4567-8517-affdd9af635c', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'599d079f-2dea-4d9e-b572-e3c7737f398f', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'62508dc0-d022-4b74-8207-5e3ca23e54b5', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6d6b1923-6745-42d9-b62a-da00556f83bd', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b8c38863-c587-4af9-81ae-8bee15f9c38d', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b9ed8045-0921-44ab-b4eb-44707c57ac51', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c645cac8-831c-4b6c-81c5-250f77f115ca', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd08ce9d6-64bf-40b5-a996-e6471623eec0', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f51c25cc-4c65-4184-b13c-874ca17427d8', N'7bbe5119-68b7-46ca-90f7-4d13880daf91')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'866051bd-ea97-4056-b2dd-f18e1f8a703c', N'a8b0769b-c98c-442d-94c5-957f9a222589')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'df5bf7a3-14a9-4ab8-8ba1-07630c14eacb', N'a8b0769b-c98c-442d-94c5-957f9a222589')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'15790df5-16f6-47a7-acd5-29d72c9fbb85', N'ngocuser2_@gmail.com', N'NGOCUSER2_@GMAIL.COM', N'ngocuser2_@gmail.com', N'NGOCUSER2_@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEJCd8TtUmJq2L/SXWWShRHceiJ9k2wN5znk44LzvcmvOzMZlvTrJs5v538E093dm8Q==', N'U5KQWOAPVGIKTBJCZSZHEFGT2UQLEYBK', N'a3e1cba9-b1cd-4794-8c58-bc1441519708', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'30eee504-cb69-4deb-8a0a-28744224a8d9', N'ngocuser3B_@gmail.com', N'NGOCUSER3B_@GMAIL.COM', N'ngocuser3B_@gmail.com', N'NGOCUSER3B_@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEDCYvOozTRZ++aE2xVAa+4RPER77G94HwJUhVZaSf4gcf6bRx7pxgPyT4mHAVdcDgg==', N'3UIMN5IRYBXU5RSO5OB5P2NMVW2PYIBV', N'045e4854-711c-41b1-bf24-1ba3f87a336b', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'4e4e866f-6418-4567-8517-affdd9af635c', N'ngoc2A__@gmail.com', N'NGOC2A__@GMAIL.COM', N'ngoc2A__@gmail.com', N'NGOC2A__@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEH1JHVY9woqKwMqLENFC2e8WvYbPYirrdY72DzXsWpHFa2scewtF3QONaemM1MwxCA==', N'PRE6IKQN7JZOLYPNEESN4OBZIMU2SR4M', N'3c99ed71-adc6-44d1-b438-8d0a947a6e21', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'599d079f-2dea-4d9e-b572-e3c7737f398f', N'ngouserc@gmail.com', N'NGOUSERC@GMAIL.COM', N'ngouserc@gmail.com', N'NGOUSERC@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEJDhtAp8lO65J3nEZP8Mwinscn8Mi6okrl/7ztAp2dtM2gbsWUtLFxoLTcLBB5gy6A==', N'3IJTSLUSFJN6UVGG5KLZ7TRII62KJFLC', N'727e7b80-a841-4162-a383-07b4c8969e0b', N'0868962245', 0, 0, CAST(N'4021-11-05T20:09:08.6634296+07:00' AS DateTimeOffset), 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'62508dc0-d022-4b74-8207-5e3ca23e54b5', N'ngocuser@gmail.com', N'NGOCUSER@GMAIL.COM', N'ngocuser@gmail.com', N'NGOCUSER@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEMhjx3l7jZpr//zGTlww9B7GrqCcb84qUfly9A2BftEWRCPOjKmGBiRgOzLaSBxZDQ==', N'DHJLVVJJUK2XWEL3GAR4ZKA556KWKTMB', N'38d212dc-e97b-4189-bafd-57c4fae360e8', N'0868962245', 0, 0, CAST(N'4021-11-06T18:02:56.6779536+07:00' AS DateTimeOffset), 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'6d6b1923-6745-42d9-b62a-da00556f83bd', N'user123A_@gmail.com', N'USER123A_@GMAIL.COM', N'user123A_@gmail.com', N'USER123A_@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEJ9cjOIhj2UyyPzMO/Db6F6QoivjEqng+qdTDG2goyqR82fAbh5RjcEM4Jdf2zIjFw==', N'6RFPT7EEOBO7K2DHQEUUXYFGFP6LGIOW', N'd91d49ec-14e5-4e86-b7e4-64f73cc39c46', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'866051bd-ea97-4056-b2dd-f18e1f8a703c', N'ngocadmin@gmail.com', N'NGOCADMIN@GMAIL.COM', N'ngocadmin@gmail.com', N'NGOCADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEKA66FjPGGsxGS30UWGyORbes651qAK9PrLSfWtxtDYSBQoytOhKyNk2nxfbdWQC4g==', N'2TIXCOOBFRKLY4LKYFLDL5W5SMUDGKEA', N'2a48d176-3b01-45cf-a1cb-c5eab0f4f014', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Locccc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'b8c38863-c587-4af9-81ae-8bee15f9c38d', N'ngocuser3C_@gmail.com', N'NGOCUSER3C_@GMAIL.COM', N'ngocuser3C_@gmail.com', N'NGOCUSER3C_@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEK3cHWuIFgzY6DWGogstffV9pRMDh9pzNMlEM9ayRnCz/1Jh6VcwiLhM5IX+UQlEQA==', N'4KLGRQ73QC32RQWSRJRACM2QPINHY5ZL', N'd4d3f775-ac28-4b32-b463-9df1da2cec82', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'b9ed8045-0921-44ab-b4eb-44707c57ac51', N'ngocuser2@gmail.com', N'NGOCUSER2@GMAIL.COM', N'ngocuser2@gmail.com', N'NGOCUSER2@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAECK4CFOWDXesMkYtbEeHj2t/9E2NAmX1+i14FLD7axp0kxO59pAoM6wbYuZiD+Qcww==', N'I4ZQP5VZNTDXSH34SDSQQS3FTSOYQNUA', N'70da08ed-8e51-4cfe-bb74-6e8159929c1f', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'c645cac8-831c-4b6c-81c5-250f77f115ca', N'ngocuser3A_@gmail.com', N'NGOCUSER3A_@GMAIL.COM', N'ngocuser3A_@gmail.com', N'NGOCUSER3A_@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEF0E5LrANz4yI/K+KUG4jj4k31x7OKElo08P2iCqznKbAguufGYlSNLcymFgugP79A==', N'VIC7XR7AZ6FVQKV2N72ZNSHT5PZYDUGY', N'da344356-e248-4218-a0d7-5c2ff3f91dd9', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'd08ce9d6-64bf-40b5-a996-e6471623eec0', N'ngoc23A__@gmail.com', N'NGOC23A__@GMAIL.COM', N'ngoc23A__@gmail.com', N'NGOC23A__@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAENysNRCmHkL7/5+TzUfnawxLXPyeAm0Uv4bcfWxNLAvDgpcfosOcBlU64TfDdxpNsQ==', N'ZMNQE7DCCYB36JGR5D4PTDZM6DMSVCEJ', N'e37f75ee-d90d-4694-815b-fd03e6f49739', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngocccc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'df5bf7a3-14a9-4ab8-8ba1-07630c14eacb', N'ngocadmin2@gmail.com', N'NGOCADMIN2@GMAIL.COM', N'ngocadmin2@gmail.com', N'NGOCADMIN2@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEF9kxtgC4D59GrksJ4yYFq8ce5K1c+hd7zDicx7ZmjiVb7UrQ8YHjbynOA9HwuOr4Q==', N'F4QLB7NMZAVKLDIFDN35EKULLEYZAEA6', N'b451e0ad-4f1f-4a52-8fbd-b0ccd8c19db4', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc Admin', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'123')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [City], [Discriminator], [Name], [PostalCode], [StreetAddress], [State]) VALUES (N'f51c25cc-4c65-4184-b13c-874ca17427d8', N'ngocadmin12A_@gmail.com', N'NGOCADMIN12A_@GMAIL.COM', N'ngocadmin12A_@gmail.com', N'NGOCADMIN12A_@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEEp04RJHTE4E4F7jpWt1AT/3QDmcXKL2NyZ/G5JlAb7NNZ9AqTWEaHmK0jdZNDK8DA==', N'ZGTYEL6AHCMW36UEHGAFTTF7HCCJO5HJ', N'282162b8-e797-4b7c-9055-8bb79a8ce9f2', N'0868962245', 0, 0, NULL, 1, 0, N'huyen Dai Loc', N'ApplicationUser', N'Phan Thieu Ngoc', N'456', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia ne', N'123')
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Realistic fiction')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'onf lif dius')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'fwfsvsfvfsv')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'sdvdsvsdv')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (5, N'hfggfbdg')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (1, 1, 1, 2, 199)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (2, 2, 1, 1, 199)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (3, 2, 7, 1, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (4, 3, 1, 12, 199)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (5, 4, 1, 20, 199)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (6, 4, 7, 5, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (7, 5, 1, 10, 199)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (8, 5, 7, 11, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (9, 6, 1, 9, 1992)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (10, 6, 7, 4, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (11, 7, 1, 199, 121)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (12, 7, 7, 10, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (13, 8, 1, 10, 191)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (14, 8, 7, 13, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (15, 9, 1, 1, 191)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (16, 9, 7, 12, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (17, 10, 1, 1, 191)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (18, 10, 7, 11, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (19, 11, 1, 1, 191)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (20, 11, 7, 22, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (21, 12, 1, 4, 191)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (22, 12, 7, 11, 555)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (23, 13, 1, 11, 191)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Count], [Price]) VALUES (24, 13, 7, 12, 555)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderHeader] ON 

INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (1, N'599d079f-2dea-4d9e-b572-e3c7737f398f', CAST(N'2021-11-04T22:08:21.0932612' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 398, NULL, NULL, N'Refunded', N'Refunded', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3Js7Y7GUvJTZ96fL1eF1jPQo', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (2, N'62508dc0-d022-4b74-8207-5e3ca23e54b5', CAST(N'2021-11-05T20:41:44.3373974' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 754, NULL, NULL, N'Refunded', N'Refunded', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JsSfoGUvJTZ96fL1Kekkczc', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (3, N'62508dc0-d022-4b74-8207-5e3ca23e54b5', CAST(N'2021-11-06T15:41:38.7209488' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2388, NULL, NULL, N'Processing', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JskSzGUvJTZ96fL3YkLpRJF', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (4, N'62508dc0-d022-4b74-8207-5e3ca23e54b5', CAST(N'2021-11-06T16:48:44.4662562' AS DateTime2), CAST(N'2021-11-06T19:55:19.1544627' AS DateTime2), 6755, N'123345', N'1234', N'Shipped', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JslVvGUvJTZ96fL2EF77q1C', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (5, N'62508dc0-d022-4b74-8207-5e3ca23e54b5', CAST(N'2021-11-06T17:45:36.7836475' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 8095, NULL, NULL, N'Approved', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JsmOxGUvJTZ96fL0WDbpFU7', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (6, N'c645cac8-831c-4b6c-81c5-250f77f115ca', CAST(N'2021-11-06T20:36:39.3616171' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 20148, NULL, NULL, N'Processing', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3Jsp4TGUvJTZ96fL339xbgTU', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (7, N'15790df5-16f6-47a7-acd5-29d72c9fbb85', CAST(N'2021-11-06T20:50:13.1825428' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 29629, NULL, NULL, N'Approved', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JspHbGUvJTZ96fL1LLvkpsD', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (8, N'15790df5-16f6-47a7-acd5-29d72c9fbb85', CAST(N'2021-11-06T20:52:10.2279874' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 9125, NULL, NULL, N'Approved', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JspJUGUvJTZ96fL0OgEoHP4', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (9, N'30eee504-cb69-4deb-8a0a-28744224a8d9', CAST(N'2021-11-06T20:54:12.0842912' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 6851, NULL, NULL, N'Refunded', N'Refunded', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JspLSGUvJTZ96fL2UAxwEoa', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (10, N'4e4e866f-6418-4567-8517-affdd9af635c', CAST(N'2021-11-06T20:59:41.8767111' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 6296, NULL, NULL, N'Approved', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JspQmGUvJTZ96fL0E83u6Rj', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (11, N'd08ce9d6-64bf-40b5-a996-e6471623eec0', CAST(N'2021-11-06T21:04:03.3090644' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 12401, NULL, NULL, N'Approved', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JspUzGUvJTZ96fL106iR2j9', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (12, N'f51c25cc-4c65-4184-b13c-874ca17427d8', CAST(N'2021-11-06T21:12:40.4930570' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 6869, NULL, NULL, N'Approved', N'Approved', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3JspdLGUvJTZ96fL2Atooum6', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
INSERT [dbo].[OrderHeader] ([Id], [ApplicationUserId], [OrderDate], [ShippingDate], [OrderTotal], [TrackingNumber], [Carrier], [OrderStatus], [PaymentStatus], [PaymentDate], [PaymentDueDate], [TransactionId], [PhoneNumber], [StreetAddress], [City], [State], [PostalCode], [Name]) VALUES (13, N'6d6b1923-6745-42d9-b62a-da00556f83bd', CAST(N'2021-11-06T21:37:09.5626131' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 8761, NULL, NULL, N'Refunded', N'Refunded', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'ch_3Jsq12GUvJTZ96fL1FS9nmF4', N'0868962245', N'149 Do Dang Tuyen, khu Song My, thi tran Ai Nghia', N'huyen Dai Loc', N'123', N'456', N'Phan Thieu Ngoc')
SET IDENTITY_INSERT [dbo].[OrderHeader] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Title], [Description], [ISBN], [Author], [Price], [Price50], [Price100], [ImageUrl], [CategoryId], [Quantity], [CreatedDate]) VALUES (1, N'NGOC CREATEeee', N'<p>123rthjbn</p>', N'12345678', N' NGOc ne', 191, 123, 121, N'https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg', 1, 19, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Product] ([Id], [Title], [Description], [ISBN], [Author], [Price], [Price50], [Price100], [ImageUrl], [CategoryId], [Quantity], [CreatedDate]) VALUES (7, N'NGOC CREATE 2', N'345678jmhnbgvf', N'1234567889', N'nggggg', 555, 234, 345, N'https://interactive-examples.mdn.mozilla.net/media/cc0-images/grapefruit-slice-332-332.jpg', 1, 443, CAST(N'2001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetail_OrderId]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetail_OrderId] ON [dbo].[OrderDetail]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetail_ProductId]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetail_ProductId] ON [dbo].[OrderDetail]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrderHeader_ApplicationUserId]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderHeader_ApplicationUserId] ON [dbo].[OrderHeader]
(
	[ApplicationUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Product_CategoryId]    Script Date: 11/6/2021 10:39:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Product_CategoryId] ON [dbo].[Product]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Discriminator]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'') FOR [Title]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'') FOR [Description]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'') FOR [ISBN]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'') FOR [Author]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
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
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_OrderHeader_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[OrderHeader] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_OrderHeader_OrderId]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product_ProductId]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_AspNetUsers_ApplicationUserId] FOREIGN KEY([ApplicationUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_AspNetUsers_ApplicationUserId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category_CategoryId]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateCoverType]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_CreateCoverType]
                                   @Name varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.CoverType(Name)
                                    VALUES (@Name)
                                   END
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteCoverType]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_DeleteCoverType]
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.CoverType
                                     WHERE  Id = @Id
                                    END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCoverType]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetCoverType] 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.CoverType  WHERE  (Id = @Id) 
                                    END 
GO
/****** Object:  StoredProcedure [dbo].[usp_GetCoverTypes]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_GetCoverTypes]
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.CoverType
                                    END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateCoverType]    Script Date: 11/6/2021 10:39:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UpdateCoverType]
	                                @Id int,
	                                @Name varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.CoverType
                                     SET  Name = @Name
                                     WHERE  Id = @Id
                                    END
GO
USE [master]
GO
ALTER DATABASE [FPTBookStore] SET  READ_WRITE 
GO
