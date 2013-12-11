USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Delete_RoutineById]    Script Date: 12/10/2013 9:59:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_RoutineById]
	
	@RoutineId		INT

AS
BEGIN

	SET NOCOUNT ON;

    DELETE FROM Routine
	WHERE RoutineId = @RoutineId

	DELETE FROM RoutineTask
	WHERE RoutineId = @RoutineId

END

GO

