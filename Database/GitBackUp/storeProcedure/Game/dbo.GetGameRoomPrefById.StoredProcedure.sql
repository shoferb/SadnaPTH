USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomPrefById]    Script Date: 15/06/2017 17:16:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomPrefById] @roomid int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoomPreferance
WHERE RoomId = @roomId 
END




GO
