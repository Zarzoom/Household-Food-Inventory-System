import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {useState, useEffect} from "react";
import {fetchPantryContents, updatePantryContents} from "../../Thunks/PantryContentsThunk"
import Pantry from "../../DataModels/Pantry"
import PantryContents from "../../DataModels/PantryContents"
import {selectItemsByID} from "../../slices/ItemsReducer"
import getItem from "../../DataModels/getItem"
import { Button, IconButton, ButtonGroup, ButtonToolbar, InputNumber } from 'rsuite';
import {DeletePantryContents} from './DeletePantryContents'



export function SinglePantryContentDisplay(pantryContents: PantryContents){
    const [openModal, setOpenModal] = useState(false);
    
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
                    <p>
                        Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                    </p>
                </div>
                <div className= "col-md-12">
                    <p>
                        <label>Quantity</label><br/>
                        <InputNumber defaultValue={quantity.quantity} min={1} onChange={ (value:number|string, event) => updateQuantity(+value)}/>
                    </p>
                    <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => updatePantryContentDispatch()}>Edit Quantity</Button>
                    <DeletePantryContents {...pantryContent}></DeletePantryContents>
                </div>
            </div>
        </div>
    )
}