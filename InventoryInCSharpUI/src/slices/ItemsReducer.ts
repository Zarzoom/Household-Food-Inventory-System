import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {useSelector} from 'react-redux'
import {RootState} from '../Stores/Store'
import getItem from '../DataModels/getItem'
import Item from '../DataModels/Item'

interface ItemsState{
    StateOfItems: getItem[]
    status: 'idle' | 'succeeded' | 'failed',
    error: string | null
}

const initialState: ItemsState={
    status: 'idle',
    error: null,
    StateOfItems: new Array()
}
export const selectAllItems = (state: RootState) => state.Items.StateOfItems;

export const ItemsReducer = createSlice({
        name: 'items',
        initialState,
        reducers:{
           goFetchItems: (state, action: PayloadAction<getItem[]>) =>{ 
               state.StateOfItems = action.payload;
               state.status = 'succeeded'
           },
           goCreateItem: (state, action: PayloadAction<getItem>)=>{
               state.StateOfItems.push(action.payload);
           }
        },
})
export const {goFetchItems, goCreateItem} = ItemsReducer.actions;
