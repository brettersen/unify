USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_Task]    Script Date: 12/10/2013 9:29:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_Task] 
AS
BEGIN

	SET NOCOUNT ON;

	SELECT TaskId,
		   TaskName,
		   TaskDescription,
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

END

GO

