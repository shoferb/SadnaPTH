USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetAllGames]    Script Date: 03/06/2017 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllGames] 
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom
END


GO
