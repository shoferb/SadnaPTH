USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetAllActiveGameRooms]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllActiveGameRooms]
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom
WHERE isActive = 1
END



GO
/****** Object:  StoredProcedure [dbo].[GetAllSpectableGameRooms]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllSpectableGameRooms] @canSpec bit
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.CanSpectate = @canSpec
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomById]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomById] @id int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom
WHERE RoomId = @id
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomReplyById]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomReplyById] @id int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT RoomId, Replay
FROM GameRoom
WHERE RoomId = @id
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByBuyInPolicy]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByBuyInPolicy] @biPol int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.BuyInPolicy = @biPol
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByGameMode]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByGameMode] @mode int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.GameMode = @mode
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMaxPlayers]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByMaxPlayers] @max int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.MaxPlayers = @max
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMinBet]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByMinBet] @min int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.MinBet = @min
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMinPlayers]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByMinPlayers] @min int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.MinPlayers = @min
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByPotSize]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByPotSize] @potSize int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.PotSize = @potSize
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByStaringChip]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomsByStaringChip] @scpol int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT GameRoom.RoomId, GameRoom.GameXML
FROM GameRoomPreferance join GameRoom on GameRoomPreferance.Roomid = GameRoom.RoomId
WHERE GameRoomPreferance.EnterGamePolicy = @scpol
END



GO
/****** Object:  StoredProcedure [dbo].[InsertGameRoomToDb]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertGameRoomToDb] @roomid int, @gameid int, @replay varchar(MAX),
                        @gameXML XML, @isActive bit
AS
BEGIN 
      Insert into GameRoom values(@roomid, @gameid, @replay, @gameXML, @isActive)
END





GO
/****** Object:  StoredProcedure [dbo].[InsertPrefToDb]    Script Date: 14/06/2017 13:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertPrefToDb] @room_id int, @is_Spectetor bit, @Min_player_in_room int,
                        @max_player_in_room int, @BuyInPolicy int, @starting_chip int,
                       @minBet int, @League_name int, @Game_Mode int, @Game_Id int, @potSize int
AS
BEGIN 
      Insert into GameRoomPreferance values( @room_id , @is_Spectetor, @Min_player_in_room,
                        @max_player_in_room , @BuyInPolicy , @starting_chip ,
                        @minBet, @League_name , @Game_Mode , @Game_Id, @potSize )
END





GO
