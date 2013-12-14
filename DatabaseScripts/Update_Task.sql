USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Update_Task]    Script Date: 12/13/2013 9:46:56 PM ******/
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
	@TaskIndex				TINYINT,
	@SourceDirectory		VARCHAR(255),
	@DestinationDirectory	VARCHAR(255),
	@AddFiles				BIT,
	@ReplaceFiles			BIT,
	@RemoveFiles			BIT,
	@SearchRecursively		BIT,
	@ExcludeHiddenFiles		BIT

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE Task
	SET TaskIndex = @TaskIndex,
	    SourceDirectory = @SourceDirectory,
		DestinationDirectory = @DestinationDirectory,
		AddFiles = @AddFiles,
		ReplaceFiles = @ReplaceFiles,
		RemoveFiles = @RemoveFiles,
		SearchRecursively = @SearchRecursively,
		ExcludeHiddenFiles = @ExcludeHiddenFiles
	WHERE TaskId = @TaskId

END

GO

