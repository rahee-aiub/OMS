USE [A2ZACOMS]
GO

/****** Object:  StoredProcedure [dbo].[Sp_GenerateDeliveryTransaction]    Script Date: 04/07/2019 10:02:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO










CREATE PROCEDURE [dbo].[Sp_GenerateDeliveryTransaction] (@fDate VARCHAR(10),@tDate VARCHAR(10),@nFlag INT)
AS
/*
EXECUTE Sp_GenerateDeliveryTransaction '2019-03-01','2019-03-03',4


*/

BEGIN

	DECLARE @fYear INT;
	DECLARE @tYear INT;
	DECLARE @strSQL NVARCHAR(MAX);
	DECLARE @nCount INT;
	DECLARE @trnDrCr SMALLINT;
	DECLARE @VType NVARCHAR(1);
    DECLARE @prmUnitFlag INT;

  
	
	TRUNCATE TABLE WFTRANSACTIONLIST;

	
    SET @VType = 'C';    

    SET @fYear = LEFT(@fDate,4);
	SET @tYear = LEFT(@tDate,4);

	SET @nCount = @fYear

	WHILE (@nCount <> 0)
		BEGIN
			
			SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (TrnDate,VchNo,AccType,AccNo,RefAccType,RefAccNo,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,AccType,AccNo,RefAccType,RefAccNo,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +	
				' FROM A2ZACOMST' + CAST(@nCount AS VARCHAR(4)) + '..A2ZTRANSACTION ' +	
                ' WHERE TrnFlag = 0' +
       	        ' AND (TrnDate' + ' BETWEEN ' + '''' +@fDate + '''' + ' AND ' + '''' + @tDate + '''' + ')'	

			
            IF @nFlag <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@nFlag AS VARCHAR(2));
				END	


                 
                  
            
			EXECUTE (@strSQL);            

			SET @nCount = @nCount + 1;
			IF @nCount > @tYear
				BEGIN
					SET @nCount = 0;
				END
		END 

		SET @strSQL = 'INSERT INTO WFTRANSACTIONLIST (TrnDate,VchNo,AccType,AccNo,RefAccType,RefAccNo,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate)' +
				' SELECT ' +
				'TrnDate,VchNo,AccType,AccNo,RefAccType,RefAccNo,Location,FuncOpt,FuncOptDesc,' +
				'PayType,TrnType,TrnDrCr,TrnDebitAmt,TrnCreditAmt,TrnDesc,TrnVchType,TrnChqNo,' +
				'TrnCSGL,TrnFlag,TrnProcStat,TrnSysUser,UserID,VerifyUserID,CreateDate' +		
				' FROM A2ZACOMS..A2ZTRANSACTION WHERE TrnFlag = 0 AND TrnDate' + 
				' BETWEEN ''' + @fDate + '''' + ' AND ' + '''' + @tDate + '''';


            IF @nFlag <> 0
				BEGIN
                    SET @strSQL = @strSQL + ' AND FuncOpt = ' + CAST(@nFlag AS VARCHAR(2));
				END	


            
		                   
	
	EXECUTE (@strSQL);
       
	  

	--SELECT * FROM WFTRANSACTIONLIST;


END































GO

