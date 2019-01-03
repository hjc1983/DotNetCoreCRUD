
USE [DotNetCore]
GO

IF NOT EXISTS ( SELECT  * FROM sys.schemas WHERE name = N'Project' ) 
    EXEC('CREATE SCHEMA [Project]');
GO


IF OBJECT_ID('Project.Movie') IS NULL
BEGIN
	CREATE TABLE [Project].[Movie]
	(
		[Id] [bigint] IDENTITY(1,1) NOT NULL,

		[Title] [varchar](255) NULL,
		[Description] [varchar](1000) NULL,
		[Price] [decimal](6, 2) NOT NULL,
		[ReleaseDate] [datetime] NOT NULL,
		[CreatedDate] [datetime] NOT NULL,
		[ModifiedDate] [datetime] NOT NULL
		
		CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	) ON [PRIMARY]
END
GO