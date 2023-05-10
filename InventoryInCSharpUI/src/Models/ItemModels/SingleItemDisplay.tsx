﻿import getItem from "../../DataModels/getItem"
import {useState, useEffect, Component} from "react";
import {EditItem} from "./EditItem"
import {DeleteItem} from "./DeleteItem"
import ObjectAndState from "../../DataModels/ObjectAndState"
import {FlexboxGrid, Col} from "rsuite"
// import Popup from 'reactjs-popup';
// import 'reactjs-popup/dist/index.css';


export function SingleItemDisplay(item: getItem)
{
const ItemAndState: ObjectAndState = {itemForGet: item, state:1}


    return(

        <div>
            <p>

                Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
            </p>
            <EditItem{...item}></EditItem>
            <DeleteItem {...item}></DeleteItem>
        </div>
)}

