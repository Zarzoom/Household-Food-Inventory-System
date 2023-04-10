import {useState, useEffect, Component} from "react";
import Item from "../../DataModels/Item";
import getItem from "../../DataModels/getItem";
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {selectAllItems} from "../../slices/ItemsReducer"
import {fetchItems} from "../../Thunks/ItemsThunk"
import {SingleItemDisplay} from "./SingleItemDisplay"

export const ItemDisplay =() => {
    const dispatch = useAppDispatch(); 
    const ItemStatus = useAppSelector(state => state.Items.status)
    useEffect(()=> {
        if(ItemStatus === 'idle') {
            const ItemList = dispatch(fetchItems())
        }
            
    }, [ItemStatus, dispatch])

   const AllItems = useAppSelector(selectAllItems)
    const renderedAllItems = AllItems.map((item: getItem) => { return (
        <div  key={"" + item.itemID}>
            <SingleItemDisplay {...item}></SingleItemDisplay>
            </div>
            )})
    return <div className="col-md-3">{renderedAllItems}</div>
}




