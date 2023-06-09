# Program Description
Household food inventory is an application to manage the items that are in your pantry and your shopping list. Add Items with details such as the brand, quantity, and price. Then organize them into pantries such as fridge, freezer, and cupboard. Have you finished an item and need more? Add it to your handy grocery list and make your next shoping trip a breeze.

# Current Capabilities
Create Items with the brand, generic name, price, and size. Create pantries that hold items and keep track of each item's quantity. Find items and pantries with either a primary key search or a contents search. Items and pantries can be updated making mistakes easy to correct. Users can delete items and pantries. When a pantry is deleted, all contents in the pantry will also be deleted from the pantry.

# How to Run the Application
To run the application you will need to have docker installed or to setup the database connections yourself.
`docker compose up`

To run the Tests for this project. 
`docker-compose -f docker-compose-test.yml up --build`

# ToDo
Fix background color to improve contrast of text. 
