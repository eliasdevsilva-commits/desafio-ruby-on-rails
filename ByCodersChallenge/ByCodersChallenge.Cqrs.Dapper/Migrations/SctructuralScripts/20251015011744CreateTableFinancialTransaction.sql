CREATE TABLE IF NOT EXISTS `financialtransaction` (
  `Id` VARCHAR(36) NOT NULL,
  `TransactionTypeId` VARCHAR(36) NOT NULL,
  `OccurrenceDate` DATETIME NOT NULL,
  `Value` DECIMAL(0,0) NOT NULL,
  `CPF` VARCHAR(11) NOT NULL,
  `Card` VARCHAR(12) NOT NULL,
  `StoreId` VARCHAR(36) NOT NULL,
  CONSTRAINT `FK_FinancialTransactionStoreId_Store` FOREIGN KEY (`StoreId`) REFERENCES `Store` (`Id`) ON UPDATE CASCADE,
  PRIMARY KEY (`Id`));
