USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_TaskByRoutineId]    Script Date: 12/10/2013 9:35:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_TaskByRoutineId]

	@RoutineId		INT

AS
BEGIN

	SET NOCOUNT ON;

    SELECT t.TaskId,
		   t.TaskName,
		   t.TaskDescription,
		   t.SourceDirectory,
		   t.DestinationDirectory,
		   t.AddFiles,
		   t.ReplaceFiles,
		   t.RemoveFiles,
	       t.SearchRecursively,
		   t.ExcludeHiddenFiles,
		   t.CreatedOn,
		   t.UpdatedOn
	FROM Task t
	JOIN RoutineTask rt
	ON t.TaskId = rt.TaskId
	WHERE rt.RoutineId = @RoutineId

END

GO

