USE [master]
GO
/****** Object:  Database [bd_bazar]    Script Date: 15/03/2021 16:10:32 ******/
CREATE DATABASE [bd_bazar]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bd_bazar', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\bd_bazar.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bd_bazar_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\bd_bazar_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [bd_bazar] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bd_bazar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bd_bazar] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bd_bazar] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bd_bazar] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bd_bazar] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bd_bazar] SET ARITHABORT OFF 
GO
ALTER DATABASE [bd_bazar] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [bd_bazar] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bd_bazar] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bd_bazar] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bd_bazar] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bd_bazar] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bd_bazar] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bd_bazar] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bd_bazar] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bd_bazar] SET  ENABLE_BROKER 
GO
ALTER DATABASE [bd_bazar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bd_bazar] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bd_bazar] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bd_bazar] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bd_bazar] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bd_bazar] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bd_bazar] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bd_bazar] SET RECOVERY FULL 
GO
ALTER DATABASE [bd_bazar] SET  MULTI_USER 
GO
ALTER DATABASE [bd_bazar] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bd_bazar] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bd_bazar] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bd_bazar] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [bd_bazar] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'bd_bazar', N'ON'
GO
ALTER DATABASE [bd_bazar] SET QUERY_STORE = OFF
GO
USE [bd_bazar]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [bd_bazar]
GO
/****** Object:  Table [dbo].[administrador]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[administrador](
	[idAdministrador] [int] IDENTITY(1,1) NOT NULL,
	[cedula] [varchar](13) NULL,
	[nombre] [varchar](250) NULL,
	[apellido] [varchar](250) NULL,
	[telefono] [varchar](10) NULL,
	[mail] [varchar](250) NULL,
	[usuario] [varchar](100) NULL,
	[contra] [varchar](100) NULL,
	[activo] [bit] NULL,
	[fecha_registro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idAdministrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categoria]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoria](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[idCliente] [int] IDENTITY(1,1) NOT NULL,
	[idCobertura] [int] NULL,
	[nombre] [varchar](150) NULL,
	[apellido] [varchar](150) NULL,
	[celular] [varchar](10) NULL,
	[direccion] [varchar](500) NULL,
	[correo] [varchar](100) NULL,
	[contra] [varchar](100) NULL,
	[activo] [bit] NULL,
	[fecha_registro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cobertura]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cobertura](
	[idCobertura] [int] IDENTITY(1,1) NOT NULL,
	[ciudad] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[idCobertura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[configuracion]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[configuracion](
	[idConfiguracion] [int] IDENTITY(1,1) NOT NULL,
	[about] [text] NULL,
	[telefono] [varchar](15) NULL,
	[fecha_registro] [datetime] NULL,
	[direccion] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[idConfiguracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detallePedido]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detallePedido](
	[idDetalle] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NULL,
	[precio] [decimal](8, 2) NULL,
	[cantidad] [int] NULL,
	[descuento] [int] NULL,
	[idPedido] [int] NULL,
	[subtotal] [decimal](8, 2) NULL,
	[envio] [decimal](8, 2) NULL,
	[iva] [decimal](8, 2) NULL,
	[total] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[devolucion]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[devolucion](
	[idDevolucion] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[idPedido] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDevolucion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estado]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estado](
	[idEstado] [int] IDENTITY(1,1) NOT NULL,
	[estado] [varchar](100) NULL,
	[activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[imagenes_producto]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[imagenes_producto](
	[idImagen] [int] IDENTITY(1,1) NOT NULL,
	[referencia] [text] NULL,
	[idProducto] [int] NULL,
	[size] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[idImagen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[logpedido]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logpedido](
	[idLog] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NULL,
	[fecha] [datetime] NULL,
	[idEstado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[oferta]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oferta](
	[idOferta] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NULL,
	[porcentaje] [int] NULL,
	[descripcion] [text] NULL,
	[fecha_inicio] [datetime] NULL,
	[fecha_fin] [datetime] NULL,
	[activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idOferta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pedido]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedido](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[fecha_pedido] [datetime] NULL,
	[idEstado] [int] NULL,
	[idCliente] [int] NULL,
	[comentario] [varchar](500) NULL,
	[total] [decimal](8, 2) NULL,
	[subtotal] [decimal](8, 2) NULL,
	[iva] [decimal](8, 2) NULL,
	[envio] [decimal](8, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[descripcion] [text] NULL,
	[pvp] [decimal](8, 2) NULL,
	[stock] [decimal](8, 2) NULL,
	[iva] [decimal](8, 2) NULL,
	[paga_envio] [bit] NULL,
	[idCategoria] [int] NULL,
	[fecha_registro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recuperaClave]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recuperaClave](
	[idRecupera] [int] IDENTITY(1,1) NOT NULL,
	[fecha_inicio] [datetime] NULL,
	[fecha_fin] [datetime] NULL,
	[validacion] [varchar](500) NULL,
	[activo] [bit] NULL,
	[idCliente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRecupera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reposicion]    Script Date: 15/03/2021 16:10:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reposicion](
	[idReposicion] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NULL,
	[cantidad] [decimal](8, 2) NULL,
	[fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idReposicion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[administrador] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[administrador] ADD  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[cliente] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[cliente] ADD  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[configuracion] ADD  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD FOREIGN KEY([idCobertura])
REFERENCES [dbo].[cobertura] ([idCobertura])
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[detallePedido]  WITH CHECK ADD  CONSTRAINT [FK_detallePedido_pedido] FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])
GO
ALTER TABLE [dbo].[detallePedido] CHECK CONSTRAINT [FK_detallePedido_pedido]
GO
ALTER TABLE [dbo].[devolucion]  WITH CHECK ADD FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])
GO
ALTER TABLE [dbo].[imagenes_producto]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[logpedido]  WITH CHECK ADD FOREIGN KEY([idEstado])
REFERENCES [dbo].[estado] ([idEstado])
GO
ALTER TABLE [dbo].[logpedido]  WITH CHECK ADD FOREIGN KEY([idPedido])
REFERENCES [dbo].[pedido] ([idPedido])
GO
ALTER TABLE [dbo].[oferta]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD FOREIGN KEY([idCliente])
REFERENCES [dbo].[cliente] ([idCliente])
GO
ALTER TABLE [dbo].[pedido]  WITH CHECK ADD FOREIGN KEY([idEstado])
REFERENCES [dbo].[estado] ([idEstado])
GO
ALTER TABLE [dbo].[producto]  WITH CHECK ADD FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categoria] ([idCategoria])
GO
ALTER TABLE [dbo].[recuperaClave]  WITH CHECK ADD FOREIGN KEY([idCliente])
REFERENCES [dbo].[cliente] ([idCliente])
GO
ALTER TABLE [dbo].[reposicion]  WITH CHECK ADD  CONSTRAINT [FK_reposicion_producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[producto] ([idProducto])
GO
ALTER TABLE [dbo].[reposicion] CHECK CONSTRAINT [FK_reposicion_producto]
GO
USE [master]
GO
ALTER DATABASE [bd_bazar] SET  READ_WRITE 
GO
