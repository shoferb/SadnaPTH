USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[DeleteGameRoom]    Script Date: 14/06/2017 15:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteGameRoom] @roomId int, @gameid int
AS
BEGIN 
     SET NOCOUNT ON 
DELETE 
FROM GameRoom
WHERE Roomid = @roomId AND GameId = @gameid
END




GO
