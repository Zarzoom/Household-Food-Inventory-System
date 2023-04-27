
import {useEffect, useState} from "react";
import {fetchPantries, contentsPantrySearch} from "../../Thunks/PantriesThunk"
import {goSetSearch} from '../../slices/PantriesReducer'
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import 'reactjs-popup/dist/index.css';
import Popup from 'reactjs-popup';

//TODO: The search needs to clear on page change.

export function SearchPantry() {
    const [search, setSearch] = useState("");


    
    const dispatch = useAppDispatch();
    useEffect(()=>{
        if (search === ""){
            dispatch(goSetSearch(""));
        }})
    const SearchDispatch = () =>{
        dispatch(goSetSearch(search))
    }
    const CancelDispatch = () =>{
        setSearch("");
        dispatch(goSetSearch(""));
    }

    return (
        <div className="col-md-3">
            <div className="BlueBox">
                <p>
                    <label>Search:</label><br/>
                    <input type="text" placeholder="Pantry Name" value={search}
                           onChange={(event) => setSearch(event.target.value)}/><br/>

                </p>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => SearchDispatch()}>Search</a>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => CancelDispatch()}>Cancel</a>
            </div>
        </div>
    )
}