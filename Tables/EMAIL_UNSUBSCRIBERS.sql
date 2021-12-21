CREATE TABLE [gapsnap].[EMAIL_UNSUBSCRIBERS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [varchar](250) NOT NULL,
	[Email_Category_Id] [tinyint] NOT NULL,
	[Date_Unsubscribed] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [gapsnap].[EMAIL_UNSUBSCRIBERS]  WITH CHECK ADD  CONSTRAINT [FK_NEWSLETTER_UNSUBSCRIBERS_NEWSLETTER_CATEGORY] FOREIGN KEY([Email_Category_Id])
REFERENCES [gapsnap].[EMAIL_CATEGORY] ([Id])
GO

ALTER TABLE [gapsnap].[EMAIL_UNSUBSCRIBERS] CHECK CONSTRAINT [FK_NEWSLETTER_UNSUBSCRIBERS_NEWSLETTER_CATEGORY]