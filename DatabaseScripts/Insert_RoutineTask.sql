USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Insert_RoutineTask]    Script Date: 12/10/2013 9:01:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insert_RoutineTask]
	
	@RoutineId		INT,
	@TaskId			INT,
	@TaskIndex		TINYINT

AS
BEGIN

	SET NOCOUNT ON;

    INSERT INTO RoutineTask
	(
		RoutineId,
		TaskId,
		TaskIndex
	)
	VALUES
	(
		@RoutineId,
		@TaskId,
		@TaskIndex
	)

END

GO

