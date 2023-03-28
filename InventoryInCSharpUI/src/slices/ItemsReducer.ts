import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {useSelector} from 'react-redux'
import {RootState} from '../Stores/Store'
import getItem from '../DataModels/getItem'

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
           }
        },
})
export const {goFetchItems} = ItemsReducer.actions;