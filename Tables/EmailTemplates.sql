CREATE TABLE [gapsnap].[EmailTemplates](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[EmailTypeId] [tinyint] NULL,
	[HTMLTemplate] [varchar](8000) NULL,
	[TextTemplate] [varchar](4000) NULL,
	[Subject] [varchar](500) NOT NULL,
	[MailPriority] [tinyint] NULL,
	[FromEmail] [varchar](150) NULL,
	[FromName] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [gapsnap].[EmailTemplates]  WITH CHECK ADD FOREIGN KEY([EmailTypeId])
REFERENCES [gapsnap].[EmailTypes] ([Id])
GO
ALTER TABLE [gapsnap].[EmailTemplates] ADD  DEFAULT ((2)) FOR [MailPriority]