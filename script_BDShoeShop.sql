CREATE DATABASE BDShoeShop;
USE BDShoeShop
GO
CREATE TABLE [dbo].[stores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) COLLATE Modern_Spanish_CI_AS NULL,
	[address] [varchar](2000) COLLATE Modern_Spanish_CI_AS NULL,
 CONSTRAINT [PK_stores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

CREATE TABLE [dbo].[articles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) COLLATE Modern_Spanish_CI_AS NULL,
	[description] [varchar](max) COLLATE Modern_Spanish_CI_AS NULL,
	[total_in_shelf] [int] NULL,
	[total_in_vault] [int] NULL,
	[store_id] [int] NULL,
 CONSTRAINT [PK_articles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [BDShoeShop]
GO
ALTER TABLE [dbo].[articles]  WITH CHECK ADD  CONSTRAINT [FK_articles_stores] FOREIGN KEY([store_id])
REFERENCES [dbo].[stores] ([id])

INSERT INTO [BDShoeShop].[dbo].[stores]
           ([name]
           ,[address])
     VALUES
           ('Tienda Prueba 1','Costa Rica')

INSERT INTO [BDShoeShop].[dbo].[articles]
           ([name]
           ,[description]
           ,[total_in_shelf]
           ,[total_in_vault]
           ,[store_id])
     VALUES
           ('Articulo Prueba','Articulo Prueba',100,500,1)

