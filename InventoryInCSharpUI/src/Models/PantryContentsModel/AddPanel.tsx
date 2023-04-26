import PantryContents from "../../DataModels/PantryContents"
import getItem from "../../DataModels/getItem"
import Drawer from "rsuite/Drawer";
import { Input, InputGroup } from 'rsuite';
import { Button, IconButton, ButtonGroup, ButtonToolbar } from 'rsuite';
import {useState, useEffect, Component} from "react";
import 'rsuite/dist/rsuite.min.css';
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {selectAllItems, selectContainsSearch,goSetSearch} from "../../slices/ItemsReducer"
import {fetchItems} from "../../Thunks/ItemsThunk"
import {SingleItemForPantryContents} from "./SingleItemForPantryContents"
import SearchIcon from '@rsuite/icons/Search';
import StarIcon from '@rsuite/icons/legacy/Star';


export const AddPanel = () =>{
    // const AllItems = useAppSelector(selectAllItems)

    const [open,setOpen] = useState(false);
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
    

    
    const renderedAllItems = currentItems.map((item: getItem) => { return (
        <div  key={"" + item.itemID}>
            <SingleItemForPantryContents {...item}></SingleItemForPantryContents>
        </div>
    )})

//TODO: have clear search empty search field. 

return(
    <div>
<Button appearance= 'primary' color={'cyan'} onClick={() => openPanel(true)}>Add Item To Pantry</Button>
<Drawer placement= {placement} open={open} onClose={() => openPanel(false)}>
    <Drawer.Header>
        {/*<InputGroup size={'md'}>*/}
        {/*    <Input type= "text" defaultValue= "" onChange={(value: string, event: any)=>setSearchValue(value)}/>*/}
        {/*    <InputGroup.Button >*/}
        {/*        <SearchIcon onClick = {(event:any)=>SearchDispatch()}/>*/}
        {/*    </InputGroup.Button>*/}
        {/*</InputGroup>*/}
        <label>Search</label><input type= "text" onChange={(event: any)=>setSearchInput(event.target.value)}/>
        <IconButton size={'sm'} icon={<SearchIcon/>} onClick = {(event:any)=>updateSearch(searchInput)}></IconButton>
        <IconButton size={'sm'} icon={<StarIcon/>} onClick = {(event:any)=> updateSearch(" ")}></IconButton>
    </Drawer.Header>
    <Drawer.Body>
        {renderedAllItems}
    </Drawer.Body>
</Drawer>
    </div>
)
}
export default AddPanel;