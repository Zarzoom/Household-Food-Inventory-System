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
            setOpen(false)
            return {...previousState, pantryName: ""}});
    }
    return (
        <div>
            <Button appearance={ 'primary'} color={'cyan'} onClick={()=>setOpen(true)}>Add Pantry</Button>
            <Modal open={open} onClose={()=>setOpen(false)}>
                
        <div className="col-md-12">
            <div className= "BlueBox">
                <Modal.Body>
                <p>
                    <label>Pantry Name:</label><br/>
                    <Input type="text" placeholder="Freezer" value={pantry.pantryName}
                           onChange={(value: string, event) => updatePantryName(value)}/><br/>

                </p>
            </Modal.Body>
            <Modal.Footer>
                <Button appearance={'primary'} color={'cyan'} onClick={(event: any) => NewPantryDispatch()}>Add</Button>
                <Button appearance={'primary'} color={'cyan'} onClick={(event: any) => CancelAddPantry()}>Cancel</Button>
            </Modal.Footer>
                
            </div>
        </div>
</Modal>
</div>
    );
};




export default AddPantryModal;