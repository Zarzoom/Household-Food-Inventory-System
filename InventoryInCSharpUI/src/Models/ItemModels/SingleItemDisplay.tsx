import getItem from "../../DataModels/getItem"
import {useState, useEffect, Component} from "react";
import {EditItem} from "./EditItem"
import {DeleteItem} from "./DeleteItem"
import ObjectAndState from "../../DataModels/ObjectAndState"
import Popup from 'reactjs-popup';
import 'reactjs-popup/dist/index.css';


export function SingleItemDisplay(item: getItem)
{
const ItemAndState: ObjectAndState = {itemForGet: item, state:1}


    return(
    <div className="SingleItemDisplay">
    <div className="BlueBox" key ={"" + item.itemID}>
        <p>
            Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
        </p>
        {/*<a className="btn btn-sm" href="#" role="button" onClick={(event: any) => }>Edit</a>*/}
        {/*<Popup trigger={<a className="btn btn-sm" href="#" role="button"> Edit </a>} modal nested>{(EditItem(ItemAndState))}</Popup>*/}
        <EditItem{...item}></EditItem>
        <DeleteItem {...item}></DeleteItem>
        {/*<Popup trigger={<a className="btn btn-sm" href="#" role="button"> Delete </a>} modal nested>{(DeleteItem(ItemAndState))}</Popup>*/}
    </div>
    </div>

)}

