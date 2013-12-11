USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Update_Routine]    Script Date: 12/10/2013 9:13:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Update_Routine]
	
	@RoutineId				INT,
	@RoutineName			VARCHAR(50),
	@RoutineDescription		VARCHAR(500),
	@UpdatedOn				DATETIME2			OUT

AS
BEGIN

	SET NOCOUNT ON

	UPDATE Routine
	SET RoutineName = @RoutineName,
	    RoutineDescription = @RoutineDescription,
		UpdatedOn = GETDATE()
	WHERE RoutineId = @RoutineId

	SELECT @UpdatedOn = UpdatedOn
	FROM Routine
	WHERE RoutineId = @RoutineId

END

GO

