USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetSpectetorByRoomIdAndUserId]    Script Date: 03/06/2017 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSpectetorByRoomIdAndUserId] @UserId int, @RoomId int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM SpectetorGamesOfUser
WHERE userId = @UserId AND roomId=@RoomId
END


GO
