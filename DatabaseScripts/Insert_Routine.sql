USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Insert_Routine]    Script Date: 12/12/2013 8:39:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insert_Routine]

	@RoutineId				INT				OUT,
	@RoutineName			VARCHAR(50),
	@CreatedOn				DATETIME2		OUT,
	@UpdatedOn				DATETIME2		OUT

AS
BEGIN

	SET NOCOUNT ON;

    INSERT INTO Routine
	(
		RoutineName
	)
	VALUES
	(
		@RoutineName
	)

	SELECT @RoutineId = IDENT_CURRENT('Routine')

	SELECT @CreatedOn = CreatedOn,
	       @UpdatedOn = UpdatedOn
	FROM Routine
	WHERE RoutineId = @RoutineId

END

GO

