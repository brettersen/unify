USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Update_Task]    Script Date: 12/10/2013 9:18:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Task]
	
	@TaskId					INT,
	@TaskName				VARCHAR(50),
	@TaskDescription		VARCHAR(500),
	@SourceDirectory		VARCHAR(255),
	@DestinationDirectory	VARCHAR(255),
	@AddFiles				BIT,
	@ReplaceFiles			BIT,
	@RemoveFiles			BIT,
	@SearchRecursively		BIT,
	@ExcludeHiddenFiles		BIT,
	@UpdatedOn				DATETIME2		OUT

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE Task
	SET TaskName = @TaskName,
	    TaskDescription = @TaskDescription,
		SourceDirectory = @SourceDirectory,
		DestinationDirectory = @DestinationDirectory,
		AddFiles = @AddFiles,
		ReplaceFiles = @ReplaceFiles,
		RemoveFiles = @RemoveFiles,
		SearchRecursively = @SearchRecursively,
		ExcludeHiddenFiles = @ExcludeHiddenFiles,
		UpdatedOn = GETDATE()
	WHERE TaskId = @TaskId

	SELECT @UpdatedOn = UpdatedOn
	FROM Task
	WHERE TaskId = @TaskId

END

GO

