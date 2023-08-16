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
        setOpen(false);
        return 'Added'
    };

    return (

        <div>
            <Button className={"cardButton"} appearance={'primary'} onClick={()=>setOpen(true)}>Edit</Button>
            <Modal open={open} onClose={()=> setOpen(false)}>
                <Modal.Header>
                </Modal.Header>
                <div className= "modalBackground">
                    <Modal.Body>
                        
                            <p>
                                <label className={"whiteText"}>Pantry Name:</label><br/>
                                <Input type="text" placeholder="beans" value={pantry.pantryName}
                                       onChange={(value: string, event) => updatePantryName(value)}/>                          
                            </p>
                        
                    </Modal.Body>
                    <Modal.Footer>
                        <Button appearance={'primary'} className={"modalButton"} onClick={(event: any) => UpdatedPantryDispatch()}>Edit</Button>
                        <Button appearance={'primary'} className={"modalButton"} onClick={()=>setOpen(false)}>Cancel</Button> 
                    </Modal.Footer>
                </div>
            </Modal>
        </div>

    );
}



