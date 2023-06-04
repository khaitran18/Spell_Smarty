USE [master]
GO
/****** Object:  Database [SpellSmarty]    Script Date: 6/4/2023 3:27:06 PM ******/
CREATE DATABASE [SpellSmarty]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SpellSmarty', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SpellSmarty.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SpellSmarty_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SpellSmarty_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SpellSmarty] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SpellSmarty].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SpellSmarty] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SpellSmarty] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SpellSmarty] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SpellSmarty] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SpellSmarty] SET ARITHABORT OFF 
GO
ALTER DATABASE [SpellSmarty] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SpellSmarty] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SpellSmarty] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SpellSmarty] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SpellSmarty] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SpellSmarty] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SpellSmarty] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SpellSmarty] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SpellSmarty] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SpellSmarty] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SpellSmarty] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SpellSmarty] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SpellSmarty] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SpellSmarty] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SpellSmarty] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SpellSmarty] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SpellSmarty] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SpellSmarty] SET RECOVERY FULL 
GO
ALTER DATABASE [SpellSmarty] SET  MULTI_USER 
GO
ALTER DATABASE [SpellSmarty] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SpellSmarty] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SpellSmarty] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SpellSmarty] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SpellSmarty] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SpellSmarty', N'ON'
GO
ALTER DATABASE [SpellSmarty] SET QUERY_STORE = OFF
GO
USE [SpellSmarty]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[name] [nvarchar](255) NULL,
	[planid] [int] NOT NULL,
	[subribe_date] [datetime] NULL,
	[end_date] [datetime] NULL,
 CONSTRAINT [PK__Accounts__3213E83FBF275E5B] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[content] [nvarchar](255) NULL,
	[date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[genre_id] [int] IDENTITY(1,1) NOT NULL,
	[genre_name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Levels]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Levels](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plans]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plans](
	[planid] [int] IDENTITY(1,1) NOT NULL,
	[plan_name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[planid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoGenre]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoGenre](
	[video_id] [int] NOT NULL,
	[genre_id] [int] NOT NULL,
	[video_genre_id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[video_genre_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[videoid] [int] IDENTITY(1,1) NOT NULL,
	[rating] [float] NULL,
	[subtitle] [nvarchar](max) NOT NULL,
	[src_id] [nvarchar](255) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[thumbnail_link] [nvarchar](255) NULL,
	[channel_name] [nvarchar](255) NULL,
	[learnt_count] [int] NOT NULL,
	[video_description] [nvarchar](255) NULL,
	[level] [int] NOT NULL,
	[added_date] [datetime] NOT NULL,
 CONSTRAINT [PK__Video__14B3E0AE13B984ED] PRIMARY KEY CLUSTERED 
(
	[videoid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoStat]    Script Date: 6/4/2023 3:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoStat](
	[stat_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[video_id] [int] NOT NULL,
	[progress] [int] NOT NULL,
 CONSTRAINT [PK__VideoSta__B8A525606C1C7598] PRIMARY KEY CLUSTERED 
(
	[stat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([id], [username], [password], [email], [name], [planid], [subribe_date], [end_date]) VALUES (1, N'khaitran', N'1', N'khaitranquang@gmail.com', N'Khai', 1, NULL, NULL)
GO
INSERT [dbo].[Accounts] ([id], [username], [password], [email], [name], [planid], [subribe_date], [end_date]) VALUES (3, N'khaine', N'1', N'khaine@gmail.com', NULL, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 
GO
INSERT [dbo].[Genres] ([genre_id], [genre_name]) VALUES (1, N'Cartoon')
GO
INSERT [dbo].[Genres] ([genre_id], [genre_name]) VALUES (2, N'Documentary')
GO
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[Levels] ON 
GO
INSERT [dbo].[Levels] ([id], [name]) VALUES (1, N'A1')
GO
INSERT [dbo].[Levels] ([id], [name]) VALUES (2, N'A2')
GO
INSERT [dbo].[Levels] ([id], [name]) VALUES (3, N'B1')
GO
INSERT [dbo].[Levels] ([id], [name]) VALUES (4, N'B2')
GO
INSERT [dbo].[Levels] ([id], [name]) VALUES (5, N'C1')
GO
INSERT [dbo].[Levels] ([id], [name]) VALUES (6, N'C2')
GO
SET IDENTITY_INSERT [dbo].[Levels] OFF
GO
SET IDENTITY_INSERT [dbo].[Plans] ON 
GO
INSERT [dbo].[Plans] ([planid], [plan_name]) VALUES (1, N'Free')
GO
INSERT [dbo].[Plans] ([planid], [plan_name]) VALUES (2, N'Premium')
GO
SET IDENTITY_INSERT [dbo].[Plans] OFF
GO
SET IDENTITY_INSERT [dbo].[VideoGenre] ON 
GO
INSERT [dbo].[VideoGenre] ([video_id], [genre_id], [video_genre_id]) VALUES (2, 1, 4)
GO
INSERT [dbo].[VideoGenre] ([video_id], [genre_id], [video_genre_id]) VALUES (2, 2, 5)
GO
INSERT [dbo].[VideoGenre] ([video_id], [genre_id], [video_genre_id]) VALUES (3, 1, 7)
GO
SET IDENTITY_INSERT [dbo].[VideoGenre] OFF
GO
SET IDENTITY_INSERT [dbo].[Videos] ON 
GO
INSERT [dbo].[Videos] ([videoid], [rating], [subtitle], [src_id], [title], [thumbnail_link], [channel_name], [learnt_count], [video_description], [level], [added_date]) VALUES (2, 5, N'aa', N'aa', N'aa', N'aa', N'aa', 0, N'hehe', 1, CAST(N'2023-03-06T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Videos] ([videoid], [rating], [subtitle], [src_id], [title], [thumbnail_link], [channel_name], [learnt_count], [video_description], [level], [added_date]) VALUES (3, 4, N'hehe', N'hehe', N'hehe', N'hee', N'KhaiChannel', 100, N'Dictation online', 3, CAST(N'2023-04-06T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Videos] OFF
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_planid]  DEFAULT ((1)) FOR [planid]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK__Accounts__planid__267ABA7A] FOREIGN KEY([planid])
REFERENCES [dbo].[Plans] ([planid])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK__Accounts__planid__267ABA7A]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK__Feedbacks__accou__403A8C7D] FOREIGN KEY([account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK__Feedbacks__accou__403A8C7D]
GO
ALTER TABLE [dbo].[VideoGenre]  WITH CHECK ADD FOREIGN KEY([genre_id])
REFERENCES [dbo].[Genres] ([genre_id])
GO
ALTER TABLE [dbo].[VideoGenre]  WITH CHECK ADD  CONSTRAINT [FK__VideoGenr__video__3B75D760] FOREIGN KEY([video_id])
REFERENCES [dbo].[Videos] ([videoid])
GO
ALTER TABLE [dbo].[VideoGenre] CHECK CONSTRAINT [FK__VideoGenr__video__3B75D760]
GO
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD FOREIGN KEY([level])
REFERENCES [dbo].[Levels] ([id])
GO
ALTER TABLE [dbo].[VideoStat]  WITH CHECK ADD  CONSTRAINT [FK__VideoStat__accou__35BCFE0A] FOREIGN KEY([account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[VideoStat] CHECK CONSTRAINT [FK__VideoStat__accou__35BCFE0A]
GO
ALTER TABLE [dbo].[VideoStat]  WITH CHECK ADD  CONSTRAINT [FK__VideoStat__video__36B12243] FOREIGN KEY([video_id])
REFERENCES [dbo].[Videos] ([videoid])
GO
ALTER TABLE [dbo].[VideoStat] CHECK CONSTRAINT [FK__VideoStat__video__36B12243]
GO
USE [master]
GO
ALTER DATABASE [SpellSmarty] SET  READ_WRITE 
GO
