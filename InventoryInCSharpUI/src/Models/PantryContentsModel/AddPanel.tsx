import PantryContents from "../../DataModels/PantryContents"
import getItem from "../../DataModels/getItem"
import Drawer from "rsuite/Drawer";
import { Input, InputGroup } from 'rsuite';
import { Button, IconButton, ButtonGroup, ButtonToolbar } from 'rsuite';
import {useState, useEffect, Component} from "react";
import 'rsuite/dist/rsuite.min.css';
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {selectAllItems, selectContainsSearch} from "../../slices/ItemsReducer"
import {contentsItemSearch, fetchItems, getSelectedItemData} from "../../Thunks/ItemsThunk"
import {SingleItemForPantryContents} from "./SingleItemForPantryContents"
import SearchIcon from '@rsuite/icons/Search';
import StarIcon from '@rsuite/icons/legacy/Star';


export const AddPanel = () =>{
    // const AllItems = useAppSelector(selectAllItems)
    const [open,setOpen] = useState(false);
    const [inputValue, setInputValue] = useState("")
    const [searchValue, setSearchValue] = useState("");
    const placement= "right";
    const SearchSelector = useAppSelector(state => selectContainsSearch(state, searchValue));
    
    let currentItems: getItem[] = SearchSelector;
    
    const cancel= () =>{
        setSearchValue("");
        setInputValue("");
    }
    
    // if (searchValue!= ""){
    //     currentItems = SearchSelector;
    // }
    // else if (searchValue == ""){
    //     currentItems = SearchSelector;
    // }
    // const Search = (search: string) =>{
    //     setSearchValue(inputValue);
    //     currentItems = SearchSelector;
    // }
    // const CancelDispatch = (search: string) =>{
    //     setSearchValue("");
    //     currentItems = AllItems
    // }
    const renderedAllItems = currentItems.map((item: getItem) => { return (
        <div  key={"" + item.itemID}>
            <SingleItemForPantryContents {...item}></SingleItemForPantryContents>
        </div>
    )})
    const dispatch = useAppDispatch();

//TODO: have clear search empty search field. 

return(
    <div>
<Button appearance= 'primary' color={'cyan'} onClick={() => setOpen(true)}>Add Item To Pantry</Button>
<Drawer placement= {placement} open={open} onClose={() => setOpen(false)}>
    <Drawer.Header>
        {/*<InputGroup size={'md'}>*/}
        {/*    <Input type= "text" defaultValue= "" onChange={(value: string, event: any)=>setSearchValue(value)}/>*/}
        {/*    <InputGroup.Button >*/}
        {/*        <SearchIcon onClick = {(event:any)=>SearchDispatch()}/>*/}
        {/*    </InputGroup.Button>*/}
        {/*</InputGroup>*/}
        <label>Search</label><input type= "text" onChange={(event: any)=>setInputValue(event.target.value)}/>
        <IconButton size={'sm'} icon={<SearchIcon/>} onClick = {(event:any)=>setSearchValue(inputValue)}></IconButton>
        <IconButton size={'sm'} icon={<StarIcon/>} onClick = {(event:any)=> setSearchValue(" ")}></IconButton>
    </Drawer.Header>
    <Drawer.Body>
        {renderedAllItems}
    </Drawer.Body>
</Drawer>
    </div>
)
}
export default AddPanel;