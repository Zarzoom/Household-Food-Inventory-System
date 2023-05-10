import getItem from "../../DataModels/getItem"
import PantryContentsNoID from "../../DataModels/PantryContentsNoID"
import 'rsuite/dist/rsuite.min.css';
import Button from 'rsuite/Button';
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useState} from "react";
import {selectPantryFilter} from "../../slices/PantryContentsReducer"
import {addPantryContents} from "../../Thunks/PantryContentsThunk"


export function SingleItemForPantryContents(item: getItem){
    let pantryID : number| null = useAppSelector(state => state.PantryContents.PantryFilter)
    pantryID = pantryID as number;
    const [newPC, setNewPC] = useState<PantryContentsNoID>({
        pcItemID: item.itemID,
        pcPantryID: pantryID,
        quantity: 1 ,
    });
    const updateQuantity = (inputQuantity: string) => {
        const newPCQuantity: number = +inputQuantity
        setNewPC(previousState => {
            return {...previousState, quantity: newPCQuantity}
        })
    }
    const dispatch = useAppDispatch();
const newPantryContentsDispatch = () =>{
    const newPantryContents = newPC;
        dispatch(addPantryContents(newPC));
}
    return(
        <div className="SingleItemDisplay">
            <div className="BlueBox" key ={"" + item.itemID}>
                <p>
                    Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                </p>
                <Button appearance={'primary'} color={'cyan'} onClick={()=> newPantryContentsDispatch()}>Add</Button>
                <input type= "number" onChange={ (event: any) => updateQuantity(event.target.value)}/>
            </div>
        </div> 
    )
}