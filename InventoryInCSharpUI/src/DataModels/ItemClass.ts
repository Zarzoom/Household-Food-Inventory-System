class Item{
    itemID?: number
    brand: string
    price: number
    genericName: string
    size: string
    quantity?: number
    
    constructor (itemID: number, brand: string, price: number, genericName: string, size: string, quantity: number){
        this.itemID = itemID;
        this.brand = brand;
        this.price = price;
        this.genericName = genericName;
        this.size = size;
        this.quantity = quantity;
    }
}
export default Item;