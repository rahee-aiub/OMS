USE [A2ZACOMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_rptRDPSummaryReport]    Script Date: 04/07/2019 1:41:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER PROCEDURE [dbo].[Sp_rptRDPSummaryReport] (@fDate VARCHAR(10),@tDate VARCHAR(10))


AS

/*


EXECUTE Sp_rptRDPSummaryReport '2019-03-02','2019-03-04'



*/


BEGIN

DECLARE @strSQL NVARCHAR(MAX);

DECLARE @LoanApplicationNo INT;
DECLARE @PrmUnitFlag INT;

DECLARE @PrmSurityPictureFrom INT;

DECLARE @OrderNo BIGINT;
DECLARE @OrderParty INT;
DECLARE @TrnPartyAccNo BIGINT;

DECLARE @OrderDate SMALLDATETIME;
DECLARE @LastOrderDate SMALLDATETIME;
DECLARE @OrderDeliveryDate SMALLDATETIME;

DECLARE @OrderPartyAccNo BIGINT;
DECLARE @OrderPartyName NVARCHAR(50);
DECLARE @ItemWeight MONEY;

DECLARE @LineNo INT;
DECLARE @SubLineNo INT;

DECLARE @TrnDate  SMALLDATETIME;
DECLARE @LineSL   INT;

DECLARE @WriteFlag   int;

DECLARE @BegDate   SMALLDATETIME;
DECLARE @CalDate   VARCHAR(10);  
DECLARE @FirstTime INT;

DECLARE @ChkWt  money;
DECLARE @NetWt  money;
DECLARE @ChkDate  SMALLDATETIME;

