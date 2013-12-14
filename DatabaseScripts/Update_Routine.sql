USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Update_Routine]    Script Date: 12/12/2013 8:40:33 PM ******/
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
	@UpdatedOn				DATETIME2			OUT

AS
BEGIN

	SET NOCOUNT ON

	UPDATE Routine
	SET RoutineName = @RoutineName,
		UpdatedOn = GETDATE()
	WHERE RoutineId = @RoutineId

	SELECT @UpdatedOn = UpdatedOn
	FROM Routine
	WHERE RoutineId = @RoutineId

END

GO

