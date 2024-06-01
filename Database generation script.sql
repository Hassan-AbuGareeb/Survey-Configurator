IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Questions_DB')
BEGIN
    CREATE DATABASE [Questions_DB]
    ALTER DATABASE [Questions_DB] SET COMPATIBILITY_LEVEL = 160
END
GO

USE [Questions_DB]
GO

IF OBJECT_ID('dbo.Question', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Question](
        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
        [Text] [nvarchar](350) NOT NULL,
        [Order] [int] NOT NULL,
        [Type] [int] NOT NULL
    )
END
GO

IF OBJECT_ID('dbo.QuestionType', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[QuestionType](
        [Id] [int] NOT NULL PRIMARY KEY,
        [Type] [varchar](30)NOT NULL
    )
END
GO

IF OBJECT_ID('dbo.QuestionType', 'U') IS NOT NULL
BEGIN
	IF (NOT EXISTS (SELECT 1 FROM QuestionType))
	Begin
		INSERT INTO QuestionType ([Id], [Type])
		Values 
		(0, 'Stars'),
		(1, 'Smiley'),
		(2, 'Slider')
	END
END
GO

IF OBJECT_ID('dbo.Slider', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Slider](
        [Id] [int] NOT NULL,
        [StartValue] [int] NOT NULL,
        [EndValue] [int] NOT NULL,
        [StartValueCaption] [nvarchar](25) NOT NULL,
        [EndValueCaption] [nvarchar](25) NOT NULL
    )

    ALTER TABLE [dbo].[Slider] WITH CHECK ADD FOREIGN KEY([Id])
    REFERENCES [dbo].[Question] ([Id])
END
GO

IF OBJECT_ID('dbo.Smiley', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Smiley](
        [Id] [int] NOT NULL,
        [NumberOfFaces] [int] NOT NULL
    )

    ALTER TABLE [dbo].[Smiley] WITH CHECK ADD FOREIGN KEY([Id])
    REFERENCES [dbo].[Question] ([Id])
END
GO

IF OBJECT_ID('dbo.Stars', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Stars](
        [Id] [int] NOT NULL,
        [NumberOfStars] [int] NOT NULL
    )

    ALTER TABLE [dbo].[Stars] WITH CHECK ADD FOREIGN KEY([Id])
    REFERENCES [dbo].[Question] ([Id])
END
GO

ALTER TABLE [dbo].[Question] WITH CHECK ADD FOREIGN KEY([Type])
REFERENCES [dbo].[QuestionType] ([Id])
GO

ALTER DATABASE [Questions_DB] SET READ_WRITE 
GO
