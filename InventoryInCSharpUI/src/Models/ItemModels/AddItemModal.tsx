import {useEffect, useState} from "react";
import {createItem} from "../../Thunks/ItemsThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import {useAppSelector} from "../../Hooks/hooks";
import { Button, Input, Modal } from 'rsuite';
import Login from "../../DataModels/Login";


function AddItemModal() {
    
    const [user, setUser] = useState({
        userUsername: "",
        userPassword: ""
    })
    const [open, setOpen] = useState(false);
    const loginState = useAppSelector(state=> state.Login.StateOfLogin);
    const passwordString = loginState?.password;
    
    const [item, setItem] = useState({
        brand: "",
        price: "",
        genericName: "",
        size: "",
        password: passwordString,
    });

    const updateUserUsername = (newUserUsername: string) =>{
        setUser(previousState => {
            return {...previousState, userUsername: newUserUsername}
        });
    };
    const updateUserPassword = (newUserPassword: string) =>{
        setUser(previousState => {
            return { ...previousState, userPassword: newUserPassword}
        });
    };
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
        if(item.price !== "" && item.size !== "" && item.brand !== "" && item.genericName !== "") {
            const ItemToJSONStringify = JSON.stringify(item);
            const ItemToJsonParse = JSON.parse(ItemToJSONStringify);
            dispatch(createItem(ItemToJsonParse));
        }
        else{
          
            alert("Please fill in all fields.");
        }
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
        <Button className={"displayBoxButton"} appearance={ 'primary'} onClick={(event: any)=>setOpen(true)}>Add New Item</Button>
            <Modal open={open} onClose={()=>setOpen(false)} >
                <Modal.Header></Modal.Header>
            <div className={"modalBackground"}>
                <Modal.Body>

                        <label className={"whiteText"}>Generic Name:</label><br/>
                        <Input placeholder="beans" value={item.genericName}
                               onChange={(value: string, event) => updateGenericName(value)}/><br/>
                        <label className={"whiteText"}>Brand:</label><br/>
                        <Input placeholder="World Famous Beans" value={item.brand}
                               onChange={(value: string, event) => updateBrand(value)}/><br/>
                        <label className={"whiteText"}>Size:</label><br/>
                        <Input placeholder="3oz" value={item.size}
                               onChange={(value: string, event) => updateSize(value)}/><br/>
                        <label className={"whiteText"}>Price:</label><br/>
                        <Input step="any" placeholder="0.00" value={item.price}
                               onChange={(value: string, event) => updatePrice(value)}/><br/>
                    <Button className={"cardButton"} appearance={'primary'} onClick={(event: any) => NewItemDispatch()}>Add</Button>
                    <Button className={"cardButton"} appearance={'primary'} onClick={(event: any) => CancelAddItem()}>Cancel</Button>
                </Modal.Body>
            </div>
        </Modal>
        </div>
    );
}

export default AddItemModal;