
import {useEffect, useState} from "react";
import {fetchItems} from "../../Thunks/ItemsThunk"
import {selectContainsSearch, goSetSearch} from "../../slices/ItemsReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import getItem from "../../DataModels/getItem"
import {ItemDisplay} from './ItemDisplay'
import 'reactjs-popup/dist/index.css';
import Popup from 'reactjs-popup';
import {Grid, Row, Col, Input, Button} from 'rsuite'

//TODO: The search needs to clear on page change.
export function SearchItem() {
    const [searchInput, setSearchInput] = useState("");
    const dispatch = useAppDispatch();
    
    useEffect(()=>{
        if (searchInput === ""){
            dispatch(goSetSearch(""));
        }
    })
    const updateSearch = (searchValue: string) =>{
        dispatch(goSetSearch(searchValue));
    }
    const cancelSearch = (searchValue: string) =>{
        dispatch(goSetSearch(searchValue));
        setSearchInput(searchValue);
    }
    
return (
    <div className="col-md-3">
        <div className="BlueBox">
            <p>
                <label>Search:</label><br/>
                <Input type="text" placeholder="Generic or Brand Name" value={searchInput}
                       onChange={(value: string, event) => setSearchInput(value)}/><br/>

            </p>
            <Button appearance={'primary'} color={'cyan'} onClick={(event: any) => updateSearch(searchInput)}>Search</Button>
            <Button appearance={'primary'} color={'cyan'} onClick={(event: any) => cancelSearch("")}>Cancel</Button>
            
            {/*<a className="btn btn-sm" href="#" role="button" onClick={(event: any) => updateSearch(searchInput)}>Search</a>*/}
            {/*<a className="btn btn-sm" href="#" role="button" onClick={(event: any) => cancelSearch("")}>Cancel</a>*/}
        </div>
    </div>
)
}