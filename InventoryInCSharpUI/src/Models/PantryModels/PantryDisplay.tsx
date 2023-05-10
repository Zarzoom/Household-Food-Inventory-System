import {useState, useEffect, Component} from "react";
import Pantry from "../../DataModels/Pantry";
import PantryNoID from "../../DataModels/PantryNoID";
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {selectContainsSearch} from "../../slices/PantriesReducer"
import {fetchPantries} from "../../Thunks/PantriesThunk"
import {SinglePantryDisplay} from "./SinglePantryDisplay"
import {List, FlexboxGrid, Panel, Col} from "rsuite";




export const PantryDisplay =() => {

   const AllPantries = useAppSelector(state=> selectContainsSearch(state))
    const renderedAllPantries = AllPantries.map((pantry: Pantry) => { return (
        <FlexboxGrid.Item as={Col} colspan={24}>
            <List.Item key={"" + pantry.pantryID}>
                <SinglePantryDisplay {...pantry}></SinglePantryDisplay>
            </List.Item>
        </FlexboxGrid.Item>
            )})
    return(
                <List>
                    {renderedAllPantries}
                </List>
    )
}




