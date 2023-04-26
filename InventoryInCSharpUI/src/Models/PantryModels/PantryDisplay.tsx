﻿import {useState, useEffect, Component} from "react";
import Pantry from "../../DataModels/Pantry";
import PantryNoID from "../../DataModels/PantryNoID";
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {selectContainsSearch} from "../../slices/PantriesReducer"
import {fetchPantries} from "../../Thunks/PantriesThunk"
import {SinglePantryDisplay} from "./SinglePantryDisplay"



export const PantryDisplay =() => {

   const AllPantries = useAppSelector(state=> selectContainsSearch(state))
    const renderedAllPantries = AllPantries.map((pantry: Pantry) => { return (
        <div  key={"" + pantry.pantryID}>
            <SinglePantryDisplay {...pantry}></SinglePantryDisplay>
            </div>
            )})
    return <div className="col-md-3">{renderedAllPantries}</div>
}




