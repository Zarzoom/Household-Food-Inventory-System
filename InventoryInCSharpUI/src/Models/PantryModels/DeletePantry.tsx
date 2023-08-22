import Pantry from "../../DataModels/Pantry"
import {useAppDispatch} from '../../Hooks/hooks'
import {deletePantry} from "../../Thunks/PantriesThunk"
import {useState} from "react";
import {Modal, Button} from 'rsuite'

export function DeletePantry(pantryForDelete: Pantry) {

    const [open, setOpen] = useState(false)
    const dispatch = useAppDispatch();
    const deletePantryDispatch = () => {
        const PantryIDForDeleting = pantryForDelete.pantryID;
        dispatch(deletePantry(PantryIDForDeleting));
            };
 
    
    return (
        <div>
            <Button className={"cardButton"} appearance={'primary'} onClick={()=> setOpen(true)}>Delete</Button>
            <Modal open={open} onClose= {() => setOpen(false)}>
                    <Modal.Header>
                    </Modal.Header>
                    <div className={"modalBackground"}>
                        <Modal.Body>
                            <p className={"whiteText"}>
                                Pantry Name: {pantryForDelete.pantryName}
                            </p>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button appearance={'primary'} className={"modalButton"} onClick={()=>setOpen(false)}>Cancel</Button>
                            <Button appearance={'primary'} className={"modalButton"} onClick={(event: any) => deletePantryDispatch()}>Delete</Button>
                        </Modal.Footer>
                    </div>
                </Modal>
        </div>
    )
};
