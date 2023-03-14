import {useState} from "react";
import {_Item} from "../../DataModels/_Item";
import ItemManager from "../../Services/Managers/ItemManager"

const newItemManager = new ItemManager();
function EditItem(itemForUpdate: _Item) {

    const [item, setItem] = useState<_Item>({
        brand: itemForUpdate.brand,
        price: itemForUpdate.price,
        genericName: itemForUpdate.genericName,
        size: itemForUpdate.size,
        itemID: itemForUpdate.itemID,
    });
    const ItemToJSONStringify = JSON.stringify(item);
    const ItemToJsonParse = JSON.parse(ItemToJSONStringify);
    const updateBrand = (newBrand: string) => {
        setItem(previousState => {
            return {...previousState, brand: newBrand}
        });
    };
    const updatePrice = (newPrice: string) => {
        const newPriceAsNumber = +newPrice;
        setItem(previousState => {
            return {...previousState, price: newPriceAsNumber}
        });
    };
    const updateGenericName = (newGenericName: string) => {
        setItem(previousState => {
            return {...previousState, genericName: newGenericName}
        });
    };
    const updateSize = (newSize: string) => {
        setItem(previousState => {
            return {...previousState, size: newSize}
        });
    };

    return (
        <div className="modal" id="EditItemModal">
            <div className="modal-dialog">
                <div className="modal-content">
        <div className="modal-body">
            <div className= "BlueBox">
                
                    <label>Generic Name:</label><br/>
                    <input type="text" placeholder="beans" value={item.genericName}
                           onChange={(event) => updateGenericName(event.target.value)}/><br/>
                    <label>Brand:</label><br/>
                    <input type="text" placeholder="World Famous Beans" value={item.brand}
                           onChange={(event) => updateBrand(event.target.value)}/><br/>
                    <label>Size:</label><br/>
                    <input type="text" placeholder="3oz" value={item.size}
                           onChange={(event) => updateSize(event.target.value)}/><br/>
                    <label>Price:</label><br/>
                    <input type="text" step="any" placeholder="0.00" value={item.price}
                           onChange={(event) => updatePrice(event.target.value)}/><br/>
                
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => newItemManager.editItem(ItemToJsonParse)}>Edit</a>
                <a className="btn btn-sm" href="#" role="button">Cancel</a>
            </div>
        </div>
                </div>
            </div>
        </div>

    );
};


export default EditItem;
