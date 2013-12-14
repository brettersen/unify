USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_RoutineList]    Script Date: 12/12/2013 8:39:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_RoutineList]
AS
BEGIN

	SET NOCOUNT ON;

    SELECT RoutineId,
	       RoutineName,
		   CreatedOn,
		   UpdatedOn
	FROM Routine

END

GO

