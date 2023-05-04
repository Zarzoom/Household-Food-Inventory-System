
import {useEffect, useState} from "react";
import {fetchPantries, contentsPantrySearch} from "../../Thunks/PantriesThunk"
import {goSetSearch} from '../../slices/PantriesReducer'
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import {Button, Input} from 'rsuite'

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
                    <Input type="text" placeholder="Pantry Name" value={search}
                           onChange={(value: string, event) => setSearch(value)}/><br/>
                </p>
                <Button appearance={'primary'} color={'cyan'} onClick={(event: any) => SearchDispatch()}>Search</Button>
                <Button appearance={'primary'} color={'cyan'} onClick={(event: any) => CancelDispatch()}>Cancel</Button>
            </div>
        </div>
    )
}