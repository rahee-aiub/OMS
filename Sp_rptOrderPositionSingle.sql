USE [A2ZACOMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptOrderPositionSingle]    Script Date: 03/25/2019 12:28:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [dbo].[Sp_rptOrderPositionSingle] (@OrderNo bigint)


AS

/*


EXECUTE Sp_rptOrderPositionSingle 55000062



*/


BEGIN

DECLARE @strSQL NVARCHAR(MAX);

DECLARE @LoanApplicationNo INT;
DECLARE @PrmUnitFlag INT;

DECLARE @PrmSurityPictureFrom INT;

DECLARE @TrnPartyAccNo bigint;
DECLARE @TrnPartyName VARCHAR(50);

TRUNCATE TABLE WFA2ZORDERDETAILS;



------ Order Informations



        SET @strSQL = 'INSERT INTO WFA2ZORDERDETAILS (OrderNo,OrderDate,RecType,OrderParty,ItemCode,ItemName,ItemImage,ItemKarat,ItemSize,ItemLength,ItemPiece,ItemWide,ItemColor,' +
		         	  'ItemWeight,ItemTotalWeight,WayToOrder,WayToOrderNo,WhoseOrder,WhoseOrderPhone,DeliveryPossibleDate,OrderStatus,OrderStatusDesc,OrderStatusDate,FactoryParty,' +
					  'SendToFactoryNote,SendToFactoryDate,FactoryReceiveNote,FactoryReceiveDate,FactoryWhomReceive,FactoryWhomPhone,ReadyInFactoryNote,ReadyInFactoryDate,ReadyInFactoryWeight,' +
					  'SendToTransitDate,SendToTransitWeight,ReceiveFromTransitDate,ReceiveFromTransitWeight,OrderDeliveryDate,OrderDeliveryWeight,OrderReEditDate,OrderCancelNote,TransitWhomReceive,DeliveryWhomReceive)' +
			          ' SELECT ' +
                      'OrderNo,OrderDate,0,OrderParty,ItemCode,ItemName,ItemImage,ItemKarat,ItemSize,ItemLength,ItemPiece,ItemWide,ItemColor,' +
		         	  'ItemWeight,ItemTotalWeight,WayToOrder,WayToOrderNo,WhoseOrder,WhoseOrderPhone,DeliveryPossibleDate,OrderStatus,OrderStatusDesc,OrderStatusDate,FactoryParty,' +
					  'SendToFactoryNote,SendToFactoryDate,FactoryReceiveNote,FactoryReceiveDate,FactoryWhomReceive,FactoryWhomPhone,ReadyInFactoryNote,ReadyInFactoryDate,ReadyInFactoryWeight,' +
					  'SendToTransitDate,SendToTransitWeight,ReceiveFromTransitDate,ReceiveFromTransitWeight,OrderDeliveryDate,OrderDeliveryWeight,OrderReEditDate,OrderCancelNote,TransitWhomReceive,DeliveryWhomReceive' +
			          ' FROM A2ZORDERDETAILS' + 
                      ' WHERE A2ZORDERDETAILS.OrderNo =  ' + CAST(@OrderNo AS VARCHAR(10)) + '';

        
            
            
            EXECUTE (@strSQL);  


      EXECUTE SpM_GenerateOrderTransaction;


	  SET @strSQL = 'INSERT INTO WFA2ZORDERDETAILS (OrderNo,RecType,TrnDate,TrnVchNo,TrnPartyAccNo,TrnWeight)' +
			        ' SELECT ' +
                    'OrderNo,RecType,TrnDate,TrnVchNo,TrnPartyAccNo,TrnWeight' +
			        ' FROM WFORDERTRANSACTION;' 
                
            
      EXECUTE (@strSQL);  

	
		
	 
	  
      UPDATE WFA2ZORDERDETAILS SET OrderPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
      WHERE WFA2ZORDERDETAILS.OrderParty = A2ZPARTYCODE.PartyCode AND A2ZPARTYCODE.GroupCode = 13 AND WFA2ZORDERDETAILS.RecType = 0)
	   WHERE WFA2ZORDERDETAILS.RecType = 0;

      UPDATE WFA2ZORDERDETAILS SET FactoryPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
      WHERE WFA2ZORDERDETAILS.FactoryParty = A2ZPARTYCODE.PartyCode AND A2ZPARTYCODE.GroupCode = 11 AND WFA2ZORDERDETAILS.RecType = 0)
	   WHERE WFA2ZORDERDETAILS.RecType = 0;

      UPDATE WFA2ZORDERDETAILS SET TrnPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
      WHERE WFA2ZORDERDETAILS.TrnPartyAccNo = A2ZPARTYCODE.PartyCode AND A2ZPARTYCODE.GroupCode = 12 AND (WFA2ZORDERDETAILS.RecType = 1 or WFA2ZORDERDETAILS.RecType = 2))
	  WHERE WFA2ZORDERDETAILS.RecType = 1 OR WFA2ZORDERDETAILS.RecType = 2;

      UPDATE WFA2ZORDERDETAILS SET TrnPartyName = (SELECT PartyName FROM A2ZPARTYCODE 
      WHERE WFA2ZORDERDETAILS.TrnPartyAccNo = A2ZPARTYCODE.PartyCode AND A2ZPARTYCODE.GroupCode = 13 AND WFA2ZORDERDETAILS.RecType = 3)
	  WHERE WFA2ZORDERDETAILS.RecType = 3;


      SET @TrnPartyAccNo = (SELECT TOP (1) TrnPartyAccNo FROM WFA2ZORDERDETAILS WHERE RecType = 1 AND OrderNo = @OrderNo);
	  SET @TrnPartyName = (SELECT TOP (1) TrnPartyName FROM WFA2ZORDERDETAILS WHERE RecType = 1 AND OrderNo = @OrderNo);


		UPDATE WFA2ZORDERDETAILS SET TrnPartyAccNo = @TrnPartyAccNo WHERE RecType = 0 AND OrderNo = @OrderNo;
		UPDATE WFA2ZORDERDETAILS SET TrnPartyName = @TrnPartyName WHERE RecType = 0 AND OrderNo = @OrderNo;


		DELETE FROM WFA2ZORDERDETAILS WHERE RecType <> 0;

		--SELECT * FROM A2ZACOMS..WFA2ZORDERDETAILS;



END



GO

