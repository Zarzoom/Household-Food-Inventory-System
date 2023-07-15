import {useState} from "react";
import {createPantry} from "../../Thunks/PantriesThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import {Button, Input, Modal} from 'rsuite';

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
        if(pantry.pantryName !== "") {
            const PantryToJSONStringify = JSON.stringify(pantry);
            const PantryToJsonParse = JSON.parse(PantryToJSONStringify);
            dispatch(createPantry(PantryToJsonParse));
        }
        else{
            alert("Please provide a Pantry Name.");
        }
    };
    
    const CancelAddPantry= () =>{
        setPantry(previousState => {
            setOpen(false)
            return {...previousState, pantryName: ""}});
    }
    return (
        <div>
            <Button className={"yellowButton"} appearance={ 'primary'} onClick={()=>setOpen(true)}>Add Pantry</Button>
            <Modal open={open} onClose={()=>setOpen(false)}>
                
        <div className="col-md-12">
            <div className= "BlueBox">
                <Modal.Body>
                <p>
                    <label className={"whiteText"}>Pantry Name:</label><br/>
                    <Input type="text" placeholder="Freezer" value={pantry.pantryName}
                           onChange={(value: string, event) => updatePantryName(value)}/><br/>

                </p>
            </Modal.Body>
            <Modal.Footer>
                <Button appearance={'primary'} className={"yellowButton"} onClick={(event: any) => NewPantryDispatch()}>Add</Button>
                <Button appearance={'primary'} className={"yellowButton"} onClick={(event: any) => CancelAddPantry()}>Cancel</Button>
            </Modal.Footer>
                
            </div>
        </div>
</Modal>
</div>
    );
}




export default AddPantryModal;