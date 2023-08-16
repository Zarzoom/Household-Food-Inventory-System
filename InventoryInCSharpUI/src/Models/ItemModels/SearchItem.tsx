
import {useEffect, useState} from "react";
import {goSetSearch} from "../../slices/ItemsReducer"
import {useAppDispatch} from '../../Hooks/hooks'
import 'reactjs-popup/dist/index.css';
import {Input, Button, InputGroup} from 'rsuite'
import SearchIcon from '@rsuite/icons/Search';
import CloseIcon from '@rsuite/icons/Close';

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
        <div >
            <>
                <InputGroup>
                    <label className={"labelForSearch"}>Search:</label><br/>
                    <Input type="text" placeholder="Generic or Brand Name" value={searchInput}
                           onChange={(value: string, event) => setSearchInput(value)}/>
                    <InputGroup.Button onClick={(event: any) => updateSearch(searchInput)}>
                        <SearchIcon />
                    </InputGroup.Button>
                    <InputGroup.Button onClick={(event: any) => cancelSearch("")}>
                    <CloseIcon/>
                    </InputGroup.Button>
                </InputGroup>

            </>

        </div>
    </div>
)
}