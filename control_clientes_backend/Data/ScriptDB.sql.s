CREATE DATABASE ControlClientesDB;
GO

USE ControlClientesDB;
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[Clientes](
    [ClienteId] [int] IDENTITY(1,1) NOT NULL,
    [Nombre] [nvarchar](max) NOT NULL,
    [Email] [nvarchar](max) NOT NULL,
    [Telefono] [nvarchar](max) NOT NULL,
    [OtrosDatos] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

ALTER TABLE [dbo].[Clientes] ADD CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
    [ClienteId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO

CREATE TABLE [dbo].[Direcciones](
    [DireccionId] [int] IDENTITY(1,1) NOT NULL,
    [ClienteId] [int] NOT NULL,
    [Calle] [nvarchar](max) NOT NULL,
    [Sector] [nvarchar](max) NOT NULL,
    [Provincia] [nvarchar](max) NOT NULL,
    [Pais] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

ALTER TABLE [dbo].[Direcciones] ADD CONSTRAINT [PK_Direcciones] PRIMARY KEY CLUSTERED 
(
    [DireccionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO

CREATE NONCLUSTERED INDEX [IX_Direcciones_ClienteId] ON [dbo].[Direcciones]
(
    [ClienteId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO

ALTER TABLE [dbo].[Direcciones] WITH CHECK ADD CONSTRAINT [FK_Direcciones_Clientes_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([ClienteId])
ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[Direcciones] CHECK CONSTRAINT [FK_Direcciones_Clientes_ClienteId];
GO
