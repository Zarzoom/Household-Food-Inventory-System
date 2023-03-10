
CREATE DATABASE IF NOT EXISTS  InventoryData;

USE InventoryData;


CREATE TABLE IF NOT EXISTS ItemList(
Brand VARCHAR(255),
GenericName VARCHAR(255),
Price DECIMAL(8,2),
Size VARCHAR(255),
ItemID INT NOT NULL AUTO_INCREMENT,
PRIMARY KEY(ItemID)
);

CREATE TABLE IF NOT EXISTS PantryList(
PantryName VARCHAR(255),
PantryID INT NOT NULL AUTO_INCREMENT,
PRIMARY KEY(PantryID)
);

CREATE TABLE IF NOT EXISTS PantryContents(
Quantity INT,
PCItemID INT,
PCPantryID INT,
PantryContentID INT NOT NULL AUTO_INCREMENT,
PRIMARY KEY(PantryContentID)
);