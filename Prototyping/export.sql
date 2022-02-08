USE [master]
GO
/****** Object:  Database [RecomendationSystem]    Script Date: 08.02.2022 12:56:18 ******/
CREATE DATABASE [RecomendationSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RecomendationSystem', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RecomendationSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RecomendationSystem_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RecomendationSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RecomendationSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RecomendationSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RecomendationSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RecomendationSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RecomendationSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RecomendationSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RecomendationSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [RecomendationSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RecomendationSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RecomendationSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RecomendationSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RecomendationSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RecomendationSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RecomendationSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RecomendationSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RecomendationSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RecomendationSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RecomendationSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RecomendationSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RecomendationSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RecomendationSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RecomendationSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RecomendationSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RecomendationSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RecomendationSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RecomendationSystem] SET  MULTI_USER 
GO
ALTER DATABASE [RecomendationSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RecomendationSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RecomendationSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RecomendationSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RecomendationSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RecomendationSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RecomendationSystem] SET QUERY_STORE = OFF
GO
USE [RecomendationSystem]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 08.02.2022 12:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[photo] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[type_id] [int] NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 08.02.2022 12:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [date] NOT NULL,
	[user_id] [int] NOT NULL,
	[rating] [int] NOT NULL,
	[text] [ntext] NULL,
	[item_id] [int] NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 08.02.2022 12:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [ntext] NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 08.02.2022 12:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[mobile] [nvarchar](11) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Item] ON 
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (2, N'Консервированный хлеб', N'canned_bread.jpg', N'Бостонский коричневый хлеб (Boston Brown Bread) производится компанией B & M Baked Beans в штате Мэн, и также известен как «хлеб в банке». Это сверхплотный, темно-коричневый хлеб из пшеничной и ржаной муки, который слегка пряный на вкус и влажноватый по консистенции, а также подслащен мелассой.', 1)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (5, N'Хлеб из древесного угля', N'coal_bread.jpg', N'Активированный уголь, как известно, придает почти любой еде смоляно-черный оттенок. Он также, как предполагается, полезен для здоровья, поскольку выводит токсины из организма, подавляет желудочно-кишечные расстройства и облегчает похмелье. К сожалению для пекарей, в Европе активированный уголь считается пищевым красителем (E153 Carbon Black), добавление которых в выпечку запрещено в соответствии с законодательством ЕС. Тем не менее, итальянцы продают такие буханки не как хлеб, а как лекарство, тем самым обойдя закон.', 1)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (7, N'Конфеты со вкусом бекона', N'bekon_candy.jpg', N'Производители этой сладости соединили, казалось бы, невозможное — вкус копченого сала со свежим вкусом мяты. Как говорится, «два в одном» — солёное сало и сладкая мята. Бомба вкуса!
', 2)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (8, N'Sour Flush', N'sour_flush.jpg', N'"Sour Flush" — карамель с унитазом в придачу. В унитаз насыпан кислый порошок, в который нужно постоянно обмакивать леденец. Просто прелесть!
', 2)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (10, N'Крот "Турбо" гель 1 л', N'krot.jpg', N'КРОТ устраняет засоры в течении 10-15 минут! Благодаря своей гелеобразной форме и высокому содержанию активных веществ, гель быстро проникает до места загрязнения и интенсивно его устраняет. Эффективно растворяет волосы, остатки пищи, жиры, белки, соли жирных кислот, коллагеновые волокна, обладает дезинфицирующим эффектом. Безопасен для всех видов труб, в том числе пластиковых. Удобная упаковка с колпачком "защита от детей" обеспечивает безопасность применения средства. Средство для прочистки труб КРОТ - лауреат конкурса 100 лучших товаров России. Уничтожает микробы и устраняет неприятный запах канализации. Густая формула геля обеспечивает экономичный расход. КРОТ для труб - самое эффективное средство от засоров в трубах! В этом убедились десятки тысяч покупателей, убедитесь и Вы! Купить рекомендуем с запасом и тогда засор не застанет врасплох. Используйте небольшие порции средства для профилактики раз в месяц и забудете о засорах', 3)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (12, N'Шампунь Шрек 2 в 1 для детей', N'shrek_shampoo.jpg', N'Рекомендуется для ежедневного использования; Пожалуйста, храните в сухом прохладном месте Шампунь Shrek 2 в 1 был выпущен дизайнерским домом Shrek. 100% качественный продукт', 3)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (13, N'Ребрышки бараньи Эколь, 400г', N'sheepmeat.jpg', N'Баранина', 4)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (14, N'Филе индейки Перекрёсток, кг', N'turkey.jpg', N'Индейка', 4)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (15, N'Карп живой, кг', N'carp.jpg', N'карп.', 5)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (16, N'Килька За Родину балтийская неразделанная обжаренная в томатном соусе, 270г', N'sprat.jpg', N'Килька балтийская, паста томатная, сахар, мука пшеничная масло растительное, лук, соль, регулятор кислотности-кислота уксусная, пряности', 5)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (17, N'Айран Долголетие Турецкий 1,8%, 300г', N'ayran.jpg', N'Настоящий турецкий айран мы готовим из свежего цельного молока и специальной закваски, привезенной из Турции. Айран получется густым с нежным сливочным вкусом. Каждый, кто бывал в Турции и пробовал турецкий айран, может приобретать теперь любимый напиток в России.', 6)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (18, N'Напиток кисломолочный Эдельвейс Кумыснор 1.8%, 500мл', N'koumiss.jpg', N'Восстановленное молоко из сухого молока, вода, соль поваренная пищевая, бактериальная закваска (чистые культура молочнокислой болгарской палочки и лактосбраживающих дрожжей)', 6)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (19, N'Яйца куриные Фермер Александров пищевое С1, 9шт', N'eggs.jpg', N'яйцо куриное', 7)
GO
INSERT [dbo].[Item] ([id], [name], [photo], [description], [type_id]) VALUES (21, N'
Желток яичный Grovo Удобное яйцо 28 пастеризованных желтков 500г', N'yolk.jpeg', N'28 желтков', 7)
GO
SET IDENTITY_INSERT [dbo].[Item] OFF
GO
SET IDENTITY_INSERT [dbo].[Review] ON 
GO
INSERT [dbo].[Review] ([id], [date], [user_id], [rating], [text], [item_id]) VALUES (1, CAST(N'2022-02-08' AS Date), 1, 5, N'Спасибо.', 12)
GO
SET IDENTITY_INSERT [dbo].[Review] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 
GO
INSERT [dbo].[Type] ([id], [name], [description]) VALUES (1, N'Хлебобулочные', NULL)
GO
INSERT [dbo].[Type] ([id], [name], [description]) VALUES (2, N'Кондитерские', NULL)
GO
INSERT [dbo].[Type] ([id], [name], [description]) VALUES (3, N'Бытовая химия', NULL)
GO
INSERT [dbo].[Type] ([id], [name], [description]) VALUES (4, N'Мясо и мясосодержащие продукты', NULL)
GO
INSERT [dbo].[Type] ([id], [name], [description]) VALUES (5, N'Рыбная продукция', NULL)
GO
INSERT [dbo].[Type] ([id], [name], [description]) VALUES (6, N'Молочные продукты', NULL)
GO
INSERT [dbo].[Type] ([id], [name], [description]) VALUES (7, N'Яйца', NULL)
GO
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([id], [username], [password], [mobile]) VALUES (1, N'admin', N'admin', N'89162179590')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[Type] ([id])
GO
ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Type]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Item] FOREIGN KEY([item_id])
REFERENCES [dbo].[Item] ([id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Item]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_User]
GO
USE [master]
GO
ALTER DATABASE [RecomendationSystem] SET  READ_WRITE 
GO
