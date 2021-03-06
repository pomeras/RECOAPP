USE [RECO]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 9/18/2018 6:54:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[Id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](150) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 9/18/2018 6:54:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[Id] [uniqueidentifier] NOT NULL,
	[number] [int] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[documentId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[createdBy] [nvarchar](150) NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Document] ([Id], [name], [createdDate], [createdBy]) VALUES (N'90fdd68d-abc7-4d7b-8bd7-2f5ece9adfa2', N'Test5', CAST(N'2018-09-18T15:30:45.000' AS DateTime), N'dbo3')
GO
INSERT [dbo].[Document] ([Id], [name], [createdDate], [createdBy]) VALUES (N'bfa87547-737f-431a-a69f-db7e60ac0cbc', N'Test 3', CAST(N'2018-09-18T17:06:34.320' AS DateTime), N'dbo')
GO
INSERT [dbo].[Page] ([Id], [number], [content], [documentId], [createdDate], [createdBy]) VALUES (N'2f87d7c1-b6e8-4cef-8400-86260755a495', 2, N'Content 1', N'90fdd68d-abc7-4d7b-8bd7-2f5ece9adfa2', CAST(N'2018-09-18T17:33:36.700' AS DateTime), N'dbo')
GO
INSERT [dbo].[Page] ([Id], [number], [content], [documentId], [createdDate], [createdBy]) VALUES (N'0a11bb8b-b67a-4b7e-9d32-a770440c4956', 1, N'Test Content', N'90fdd68d-abc7-4d7b-8bd7-2f5ece9adfa2', CAST(N'2018-09-18T16:09:47.273' AS DateTime), N'dbo')
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Document] FOREIGN KEY([Id])
REFERENCES [dbo].[Document] ([Id])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Document]
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Document] FOREIGN KEY([documentId])
REFERENCES [dbo].[Document] ([Id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Document]
GO
USE [master]
GO
ALTER DATABASE [RECO] SET  READ_WRITE 
GO
