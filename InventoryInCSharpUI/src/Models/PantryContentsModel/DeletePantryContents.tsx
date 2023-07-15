import PantryContents from '../../DataModels/PantryContents'
import {deletePantryContents} from '../../Thunks/PantryContentsThunk'
import {useAppDispatch} from '../../Hooks/hooks'
import {Button, Modal} from "rsuite";
import {useState} from "react";

export function DeletePantryContents(pantryContentsForDelete: PantryContents){
    const dispatch = useAppDispatch();
    const [openModal, setOpenModal] = useState(false);
    
    const deletePantryContentDispatch = () =>{
        const pantryContentIDForDelete: number = pantryContentsForDelete.pantryContentID;
        dispatch(deletePantryContents(pantryContentIDForDelete));
        setOpenModal(false);
    };
    
    return(
        <div>
            <Button className={"yellowButton"} appearance={'primary'} onClick={()=>setOpenModal(true)}>Delete</Button>
            <Modal open={openModal} onClose={()=>setOpenModal(false)}>
                <Modal.Header></Modal.Header>
            <div>
                <div className=" BlueBox">
                    <Modal.Body>
                    <p className={"whiteText"}>
                        Are you sure?
                    </p>
                    </Modal.Body>
                    <Modal.Footer>
                    <Button className={"yellowButton"} onClick={()=>setOpenModal(false)}>Cancel</Button>
                    <Button className={"yellowButton"} onClick={(event: any) => deletePantryContentDispatch()}>Delete</Button>
                </Modal.Footer>
                </div>
            </div>
            </Modal>
        </div> 
    );
}