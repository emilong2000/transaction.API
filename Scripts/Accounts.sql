USE [TransDB]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 18/02/2021 1:39:20 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Accounts](
	[ID] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Balance] [decimal](18, 2) NULL,
	[PersonID] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

