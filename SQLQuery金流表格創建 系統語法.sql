USE [Flex]
GO
/****** Object:  Table [dbo].[EcpayOrders]    Script Date: 2023/8/20 下午 07:26:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EcpayOrders](
	[MerchantTradeNo] [nvarchar](50) NOT NULL,
	[MemberID] [int] NOT NULL,
	[RtnCode] [int] NULL,
	[RtnMsg] [nvarchar](50) NULL,
	[TradeNo] [nvarchar](50) NULL,
	[TradeAmt] [int] NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentType] [nvarchar](50) NULL,
	[PaymentTypeChargeFee] [nvarchar](50) NULL,
	[TradeDate] [nvarchar](50) NULL,
	[SimulatePaid] [int] NULL,
	[TradeDesc] [nvarchar](300) NULL,
	[ItemId] [int] NULL,
	[fk_typeId] [int] NULL,
 CONSTRAINT [PK_EcpayOrders] PRIMARY KEY CLUSTERED 
(
	[MerchantTradeNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EcpayOrders]  WITH CHECK ADD  CONSTRAINT [FK_EcpayOrders_Type] FOREIGN KEY([fk_typeId])
REFERENCES [dbo].[Type] ([TypeId])
GO
ALTER TABLE [dbo].[EcpayOrders] CHECK CONSTRAINT [FK_EcpayOrders_Type]
GO
