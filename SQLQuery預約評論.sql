USE [Flex]
GO
/****** Object:  Table [dbo].[ReservationReviews]    Script Date: 2023/8/20 下午 07:31:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationReviews](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fk_speakerId] [int] NOT NULL,
	[fk_memberId] [int] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[creationTime] [datetime] NOT NULL,
	[rating] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ReservationReviews] ADD  DEFAULT (getdate()) FOR [creationTime]
GO
ALTER TABLE [dbo].[ReservationReviews]  WITH CHECK ADD FOREIGN KEY([fk_memberId])
REFERENCES [dbo].[Members] ([MemberId])
GO
ALTER TABLE [dbo].[ReservationReviews]  WITH CHECK ADD FOREIGN KEY([fk_speakerId])
REFERENCES [dbo].[Speakers] ([SpeakerId])
GO
