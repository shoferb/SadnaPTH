USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetPublicCardsByRoomId]    Script Date: 03/06/2017 15:24:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetPublicCardsByRoomId] @RoomId int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM [Public Cards]
WHERE [room Id]=@RoomId
END


GO
