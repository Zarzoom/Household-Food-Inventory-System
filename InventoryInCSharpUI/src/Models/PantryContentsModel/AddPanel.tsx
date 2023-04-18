import PantryContents from "../../DataModels/PantryContents"
import getItem from "../../DataModels/getItem"
import Drawer from "rsuite/Drawer";
import Button from 'rsuite/Button';
import IconButton from 'rsuite/IconButton';
import {useState, useEffect, Component} from "react";
import 'rsuite/dist/rsuite.min.css';
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {selectAllItems} from "../../slices/ItemsReducer"
import {SingleItemForPantryContents} from "./SingleItemForPantryContents"



export const AddPanel = () =>{
    const AllItems = useAppSelector(selectAllItems)
    const [open,setOpen] = useState(false);
    const placement= "right";

    const renderedAllItems = AllItems.map((item: getItem) => { return (
        <div  key={"" + item.itemID}>
            <SingleItemForPantryContents {...item}></SingleItemForPantryContents>
        </div>
    )})
return(
    <div>
<Button appearance= 'primary' color={'cyan'} onClick={() => setOpen(true)}>Add Item To Pantry</Button>
<Drawer placement= {placement} open={open} onClose={() => setOpen(false)}>
    <Drawer.Body>
        {renderedAllItems}
    </Drawer.Body>
</Drawer>
    </div>
)
}
export default AddPanel;