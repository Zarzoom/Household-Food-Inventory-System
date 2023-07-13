import {useState} from "react";
import {updatePantry} from "../../Thunks/PantriesThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import {Modal, Button, Input} from 'rsuite'
export function EditPantry(pantryForUpdate: Pantry) {

    const[open, setOpen] = useState(false);
    
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

        <div>
            <Button className={"yellowButton"} appearance={'primary'} onClick={()=>setOpen(true)}>Edit</Button>
            <Modal open={open} onClose={()=> setOpen(false)}>
                <Modal.Header>
                    
                </Modal.Header>
                <Modal.Body>
                    <div className= "BlueBox">
                        <p>
                            <label className={"whiteText"}>Pantry Name:</label><br/>
                            <Input type="text" placeholder="beans" value={pantry.pantryName}
                                   onChange={(value: string, event) => updatePantryName(value)}/>                          
                        </p>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button appearance={'primary'} className={"yellowButton"} onClick={(event: any) => UpdatedPantryDispatch()}>Edit</Button>
                    <Button appearance={'primary'} className={"yellowButton"}>Cancel</Button> 
                </Modal.Footer>
            </Modal>
        </div>

    );
}



