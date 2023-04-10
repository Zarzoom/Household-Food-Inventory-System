import Pantry from "../../DataModels/Pantry"
import {useState, useEffect, Component} from "react";
// import {EditItem} from "./EditItem"
// import {DeleteItem} from "./DeleteItem"
import ObjectAndState from "../../DataModels/ObjectAndState"
import Popup from 'reactjs-popup';
import 'reactjs-popup/dist/index.css';


export function SinglePantryDisplay(pantry: Pantry)
{
// const ItemAndState: ObjectAndState = {itemForGet: item, state:1}


    return(
    <div className="SinglePantryDisplay">
    <div className="BlueBox" key ={"" + pantry.pantryName}>
        <p>
            Pantry Name: {pantry.pantryName}
        </p>
        {/*<Popup trigger={<a className="btn btn-sm" href="#" role="button"> Edit </a>} modal nested>{(EditItem(ItemAndState))}</Popup>*/}
        
        {/*<Popup trigger={<a className="btn btn-sm" href="#" role="button"> Delete </a>} modal nested>{(DeleteItem(ItemAndState))}</Popup>*/}
    </div>
    </div>

)}

