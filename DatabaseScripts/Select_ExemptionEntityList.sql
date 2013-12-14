USE [Unify]
GO

/****** Object:  StoredProcedure [dbo].[Select_ExemptionEntityList]    Script Date: 12/13/2013 8:52:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Select_ExemptionEntityList]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT ExemptionEntityId,
	       EntityName
	FROM ExemptionEntity
	ORDER BY ExemptionEntityId ASC

END

GO

