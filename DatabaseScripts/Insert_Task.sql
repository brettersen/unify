USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Insert_Task]    Script Date: 12/10/2013 8:49:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insert_Task]
	
	@TaskId					INT				OUT,
	@TaskName				VARCHAR(50),
	@TaskDescription		VARCHAR(500),
	@SourceDirectory		VARCHAR(255),
	@DestinationDirectory	VARCHAR(255),
	@AddFiles				BIT,
	@ReplaceFiles			BIT,
	@RemoveFiles			BIT,
	@SearchRecursively		BIT,
	@ExcludeHiddenFiles		BIT,
	@CreatedOn				DATETIME2		OUT,
	@UpdatedOn				DATETIME2		OUT

AS
BEGIN

	SET NOCOUNT ON;

    INSERT INTO Task
	(
		TaskName,
		TaskDescription,
		SourceDirectory,
		DestinationDirectory,
		AddFiles,
		ReplaceFiles,
		RemoveFiles,
		SearchRecursively,
		ExcludeHiddenFiles
	)
	VALUES
	(
		@TaskName,
		@TaskDescription,
		@SourceDirectory,
		@DestinationDirectory,
		@AddFiles,
		@ReplaceFiles,
		@RemoveFiles,
		@SearchRecursively,
		@ExcludeHiddenFiles
	)

	SELECT @TaskId = IDENT_CURRENT('Task')

	SELECT @CreatedOn = CreatedOn,
	       @UpdatedOn = UpdatedOn
	FROM Task
	WHERE TaskId = @TaskId

END

GO

