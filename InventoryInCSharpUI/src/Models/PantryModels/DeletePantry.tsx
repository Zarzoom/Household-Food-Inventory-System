﻿import Pantry from "../../DataModels/Pantry"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import {deletePantry} from "../../Thunks/PantriesThunk"
import {useState, useEffect, Component, SetStateAction} from "react";
import {Modal, Button} from 'rsuite'

//working on installing a modal

export function DeletePantry(pantryForDelete: Pantry) {

    const [open, setOpen] = useState(false)
    const dispatch = useAppDispatch();
    const deletePantryDispatch = () => {
        const PantryIDForDeleting = pantryForDelete.pantryID;
        dispatch(deletePantry(PantryIDForDeleting));
            };
 
    
        return (
            <div>
                <Button className={"yellowButton"} appearance={'primary'} onClick={()=> setOpen(true)}>Delete</Button>
                <Modal open={open} onClose= {() => setOpen(false)}>
                    <div className={"BlueBox"}>
                        <Modal.Header>
                        </Modal.Header>
                        <Modal.Body>
                            <p className={"whiteText"}>
                                Pantry Name: {pantryForDelete.pantryName}
                            </p>
    
                        </Modal.Body>
                        <Modal.Footer>
                            <Button appearance={'primary'} className={"yellowButton"} onClick={()=>setOpen(false)}>Cancel</Button>
                            <Button appearance={'primary'} className={"yellowButton"} onClick={(event: any) => deletePantryDispatch()}>Delete</Button>
                        </Modal.Footer>
                    </div>
                </Modal>
            </div>
        );
}
