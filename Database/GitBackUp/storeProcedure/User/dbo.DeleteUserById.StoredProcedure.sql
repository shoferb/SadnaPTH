USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[DeleteUserById]    Script Date: 6/3/2017 2:03:03 PM ******/
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
