USE [A2ZACOMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_UpdateOrderTransaction]    Script Date: 03/25/2019 11:15:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO














ALTER PROCEDURE  [dbo].[Sp_UpdateOrderTransaction](@VchNo VARCHAR(20),@TrnDesc VARCHAR(50),@WhomReceive VARCHAR(50),@UserId int)

AS
BEGIN



DECLARE @TransitWeight money;
DECLARE @processDate SMALLDATETIME;

DECLARE  @AccNo  BIGINT;
DECLARE  @RefAccNo  BIGINT;
DECLARE  @FuncOpt  INT;

 /*

 EXECUTE Sp_UpdateOrderTransaction 

 */


	


BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON


		SET @processDate = (SELECT ProcessDate FROM A2ZACOMS..A2ZCSPARAMETER);

		DECLARE wfTranTable CURSOR FOR
        SELECT AccNo,RefAccNo,FuncOpt,TrnAmount
        FROM WFA2ZTRANSACTION WHERE UserId = @UserId AND TrnFlag = 0;

        OPEN wfTranTable;
        FETCH NEXT FROM wfTranTable INTO
        @AccNo,@RefAccNo,@FuncOpt,@TransitWeight;

        WHILE @@FETCH_STATUS = 0 
	    BEGIN
		
        IF @FuncOpt = 3
		   BEGIN
		        UPDATE A2ZORDERDETAILS SET ReceiveFromTransitDate = @processDate,TransitWhomReceive = @WhomReceive, 
					               ReceiveFromTransitWeight = isnull(ReceiveFromTransitWeight,0) + @TransitWeight 									
                FROM A2ZORDERDETAILS
                WHERE OrderNo = @RefAccNo; 
		   END
		
		IF @FuncOpt = 4
		   BEGIN
		        UPDATE A2ZORDERDETAILS SET OrderDeliveryDate = @processDate, DeliveryWhomReceive = @WhomReceive,  
					               OrderDeliveryWeight = isnull(OrderDeliveryWeight,0) + @TransitWeight 									
                FROM A2ZORDERDETAILS
                WHERE OrderNo = @RefAccNo; 
		   END


		FETCH NEXT FROM wfTranTable INTO
        @AccNo,@RefAccNo,@FuncOpt,@TransitWeight;        


	    END

        CLOSE wfTranTable; 
        DEALLOCATE wfTranTable;



		INSERT INTO A2ZTRANSACTION (TrnDate,VchNo,AccType,AccNo,RefAccType,RefAccNo,
	    Location,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,TrnDesc,TrnDebitAmt,TrnCreditAmt,TrnAmount,
		TrnFlag,TrnProcStat,UserId)

	    SELECT TrnDate,@VchNo,AccType,AccNo,RefAccType,RefAccNo,
	    Location,FuncOpt,FuncOptDesc,PayType,TrnType,TrnDrCr,@TrnDesc,TrnDebitAmt,TrnCreditAmt,TrnAmount,
		TrnFlag,TrnProcStat,UserId FROM WFA2ZTRANSACTION WHERE UserId = @UserId;


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

