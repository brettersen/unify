USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_ExemptionOperatorList]    Script Date: 12/13/2013 8:54:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_ExemptionOperatorList]
AS
BEGIN

	SET NOCOUNT ON;

    SELECT ExemptionOperatorId,
	       OperatorName
	FROM ExemptionOperator
	ORDER BY ExemptionOperatorId ASC

END

GO

