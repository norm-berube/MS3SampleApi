USE [MS3_Sample]
GO

/****** Object:  Table [dbo].[Client]    Script Date: 9/18/2021 3:30:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Client]') AND type in (N'U'))
DROP TABLE [dbo].[Client]
GO

/****** Object:  Table [dbo].[Client]    Script Date: 9/18/2021 3:30:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Client](
	[PKID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NOT NULL,
	[DOB] [datetime2](7) NULL,
	[Gender] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[UpdatedBy] [varchar](50) NOT NULL,
	[UpdatedOn] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO


