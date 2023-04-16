import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {useState, useEffect, Component} from "react";
import {fetchPantryContents} from "../../Thunks/PantryContentsThunk"
import Pantry from "../../DataModels/Pantry"
import PantryContents from "../../DataModels/PantryContents"
import {selectItemsByID} from "../../slices/ItemsReducer"
import getItem from "../../DataModels/getItem"

export function SinglePantryContentDisplay(pantryContents: PantryContents){
    const pantryContentsForSelection = pantryContents as PantryContents;
    
    const pantryItem = useAppSelector(state=>selectItemsByID(state, pantryContents.pcItemID))
    const item = pantryItem as getItem;
    console.log(item);
    return(
        <div className= "SinglePantryContentDisplay">
            <div className= "BlueBox" key = {"" + pantryContents.pantryContentID}>
                <p>
                    Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                </p>
            </div>
        </div>
    )
}