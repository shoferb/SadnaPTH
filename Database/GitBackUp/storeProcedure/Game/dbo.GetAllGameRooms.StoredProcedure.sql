USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetAllGameRooms]    Script Date: 14/06/2017 13:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllGameRooms]
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom

END





GO
