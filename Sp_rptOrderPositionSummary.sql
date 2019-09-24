USE [A2ZACOMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptOrderPositionSummary]    Script Date: 03/25/2019 1:57:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE [dbo].[Sp_rptOrderPositionSummary] (@OrderParty int,@fDate VARCHAR(10),@tDate VARCHAR(10))


AS

/*


EXECUTE Sp_rptOrderPositionSummary 0,'2019-01-02','2019-01-02'



*/


BEGIN

DECLARE @strSQL NVARCHAR(MAX);

DECLARE @LoanApplicationNo INT;
DECLARE @PrmUnitFlag INT;

DECLARE @PrmSurityPictureFrom INT;

DECLARE @OrderNo BIGINT;
DECLARE @TrnPartyAccNo BIGINT;

TRUNCATE TABLE WFA2ZORDERDETAILS;



------ Order Informations



        SET @strSQL = 'INSERT INTO WFA2ZORDERDETAILS (OrderNo,OrderDate,RecType,OrderParty,ItemCode,ItemName,ItemImage,ItemKarat,ItemSize,ItemLength,ItemPiece,ItemWide,ItemColor,' +
		         	  'ItemWeight,ItemTotalWeight,WayToOrder,WayToOrderNo,WhoseOrder,WhoseOrderPhone,DeliveryPossibleDate,OrderStatus,OrderStatusDesc,OrderStatusDate,FactoryParty,' +
					  'SendToFactoryNote,SendToFactoryDate,FactoryReceiveNote,FactoryReceiveDate,FactoryWhomReceive,FactoryWhomPhone,ReadyInFactoryNote,ReadyInFactoryDate,ReadyInFactoryWeight,' +
					  'SendToTransitDate,SendToTransitWeight,ReceiveFromTransitDate,ReceiveFromTransitWeight,OrderDeliveryDate,OrderDeliveryWeight,OrderReEditDate,OrderCancelNote)' +
			          ' SELECT ' +
                      'OrderNo,OrderDate,0,OrderParty,ItemCode,ItemName,ItemImage,ItemKarat,ItemSize,ItemLength,ItemPiece,ItemWide,ItemColor,' +
		         	  'ItemWeight,ItemTotalWeight,WayToOrder,WayToOrderNo,WhoseOrder,WhoseOrderPhone,DeliveryPossibleDate,OrderStatus,OrderStatusDesc,OrderStatusDate,FactoryParty,' +
					  'SendToFactoryNote,SendToFactoryDate,FactoryReceiveNote,FactoryReceiveDate,FactoryWhomReceive,FactoryWhomPhone,ReadyInFactoryNote,ReadyInFactoryDate,ReadyInFactoryWeight,' +
					  'SendToTransitDate,SendToTransitWeight,ReceiveFromTransitDate,ReceiveFromTransitWeight,OrderDeliveryDate,OrderDeliveryWeight,OrderReEditDate,OrderCancelNote' +
			          ' FROM A2ZORDERDETAILS' + 
                      ' WHERE OrderDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';

           IF @OrderParty <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND A2ZORDERDETAILS.OrderParty = ' + CAST(@OrderParty AS VARCHAR(8));
				END	


            
            
            EXECUTE (@strSQL);  


      EXECUTE SpM_GenerateOrderTransaction;

	  	  SET @strSQL = 'INSERT INTO WFA2ZORDERDETAILS (OrderNo,RecType,TrnDate,TrnVchNo,TrnPartyAccNo,TrnWeight)' +
						' SELECT ' +
						' OrderNo,RecType,TrnDate,TrnVchNo,TrnPartyAccNo,TrnWeight' +
						' FROM WFORDERTRANSACTION;' 
                
            
			EXECUTE (@strSQL);  
	  
	 -- UPDATE WFA2ZORDERDETAILS SET TrnPartyAccNo = (SELECT TOP(1) TrnPartyAccNo FROM WFORDERTRANSACTION WHERE WFORDERTRANSACTION.OrderNo = WFA2ZORDERDETAILS.OrderNo);
		
	DECLARE GoldTable CURSOR FOR
	SELECT OrderNo,TrnPartyAccNo FROM WFA2ZORDERDETAILS WHERE RecType = 1;

    OPEN GoldTable;
    FETCH NEXT FROM GoldTable INTO @OrderNo,@TrnPartyAccNo

    WHILE @@FETCH_STATUS = 0 
	   BEGIN

	      UPDATE WFA2ZORDERDETAILS SET TrnPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
		  WHERE A2ZPARTYCODE.PartyCode = @TrnPartyAccNo) + ',' + isnull(TrnPartyName,'')
		  FROM A2ZPARTYCODE,WFA2ZORDERDETAILS
		  WHERE A2ZPARTYCODE.PartyCode = @TrnPartyAccNo AND OrderNo = @OrderNo AND RecType = 0;


		  --UPDATE WFA2ZORDERDETAILS SET TrnPartyAccNo = @TrnPartyAccNo WHERE OrderNo = @OrderNo AND RecType = 0;
	       


	        FETCH NEXT FROM GoldTable INTO
             @OrderNo,@TrnPartyAccNo;        
	END

    CLOSE GoldTable; 
    DEALLOCATE GoldTable;

	  
	  
	        DELETE FROM WFA2ZORDERDETAILS WHERE RecType <> 0; 
		

	  
			UPDATE WFA2ZORDERDETAILS SET OrderPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
			WHERE WFA2ZORDERDETAILS.OrderParty = A2ZPARTYCODE.PartyCode)
			

			UPDATE WFA2ZORDERDETAILS SET FactoryPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
			WHERE WFA2ZORDERDETAILS.FactoryParty = A2ZPARTYCODE.PartyCode)
			

			--UPDATE WFA2ZORDERDETAILS SET TrnPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
			--WHERE WFA2ZORDERDETAILS.TrnPartyAccNo = A2ZPARTYCODE.PartyCode)
			
			
			UPDATE WFA2ZORDERDETAILS SET Remarks = 'Not Deliver' WHERE OrderDeliveryWeight = 0 or OrderDeliveryWeight is null;
            
			UPDATE WFA2ZORDERDETAILS SET Remarks = 'Part Deliver' WHERE OrderDeliveryWeight <> ReadyInFactoryWeight AND OrderDeliveryWeight <> 0 AND OrderDeliveryWeight is not null;

            UPDATE WFA2ZORDERDETAILS SET Remarks = 'Full Deliver' WHERE OrderDeliveryWeight = ReadyInFactoryWeight AND OrderDeliveryWeight <> 0 AND OrderDeliveryWeight is not null;



 SELECT * FROM A2ZACOMS..WFA2ZORDERDETAILS;



END


































GO

