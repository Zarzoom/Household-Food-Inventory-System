
import {useEffect, useState} from "react";
import {fetchPantries, contentsPantrySearch} from "../../Thunks/PantriesThunk"
import {goSetSearch} from '../../slices/PantriesReducer'
import {useAppDispatch} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import {Button, Input, InputGroup} from 'rsuite'
import SearchIcon from "@rsuite/icons/Search";
import StarIcon from "@rsuite/icons/legacy/Star";

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
        <div>
            <div className={"BlueBox"}>
                <>
                    <InputGroup>
                        <label>Search:</label><br/>
                        <Input type="text" placeholder="Pantry Name" value={search}
                               onChange={(value: string, event) => setSearch(value)}/>

                        <InputGroup.Button onClick={(event: any) => SearchDispatch()}>
                            <SearchIcon/>
                        </InputGroup.Button>
                        <InputGroup.Button onClick={(event: any) => CancelDispatch()}>
                            <StarIcon/>
                        </InputGroup.Button>
                    </InputGroup>
                </>
            </div>
        </div>
    )
}