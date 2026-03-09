  USE [PruebaTecnicaMultiMoneyCarlosAguilar]
  GO
  
  INSERT INTO TransactionTypes VALUES('DEPOSIT');
  INSERT INTO TransactionTypes VALUES('WITHDRAWAL');
  INSERT INTO TransactionTypes VALUES('TRANSFER_IN');
  INSERT INTO TransactionTypes VALUES('TRANSFER_OUT');
  
  INSERT INTO Accounts VALUES('12005689','Carlos Aguilar',2500, GETDATE());
  INSERT INTO Accounts VALUES('12005690','Eduardo Diaz',4200.56, GETDATE());
  INSERT INTO Accounts VALUES('12005691','Ana Andino',8500, GETDATE());