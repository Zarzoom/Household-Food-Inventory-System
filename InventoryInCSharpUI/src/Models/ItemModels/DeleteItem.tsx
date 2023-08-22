import getItem from "../../DataModels/getItem"
import {useAppDispatch} from '../../Hooks/hooks'
import {deleteItem} from "../../Thunks/ItemsThunk"
import {useState} from "react";
import {Button, Modal} from 'rsuite';

export function DeleteItem(ItemForDeleting: getItem) {
    const [open, setOpen] = useState(false);
    
    const dispatch = useAppDispatch();
    const deleteItemDispatch = () => {
        const ItemIDForDeleting = ItemForDeleting.itemID;
        const ItemPasswordForDeleting = ItemForDeleting.password;
        dispatch(deleteItem(ItemIDForDeleting, ItemPasswordForDeleting));
            };
 
        return (
            <div>
                <Button className={"cardButton"} appearance={ 'primary'} onClick={()=>setOpen(true)}>Delete</Button>
                <Modal open={open} onClose= {() => setOpen(false)}>
                        <Modal.Header>
                            
                        </Modal.Header>
                        <div className={"modalBackground"}>
                            <Modal.Body>
                                <p className={"whiteText"}>
                                    Generic Name: {ItemForDeleting.genericName}<br/>Brand
                                    Name: {ItemForDeleting.brand}<br/>Size: {ItemForDeleting.size}<br/>Price: {ItemForDeleting.price}
                                </p>
                            </Modal.Body>
                            <Modal.Footer>
                                <Button appearance={'primary'} className={"modalButton"} onClick={(event: any) => deleteItemDispatch()}>Delete</Button>
                                <Button appearance={'primary'} className={"modalButton"} onClick={()=> setOpen(false)}>Cancel</Button>
                                
                            </Modal.Footer>
                    </div>
                </Modal>
            </div>
        )

}
