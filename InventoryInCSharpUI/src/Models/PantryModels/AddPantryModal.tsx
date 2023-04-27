import {useState} from "react";
import {createPantry} from "../../Thunks/PantriesThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import PantryNoID from "../../DataModels/PantryNoID"
import { Form, Button, Input, Modal } from 'rsuite';




function AddPantryModal() {
    
    const [open, setOpen] = useState(false);
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
        <div>
            <Button appearance={ 'primary'} color={'cyan'} onClick={()=>setOpen(true)}>Add Item</Button>
            <Modal open={open} onClose={()=>setOpen(false)}>
                
        <div className="col-md-12">
            <div className= "BlueBox">
                <Modal.Body>
                <p>
                    <label>Pantry Name:</label><br/>
                    <input type="text" placeholder="Freezer" value={pantry.pantryName}
                           onChange={(event) => updatePantryName(event.target.value)}/><br/>

                </p>
            </Modal.Body>
            <Modal.Footer>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => NewPantryDispatch()}>Add</a>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => CancelAddPantry()}>Cancel</a>
            </Modal.Footer>
                
            </div>
        </div>
</Modal>
</div>
    );
};




export default AddPantryModal;