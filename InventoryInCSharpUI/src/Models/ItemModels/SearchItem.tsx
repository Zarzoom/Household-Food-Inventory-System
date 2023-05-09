
import {useEffect, useState} from "react";

import {selectContainsSearch, goSetSearch} from "../../slices/ItemsReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'

import 'reactjs-popup/dist/index.css';

import {Input, Button} from 'rsuite'

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

        </div>
    </div>
)
}