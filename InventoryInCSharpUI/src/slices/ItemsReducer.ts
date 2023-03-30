import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {useSelector} from 'react-redux'
import {RootState} from '../Stores/Store'
import getItem from '../DataModels/getItem'
import Item from '../DataModels/Item'

interface ItemsState{
    status: String,
    StateOfItems: getItem[]
}

const initialState: ItemsState={
    status: 'idle',
    StateOfItems: new Array()
}
export const selectAllItems = (state: RootState) => state.Items.StateOfItems;

export const ItemsReducer = createSlice({
        name: 'items',
        initialState,
        reducers:{
           goFetchItems: (state, action: PayloadAction<getItem[]>) =>{ 
               state.StateOfItems= action.payload
           },
           goCreateItem: (state, action: PayloadAction<getItem>)=>{
               state.StateOfItems.push(action.payload);
           }
        },
})
export const {goFetchItems, goCreateItem} = ItemsReducer.actions;
