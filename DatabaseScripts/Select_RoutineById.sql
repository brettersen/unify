USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_RoutineById]    Script Date: 12/12/2013 8:39:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_RoutineById]
	
	@RoutineId		INT

AS
BEGIN

	SET NOCOUNT ON;

	SELECT RoutineId,
	       RoutineName,
		   CreatedOn,
		   UpdatedOn
	FROM Routine
	WHERE RoutineId = @RoutineId

END

GO

