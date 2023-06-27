import {useState} from "react";
import {createItem} from "../../Thunks/ItemsThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import Item from "../../DataModels/Item"
import { InputGroup, Button, Input, Modal, Grid, Row, Col, Form, Schema} from 'rsuite';
import React from "react";


function AddItemModalForm() {
    
    const [open, setOpen] = useState(false);

    
    const [item, setItem] = useState<Record<string, any>>({
        brand: "",
        price: "",
        genericName: "",
        size: "",
    });

    const itemModel = Schema.Model({
        brand: Schema.Types.StringType().isRequired("This field is required"),
        price: Schema.Types.StringType().isRequired("This field is required"),
        genericName: Schema.Types.StringType().isRequired("This field is required"),
        size: Schema.Types.StringType().isRequired("This field is required"),
    })
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
                    <Form fluid onChange={(formValue, event?) => setItem(formValue)} formValue={item} model={itemModel}>
                        <Form.Group>
                            <Form.ControlLabel>Generic Name</Form.ControlLabel>
                            <Form.Control 
                                    name={"genericName"}
                                    placeholder={"pie"}Yeah
                                    
                            />
                        </Form.Group>
                        <Form.Group>
                            <Form.ControlLabel>size</Form.ControlLabel>
                            <Form.Control name={"size"}/>
                        </Form.Group>
                        <Form.Group>
                            <Form.ControlLabel>brand</Form.ControlLabel>
                            <Form.Control name={"brand"}/>
                        </Form.Group>
                        <Form.Group>
                            <Form.ControlLabel>price</Form.ControlLabel>
                            <Form.Control name={"price"}/>
                        </Form.Group>
                        <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => NewItemDispatch()}>Add</Button>
                        <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => CancelAddItem()}>Cancel</Button>
                    </Form>
                </Modal.Body>
            </div>
        </Modal>
        </div>
    );
};

export default AddItemModalForm;