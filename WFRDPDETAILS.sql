USE [A2ZACOMS]
GO

/****** Object:  Table [dbo].[WFRDPDETAILS]    Script Date: 04/07/2019 10:03:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WFRDPDETAILS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrnDate] [smalldatetime] NULL,
	[LineSL] [int] NULL,
	[PartyCode1] [bigint] NULL,
	[PartyName1] [varchar](50) NULL,
	[Weight1] [money] NULL,
	[PartyCode2] [bigint] NULL,
	[PartyName2] [varchar](50) NULL,
	[Weight2] [money] NULL,
	[PartyCode3] [bigint] NULL,
	[PartyName3] [varchar](50) NULL,
	[Weight3] [money] NULL,
 CONSTRAINT [PK_WFRDPDETAILS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

