USE [master]
GO
/****** Object:  Database [Booking_Hotel_DB]    Script Date: 3/9/2023 4:08:22 PM ******/
CREATE DATABASE [Booking_Hotel_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Booking_Hotel_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Booking_Hotel_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Booking_Hotel_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Booking_Hotel_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Booking_Hotel_DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Booking_Hotel_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Booking_Hotel_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Booking_Hotel_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Booking_Hotel_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Booking_Hotel_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Booking_Hotel_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Booking_Hotel_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Booking_Hotel_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Booking_Hotel_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Booking_Hotel_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Booking_Hotel_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Booking_Hotel_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Booking_Hotel_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Booking_Hotel_DB', N'ON'
GO
ALTER DATABASE [Booking_Hotel_DB] SET QUERY_STORE = OFF
GO
USE [Booking_Hotel_DB]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 3/9/2023 4:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[order_note] [nvarchar](max) NULL,
	[order_checkin] [datetime] NULL,
	[order_checkout] [datetime] NULL,
	[order_created] [datetime] NULL,
	[order_status] [smallint] NULL,
	[user_id] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Detail]    Script Date: 3/9/2023 4:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Detail](
	[detail_id] [int] IDENTITY(1,1) NOT NULL,
	[room_id] [int] NULL,
	[detail_price] [decimal](18, 2) NULL,
	[detail_count] [smallint] NULL,
	[order_id] [int] NULL,
 CONSTRAINT [PK_Order_Detail] PRIMARY KEY CLUSTERED 
(
	[detail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room_Hotel]    Script Date: 3/9/2023 4:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room_Hotel](
	[room_id] [int] IDENTITY(1,1) NOT NULL,
	[room_name] [nvarchar](150) NULL,
	[room_image] [nvarchar](max) NULL,
	[room_bed] [smallint] NULL,
	[room_desc] [nvarchar](max) NULL,
	[room_price] [decimal](18, 2) NULL,
	[room_status] [int] NULL,
	[type_id] [int] NULL,
 CONSTRAINT [PK_Room_Hotel] PRIMARY KEY CLUSTERED 
(
	[room_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type_Room]    Script Date: 3/9/2023 4:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_Room](
	[type_id] [int] IDENTITY(1,1) NOT NULL,
	[type_name] [nvarchar](150) NULL,
	[type_image] [nvarchar](max) NULL,
	[type_desc] [nvarchar](250) NULL,
	[type_status] [int] NULL,
 CONSTRAINT [PK_Type_Room] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/9/2023 4:08:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_phone] [nchar](20) NULL,
	[user_password] [nvarchar](10) NULL,
	[user_name] [nvarchar](50) NULL,
	[user_image] [nvarchar](max) NULL,
	[user_wallet] [decimal](18, 2) NULL,
	[user_status] [smallint] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Room_Hotel] ON 

INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (1, N'SR-101', N'https://www.mailamhotel.com/images/product/goc/goc_1615972559.jpg', 1, N'A room designed to accommodate one guest with a single bed.', CAST(199.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (2, N'SR-102', N'https://www.mailamhotel.com/images/product/goc/goc_1615972559.jpg', 1, N'A room designed to accommodate one guest with a single bed.', CAST(199.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (3, N'SR-103', N'https://www.mailamhotel.com/images/product/goc/goc_1615972559.jpg', 1, N'A room designed to accommodate one guest with a single bed.', CAST(199.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (4, N'SR-104', N'https://www.mailamhotel.com/images/product/goc/goc_1615972559.jpg', 1, N'A room designed to accommodate one guest with a single bed.', CAST(199.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (5, N'DR-201', N'https://www.mailamhotel.com/images/product/goc/goc_1615361772.jpg', 2, N'A room designed to accommodate two guests with a double bed or two twin beds.', CAST(399.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (6, N'DR-202', N'https://www.mailamhotel.com/images/product/goc/goc_1615361772.jpg', 2, N'A room designed to accommodate two guests with a double bed or two twin beds.', CAST(399.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (7, N'DR-203', N'https://www.mailamhotel.com/images/product/goc/goc_1615361772.jpg', 2, N'A room designed to accommodate two guests with a double bed or two twin beds.', CAST(399.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (8, N'DR-204', N'https://www.mailamhotel.com/images/product/goc/goc_1615361772.jpg', 2, N'A room designed to accommodate two guests with a double bed or two twin beds.', CAST(399.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (9, N'TR-301', N'https://www.mailamhotel.com/images/product/goc/goc_1615361444.jpg', 2, N' A room designed to accommodate two guests with two separate single beds.', CAST(499.00 AS Decimal(18, 2)), 1, 3)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (10, N'TR-302', N'https://www.mailamhotel.com/images/product/goc/goc_1615361444.jpg', 2, N' A room designed to accommodate two guests with two separate single beds.', CAST(499.00 AS Decimal(18, 2)), 1, 3)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (11, N'TR-303', N'https://www.mailamhotel.com/images/product/goc/goc_1615361444.jpg', 2, N' A room designed to accommodate two guests with two separate single beds.', CAST(499.00 AS Decimal(18, 2)), 1, 3)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (12, N'TR-304', N'https://www.mailamhotel.com/images/product/goc/goc_1615361444.jpg', 2, N' A room designed to accommodate two guests with two separate single beds.', CAST(499.00 AS Decimal(18, 2)), 1, 3)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (13, N'CR-401', N'http://www.residencebaron.hu/images/info/11_44_pic_93.jpg', 4, N'Two or more rooms that are connected by a door and are often booked together for families or groups.', CAST(699.00 AS Decimal(18, 2)), 1, 4)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (14, N'CR-402', N'http://www.residencebaron.hu/images/info/11_44_pic_93.jpg', 4, N'Two or more rooms that are connected by a door and are often booked together for families or groups.', CAST(699.00 AS Decimal(18, 2)), 1, 4)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (15, N'CR-403', N'http://www.residencebaron.hu/images/info/11_44_pic_93.jpg', 4, N'Two or more rooms that are connected by a door and are often booked together for families or groups.', CAST(699.00 AS Decimal(18, 2)), 1, 4)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (16, N'CR-404', N'http://www.residencebaron.hu/images/info/11_44_pic_93.jpg', 4, N'Two or more rooms that are connected by a door and are often booked together for families or groups.', CAST(699.00 AS Decimal(18, 2)), 1, 4)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (17, N'FR-501', N'https://www.mailamhotel.com/images/product/goc/goc_1615973314.jpg', 3, N'A room designed to accommodate families with children, often with additional beds or a sofa bed.', CAST(599.00 AS Decimal(18, 2)), 1, 5)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (18, N'FR-502', N'https://www.mailamhotel.com/images/product/goc/goc_1615973314.jpg', 3, N'A room designed to accommodate families with children, often with additional beds or a sofa bed.', CAST(599.00 AS Decimal(18, 2)), 1, 5)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (19, N'FR-503', N'https://www.mailamhotel.com/images/product/goc/goc_1615973314.jpg', 3, N'A room designed to accommodate families with children, often with additional beds or a sofa bed.', CAST(599.00 AS Decimal(18, 2)), 1, 5)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (20, N'FR-504', N'https://www.mailamhotel.com/images/product/goc/goc_1615973314.jpg', 3, N'A room designed to accommodate families with children, often with additional beds or a sofa bed.', CAST(599.00 AS Decimal(18, 2)), 1, 5)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (21, N'HS-601', N'https://blog.grabhotel.net/content/images/2020/10/phong-long-chim-ngan-le-mot-dem.jpg', 1, N'A special suite designed for newlyweds with romantic amenities such as a king-size bed, a Jacuzzi or a balcony with a view.', CAST(999.00 AS Decimal(18, 2)), 1, 6)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (22, N'HS-602', N'https://blog.grabhotel.net/content/images/2020/10/phong-long-chim-ngan-le-mot-dem.jpg', 1, N'A special suite designed for newlyweds with romantic amenities such as a king-size bed, a Jacuzzi or a balcony with a view.', CAST(999.00 AS Decimal(18, 2)), 1, 6)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (23, N'HS-603', N'https://blog.grabhotel.net/content/images/2020/10/phong-long-chim-ngan-le-mot-dem.jpg', 1, N'A special suite designed for newlyweds with romantic amenities such as a king-size bed, a Jacuzzi or a balcony with a view.', CAST(999.00 AS Decimal(18, 2)), 1, 6)
INSERT [dbo].[Room_Hotel] ([room_id], [room_name], [room_image], [room_bed], [room_desc], [room_price], [room_status], [type_id]) VALUES (24, N'HS-604', N'https://blog.grabhotel.net/content/images/2020/10/phong-long-chim-ngan-le-mot-dem.jpg', 1, N'A special suite designed for newlyweds with romantic amenities such as a king-size bed, a Jacuzzi or a balcony with a view.', CAST(999.00 AS Decimal(18, 2)), 1, 6)
SET IDENTITY_INSERT [dbo].[Room_Hotel] OFF
GO
SET IDENTITY_INSERT [dbo].[Type_Room] ON 

INSERT [dbo].[Type_Room] ([type_id], [type_name], [type_image], [type_desc], [type_status]) VALUES (1, N'Single Room', N'https://www.mailamhotel.com/images/product/goc/goc_1615972559.jpg', N'A room designed to accommodate one guest with a single bed.', 1)
INSERT [dbo].[Type_Room] ([type_id], [type_name], [type_image], [type_desc], [type_status]) VALUES (2, N'Double Room', N'https://www.mailamhotel.com/images/product/goc/goc_1615361772.jpg', N'A room designed to accommodate two guests with a double bed or two twin beds.', 1)
INSERT [dbo].[Type_Room] ([type_id], [type_name], [type_image], [type_desc], [type_status]) VALUES (3, N'Twin Room', N'https://www.mailamhotel.com/images/product/goc/goc_1615361444.jpg', N' A room designed to accommodate two guests with two separate single beds.', 1)
INSERT [dbo].[Type_Room] ([type_id], [type_name], [type_image], [type_desc], [type_status]) VALUES (4, N'Connecting Rooms', N'http://www.residencebaron.hu/images/info/11_44_pic_93.jpg', N'Two or more rooms that are connected by a door and are often booked together for families or groups.', 1)
INSERT [dbo].[Type_Room] ([type_id], [type_name], [type_image], [type_desc], [type_status]) VALUES (5, N'Family Room', N'https://www.mailamhotel.com/images/product/goc/goc_1615973314.jpg', N'A room designed to accommodate families with children, often with additional beds or a sofa bed.', 1)
INSERT [dbo].[Type_Room] ([type_id], [type_name], [type_image], [type_desc], [type_status]) VALUES (6, N'Honeymoon Suite', N'https://blog.grabhotel.net/content/images/2020/10/phong-long-chim-ngan-le-mot-dem.jpg', N'A special suite designed for newlyweds with romantic amenities such as a king-size bed, a Jacuzzi or a balcony with a view.', 1)
SET IDENTITY_INSERT [dbo].[Type_Room] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([user_id], [user_phone], [user_password], [user_name], [user_image], [user_wallet], [user_status]) VALUES (1, N'0987654321          ', N'123456', N'admin', N'admin', CAST(1000000000.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[Order_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Order_Detail_Order] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO
ALTER TABLE [dbo].[Order_Detail] CHECK CONSTRAINT [FK_Order_Detail_Order]
GO
ALTER TABLE [dbo].[Order_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Order_Detail_Room_Hotel] FOREIGN KEY([room_id])
REFERENCES [dbo].[Room_Hotel] ([room_id])
GO
ALTER TABLE [dbo].[Order_Detail] CHECK CONSTRAINT [FK_Order_Detail_Room_Hotel]
GO
ALTER TABLE [dbo].[Room_Hotel]  WITH CHECK ADD  CONSTRAINT [FK_Room_Hotel_Type_Room] FOREIGN KEY([type_id])
REFERENCES [dbo].[Type_Room] ([type_id])
GO
ALTER TABLE [dbo].[Room_Hotel] CHECK CONSTRAINT [FK_Room_Hotel_Type_Room]
GO
USE [master]
GO
ALTER DATABASE [Booking_Hotel_DB] SET  READ_WRITE 
GO
