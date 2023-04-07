
import {useState} from "react";
import {contentsItemSearch, fetchItems} from "../../Thunks/ItemsThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import getItem from "../../DataModels/getItem"
import {ItemDisplay} from './ItemDisplay'
import 'reactjs-popup/dist/index.css';
import Popup from 'reactjs-popup';

export function SearchItem() {
    const [search, setSearch] = useState("");

    const updateSearch = (newSearch: string) => {
        setSearch(newSearch);
    };
    
    const dispatch = useAppDispatch();
    const SearchDispatch = () =>{
        if(search === ""){
            dispatch(fetchItems());
        }
        else {
            dispatch(contentsItemSearch(search));
        }
    }
    const CancelDispatch = () =>{
        setSearch("");
        dispatch(fetchItems());
    }

    return (
        <div className="col-md-3">
            <div className="BlueBox">
                <p>
                    <label>Search:</label><br/>
                    <input type="text" placeholder="Generic or Brand Name" value={search}
                           onChange={(event) => updateSearch(event.target.value)}/><br/>

                </p>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => SearchDispatch()}>Search</a>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => CancelDispatch()}>Cancel</a>
            </div>
        </div>
    )
}