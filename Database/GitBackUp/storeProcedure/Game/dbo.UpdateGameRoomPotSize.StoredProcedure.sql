USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[UpdateGameRoomPotSize]    Script Date: 14/06/2017 13:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateGameRoomPotSize] @newPotSize int, @id int
AS
BEGIN 
     SET NOCOUNT ON 
UPDATE GameRoomPreferance
SET PotSize = @newPotSize
WHERE Roomid = @id
END




GO
