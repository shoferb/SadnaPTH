USE [DataBaseSadna]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[logId] [int] NOT NULL,
	[msg] [varchar](150) NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[logId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GameMode]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GameMode](
	[Game mode value] [int] NOT NULL,
	[game mode name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GameMode] PRIMARY KEY CLUSTERED 
(
	[Game mode value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GameRoom]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GameRoom](
	[RoomId] [int] NOT NULL,
	[GameId] [int] NOT NULL,
	[Replay] [varchar](max) NOT NULL,
	[GameXML] [xml] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_GameRoom] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC,
	[GameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GameRoomPreferance]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameRoomPreferance](
	[Roomid] [int] NOT NULL,
	[CanSpectate] [bit] NULL,
	[MinPlayers] [int] NULL,
	[MaxPlayers] [int] NULL,
	[BuyInPolicy] [int] NULL,
	[EnterGamePolicy] [int] NULL,
	[MinBet] [int] NULL,
	[League] [int] NULL,
	[GameMode] [int] NULL,
	[GameId] [int] NOT NULL,
	[PotSize] [int] NULL,
 CONSTRAINT [PK_GameRoomPreferance] PRIMARY KEY CLUSTERED 
(
	[Roomid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeagueName]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LeagueName](
	[League Value] [int] NOT NULL,
	[League Name] [varchar](10) NULL,
 CONSTRAINT [PK_LeagueName] PRIMARY KEY CLUSTERED 
(
	[League Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Log]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[LogId] [int] NOT NULL,
	[LogPriority] [int] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Players]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Players](
	[room Id] [int] NOT NULL,
	[user Id] [int] NOT NULL,
	[is player active] [bit] NOT NULL,
	[player name] [varchar](50) NOT NULL,
	[Total chip] [int] NOT NULL,
	[Round chip bet] [int] NOT NULL,
	[Player action the round] [bit] NOT NULL,
	[first card] [int] NOT NULL,
	[secund card] [int] NOT NULL,
	[Game Id] [int] NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[room Id] ASC,
	[user Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PriorityLogEnum]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PriorityLogEnum](
	[PriorityValue] [int] NOT NULL,
	[ProprityName] [varchar](10) NOT NULL,
 CONSTRAINT [PK_PriorityLogEnum] PRIMARY KEY CLUSTERED 
(
	[PriorityValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SpectetorGamesOfUser]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpectetorGamesOfUser](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[Game Id] [int] NOT NULL,
 CONSTRAINT [PK_SpectetorGamesOfUser] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemLog]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemLog](
	[logId] [int] NOT NULL,
	[msg] [varchar](150) NULL,
	[roomId] [int] NOT NULL,
	[game Id] [int] NOT NULL,
 CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED 
(
	[logId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserActiveGames]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActiveGames](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[Game Id] [int] NOT NULL,
 CONSTRAINT [PK_UserActiveGames] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserReplaySavedGames]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserReplaySavedGames](
	[userId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[gameId] [int] NOT NULL,
 CONSTRAINT [PK_UserReplaySavedGames] PRIMARY KEY CLUSTERED 
(
	[roomId] ASC,
	[gameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTable]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserTable](
	[userId] [int] NOT NULL,
	[username] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[avatar] [varchar](150) NOT NULL,
	[points] [int] NOT NULL,
	[money] [int] NOT NULL,
	[gamesPlayed] [int] NOT NULL,
	[leagueName] [int] NOT NULL,
	[winNum] [int] NOT NULL,
	[HighestCashGainInGame] [int] NOT NULL,
	[TotalProfit] [int] NOT NULL,
	[inActive] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (1, N'Error: while trying ceate user - id: 95959595 is NOT free')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (2, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (3, N'Error: while trying ceate user - username: orelie95959596 is NOT free')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (4, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (5, N'Error: while trying ceate user with id84848485 and username: orelie84848485email: oreliepost.bgu.ac.il is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (6, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (7, N'Error: while trying ceate user with id465465487 and username: orelie465465487password is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (8, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (9, N'Error: while trying register user: name is empty')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (10, N'Error: while trying ceate user with id2070596700 and username: yardenpassword is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (11, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (12, N'Error: while trying register user:money smaller than zero')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (32, N'Error while trying to remove game room')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (35, N'Error while trying to remove game room')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (75, N'Error while tring to add player to room - user with Id: 8585000 to room: 9999 user is already a player in this room')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (96, N'Error: while trying ceate user - id: 90520650 is NOT free')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (97, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (98, N'Error: while trying ceate user - username: orelie880088052 is NOT free')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (99, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (100, N'Error: while trying ceate user with id5522525 and username: RegisterToSystemTest_bad_Not_Valid_email()email: oreliepost.bgu.ac.il is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (101, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (102, N'Error: while trying ceate user with id55225265 and username: RegisterToSystemTest_bad_Not_Valid_passWord()password is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (103, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (104, N'Error: while trying register user: name is empty')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (105, N'Error: while trying ceate user - id: -1 is NOT free')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (106, N'Error: while trying register user: onr or more Invalid fields')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (107, N'Error: while trying register user:money smaller than zero')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (108, N'Error: while trying ceate user - id: -1 is NOT free')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (109, N'Error: while trying ceate user - username:   is NOT free')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (110, N'Error: while trying ceate user with id305077901 and username: orelie26email: oreliepost.bgu.ac.il is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (111, N'Error: while trying ceate user with id305077901 and username: orelie26email: oreliepost.bgu.ac.il is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (112, N'Error: while trying ceate user with id305077901 and username: orelie26password is not valid')
INSERT [dbo].[ErrorLog] ([logId], [msg]) VALUES (113, N'Error: while trying ceate user with id305077901 and username: orelie26password is not valid')
INSERT [dbo].[GameMode] ([Game mode value], [game mode name]) VALUES (1, N'Limit')
INSERT [dbo].[GameMode] ([Game mode value], [game mode name]) VALUES (2, N'PotLimit')
INSERT [dbo].[GameMode] ([Game mode value], [game mode name]) VALUES (3, N'NoLimit')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (1, N'A')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (2, N'B')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (3, N'C')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (4, N'D')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (5, N'E')
INSERT [dbo].[LeagueName] ([League Value], [League Name]) VALUES (6, N'UnKnow')
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (1, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (2, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (3, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (4, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (5, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (6, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (7, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (8, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (9, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (10, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (11, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (12, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (32, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (35, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (75, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (96, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (97, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (98, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (99, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (100, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (101, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (102, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (103, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (104, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (105, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (106, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (107, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (108, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (109, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (110, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (111, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (112, 1)
INSERT [dbo].[Log] ([LogId], [LogPriority]) VALUES (113, 1)
INSERT [dbo].[PriorityLogEnum] ([PriorityValue], [ProprityName]) VALUES (1, N'Info')
INSERT [dbo].[PriorityLogEnum] ([PriorityValue], [ProprityName]) VALUES (2, N'Warnning')
INSERT [dbo].[PriorityLogEnum] ([PriorityValue], [ProprityName]) VALUES (3, N'Error')
ALTER TABLE [dbo].[ErrorLog]  WITH CHECK ADD  CONSTRAINT [FK_ErrorLog_Log] FOREIGN KEY([logId])
REFERENCES [dbo].[Log] ([LogId])
GO
ALTER TABLE [dbo].[ErrorLog] CHECK CONSTRAINT [FK_ErrorLog_Log]
GO
ALTER TABLE [dbo].[GameRoomPreferance]  WITH CHECK ADD  CONSTRAINT [FK_GameRoomPreferance_GameMode1] FOREIGN KEY([GameMode])
REFERENCES [dbo].[GameMode] ([Game mode value])
GO
ALTER TABLE [dbo].[GameRoomPreferance] CHECK CONSTRAINT [FK_GameRoomPreferance_GameMode1]
GO
ALTER TABLE [dbo].[GameRoomPreferance]  WITH CHECK ADD  CONSTRAINT [FK_GameRoomPreferance_GameRoom1] FOREIGN KEY([Roomid], [GameId])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[GameRoomPreferance] CHECK CONSTRAINT [FK_GameRoomPreferance_GameRoom1]
GO
ALTER TABLE [dbo].[GameRoomPreferance]  WITH CHECK ADD  CONSTRAINT [FK_GameRoomPreferance_LeagueName1] FOREIGN KEY([League])
REFERENCES [dbo].[LeagueName] ([League Value])
GO
ALTER TABLE [dbo].[GameRoomPreferance] CHECK CONSTRAINT [FK_GameRoomPreferance_LeagueName1]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_PriorityLogEnum] FOREIGN KEY([LogPriority])
REFERENCES [dbo].[PriorityLogEnum] ([PriorityValue])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_PriorityLogEnum]
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD  CONSTRAINT [FK_Players_GameRoom] FOREIGN KEY([room Id], [Game Id])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[Players] CHECK CONSTRAINT [FK_Players_GameRoom]
GO
ALTER TABLE [dbo].[Players]  WITH CHECK ADD  CONSTRAINT [FK_Players_User] FOREIGN KEY([user Id])
REFERENCES [dbo].[UserTable] ([userId])
GO
ALTER TABLE [dbo].[Players] CHECK CONSTRAINT [FK_Players_User]
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser]  WITH CHECK ADD  CONSTRAINT [FK_SpectetorGamesOfUser_GameRoom] FOREIGN KEY([roomId], [Game Id])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser] CHECK CONSTRAINT [FK_SpectetorGamesOfUser_GameRoom]
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser]  WITH CHECK ADD  CONSTRAINT [FK_SpectetorGamesOfUser_User] FOREIGN KEY([userId])
REFERENCES [dbo].[UserTable] ([userId])
GO
ALTER TABLE [dbo].[SpectetorGamesOfUser] CHECK CONSTRAINT [FK_SpectetorGamesOfUser_User]
GO
ALTER TABLE [dbo].[SystemLog]  WITH CHECK ADD  CONSTRAINT [FK_SystemLog_GameRoom] FOREIGN KEY([roomId], [game Id])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[SystemLog] CHECK CONSTRAINT [FK_SystemLog_GameRoom]
GO
ALTER TABLE [dbo].[SystemLog]  WITH CHECK ADD  CONSTRAINT [FK_SystemLog_Log] FOREIGN KEY([logId])
REFERENCES [dbo].[Log] ([LogId])
GO
ALTER TABLE [dbo].[SystemLog] CHECK CONSTRAINT [FK_SystemLog_Log]
GO
ALTER TABLE [dbo].[UserActiveGames]  WITH CHECK ADD  CONSTRAINT [FK_UserActiveGames_GameRoom] FOREIGN KEY([roomId], [Game Id])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[UserActiveGames] CHECK CONSTRAINT [FK_UserActiveGames_GameRoom]
GO
ALTER TABLE [dbo].[UserActiveGames]  WITH CHECK ADD  CONSTRAINT [FK_UserActiveGames_User] FOREIGN KEY([userId])
REFERENCES [dbo].[UserTable] ([userId])
GO
ALTER TABLE [dbo].[UserActiveGames] CHECK CONSTRAINT [FK_UserActiveGames_User]
GO
ALTER TABLE [dbo].[UserReplaySavedGames]  WITH CHECK ADD  CONSTRAINT [FK_UserReplaySavedGames_GameRoom] FOREIGN KEY([roomId], [gameId])
REFERENCES [dbo].[GameRoom] ([RoomId], [GameId])
GO
ALTER TABLE [dbo].[UserReplaySavedGames] CHECK CONSTRAINT [FK_UserReplaySavedGames_GameRoom]
GO
ALTER TABLE [dbo].[UserReplaySavedGames]  WITH CHECK ADD  CONSTRAINT [FK_UserReplaySavedGames_User] FOREIGN KEY([userId])
REFERENCES [dbo].[UserTable] ([userId])
GO
ALTER TABLE [dbo].[UserReplaySavedGames] CHECK CONSTRAINT [FK_UserReplaySavedGames_User]
GO
/****** Object:  StoredProcedure [dbo].[AddNewUser]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create procedure [dbo].[AddNewUser]  
(  @userId int,
	@username varchar(50),
	@name varchar(50),
	@email varchar(50),
	@password varchar(50), 
	@avatar varchar(50),
	@points int,
	@money int,
	@gamesPlayed int,	
	@leagueName int,
	@winNum int,
	@highestCashGainInGame int,
	@totalProfit int,
	@isActive bit
)  
as  
begin  
   Insert into UserTable values(@userId,
								@username,
								@name,
								@email,
								@password, 
								@avatar,
								@points,
								@money,
								@gamesPlayed,	
								@leagueName,
								@winNum,
								@highestCashGainInGame,
								@totalProfit,
								@isActive 
	)  
End 



GO
/****** Object:  StoredProcedure [dbo].[DeleteErrorLogById]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[DeleteErrorLogById]
       @LogId  int 
       
AS
BEGIN 
      SET NOCOUNT ON 

      DELETE dbo.ErrorLog
      FROM   dbo.ErrorLog
      WHERE  
      LogId = @LogId                  

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteGameRoom]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteGameRoomPref]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteGameRoomPref] @roomId int
AS
BEGIN 
     SET NOCOUNT ON 
DELETE 
FROM GameRoomPreferance
WHERE Roomid = @roomId 
END




GO
/****** Object:  StoredProcedure [dbo].[DeleteLogById]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[DeleteLogById]
       @LogId  int 
       
AS
BEGIN 
      SET NOCOUNT ON 

      DELETE dbo.Log
      FROM   dbo.Log
      WHERE  
      LogId = @LogId                  

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteSystemLogById]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[DeleteSystemLogById]
       @LogId  int 
       
AS
BEGIN 
      SET NOCOUNT ON 

      DELETE dbo.SystemLog
      FROM   dbo.SystemLog
      WHERE  
      LogId = @LogId                  

END


GO
/****** Object:  StoredProcedure [dbo].[DeleteUserById]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUserById]
       @UserId  int 
       
AS
BEGIN 
      SET NOCOUNT ON 

      DELETE dbo.UserTable
      FROM   dbo.UserTable
      WHERE  
      userId = @UserId                  

END



GO
/****** Object:  StoredProcedure [dbo].[DeleteUserByUserName]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUserByUserName]
       @UserName  varchar(50)
       
AS
BEGIN 
      SET NOCOUNT ON 

      DELETE dbo.UserTable
      FROM   dbo.UserTable
      WHERE  
      username = @UserName                 

END



GO
/****** Object:  StoredProcedure [dbo].[EditAvatar]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditAvatar]
       @UserId  int ,
       @newAvatar varchar(50)
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             avatar   = @newAvatar
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditEmail]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditEmail]
       @UserId  int ,
       @newEmail varchar(50)
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             email   = @newEmail
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditName]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditName]
       @UserId  int ,
       @newName varchar(50)
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             name   = @newName
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditPassword]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditPassword]
       @UserId  int ,
       @newPassword varchar(50)
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
              password  = @newPassword
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserHighestCashGainInGame]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserHighestCashGainInGame]
       @UserId  int ,
       @newHighestCashGainInGame int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             HighestCashGainInGame   = @newHighestCashGainInGame
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserId]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserId]
       @NewUserId  int ,
       @oldUserId int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             userId   = @NewUserId
      FROM   dbo.UserTable
      WHERE  
      userId    = @oldUserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserIsActive]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserIsActive]
       @UserId  int ,
       @newIsActive bit
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             inActive   = @newIsActive
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserLeagueName]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserLeagueName]
       @UserId  int ,
       @newLeague int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             leagueName   = @newLeague
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserMoney]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserMoney]
       @UserId  int ,
       @newMoney int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             money   = @newMoney
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUsername]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUsername]
       @UserId  int ,
       @newUserName varchar(50)
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             username   = @newUserName
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserNumOfGamesPlayed]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserNumOfGamesPlayed]
       @UserId  int ,
       @newNumOfGame int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             gamesPlayed   = @newNumOfGame
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserPoints]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserPoints]
       @UserId  int ,
       @newPoints int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             points   = @newPoints
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserTotalProfit]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserTotalProfit]
       @UserId  int ,
       @newTotalProfit int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             TotalProfit   = @newTotalProfit
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[EditUserWinNum]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditUserWinNum]
       @UserId  int ,
       @newWinNum int
AS
BEGIN 
      SET NOCOUNT ON 

      UPDATE dbo.UserTable
      SET 
             winNum   = @newWinNum
      FROM   dbo.UserTable
      WHERE  
      userId    = @UserId                  

END


GO
/****** Object:  StoredProcedure [dbo].[GetAllActiveGameRooms]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllGameRooms]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllSpectableGameRooms]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllUser]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllUser]
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM UserTable

END


GO
/****** Object:  StoredProcedure [dbo].[GetErrorLogById]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetErrorLogById]
        @LogId int
       
AS
BEGIN 
      SET NOCOUNT ON 

      SELECT *
      FROM   dbo.ErrorLog
      WHERE  
      LogId = @LogId                

END


GO
/****** Object:  StoredProcedure [dbo].[GetGameModeNameByVal]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameModeNameByVal] @Val int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [game mode name]
FROM GameMode
WHERE [Game mode value] = @Val
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameModeValByName]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomById]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGameRoomById] @roomid int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM GameRoom
WHERE RoomId = @roomid
ORDER BY GameId DESC
END



GO
/****** Object:  StoredProcedure [dbo].[GetGameRoomPrefById]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomReplyById]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByBuyInPolicy]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByGameMode]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMaxPlayers]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMinBet]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByMinPlayers]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByPotSize]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetGameRoomsByStaringChip]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[GetLeageValByName]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLeageValByName] @name varchar(10)
AS
BEGIN 
     SET NOCOUNT ON 
SELECT [League Value]
FROM LeagueName
WHERE [League Name] = @name
END




GO
/****** Object:  StoredProcedure [dbo].[GetSystemLogById]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[GetSystemLogById]
        @LogId int
       
AS
BEGIN 
      SET NOCOUNT ON 

      SELECT *
      FROM   dbo.SystemLog
      WHERE  
      LogId = @LogId                

END


GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserId]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByUserId] @userId int
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM UserTable
WHERE userId = @userId
END


GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetUserByUserName] @username varchar(50)
AS
BEGIN 
     SET NOCOUNT ON 
SELECT *
FROM UserTable
WHERE username = @username
END


GO
/****** Object:  StoredProcedure [dbo].[InsertGameRoomToDb]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertPrefToDb]    Script Date: 16/06/2017 13:22:30 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateGameRoom]    Script Date: 16/06/2017 13:22:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateGameRoom] @roomId int, @gameid int, @newXML XML, @newIsActive bit, @newRep varchar(MAX)
AS
BEGIN 
     SET NOCOUNT ON 
UPDATE GameRoom
SET GameXML = @newXML, isActive = @newIsActive, Replay = @newRep
WHERE Roomid = @roomId AND GameId = @gameid
END




GO
/****** Object:  StoredProcedure [dbo].[UpdateGameRoomPotSize]    Script Date: 16/06/2017 13:22:30 ******/
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
