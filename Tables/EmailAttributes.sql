CREATE TABLE [gapsnap].[EmailAttributes](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[ObjectPropertyName] [varchar](150) NOT NULL,
	[PlaceholderTag] [varchar](150) NOT NULL,
	[ObjectTypeId] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [gapsnap].[EmailAttributes]  WITH CHECK ADD FOREIGN KEY([ObjectTypeId])
REFERENCES [gapsnap].[ObjectType] ([Id])