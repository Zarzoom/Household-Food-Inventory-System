import getItem from "../../DataModels/getItem"
import PantryContentsNoID from "../../DataModels/PantryContentsNoID"
import 'rsuite/dist/rsuite.min.css';
import Button from 'rsuite/Button';
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useState} from "react";
import {addPantryContents} from "../../Thunks/PantryContentsThunk"
import {Panel} from "rsuite";

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
        dispatch(addPantryContents(newPC));
}
    return(
        <div className="SingleItemDisplay">
            <div className={"cardBackground"} key ={"" + item.itemID}>
                <p className={"whiteText"}>
                    Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                </p>
                <Button appearance={'primary'} className={"cardButton"} onClick={()=> newPantryContentsDispatch()}>Add</Button>
                <input type= "number" onChange={ (event: any) => updateQuantity(event.target.value)}/>
            </div>
        </div> 
    )
}