import {useState} from "react";
import {updateItem} from "../../Thunks/ItemsThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import getItem from "../../DataModels/getItem"
import ObjectAndState from "../../DataModels/ObjectAndState"
export function EditItem(ItemAndState: ObjectAndState) {
    const itemForUpdate = ItemAndState.itemForGet as getItem;
    
    // const [display, setDisplay] = useState(
    //     {
    //         show: ItemAndState.state
    //     }
    // )

    const [item, setItem] = useState({
        itemID: itemForUpdate.itemID,
        brand: itemForUpdate.brand,
        price: "" + itemForUpdate.price,
        genericName: itemForUpdate.genericName,
        size: itemForUpdate.size,
    });

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
    const dispatch = useAppDispatch();
    const UpdatedItemDispatch = () =>{
        const ItemToJSONStringify = JSON.stringify(item);
        const ItemToJsonParse = JSON.parse(ItemToJSONStringify);
        dispatch(updateItem(ItemToJsonParse));
        return 'Added'
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
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => UpdatedItemDispatch()}>Edit</a>
                <a className="btn btn-sm" href="#" role="button">Cancel</a>
            </div>
        </div>

    );
};



