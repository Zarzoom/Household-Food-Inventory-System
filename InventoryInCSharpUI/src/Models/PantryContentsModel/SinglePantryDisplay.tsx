import Pantry from "../../DataModels/Pantry"
import {useState, useEffect, Component} from "react";
import ObjectAndState from "../../DataModels/ObjectAndState"
import {useAppDispatch} from '../../Hooks/hooks'
import {fetchItemsInPantry} from '../../Thunks/PantryContentsThunk'
import {goSetPantryFilter} from '../../slices/PantryContentsReducer'
import {Button} from 'rsuite'

export function SinglePantryButton(pantry: Pantry){
    // const pantry = pantryAndState.pantry as Pantry;
    // const state = pantryAndState.state as Number;

    const dispatch = useAppDispatch();
    
    const updatePantryContentsState = () =>{
        const pantryID = pantry.pantryID;
        dispatch(fetchItemsInPantry(pantryID));
        dispatch(goSetPantryFilter(pantryID));
    }
    
    return(
        <div key = {"" + pantry.pantryID + pantry.pantryName}>
        <Button appearance={'primary'} color={'green'} onClick={(event: any) => updatePantryContentsState()}> {pantry.pantryName} </Button>
        </div>
    )
}