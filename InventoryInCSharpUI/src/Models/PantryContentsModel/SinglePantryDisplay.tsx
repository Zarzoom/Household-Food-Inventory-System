import Pantry from "../../DataModels/Pantry"
import {useState, useEffect, Component} from "react";
import ObjectAndState from "../../DataModels/ObjectAndState"
import {useAppDispatch} from '../../Hooks/hooks'
import {fetchItemsInPantry} from '../../Thunks/PantryContentsThunk'
export function SinglePantryButton(pantry: Pantry){
    // const pantry = pantryAndState.pantry as Pantry;
    // const state = pantryAndState.state as Number;

    const dispatch = useAppDispatch();
    
    const updatePantryContentsState = () =>{
        const pantryID = pantry.pantryID;
        dispatch(fetchItemsInPantry(pantryID))
    }
    
    return(
        <div key = {"" + pantry.pantryID}>
        <a className="btn btn-sm" href="#" role="button" onClick={(event: any) => updatePantryContentsState()}> {pantry.pantryName} </a>
        </div>
    )
}