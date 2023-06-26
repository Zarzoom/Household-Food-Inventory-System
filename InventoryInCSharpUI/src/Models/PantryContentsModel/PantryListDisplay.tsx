import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../../Stores/Store"
import {useState, useEffect, Component} from "react";
import {fetchPantries} from "../../Thunks/PantriesThunk"
import Pantry from "../../DataModels/Pantry"
import {selectAllPantries} from "../../slices/PantriesReducer"
import {SinglePantryButton} from "./SinglePantryButton"
import {Panel, Stack} from "rsuite"


export const PantryListDisplay = () => {
    const dispatch = useAppDispatch();
    const PantryStatus = useAppSelector(state => state.Pantry.status);
    const AllPantries = useAppSelector(selectAllPantries);
    
    const renderedAllPantries = AllPantries.map((pantry: Pantry) => {
        return(
        <Stack.Item key={""+ pantry.pantryID +pantry.pantryName}>
            <SinglePantryButton{...pantry}></SinglePantryButton>
        </Stack.Item>
    )})
    return(
    <Panel bordered={true} header={"Pantries"} className={"centerHorizontally"}>
        <Stack direction={"column"} wrap={false} justifyContent={"space-between"} spacing={2}>
        {renderedAllPantries}
        </Stack>
    </Panel>
    )
}