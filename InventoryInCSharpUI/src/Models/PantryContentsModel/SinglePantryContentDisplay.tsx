import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {useState, useEffect} from "react";
import {fetchPantryContents, updatePantryContents} from "../../Thunks/PantryContentsThunk"
import Pantry from "../../DataModels/Pantry"
import PantryContents from "../../DataModels/PantryContents"
import {selectItemsByID} from "../../slices/ItemsReducer"
import getItem from "../../DataModels/getItem"
import { Button, IconButton, ButtonGroup, ButtonToolbar } from 'rsuite';
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
    const updateQuantity = (inputQuantity: string) => {
        const newQuantity = +inputQuantity
        setQuantity(previousState => {
            return {...previousState, quantity: newQuantity}
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
                        <input type= "number" value = {quantity.quantity} onChange={ (event) => updateQuantity(event.target.value)}/>
                    </p>
                    <a className= "btn btn-sm" href="#" role = "button" onClick={(event: any) => updatePantryContentDispatch()}>Edit Quantity</a>
                    <DeletePantryContents {...pantryContent}></DeletePantryContents>
                </div>
            </div>
        </div>
    )
}