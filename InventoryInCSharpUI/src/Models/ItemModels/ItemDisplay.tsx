import {useState, useEffect, Component} from "react";
import Item from "../../DataModels/Item";
import getItem from "../../DataModels/getItem";
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {selectAllItems, selectContainsSearch} from "../../slices/ItemsReducer"
import {fetchItems} from "../../Thunks/ItemsThunk"
import {SingleItemDisplay} from "./SingleItemDisplay"
import {List, FlexboxGrid, Panel, Col} from "rsuite";

export const ItemDisplay =() => {

   const AllItems = useAppSelector(state => selectContainsSearch(state))
    const renderedAllItems = AllItems.map((item: getItem) => { return (
        // <FlexboxGrid.Item as={Col} colspan={24} className={"BlueBackground"}>
        <List.Item className={"blueBackground"} key={item.itemID}>
            <SingleItemDisplay {...item}></SingleItemDisplay>
        </List.Item>
        /*</FlexboxGrid.Item>*/
            )})
    return(
            <List>
                {renderedAllItems}
            </List>
    )
}




