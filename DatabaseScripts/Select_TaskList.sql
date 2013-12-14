USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_TaskList]    Script Date: 12/13/2013 9:45:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_TaskList] 
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
		   ExcludeHiddenFiles
	FROM Task

END

GO

