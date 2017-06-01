USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[EditUserIsActive]    Script Date: 6/1/2017 9:46:46 PM ******/
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
