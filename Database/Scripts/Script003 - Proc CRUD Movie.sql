USE [DotNetCore]
GO


IF EXISTS (SELECT * FROM sysobjects WHERE name = 'cusp_MovieSelect' AND user_name(uid) = 'dbo')
	DROP PROCEDURE [dbo].cusp_MovieSelect
GO

CREATE PROCEDURE [dbo].cusp_MovieSelect
AS
	SET NOCOUNT ON;
SELECT TOP(50) * FROM [project].[Movie]
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'cusp_MovieSelectId' AND user_name(uid) = 'dbo')
	DROP PROCEDURE [dbo].cusp_MovieSelectId
GO

CREATE PROCEDURE [dbo].cusp_MovieSelectId
	 @Id int 
AS
	SET NOCOUNT ON;
SELECT * FROM [project].[Movie]
WHERE (Id = @Id)
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'cusp_MovieInsert' AND user_name(uid) = 'dbo')
	DROP PROCEDURE [dbo].cusp_MovieInsert
GO

CREATE PROCEDURE [dbo].cusp_MovieInsert
(
	@Title varchar(255),
	@Description varchar(1000),
	@Price decimal(6, 2),
	@ReleaseDate datetime
)
AS
	SET NOCOUNT OFF;
INSERT INTO [project].[Movie] ([Title], [Description], [Price], [ReleaseDate], [CreatedDate], [ModifiedDate]) VALUES (@Title, @Description, @Price, @ReleaseDate, GETDATE(), GETDATE());
	
SELECT Id, Title, Description, Price, ReleaseDate, CreatedDate, ModifiedDate FROM project.Movie WHERE (Id = SCOPE_IDENTITY())
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'cusp_MovieUpdate' AND user_name(uid) = 'dbo')
	DROP PROCEDURE [dbo].cusp_MovieUpdate
GO

CREATE PROCEDURE [dbo].cusp_MovieUpdate
(
	@Title varchar(255),
	@Description varchar(1000),
	@Price decimal(6, 2),
	@ReleaseDate datetime,
	@Id bigint
)
AS
	SET NOCOUNT OFF;
UPDATE [project].[Movie] SET [Title] = @Title, [Description] = @Description, [Price] = @Price, [ReleaseDate] = @ReleaseDate, [ModifiedDate] = GETDATE() WHERE (([Id] = @Id));
	
SELECT Id, Title, Description, Price, ReleaseDate, CreatedDate, ModifiedDate FROM project.Movie WHERE (Id = @Id)
GO

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'cusp_MovieDelete' AND user_name(uid) = 'dbo')
	DROP PROCEDURE [dbo].cusp_MovieDelete
GO

CREATE PROCEDURE [dbo].cusp_MovieDelete
(
	@Id bigint
)
AS
	SET NOCOUNT OFF;
DELETE FROM [project].[Movie] WHERE (([Id] = @Id))
GO

