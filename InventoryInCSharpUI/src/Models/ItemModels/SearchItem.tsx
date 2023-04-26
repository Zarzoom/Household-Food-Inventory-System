
import {useState} from "react";
import {fetchItems} from "../../Thunks/ItemsThunk"
import {selectContainsSearch, goSetSearch} from "../../slices/ItemsReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import getItem from "../../DataModels/getItem"
import {ItemDisplay} from './ItemDisplay'
import 'reactjs-popup/dist/index.css';
import Popup from 'reactjs-popup';
import { Grid, Row, Col } from 'rsuite'

export function SearchItem() {
    const [searchInput, setSearchInput] = useState("");
    const dispatch = useAppDispatch();
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
                <input type="text" placeholder="Generic or Brand Name" value={searchInput}
                       onChange={(event) => setSearchInput(event.target.value)}/><br/>

            </p>
            <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => updateSearch(searchInput)}>Search</a>
            <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => cancelSearch("")}>Cancel</a>
        </div>
    </div>
)
}