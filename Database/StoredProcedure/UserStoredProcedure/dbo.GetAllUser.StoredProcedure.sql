USE [DataBaseSadna]
GO
/****** Object:  StoredProcedure [dbo].[GetAllUser]    Script Date: 6/1/2017 9:46:46 PM ******/
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
