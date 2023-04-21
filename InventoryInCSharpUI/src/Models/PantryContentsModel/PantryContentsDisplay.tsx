import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {useState, useEffect, Component} from "react";
import {fetchPantryContents} from "../../Thunks/PantryContentsThunk"
import Pantry from "../../DataModels/Pantry"
import PantryContents from "../../DataModels/PantryContents"
import {selectAllPantryContents, selectPantryFilter} from "../../slices/PantryContentsReducer"
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

    
    let pantryID : number| null = useAppSelector(state => state.PantryContents.PantryFilter)
    let pantryName: string = "All Pantries";


        const pantry = useAppSelector(state=>selectPantryByID(state, pantryID as number))
        const officialPantry= pantry as Pantry;


    // pantry = useAppSelector(state => selectPantryByID(state, actualSinglePantryContent.pcPantryID))
    // let pantryName: string = "No Name"
    if(pantry != null) {
        pantryName = officialPantry.pantryName;
    }
    // console.log(pantryID);
    const PantryItems = CurrentPantryContents.map((pantryContents : PantryContents) =>{
        console.log(pantryContents.pantryContentID);
    return(
    <div className= "col-md-3">

        <div key = {"" +pantryContents.pantryContentID + pantryContents.pcItemID}>
            <SinglePantryContentDisplay{...pantryContents}></SinglePantryContentDisplay>
        </div>
    </div>
    )})
  
   return( <div className="col-md-9">       
           <h2 className= 'text-left' style={{fontFamily: "'Times New Roman', Times, serif"}}>
               {pantryName}
    </h2>
        {PantryItems}
   </div>
   )
}