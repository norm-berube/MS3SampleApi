USE [MS3_Sample]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 9/18/2021 3:12:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO

/****** Object:  Table [dbo].[Address]    Script Date: 9/18/2021 3:12:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Address](
	[PKID] [int] IDENTITY(1,1) NOT NULL,
	[IdFK] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Street] [varchar](150) NULL,
	[Unit] [varchar](10) NULL,
	[City] [varchar](150) NOT NULL,
	[State] [varchar](2) NULL,
	[ZipCode] [varchar](10) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO

