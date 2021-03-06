CREATE TABLE [gapsnap].[EMAIL_CATEGORY](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[UNSUBSCRIBE_MESSAGE] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]