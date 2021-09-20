USE [MS3_Sample]
GO

ALTER TABLE [dbo].[Contact] DROP CONSTRAINT [DF_Contact_Preferred]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 9/18/2021 3:13:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact]') AND type in (N'U'))
DROP TABLE [dbo].[Contact]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 9/18/2021 3:13:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contact](
	[PKID] [int] IDENTITY(1,1) NOT NULL,
	[IdFK] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Value] [varchar](250) NOT NULL,
	[Preferred] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_Preferred]  DEFAULT ((0)) FOR [Preferred]
