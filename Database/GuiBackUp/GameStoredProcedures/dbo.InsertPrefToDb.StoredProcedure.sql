USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[InsertPrefToDb]    Script Date: 04/06/2017 15:41:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertPrefToDb] @room_id int, @is_Spectetor bit, @Min_player_in_room int,
                        @max_player_in_room int, @enter_paying_money int, @starting_chip int,
                        @Bb int, @Sb int, @League_name int, @Game_Mode int, @Game_Id int
AS
BEGIN 
      Insert into GameRoomPreferance values( @room_id , @is_Spectetor, @Min_player_in_room,
                        @max_player_in_room , @enter_paying_money , @starting_chip ,
                        @Bb , @Sb , @League_name , @Game_Mode , @Game_Id )
END




GO
