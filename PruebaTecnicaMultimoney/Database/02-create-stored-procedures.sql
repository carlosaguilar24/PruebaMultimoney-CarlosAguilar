USE [PruebaTecnicaMultiMoneyCarlosAguilar]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAccountInfo]    Script Date: 8/3/2026 23:43:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAccountInfo]  
@AccountId NVARCHAR(50)
AS
BEGIN

IF NOT EXISTS(SELECT 1 FROM Accounts WHERE AccountId = @AccountId)
RETURN 99


-- PARA RETORNAR LA INFO DE LA CUNETA
SELECT AccountId, Balance from Accounts where AccountId = @AccountId

-- PARA RETORNAR LA INFO DE LAS TRANSACCIONES
SELECT TOP 10 T.TransactionId, TT.Name as Type, T.Amount, T.Date, T.Description FROM Transactions T
INNER JOIN TransactionTypes TT ON TT.Id = T.TransactionTypeId
WHERE T.AccountId = @AccountId 
ORDER BY Date DESC

RETURN 0;

END
GO

/****** Object:  StoredProcedure [dbo].[sp_RecordTransaction]    Script Date: 8/3/2026 23:44:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RecordTransaction]
@AccountId NVARCHAR(50), 
@TransactionTypeId int,
@Amount decimal (18,2),
@Description varchar(500) null,
@TransactionId INT OUTPUT
AS
BEGIN
INSERT INTO Transactions 
(AccountId, TransactionTypeId, Amount, Date, Description)
VALUES 
(@AccountId, @TransactionTypeId, @Amount, GETDATE(), @Description)

SET @TransactionId = SCOPE_IDENTITY();
END
GO

/****** Object:  StoredProcedure [dbo].[sp_CreateDeposit]    Script Date: 8/3/2026 23:44:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreateDeposit]
@AccountId NVARCHAR(50),
@Amount decimal(18,2),
@Description nvarchar(500),
@NewBalance decimal(18,2) OUTPUT
AS
BEGIN


IF @Amount <= 0
	RETURN 98;

IF NOT EXISTS(SELECT 1 FROM Accounts WHERE AccountId = @AccountId)
	RETURN 99;

BEGIN TRY
	BEGIN TRANSACTION

UPDATE Accounts 
SET Balance = Balance + @Amount 
WHERE AccountId = @AccountId;

DECLARE @GeneratedId int;

EXEC sp_RecordTransaction
@AccountId = @AccountId,
@TransactionTypeId = 1,
@Amount = @Amount,
@Description = @Description,
@TransactionId = @GeneratedId OUTPUT;

SELECT @NewBalance = Balance FROM Accounts WHERE AccountId = @AccountId 

COMMIT TRANSACTION;
RETURN 0;

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	RETURN 97;
END CATCH
END

GO

/****** Object:  StoredProcedure [dbo].[sp_CreateWithdrawal]    Script Date: 8/3/2026 23:45:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreateWithdrawal]
@AccountId NVARCHAR(50),
@Amount decimal(18,2),
@NewBalance decimal(18,2) OUTPUT
AS
BEGIN

IF @Amount <= 0
	RETURN 98;

IF NOT EXISTS(SELECT 1 FROM Accounts WHERE AccountId = @AccountId)
	RETURN 99;

DECLARE @ActualBalance decimal (18,2)
SELECT @ActualBalance = Balance from Accounts WHERE AccountId = @AccountId
IF @ActualBalance < @Amount
	RETURN 96;

BEGIN TRY
	BEGIN TRANSACTION

UPDATE Accounts 
SET Balance = Balance - @Amount 
WHERE AccountId = @AccountId;

DECLARE @GeneratedId int;

EXEC sp_RecordTransaction 
@AccountId = @AccountId,
@TransactionTypeId = 2,
@Amount = @Amount,
@Description = '',
@TransactionId = @GeneratedId OUTPUT;

SELECT @NewBalance = Balance FROM Accounts WHERE AccountId = @AccountId 

COMMIT TRANSACTION;
RETURN 0;

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	RETURN 97;
END CATCH
END

GO

/****** Object:  StoredProcedure [dbo].[sp_ExecuteTransfer]    Script Date: 8/3/2026 23:45:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ExecuteTransfer]
@OriginAccount NVARCHAR(50),
@DestinationAccount NVARCHAR(50),
@Amount decimal(18,2),
@Description NVARCHAR(500)
AS
BEGIN

IF @Amount <= 0
	RETURN 98;

IF NOT EXISTS(SELECT 1 FROM Accounts WHERE AccountId = @OriginAccount)
	RETURN 99;

IF NOT EXISTS(SELECT 1 FROM Accounts WHERE AccountId = @DestinationAccount)
	RETURN 95;

DECLARE @ActualBalance decimal (18,2)
SELECT @ActualBalance = Balance from Accounts WHERE AccountId = @OriginAccount
IF @ActualBalance < @Amount
	RETURN 96;

BEGIN TRY
	BEGIN TRANSACTION

UPDATE Accounts 
SET Balance = Balance - @Amount 
WHERE AccountId = @OriginAccount;

UPDATE Accounts 
SET Balance = Balance + @Amount 
WHERE AccountId = @DestinationAccount;

DECLARE @GeneratedIdTransferOut INT;
DECLARE @GeneratedIdTransferIn INT;

--Primero registro transferencia
EXEC sp_RecordTransaction 
@AccountId = @OriginAccount,
@TransactionTypeId = 4,
@Amount = @Amount,
@Description = @Description,
@TransactionId = @GeneratedIdTransferOut OUTPUT;

EXEC sp_RecordTransaction 
@AccountId = @DestinationAccount,
@TransactionTypeId = 3,
@Amount = @Amount,
@Description = @Description,
@TransactionId = @GeneratedIdTransferIn OUTPUT;


---OBTENER TRANSFERENCIA ORIGEN
SELECT T.TransactionId, T.AccountId, TT.NAME AS Type, T.Amount, T.Date, T.Description FROM Transactions T
INNER JOIN TransactionTypes TT ON TT.Id = T.TransactionTypeId
WHERE T.AccountId = @OriginAccount AND T.TransactionId = @GeneratedIdTransferOut;

SELECT T.TransactionId, T.AccountId, TT.NAME AS Type, T.Amount, T.Date, T.Description FROM Transactions T
INNER JOIN TransactionTypes TT ON TT.Id = T.TransactionTypeId
WHERE T.AccountId = @DestinationAccount AND T.TransactionId = @GeneratedIdTransferIn;

COMMIT TRANSACTION;
RETURN 0;

END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
	RETURN 97;
END CATCH
END

GO






