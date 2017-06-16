USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetCardValByShapeAndRealVal]    Script Date: 04/06/2017 12:52:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCardValByShapeAndRealVal] @shape varchar(10), @val int 
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [Card Value]
FROM Card
WHERE [Card Shpe] = @shape AND [Card Real Value]= @val
END



GO
