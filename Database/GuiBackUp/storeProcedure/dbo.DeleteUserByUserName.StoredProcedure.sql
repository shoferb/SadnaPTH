USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[DeleteUserByUserName]    Script Date: 6/3/2017 2:03:03 PM ******/
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
