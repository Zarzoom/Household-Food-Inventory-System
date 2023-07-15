import {useState} from "react";
import {updateItem} from "../../Thunks/ItemsThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import getItem from "../../DataModels/getItem"
import {Button, Input, Modal} from 'rsuite';

export function EditItem(itemForUpdate: getItem) {
    
    const [open, setOpen] = useState(false);

    const [item, setItem] = useState({
        itemID: itemForUpdate.itemID,
        brand: itemForUpdate.brand,
        price: "" + itemForUpdate.price,
        genericName: itemForUpdate.genericName,
        size: itemForUpdate.size,
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
    const UpdatedItemDispatch = () =>{
        const ItemToJSONStringify = JSON.stringify(item);
        const ItemToJsonParse = JSON.parse(ItemToJSONStringify);
        dispatch(updateItem(ItemToJsonParse));
        return 'Added'
    };
    


    return (
        <div>
            <Button className={"yellowButton"} appearance={'primary'} onClick={()=> setOpen(true)}>Edit</Button>
            <Modal open={open} onClose= {() => setOpen(false)}>
                <div className={"BlueBox"}>
                    <Modal.Header>
                      
                    </Modal.Header>
                    <Modal.Body>
                        <p>
                            <label className={"whiteText"}>Generic Name:</label><br/>
                            <Input type="text" placeholder="beans" value={item.genericName}
                                   onChange={ (value:string, event) => updateGenericName(value)}/><br/>
                            <label className={"whiteText"}>Brand:</label><br/>
                            <Input type="text" placeholder="World Famous Beans" value={item.brand}
                                   onChange={ (value:string, event) => updateBrand(value)}/><br/>
                            <label className={"whiteText"}>Size:</label><br/>
                            <Input type="text" placeholder="3oz" value={item.size}
                                   onChange={(value: string, event) => updateSize(value)}/><br/>
                            <label className={"whiteText"}>Price:</label><br/>
                            <Input type="text" step="any" placeholder="0.00" value={item.price}
                                   onChange={(value: string, event) => updatePrice(value)}/><br/>
                        </p> 
                    </Modal.Body>
                    <Modal.Footer>
                        <Button appearance={'primary'} className={"yellowButton"} onClick={(event: any) => UpdatedItemDispatch()}>Edit</Button>
                        <Button appearance={'primary'} className={"yellowButton"} onClick={()=> setOpen(false)}>Cancel</Button>
                    </Modal.Footer>
                </div>
            </Modal>
        </div>
       
    );
}



