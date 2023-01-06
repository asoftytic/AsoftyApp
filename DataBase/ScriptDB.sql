#InitialSchema 1.0.0

Create database AsoftyDb;
use AsoftyDb;
 
 CREATE TABLE `User` (
	`UserId` INT NOT NULL AUTO_INCREMENT,
	`CompayCode` INT NOT NULL, 
    `Username` VARCHAR(50) NOT NULL , 
    `Password` VARCHAR(255) NOT NULL , 
	`Status` DECIMAL(1,0) NOT NULL DEFAULT '0' , 
    `Enabled` DECIMAL(1,0) NOT NULL DEFAULT '1' , 
    `RollCode` INT NOT NULL DEFAULT '1' , 	#Standar
 PRIMARY KEY (`UserId`), UNIQUE `CompayCode` (`CompayCode`, `UserId`)) ENGINE = InnoDB;
 
 CREATE TABLE `Company` (
	`CompanyId` INT NOT NULL AUTO_INCREMENT,
    `CompanyName` VARCHAR(255) NOT NULL , 
	`Status` DECIMAL(1,0) NOT NULL DEFAULT '0' , 
    `Enabled` DECIMAL(1,0) NOT NULL DEFAULT '1' , 
    `PlanCode` INT NOT NULL DEFAULT '1' , 	#Standar
 PRIMARY KEY (`CompanyId`));
 
INSERT INTO User (CompayCode, Password, Username) VALUES(0, "12345", "admin");

CREATE TABLE Program(
	ProgramId int not null AUTO_INCREMENT primary key,	#ProgramId unico para empresa
	CompanyId int not null,				#Id de la empresa que lo usa
	ProgramName varchar(50) not null,	#Nombre del programa
	IsCustom bit not null,				#Bandera para identificar si es personalizado
	HasDetail bit not null default 0,	#Bandera para identificar si tiene relacionado uno o más tablas de detalle
	AllowCreate bit not null default 0, #Bandera para identificar si permite crear registros del maestro
	AllowRead bit not null default 0,	#Bandera para identificar si permite leer registros del maestro
	AllowUpdate bit not null default 0, #Bandera para identificar si permite actualizar registros del maestro
	AllowDelete bit not null default 0, #Bandera para identificar si permite borrar registros del maestro
	RollAllowed int,					#Roll del usuario que tiene acceso a este programa (opcional)
	Eneabled bit not null default 1		#Bandera para identificar si está habilitado
);
