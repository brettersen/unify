USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Insert_Routine]    Script Date: 12/10/2013 8:55:58 PM ******/
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
	@RoutineDescription		VARCHAR(500),
	@CreatedOn				DATETIME2		OUT,
	@UpdatedOn				DATETIME2		OUT

AS
BEGIN

	SET NOCOUNT ON;

    INSERT INTO Routine
	(
		RoutineName,
		RoutineDescription
	)
	VALUES
	(
		@RoutineName,
		@RoutineDescription
	)

	SELECT @RoutineId = IDENT_CURRENT('Routine')

	SELECT @CreatedOn = CreatedOn,
	       @UpdatedOn = UpdatedOn
	FROM Routine
	WHERE RoutineId = @RoutineId

END

GO

