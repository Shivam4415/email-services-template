CREATE TABLE [gapsnap].[SMTP_SERVERS](
	[Name] [varchar](20) NOT NULL,
	[Host] [varchar](100) NOT NULL,
	[Port] [int] NOT NULL,
	[UserName] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[IsActive] [bit] NULL,
	[IsDefault] [bit] NULL,
	[FromAddress] [varchar](50) NULL,
 CONSTRAINT [PK_SMTP_SERVERS_1] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_SMTP_SERVERS_PORT_HOST_USERNAME] UNIQUE NONCLUSTERED 
(
	[UserName] ASC,
	[Host] ASC,
	[Port] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]