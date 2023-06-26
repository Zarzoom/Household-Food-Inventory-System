import getItem from "../../DataModels/getItem"
import {useAppDispatch} from '../../Hooks/hooks'
import {deleteItem} from "../../Thunks/ItemsThunk"
import {useState, useEffect, Component, SetStateAction} from "react";
import {goSetStatus} from "../../slices/PantryContentsReducer"
import{fetchPantryContents} from "../../Thunks/PantryContentsThunk"
import { Form, Button, Input, Modal } from 'rsuite';




export function DeleteItem(ItemForDeleting: getItem) {
    const [open, setOpen] = useState(false);
    
    const dispatch = useAppDispatch();
    const deleteItemDispatch = () => {
        const ItemIDForDeleting = ItemForDeleting.itemID;
        dispatch(deleteItem(ItemIDForDeleting));
            };
    // Cancel doesn't work
 
        return (
            <div>
                <Button className={"yellowButton"} appearance={ 'primary'} onClick={()=>setOpen(true)}>Delete</Button>
                <Modal open={open} onClose= {() => setOpen(false)}>
                    <div className={"BlueBox"}>
                        <Modal.Header>
                            
                        </Modal.Header>
                        <Modal.Body>
                            <p>
                                Generic Name: {ItemForDeleting.genericName}<br/>Brand
                                Name: {ItemForDeleting.brand}<br/>Size: {ItemForDeleting.size}<br/>Price: {ItemForDeleting.price}
                            </p>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button appearance={'primary'} color={'cyan'} onClick={(event: any) => deleteItemDispatch()}>Delete</Button>
                            <Button appearance={'primary'} color={'cyan'} onClick={()=> setOpen(false)}>Cancel</Button>
                            
                        </Modal.Footer>
                    </div>
                </Modal>
            </div>
        )

}
