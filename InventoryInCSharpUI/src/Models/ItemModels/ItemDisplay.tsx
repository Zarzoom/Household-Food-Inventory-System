import {useState, useEffect, Component} from "react";
import Item from "../../DataModels/Item";
import getItem from "../../DataModels/getItem";
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {selectAllItems, selectContainsSearch} from "../../slices/ItemsReducer"
import {fetchItems} from "../../Thunks/ItemsThunk"
import {SingleItemDisplay} from "./SingleItemDisplay"

export const ItemDisplay =() => {

   const AllItems = useAppSelector(state => selectContainsSearch(state))
    const renderedAllItems = AllItems.map((item: getItem) => { return (
        <div  key={"" + item.itemID}>
            <SingleItemDisplay {...item}></SingleItemDisplay>
            </div>
            )})
    return <div className="col-md-3">{renderedAllItems}</div>
}




