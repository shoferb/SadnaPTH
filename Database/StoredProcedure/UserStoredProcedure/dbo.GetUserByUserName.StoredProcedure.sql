USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 6/1/2017 9:46:46 PM ******/
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
