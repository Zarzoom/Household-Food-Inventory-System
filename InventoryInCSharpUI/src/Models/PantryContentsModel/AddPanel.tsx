import getItem from "../../DataModels/getItem"
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {selectContainsSearch, goSetSearch} from "../../slices/ItemsReducer"
import {SingleItemForPantryContents} from "./SingleItemForPantryContents"
import {useState} from "react";
import Drawer from "rsuite/Drawer";
import { Button, Input, InputGroup, Stack } from 'rsuite';
import 'rsuite/dist/rsuite.min.css';
import SearchIcon from '@rsuite/icons/Search';
import CloseIcon from "@rsuite/icons/Close";
import AddItemModal from '../ItemModels/AddItemModal';


export const AddPanel = () =>{

    const [open,setOpen] = useState(false);
    const [openModal, setOpenModal] = useState(false);
    
    const openPanel= (setVal: boolean) =>{
        setOpen(setVal);
        dispatch(goSetSearch(""));
    }
    const placement= "right";
    const [searchInput, setSearchInput] = useState("");
    const dispatch = useAppDispatch();
    const updateSearch = (searchValue: string) =>{
        dispatch(goSetSearch(searchValue));
    }
    
    const SearchSelector = useAppSelector(state => selectContainsSearch(state));
    
    let currentItems: getItem[] = SearchSelector;

    const cancelSearchDispatch = () =>{
        setSearchInput("");
        dispatch(goSetSearch(""));
    }
    
    const renderedAllItems = currentItems.map((item: getItem) => { return (
        <div  key={"" + item.itemID}>
            <SingleItemForPantryContents {...item}></SingleItemForPantryContents>
        </div>
    )})
    

return(
    <div>
        <Stack justifyContent={'flex-end'}>
            <Button className={"yellowButton"} appearance= 'primary' onClick={() => openPanel(true)}>Add Item To Pantry</Button>
        </Stack>
        <Drawer size={'sm'} placement= {placement} open={open} onClose={() => openPanel(false)}>
            <Drawer.Header>
                <InputGroup>
                    <label>Search:</label><br/>
                    <Input type="text" placeholder="Generic or Brand Name" value={searchInput}
                           onChange={(value: string, event) => setSearchInput(value)}/>
                    <InputGroup.Button onClick={(event: any) => updateSearch(searchInput)}>
                        <SearchIcon />
                    </InputGroup.Button>
                    <InputGroup.Button onClick={(event: any) => cancelSearchDispatch()}>
                        <CloseIcon/>
                    </InputGroup.Button>
                </InputGroup>
                <AddItemModal></AddItemModal>
            </Drawer.Header>
            <Drawer.Body>
                {renderedAllItems}
            </Drawer.Body>
        </Drawer>
    </div>
)
}
export default AddPanel;