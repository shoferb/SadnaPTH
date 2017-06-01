USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[EditUserLeagueName]    Script Date: 6/1/2017 9:46:46 PM ******/
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
