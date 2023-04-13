import {useState} from "react";
import {createPantry} from "../../Thunks/PantriesThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import PantryNoID from "../../DataModels/PantryNoID"


// const newItemManager = new ItemManager();
// const NewItemDispatch = (newItem: any) =>{
//     const ItemToJSONStringify = JSON.stringify(newItem);
//     const ItemToJsonParse = JSON.parse(ItemToJSONStringify);
//     const dispatch = useAppDispatch();
//     dispatch(createItem(ItemToJsonParse));
//}
function NewPantry() {

   const [pantry, setPantry] = useState({
        pantryName: ""
    });

    const updatePantryName = (newPantryName: string) => {
        setPantry(previousState => {
            return {...previousState, pantryName: newPantryName}
        });
    };
    
    const dispatch = useAppDispatch();
    const NewPantryDispatch = () =>{
        const PantryToJSONStringify = JSON.stringify(pantry);
        const PantryToJsonParse = JSON.parse(PantryToJSONStringify);
        dispatch(createPantry(PantryToJsonParse));
    };
 const CancelAddPantry= () =>{
     setPantry(previousState => {
         return {...previousState, pantryName: ""}});
 }
    return (

        <div className="col-md-3">
         <div className= "BlueBox">
            <p>
                <label>Pantry Name:</label><br/>
                <input type="text" placeholder="Freezer" value={pantry.pantryName}
                       onChange={(event) => updatePantryName(event.target.value)}/><br/>

            </p>
            <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => NewPantryDispatch()}>Add</a>
            <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => CancelAddPantry()}>Cancel</a>
         </div>
        </div>
    );
};




export default NewPantry;