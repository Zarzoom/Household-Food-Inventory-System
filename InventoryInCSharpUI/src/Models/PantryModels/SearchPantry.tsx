
import {useState} from "react";
import {fetchPantries, contentsPantrySearch} from "../../Thunks/PantriesThunk"
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import 'reactjs-popup/dist/index.css';
import Popup from 'reactjs-popup';

export function SearchPantry() {
    const [search, setSearch] = useState("");

    const updateSearch = (newSearch: string) => {
        setSearch(newSearch);
    };
    
    const dispatch = useAppDispatch();
    const SearchDispatch = () =>{
        if(search === ""){
            dispatch(fetchPantries());
        }
        else {
            dispatch(contentsPantrySearch(search));
        }
    }
    const CancelDispatch = () =>{
        setSearch("");
        dispatch(fetchPantries());
    }

    return (
        <div className="col-md-3">
            <div className="BlueBox">
                <p>
                    <label>Search:</label><br/>
                    <input type="text" placeholder="Pantry Name" value={search}
                           onChange={(event) => updateSearch(event.target.value)}/><br/>

                </p>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => SearchDispatch()}>Search</a>
                <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => CancelDispatch()}>Cancel</a>
            </div>
        </div>
    )
}