DECLARE @noDays INT;

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
					  ' WHERE OrderStatus < 90';

                      --' WHERE OrderDate BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';


            EXECUTE (@strSQL);  

			
			UPDATE WFA2ZORDERDETAILS SET Remarks = 'Not Deliver' WHERE OrderDeliveryWeight = 0 or OrderDeliveryWeight is null;
            
			UPDATE WFA2ZORDERDETAILS SET Remarks = 'Part Deliver' WHERE OrderDeliveryWeight <> ReadyInFactoryWeight AND OrderDeliveryWeight <> 0 AND OrderDeliveryWeight is not null;

            UPDATE WFA2ZORDERDETAILS SET Remarks = 'Full Deliver' WHERE OrderDeliveryWeight = ReadyInFactoryWeight AND OrderDeliveryWeight <> 0 AND OrderDeliveryWeight is not null;


		
		TRUNCATE TABLE WFRDPDETAILS;

		---------------------------------------------------------------------------

		SET @BegDate = (SELECT TOP(1) DATEADD(DAY,1,OrderDate)
	    FROM WFA2ZORDERDETAILS
	    WHERE (OrderDeliveryDate >= @fDate OR OrderDeliveryDate IS NULL) AND (Remarks = 'Not Deliver' OR Remarks = 'Part Deliver')
		GROUP BY OrderDate);

	    SET @CalDate = CAST(YEAR(@BegDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@BegDate) AS VARCHAR(2)) + '-' + CAST(DAY(@BegDate) AS VARCHAR(2))    




		;WITH cte AS
		(
			SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) - 1 AS [Incrementor]
			FROM   [master].[sys].[columns] sc1
			CROSS JOIN [master].[sys].[columns] sc2
		)
		INSERT INTO WFRDPDETAILS (TrnDate,LineSL)
		SELECT DATEADD(DAY, cte.[Incrementor], @CalDate),1
		FROM   cte
		WHERE  DATEADD(DAY, cte.[Incrementor], @CalDate) <= @tDate;


		-------------------------------Received Details---------------------------

       
		SET @LineNo = 0;
		SET @LastOrderDate = 0;

		DECLARE wf1Table CURSOR FOR
        SELECT OrderDate,OrderParty,SUM(ItemTotalWeight) AS ItemWeight
	    FROM WFA2ZORDERDETAILS
	    WHERE OrderDate BETWEEN @CalDate AND @tDate
		GROUP BY OrderDate,OrderParty ORDER BY OrderDate;

        OPEN wf1Table;
        FETCH NEXT FROM wf1Table INTO
        @OrderDate,@OrderParty,@ItemWeight;

	

        WHILE @@FETCH_STATUS = 0 
	       BEGIN

		        IF @OrderDate <> @LastOrderDate
				   BEGIN
				       SET @LastOrderDate = @OrderDate;
				       SET @LineNo = 1;
				       SET @WriteFlag = 0;
				    END
                ELSE
				   BEGIN
				       SET @WriteFlag = 0;
		               SET @LineNo = @LineNo + 1;
				   END

		        
		        DECLARE wf11Table CURSOR FOR
                SELECT TrnDate,LineSL
	            FROM WFRDPDETAILS
	            WHERE TrnDate = @LastOrderDate AND LineSL = @LineNo; 

                OPEN wf11Table;
                FETCH NEXT FROM wf11Table INTO
                @TrnDate,@LineSL;

                WHILE @@FETCH_STATUS = 0 
	               BEGIN

				         SET @WriteFlag = 1;

				         UPDATE WFRDPDETAILS SET PartyCode1 = @OrderParty,Weight1 = @ItemWeight			
					     WHERE TrnDate = @LastOrderDate AND LineSL = @LineNo;

						 
                         FETCH NEXT FROM wf11Table INTO
                         @TrnDate,@LineSL;
	               END

                CLOSE wf11Table; 
                DEALLOCATE wf11Table;


				IF @WriteFlag = 0
				   BEGIN    		
			             INSERT INTO WFRDPDETAILS(TrnDate,LineSL,PartyCode1,Weight1) 
		                 VALUES (@LastOrderDate,@LineNo,@OrderParty,@ItemWeight)
				   END

		        
	      FETCH NEXT FROM wf1Table INTO
          @OrderDate,@OrderParty,@ItemWeight; 
	      END

          CLOSE wf1Table; 
          DEALLOCATE wf1Table;




		------------------------------Delivery Details-------------------------------------

		EXECUTE Sp_GenerateDeliveryTransaction @CalDate,@tDate,4


		SET @LineNo = 0;
		SET @LastOrderDate = 0;

		DECLARE wf2Table CURSOR FOR
        SELECT TrnDate,AccNo,ISNULL(SUM(TrnCreditAmt),0) AS ItemWeight
	    FROM WFTRANSACTIONLIST
		GROUP BY TrnDate,AccNo ORDER BY TrnDate;

        OPEN wf2Table;
        FETCH NEXT FROM wf2Table INTO
        @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;

	
        WHILE @@FETCH_STATUS = 0 
	       BEGIN

		        IF @OrderDeliveryDate <> @LastOrderDate
				   BEGIN
				       SET @LastOrderDate = @OrderDeliveryDate;
				       SET @LineNo = 1;
				       SET @WriteFlag = 0;
				    END
                ELSE
				   BEGIN
				       SET @WriteFlag = 0;
		               SET @LineNo = @LineNo + 1;
				   END

		        
		        DECLARE wf22Table CURSOR FOR
                SELECT TrnDate,LineSL
	            FROM WFRDPDETAILS
	            WHERE TrnDate = @LastOrderDate AND LineSL = @LineNo; 

                OPEN wf22Table;
                FETCH NEXT FROM wf22Table INTO
                @TrnDate,@LineSL;

                WHILE @@FETCH_STATUS = 0 
	               BEGIN
  
                         SET @WriteFlag = 1;

				         UPDATE WFRDPDETAILS SET PartyCode2 = @OrderPartyAccNo,Weight2 = @ItemWeight			
					     WHERE TrnDate = @LastOrderDate AND LineSL = @LineNo;

						
                         FETCH NEXT FROM wf22Table INTO
                         @TrnDate,@LineSL;
	               END

                CLOSE wf22Table; 
                DEALLOCATE wf22Table;


				IF @WriteFlag = 0
				   BEGIN	                    		
			             INSERT INTO WFRDPDETAILS(TrnDate,LineSL,PartyCode2,Weight2) 
		                 VALUES (@LastOrderDate,@LineNo,@OrderPartyAccNo,@ItemWeight)
				    END

		        
			       

	      FETCH NEXT FROM wf2Table INTO
          @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;
	      END

          CLOSE wf2Table; 
          DEALLOCATE wf2Table;

		--------------------------------Pending Details--------------------------------------

		CREATE TABLE #PendingTable 
		(	TrnDate smalldatetime,
			PartyCode int,
			PartyName varchar(50),
			pWeight money
		);

		TRUNCATE TABLE #PendingTable;

		SET @LineNo = 0;
	    SET @LastOrderDate = 0;

		SET @FirstTime = 0;

		DECLARE wf3Table CURSOR FOR
        SELECT DATEADD(DAY,1,OrderDate),OrderParty,SUM(ItemTotalWeight) AS ItemWeight
	    FROM WFA2ZORDERDETAILS
	    WHERE (OrderDeliveryDate >= @CalDate OR OrderDeliveryDate IS NULL) AND (Remarks = 'Not Deliver' OR Remarks = 'Part Deliver')
		GROUP BY OrderDate,OrderParty ORDER BY OrderDate,OrderParty;

        OPEN wf3Table;
        FETCH NEXT FROM wf3Table INTO
        @OrderDate,@OrderParty,@ItemWeight;

        WHILE @@FETCH_STATUS = 0 
	       BEGIN

		     --   IF @OrderDate <= @tDate
				
				   --BEGIN
				        IF @FirstTime = 0
				           BEGIN
				               SET @FirstTime = 1;
						       SET @BegDate = @OrderDate;
				           END

                        SET @ChkWt = (SELECT TOP(1) pWeight FROM #PendingTable WHERE PartyCode = @OrderParty AND TrnDate < @OrderDate);


				        IF @ChkWt = 0 OR @ChkWt IS NULL
				           BEGIN       
					           SET @NetWt = @ItemWeight   
				           END
                        ELSE 
				           BEGIN
				       
				                SET @NetWt = ISNULL(@ItemWeight,0) + ISNULL(@ChkWt,0)
				            END

		        
				         INSERT INTO #PendingTable(TrnDate,PartyCode,pWeight) 
		                 VALUES (@OrderDate,@OrderParty,@NetWt)
				   --END

   
	      FETCH NEXT FROM wf3Table INTO
          @OrderDate,@OrderParty,@ItemWeight; 
	      END

          CLOSE wf3Table; 
          DEALLOCATE wf3Table;	  

		  --SELECT * FROM #PendingTable;


		  --SET @CalDate = CAST(YEAR(@BegDate) AS VARCHAR(4)) + '-' +CAST(MONTH(@BegDate) AS VARCHAR(2)) + '-' + CAST(DAY(@BegDate) AS VARCHAR(2))    

				 
		  EXECUTE Sp_GenerateDeliveryTransaction @CalDate,@tDate,4

		 		  
		  DECLARE wf4Table CURSOR FOR
          SELECT TrnDate,AccNo,ISNULL(SUM(TrnCreditAmt),0) AS ItemWeight
	      FROM WFTRANSACTIONLIST
		  GROUP BY TrnDate,AccNo ORDER BY TrnDate,AccNo;

          OPEN wf4Table;
          FETCH NEXT FROM wf4Table INTO
          @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;


		  WHILE @@FETCH_STATUS = 0 
	         BEGIN

			     SET @ChkWt = (SELECT TOP(1) pWeight FROM #PendingTable WHERE PartyCode = @OrderPartyAccNo AND TrnDate <= @OrderDeliveryDate order by TrnDate desc);


			    SET @ChkDate = (SELECT TrnDate FROM #PendingTable WHERE TrnDate = @OrderDeliveryDate AND PartyCode = @OrderPartyAccNo);


				IF @ChkWt = 0 OR @ChkWt IS NULL
				   BEGIN       
					   SET @NetWt = @ItemWeight   
				   END
                ELSE 
				   BEGIN
				       
				       SET @NetWt = ISNULL(@ChkWt,0) - ISNULL(@ItemWeight,0)
				   END

                IF @ChkWt > 0 AND @ChkWt IS NOT NULL
				   BEGIN

				         IF @ChkDate IS NULL
				            BEGIN
				                 INSERT INTO #PendingTable(TrnDate,PartyCode,pWeight) 
		                         VALUES (@OrderDeliveryDate,@OrderPartyAccNo,@NetWt)
				            END
                         ELSE
				            BEGIN
				                UPDATE #PendingTable SET pWeight = @NetWt 			
					            WHERE TrnDate = @OrderDeliveryDate AND PartyCode = @OrderPartyAccNo;
				            END
		           END
		       
	      FETCH NEXT FROM wf4Table INTO
          @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;
	      END

          CLOSE wf4Table; 
          DEALLOCATE wf4Table;

		  UPDATE #PendingTable SET PartyName = (SELECT PartyName FROM A2ZPARTYCODE 
		  WHERE #PendingTable.PartyCode = A2ZPARTYCODE.PartyCode)

	-------------------------------------------------------------------------------

	    SET @LineNo = 0;
		SET @LastOrderDate = 0;

		DECLARE wf5Table CURSOR FOR
        SELECT TrnDate,PartyCode,ISNULL(SUM(pWeight),0) AS ItemWeight
	    FROM #PendingTable
		GROUP BY TrnDate,PartyCode ORDER BY TrnDate,PartyCode;

        OPEN wf5Table;
        FETCH NEXT FROM wf5Table INTO
        @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;

	
        WHILE @@FETCH_STATUS = 0 
	       BEGIN

		        IF @OrderDeliveryDate <> @LastOrderDate
				   BEGIN
				       SET @LastOrderDate = @OrderDeliveryDate;
				       SET @LineNo = 1;
				       SET @WriteFlag = 0;
				    END
                ELSE
				   BEGIN
				       SET @WriteFlag = 0;
		               SET @LineNo = @LineNo + 1;
				   END

		        
		        DECLARE wf55Table CURSOR FOR
                SELECT TrnDate,LineSL
	            FROM WFRDPDETAILS
	            WHERE TrnDate = @LastOrderDate AND LineSL = @LineNo; 

                OPEN wf55Table;
                FETCH NEXT FROM wf55Table INTO
                @TrnDate,@LineSL;

                WHILE @@FETCH_STATUS = 0 
	               BEGIN
  
                         SET @WriteFlag = 1;

				         UPDATE WFRDPDETAILS SET PartyCode3 = @OrderPartyAccNo,Weight3 = @ItemWeight			
					     WHERE TrnDate = @LastOrderDate AND LineSL = @LineNo;

						
                         FETCH NEXT FROM wf55Table INTO
                         @TrnDate,@LineSL;
	               END

                CLOSE wf55Table; 
                DEALLOCATE wf55Table;


				IF @WriteFlag = 0
				   BEGIN	                    		
			             INSERT INTO WFRDPDETAILS(TrnDate,LineSL,PartyCode3,Weight3) 
		                 VALUES (@LastOrderDate,@LineNo,@OrderPartyAccNo,@ItemWeight)
				    END
      

	      FETCH NEXT FROM wf5Table INTO
          @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;
	      END

          CLOSE wf5Table; 
          DEALLOCATE wf5Table;


		-------------------------------------------------

		SET @noDays = ((DATEDIFF(d, @BegDate, @tDate)) + 1);


		WHILE @noDays <> 0
			BEGIN

		         DECLARE wf6Table CURSOR FOR
                 SELECT TrnDate,PartyCode3,Weight3
	             FROM WFRDPDETAILS 
		         WHERE PartyCode3 <> 0 AND PartyCode3 IS NOT NULL
		         ORDER BY TrnDate,PartyCode3;

                 OPEN wf6Table;
                 FETCH NEXT FROM wf6Table INTO
                 @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;

	
                 WHILE @@FETCH_STATUS = 0 
	                BEGIN

		                 SET @ChkDate =  DATEADD(DAY,1,@OrderDeliveryDate);

			             IF @ChkDate <= @tDate
			                BEGIN
				                 SET @ChkWt = (SELECT Weight3 FROM WFRDPDETAILS WHERE PartyCode3 = @OrderPartyAccNo AND TrnDate = @ChkDate);

			                     IF @ChkWt = 0 OR @ChkWt IS NULL
				                    BEGIN	                    		
			                             INSERT INTO WFRDPDETAILS(TrnDate,LineSL,PartyCode3,Weight3) 
		                                 VALUES (@ChkDate,99,@OrderPartyAccNo,@ItemWeight)
				                    END
				            END

	                    FETCH NEXT FROM wf6Table INTO
                        @OrderDeliveryDate,@OrderPartyAccNo,@ItemWeight;
	                END

                CLOSE wf6Table; 
                DEALLOCATE wf6Table;

		        SET @noDays = @noDays - 1;
		  END

		  ------------------------------------------------------

          UPDATE WFRDPDETAILS SET PartyName1 = (SELECT PartyName FROM A2ZPARTYCODE 
		  WHERE WFRDPDETAILS.PartyCode1 = A2ZPARTYCODE.PartyCode)

		  UPDATE WFRDPDETAILS SET PartyName2 = (SELECT PartyName FROM A2ZPARTYCODE 
		  WHERE WFRDPDETAILS.PartyCode2 = A2ZPARTYCODE.PartyCode)

		  UPDATE WFRDPDETAILS SET PartyName3 = (SELECT PartyName FROM A2ZPARTYCODE 
		  WHERE WFRDPDETAILS.PartyCode3 = A2ZPARTYCODE.PartyCode)
		
		DELETE FROM WFRDPDETAILS WHERE TrnDate NOT BETWEEN @fDate AND @tDate;

		SELECT TrnDate,ISNULL(SUM(Weight1),0) AS Weight1,ISNULL(SUM(Weight2),0) AS Weight2,ISNULL(SUM(Weight3),0) AS Weight3 FROM WFRDPDETAILS GROUP BY TrnDate ORDER BY TrnDate  


	


END



GO

