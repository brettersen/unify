USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Insert_Task]    Script Date: 12/13/2013 9:47:34 PM ******/
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
	@RoutineId				INT,
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

    INSERT INTO Task
	(
		RoutineId,
		TaskIndex,
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
		@RoutineId,
		@TaskIndex,
		@SourceDirectory,
		@DestinationDirectory,
		@AddFiles,
		@ReplaceFiles,
		@RemoveFiles,
		@SearchRecursively,
		@ExcludeHiddenFiles
	)

	SELECT @TaskId = IDENT_CURRENT('Task')

END

GO

