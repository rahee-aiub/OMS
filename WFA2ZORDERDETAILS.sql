USE [A2ZACOMS]
GO

/****** Object:  Table [dbo].[WFA2ZORDERDETAILS]    Script Date: 04/07/2019 10:02:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[WFA2ZORDERDETAILS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderNo] [bigint] NULL,
	[OrderDate] [smalldatetime] NULL,
	[RecType] [smallint] NULL,
	[OrderParty] [int] NULL,
	[ItemCode] [int] NULL,
	[ItemName] [varchar](50) NULL,
	[ItemImage] [image] NULL,
	[ItemKarat] [smallint] NULL,
	[ItemSize] [varchar](50) NULL,
	[ItemLength] [varchar](50) NULL,
	[ItemPiece] [int] NULL,
	[ItemWide] [varchar](50) NULL,
	[ItemColor] [varchar](50) NULL,
	[ItemWeight] [money] NULL,
	[ItemTotalWeight] [money] NULL,
	[WayToOrder] [int] NULL,
	[WayToOrderNo] [varchar](30) NULL,
	[WhoseOrder] [varchar](50) NULL,
	[WhoseOrderPhone] [varchar](30) NULL,
	[DeliveryPossibleDate] [smalldatetime] NULL,
	[OrderStatus] [int] NULL,
	[OrderStatusDesc] [varchar](50) NULL,
	[OrderStatusDate] [smalldatetime] NULL,
	[FactoryParty] [int] NULL,
	[SendToFactoryNote] [varchar](100) NULL,
	[SendToFactoryDate] [smalldatetime] NULL,
	[FactoryReceiveNote] [varchar](100) NULL,
	[FactoryReceiveDate] [smalldatetime] NULL,
	[FactoryWhomReceive] [varchar](50) NULL,
	[FactoryWhomPhone] [varchar](30) NULL,
	[ReadyInFactoryNote] [varchar](100) NULL,
	[ReadyInFactoryDate] [smalldatetime] NULL,
	[ReadyInFactoryWeight] [money] NULL,
	[SendToTransitDate] [smalldatetime] NULL,
	[SendToTransitWeight] [money] NULL,
	[ReceiveFromTransitDate] [smalldatetime] NULL,
	[ReceiveFromTransitWeight] [money] NULL,
	[OrderDeliveryDate] [smalldatetime] NULL,
	[OrderDeliveryWeight] [money] NULL,
	[OrderReEditDate] [smalldatetime] NULL,
	[OrderCancelNote] [varchar](100) NULL,
	[UserId] [int] NULL,
	[TrnDate] [smalldatetime] NULL,
	[TrnVchNo] [nvarchar](20) NULL,
	[TrnPartyAccNo] [bigint] NULL,
	[TrnPartyName] [nvarchar](50) NULL,
	[TrnWeight] [money] NULL,
	[OrderPartyName] [varchar](100) NULL,
	[FactoryPartyName] [varchar](100) NULL,
	[TransitWhomReceive] [varchar](50) NULL,
	[DeliveryWhomReceive] [varchar](50) NULL,
	[Remarks] [varchar](50) NULL,
 CONSTRAINT [PK_WFA2ZORDERDETAILS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

