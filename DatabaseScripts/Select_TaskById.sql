USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_TaskById]    Script Date: 12/12/2013 8:40:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_TaskById]
	
	@TaskId		INT

AS
BEGIN

	SET NOCOUNT ON;

    SELECT TaskId,
		   SourceDirectory,
		   DestinationDirectory,
		   AddFiles,
		   ReplaceFiles,
		   RemoveFiles,
	       SearchRecursively,
		   ExcludeHiddenFiles,
		   CreatedOn,
		   UpdatedOn
	FROM Task
	WHERE TaskId = @TaskId

END

GO

