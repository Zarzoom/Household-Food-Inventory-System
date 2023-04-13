import Pantry from "../../DataModels/Pantry"
import {useState, useEffect, Component} from "react";
import {EditPantry} from "./EditPantry"
import {DeletePantry} from "./DeletePantry"
import ObjectAndState from "../../DataModels/ObjectAndState"
import Popup from 'reactjs-popup';
import 'reactjs-popup/dist/index.css';


export function SinglePantryDisplay(pantry: Pantry)
{
const pantryForButtons = pantry as Pantry;


    return(
    <div className="SinglePantryDisplay">
    <div className="BlueBox" key ={"" + pantry.pantryID}>
        <p>
            Pantry Name: {pantry.pantryName}
        </p>
        <Popup trigger={<a className="btn btn-sm" href="#" role="button"> Edit </a>} modal nested>{(EditPantry(pantryForButtons))}</Popup>
        
        <Popup trigger={<a className="btn btn-sm" href="#" role="button"> Delete </a>} modal nested>{(DeletePantry(pantryForButtons))}</Popup>
    </div>
    </div>

)}

