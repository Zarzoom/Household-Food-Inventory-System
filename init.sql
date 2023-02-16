
CREATE DATABASE InventoryData;
-- WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'InventoryData')\gexec;

CREATE TABLE "ItemList"(
"Brand" VARCHAR(255),
"GenericName" VARCHAR(255),
"Price" REAL,
"Size" VARCHAR(255),
"ItemID" BIGSERIAL NOT NULL,
PRIMARY KEY("ItemID")
);

CREATE TABLE "PantryList"(
"PantryName" VARCHAR(255),
"PantryID" BIGSERIAL NOT NULL,
PRIMARY KEY("PantryID")
);