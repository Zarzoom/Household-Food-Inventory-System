
CREATE DATABASE IF NOT EXISTS  InventoryData;

USE InventoryData;


CREATE TABLE ItemList(
Brand VARCHAR(255),
GenericName VARCHAR(255),
Price DECIMAL(8,2),
Size VARCHAR(255),
ItemID INT NOT NULL AUTO_INCREMENT,
PRIMARY KEY(ItemID)
);