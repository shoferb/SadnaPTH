USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetHandStepNameByVal]    Script Date: 03/06/2017 17:00:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetHandStepNameByVal] @Val int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM HandStep
WHERE [hand Step value] = @Val
END



GO
