USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Update_RoutineTask]    Script Date: 12/10/2013 9:21:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_RoutineTask]
	
	@RoutineId		INT,
	@TaskId			INT,
	@TaskIndex		TINYINT

AS
BEGIN

	SET NOCOUNT ON;

    UPDATE RoutineTask
	SET TaskIndex = @TaskIndex
	WHERE RoutineId = @RoutineId
	AND TaskId = @TaskId

END

GO

