USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetGameModeValByName]    Script Date: 04/06/2017 15:28:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameModeValByName] @name varchar(10)
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [Game mode value]
FROM GameMode
WHERE [game mode name] = @name
END




GO
