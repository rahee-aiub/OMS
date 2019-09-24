USE [A2ZACOMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_OrderFactoryReady]    Script Date: 02/13/2019 3:14:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO











CREATE PROCEDURE  [dbo].[Sp_OrderFactoryReady](@OrderNo bigint,@Description VARCHAR(100),@OrderStatus int,@OrderStatusDesc varchar(50))

AS
BEGIN

DECLARE @processDate SMALLDATETIME;


 /*

 EXECUTE Sp_OrderFactoryReady 'OR00001','2018-10-10',110001, 'BALA','1','Rajon','2018-11-11', 1, 'REC', '2018-10-12',1,'BALA',1,1,1,1,1,'white',1,1,'Rajon'


 	
 */





BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON

		SET @processDate = (SELECT ProcessDate FROM A2ZACOMS..A2ZCSPARAMETER);


		UPDATE A2ZORDERDETAILS SET OrderStatus = @OrderStatus , OrderStatusDesc = @OrderStatusDesc,OrderStatusDate = @processDate, FactoryReadyNote=@Description,FactoryReadyDate=@processDate WHERE OrderNo = @OrderNo


		




COMMIT TRANSACTION
		SET NOCOUNT OFF
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION

		DECLARE @ErrorSeverity INT
		DECLARE @ErrorState INT
		DECLARE @ErrorMessage NVARCHAR(4000);	  
		SELECT 
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE();	  
		RAISERROR 
		(
			@ErrorMessage, -- Message text.
			@ErrorSeverity, -- Severity.
			@ErrorState -- State.
		);	
END CATCH


END;











GO

