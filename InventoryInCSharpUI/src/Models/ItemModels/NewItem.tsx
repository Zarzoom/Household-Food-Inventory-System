import {useState} from "react";
import ItemManager from "../../Services/Managers/ItemManager"

const newItemManager = new ItemManager();
function NewItem() {

    const [item, setItem] = useState({
        brand: "",
        price: "",
        genericName: "",
        size: "",
    });
const ItemToJSONStringify = JSON.stringify(item);
const ItemToJsonParse = JSON.parse(ItemToJSONStringify);
    const updateBrand = (newBrand: string) => {
        setItem(previousState => {
            return {...previousState, brand: newBrand}
        });
    };
    const updatePrice = (newPrice: string) => {
        setItem(previousState => {
            return {...previousState, price: newPrice}
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

        <div className="col-md-3">
         <div className= "BlueBox">
            <p>
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
            </p>
            <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => newItemManager.addItem(ItemToJsonParse)}>Add</a>
            <a className="btn btn-sm" href="#" role="button">Cancel</a>
         </div>
        </div>

    );
};


export default NewItem;