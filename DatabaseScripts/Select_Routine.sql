USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_Routine]    Script Date: 12/10/2013 9:25:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_Routine]
AS
BEGIN

	SET NOCOUNT ON;

    SELECT RoutineId,
	       RoutineName,
		   RoutineDescription,
		   CreatedOn,
		   UpdatedOn
	FROM Routine

END

GO

