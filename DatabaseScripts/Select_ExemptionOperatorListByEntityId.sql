USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_ExemptionOperatorListByEntityId]    Script Date: 12/13/2013 8:56:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_ExemptionOperatorListByEntityId]

	@ExemptionEntityId		INT

AS
BEGIN

	SET NOCOUNT ON;

    SELECT o.ExemptionOperatorId,
	       o.OperatorName
	FROM ExemptionOperator o
	JOIN ExemptionEntityOperator eo
	ON o.ExemptionOperatorId = eo.ExemptionOperatorId
	WHERE eo.ExemptionEntityId = @ExemptionEntityId
	ORDER BY ExemptionOperatorId ASC

END

GO

