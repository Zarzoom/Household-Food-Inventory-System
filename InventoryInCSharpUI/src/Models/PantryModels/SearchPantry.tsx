
import {useEffect, useState} from "react";
import {goSetSearch} from '../../slices/PantriesReducer'
import {useAppDispatch} from '../../Hooks/hooks'
import {Input, InputGroup} from 'rsuite'
import SearchIcon from "@rsuite/icons/Search";
import CloseIcon from "@rsuite/icons/Close";

/**
 * Summary: Dispatches the search thunk and creates component where user can type in the search and either submit or cancel the search.
 * Return: Returns component with an input box and two buttons. The input box takes in the search string.
 * There is a button to submit the search and one to cancel the search and clear the input box.
 */

export function SearchPantry() {
    // Sets the state of search in PantriesReducer Slice.
    const [search, setSearch] = useState("");

    // A variable for the type defined dispatch function that can be found in Hooks.
    const dispatch = useAppDispatch();

    //Listens to the value of search on ever change. When it changes to empty it will dispatch goSetSearch with an empty string. This clears the search when the input box is cleared.
    useEffect(()=>{
        if (search === ""){
            dispatch(goSetSearch(""));
        }})

    // Calls the thunk function goSetSearch with the input search string.
    const searchDispatch = () =>{
        dispatch(goSetSearch(search))
    }

    //Sets the value of search to an empty string so and then calls the thunk function goSetSearch. This clears the search so that all pantries are displayed.
    const cancelDispatch = () =>{
        setSearch("");
        dispatch(goSetSearch(""));
    }

    return (
        <div>
            <div>
                <>
                    <InputGroup>
                        <label className="labelForSearch">Search:</label><br/>
                        <Input type="text" placeholder="Pantry Name" value={search}
                               onChange={(value: string, event) => setSearch(value)}/>
                        <InputGroup.Button onClick={(event: any) => searchDispatch()}>
                            <SearchIcon/>
                        </InputGroup.Button>
                        <InputGroup.Button onClick={(event: any) => cancelDispatch()}>
                            <CloseIcon/>
                        </InputGroup.Button>
                    </InputGroup>
                </>
            </div>
        </div>
    )
}