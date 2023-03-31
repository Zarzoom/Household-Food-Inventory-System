import {useState, useEffect, Component} from "react";
import Item from "../../DataModels/Item";
import getItem from "../../DataModels/getItem";
import ItemManager from "../../Services/Managers/ItemManager"
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {selectAllItems} from "../../slices/ItemsReducer"
import {fetchItems} from "../../Thunks/ItemsThunk"

const newItemManager = new ItemManager();

export const ItemDisplay =() => {
    const dispatch = useAppDispatch();'' 
    const ItemStatus = useAppSelector(state => state.Items.status)
    useEffect(()=> {
        if(ItemStatus === 'idle') {
            const ItemList = dispatch(fetchItems())
        }
    }, [ItemStatus, dispatch])

   const AllItems = useAppSelector(selectAllItems)
    const renderedAllItems = AllItems.map((item: getItem) => { return (<div className="BlueBox" key ={"" + item.itemID}>
        <p>
            Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
        </p>
        {/*<a className="btn btn-sm" href="#" role="button" onClick={(event: any) => }>Edit</a>*/}
        <a className="btn btn-sm" href="#"
           role="button">Delete</a>
    </div>)})
    return <div className="col-md-3">{renderedAllItems}</div>
}

    
    




