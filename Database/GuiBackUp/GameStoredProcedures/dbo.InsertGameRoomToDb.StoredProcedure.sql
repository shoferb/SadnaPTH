USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[InsertGameRoomToDb]    Script Date: 04/06/2017 13:40:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertGameRoomToDb] @Bb int, @Bb_Player int, @curr_Player int, @curr_player_position int,
                        @Dealer_Player int, @Dealer_position int, @First_Player_In_round int, @first_player_in_round_position int,
                        @game_id int, @hand_step int, @is_Active_Game bit, @last_rise_in_round int, @league_name int,
                        @Max_Bet_In_Round int, @Pot_count int, @room_Id int, @Sb int, @SB_player int
AS
BEGIN 
      Insert into GameRoom values(@room_Id, @game_Id, @Dealer_position, @Max_Bet_In_Round, @Pot_count,
								 @Bb,@Sb,@is_Active_Game, @curr_Player, @Dealer_Player, @Bb_Player, @SB_player,
								 @hand_step, @First_Player_In_round, @curr_player_position, @first_player_in_round_position,
								 @last_rise_in_round, @league_name)
END




GO
