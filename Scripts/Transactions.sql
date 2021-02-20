USE [TransDB]
GO

/****** Object:  Table [dbo].[Transactions]    Script Date: 18/02/2021 1:39:51 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transactions](
	[ID] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[TransactionType] [int] NULL,
	[DrAccountID] [uniqueidentifier] NULL,
	[CrAccountID] [uniqueidentifier] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

