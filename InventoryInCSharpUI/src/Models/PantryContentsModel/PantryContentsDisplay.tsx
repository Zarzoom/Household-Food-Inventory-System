import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {useState, useEffect, Component} from "react";
import {fetchPantryContents} from "../../Thunks/PantryContentsThunk"
import Pantry from "../../DataModels/Pantry"
import PantryContents from "../../DataModels/PantryContents"
import {selectAllPantryContents} from "../../slices/PantryContentsReducer"
import {selectPantryByID} from "../../slices/PantriesReducer"
import {SinglePantryContentDisplay} from "./SinglePantryContentDisplay"
export const PantryContentsDisplay = () =>{
    const dispatch = useAppDispatch();
    const PantryContentsStatus = useAppSelector(state => state.PantryContents.status);
const CurrentPantryContents = useAppSelector(selectAllPantryContents);
useEffect(() =>{
    if (PantryContentsStatus === 'idle') {
        const PantryContentsList = dispatch(fetchPantryContents());
    }
}, [PantryContentsStatus, dispatch])
    // const singularPantryContent = CurrentPantryContents[0];
    // const actualSinglePantryContent = singularPantryContent as PantryContents
    console.log(CurrentPantryContents);
    // const pantry = useAppSelector(state=>selectPantryByID(state, actualSinglePantryContent.pcPantryID))
    // const pantryForName = pantry as Pantry;
    const PantryItems = CurrentPantryContents.map((pantryContents : PantryContents) =>{
    return(
    <div className= "col-md-3">

        <div key = {"" +pantryContents.pantryContentID}>
            <SinglePantryContentDisplay{...pantryContents}></SinglePantryContentDisplay>
        </div>
    </div>
    )})
  
   return( <div className="col-md-9">       
           <h2 className= 'text-left' style={{fontFamily: "'Times New Roman', Times, serif"}}>
        Pantry Name
    </h2>
        {PantryItems}
   </div>
   )
}