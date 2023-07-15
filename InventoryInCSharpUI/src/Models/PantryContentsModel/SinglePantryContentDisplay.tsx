import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useState} from "react";
import {updatePantryContents} from "../../Thunks/PantryContentsThunk"
import PantryContents from "../../DataModels/PantryContents"
import {selectItemsByID} from "../../slices/ItemsReducer"
import getItem from "../../DataModels/getItem"
import { Button, InputNumber} from 'rsuite';
import {DeletePantryContents} from './DeletePantryContents'


export function SinglePantryContentDisplay(pantryContents: PantryContents){
    
    const pantryContentsForSelection = pantryContents as PantryContents;
    const [quantity, setQuantity] = useState<PantryContents>({
        pcPantryID: pantryContentsForSelection.pcPantryID,
        pcItemID: pantryContentsForSelection.pcItemID,
        pantryContentID: pantryContentsForSelection.pantryContentID,
        quantity: pantryContentsForSelection.quantity
    })
    const pantryContent = quantity as PantryContents;
    const updateQuantity = (inputQuantity: number) => {
        setQuantity(previousState => {
            return {...previousState, quantity: inputQuantity}
        })
    }
    const dispatch = useAppDispatch();
    const updatePantryContentDispatch = () =>{
        const PantryContentsJSONStringify = JSON.stringify(quantity);
        const PantryContentsJSONParse = JSON.parse(PantryContentsJSONStringify);
        dispatch(updatePantryContents(PantryContentsJSONParse));
    }
    const pantryItem = useAppSelector(state=>selectItemsByID(state, pantryContents.pcItemID))
    const item = pantryItem as getItem;
    return(
        <div className= "SinglePantryContentDisplay">
            <div className= "BlueBox" key = {"" + pantryContents.pantryContentID +pantryContents.pcItemID}>
                <div className="col-md-9">
                    <p className={"whiteText"}>
                        Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                    </p>
                </div>
                <div className= "col-md-12">
                    <p>
                        <label className={"whiteText"}>Quantity</label><br/>
                        <InputNumber defaultValue={quantity.quantity} min={1} onChange={ (value:number|string, event) => updateQuantity(+value)}/>
                    </p>
                    <Button className={"yellowButton"}  appearance={'primary'} onClick={(event: any) => updatePantryContentDispatch()}>Edit Quantity</Button>
                    <DeletePantryContents {...pantryContent}></DeletePantryContents>
                </div>
            </div>
        </div>
    )
}