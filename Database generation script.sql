USE [master]
/****** Object:  Database [Questions_DB]    Script Date: 5/9/2024 8:51:03 AM ******/
--Database creation
GO
CREATE DATABASE [Questions_DB]
GO
ALTER DATABASE [Questions_DB] SET COMPATIBILITY_LEVEL = 160

--tables creation
USE [Questions_DB]
/****** Object:  Table [dbo].[Question]    Script Date: 5/9/2024 8:51:03 AM ******/
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[Q_id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Q_text] [varchar](350) NULL,
	[Q_order] [int] NOT NULL,
	[Q_type] [varchar](13) NOT NULL 
	)
GO
/****** Object:  Table [dbo].[Slider]    Script Date: 5/9/2024 8:51:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slider](
	[Q_id] [int] NULL,
	[Start_value] [int] NOT NULL,
	[End_value] [int] NOT NULL,
	[Start_value_caption] [varchar](25) NOT NULL,
	[End_value_caption] [varchar](25) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Smiley]    Script Date: 5/9/2024 8:51:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Smiley](
	[Q_id] [int] NULL,
	[Num_of_faces] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stars]    Script Date: 5/9/2024 8:51:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stars](
	[Q_id] [int] NULL,
	[Num_of_stars] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Slider]  WITH CHECK ADD FOREIGN KEY([Q_id])
REFERENCES [dbo].[Question] ([Q_id])
GO
ALTER TABLE [dbo].[Smiley]  WITH CHECK ADD FOREIGN KEY([Q_id])
REFERENCES [dbo].[Question] ([Q_id])
GO
ALTER TABLE [dbo].[Stars]  WITH CHECK ADD FOREIGN KEY([Q_id])
REFERENCES [dbo].[Question] ([Q_id])
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [CK__Question__Q_type__37A5467C] CHECK  (([Q_type]='Smiley' OR [Q_type]='Stars' OR [Q_type]='Slider'))
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [CK__Question__Q_type__37A5467C]
GO
USE [master]
GO
ALTER DATABASE [Questions_DB] SET  READ_WRITE 
GO
