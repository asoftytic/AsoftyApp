
 CREATE TABLE `asoftyapp2.0`.`login` (
 `CompayId` INT NOT NULL AUTO_INCREMENT , 
 `Identificacion` VARCHAR(255) NOT NULL , 
 `IdCompany` VARCHAR(255) NOT NULL , 
 `Password` VARCHAR(255) NOT NULL , 
 `Estado` DECIMAL(1,0) NOT NULL DEFAULT '0' , 
 PRIMARY KEY (`CompayId`), UNIQUE `Identificacion` (`Identificacion`(255), `IdCompany`(255))) ENGINE = InnoDB;


