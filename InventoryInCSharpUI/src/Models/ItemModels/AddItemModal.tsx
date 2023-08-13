import {useState} from "react";
import {createItem} from "../../Thunks/ItemsThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import {useAppSelector} from "../../Hooks/hooks";
import { Button, Input, Modal } from 'rsuite';
import Login from "../../DataModels/Login";


function AddItemModal() {
    
    const [open, setOpen] = useState(false);

    
    
    const [item, setItem] = useState({
        brand: "",
        price: "",
        genericName: "",
        size: "",
    });

    const updateBrand = (newBrand: string) => {
        setItem(previousState => {
            return {...previousState, brand: newBrand}
        });
    };
    const updatePrice = (newPrice: string) => {
        setItem(previousState => {
            return {...previousState, price: newPrice}
        });
    };
    const updateGenericName = (newGenericName: string) => {
        setItem(previousState => {
            return {...previousState, genericName: newGenericName}
        });
    };
    const updateSize = (newSize: string) => {
        setItem(previousState => {
            return {...previousState, size: newSize}
        });
    };
    

    const dispatch = useAppDispatch();
    const NewItemDispatch = () =>{
        const ItemToJSONStringify = JSON.stringify(item);
        const ItemToJsonParse = JSON.parse(ItemToJSONStringify);
        dispatch(createItem(ItemToJsonParse));
    };
    const CancelAddItem= () =>{
        setItem(previousState => {
            return {...previousState, size: ""}});
        setItem(previousState => {
            return {...previousState, genericName: ""}});
        setItem(previousState => {
            return {...previousState, price: ""}});
        setItem(previousState => {
            return {...previousState, brand: ""}});
        setOpen(false);
    }
    
    return (
        <div>
        <Button className={"yellowButton"} appearance={ 'primary'} onClick={(event: any)=>setOpen(true)}>Add New Item</Button>
            <Modal open={open} onClose={()=>setOpen(false)}>
                <Modal.Header></Modal.Header>
            <div className= "blueBackground">
                <Modal.Body>

                        <label>Generic Name:</label><br/>
                        <Input placeholder="beans" value={item.genericName}
                               onChange={(value: string, event) => updateGenericName(value)}/><br/>
                        <label>Brand:</label><br/>
                        <Input placeholder="World Famous Beans" value={item.brand}
                               onChange={(value: string, event) => updateBrand(value)}/><br/>
                        <label>Size:</label><br/>
                        <Input placeholder="3oz" value={item.size}
                               onChange={(value: string, event) => updateSize(value)}/><br/>
                        <label>Price:</label><br/>
                        <Input step="any" placeholder="0.00" value={item.price}
                               onChange={(value: string, event) => updatePrice(value)}/><br/>
                    <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => NewItemDispatch()}>Add</Button>
                    <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => CancelAddItem()}>Cancel</Button>
                </Modal.Body>
            </div>
        </Modal>
        </div>
    );
};

export default AddItemModal;