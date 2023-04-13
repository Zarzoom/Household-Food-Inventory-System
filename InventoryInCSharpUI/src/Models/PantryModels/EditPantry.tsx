import {useState} from "react";
import {updatePantry} from "../../Thunks/PantriesThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import ObjectAndState from "../../DataModels/ObjectAndState"
export function EditPantry(pantryForUpdate: Pantry) {


    const [pantry, setPantry] = useState({
        pantryID: pantryForUpdate.pantryID,
        pantryName: pantryForUpdate.pantryName
    });

    const updatePantryName = (newPantryName: string) => {
        setPantry(previousState => {
            return {...previousState, pantryName: newPantryName}
        });
    }
    
    const dispatch = useAppDispatch();
    const UpdatedPantryDispatch = () =>{
        const PantryToJSONStringify = JSON.stringify(pantry);
        const PantryToJsonParse = JSON.parse(PantryToJSONStringify);
        dispatch(updatePantry(PantryToJsonParse));
        return 'Added'
    };

    return (

        <div className="col-md-3">
            <div className= "BlueBox">
                <p>
                    <label>Pantry Name:</label><br/>
                    <input type="text" placeholder="beans" value={pantry.pantryName}
                           onChange={(event) => updatePantryName(event.target.value)}/><br/>
                    
                </p>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => UpdatedPantryDispatch()}>Edit</a>
                <a className="btn btn-sm" href="#" role="button">Cancel</a>
            </div>
        </div>

    );
};



