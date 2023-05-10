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
    };
    
    return(
        <div>
            <Button appearance={'primary'} color={"cyan"} onClick={()=>setOpenModal(true)}>Delete</Button>
            <Modal open={openModal} onClose={()=>setOpenModal(false)}>
                <Modal.Header></Modal.Header>
            <div>
                <div className=" BlueBox">
                    <Modal.Body>
                    <p>
                        Are you sure?
                    </p>
                    </Modal.Body>
                    <Modal.Footer>
                    <a className="btn btn-sm" href="#" role="button">Cancel</a>
                    <a className="btn btn-sm" href="#" role="button"
                       onClick={(event: any) => deletePantryContentDispatch()}>Delete</a>
                </Modal.Footer>
                </div>
            </div>
            </Modal>
        </div> 
    );
}