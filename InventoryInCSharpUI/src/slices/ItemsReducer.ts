import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {useSelector} from 'react-redux'
import {RootState} from '../Stores/Store'
import getItem from '../DataModels/getItem'
import Item from '../DataModels/Item'

interface ItemsState{
    StateOfItems: getItem[]
    status: 'idle' | 'succeeded' | 'search'| 'failed',
    error: string | null
}

const initialState: ItemsState={
    status: 'idle',
    error: null,
    StateOfItems: new Array()
}
export const selectAllItems = (state: RootState) => state.Items.StateOfItems;
export const selectItemsByID = (state: RootState, ID: number) => state.Items.StateOfItems.find(item=> item.itemID === ID);

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
           },
            goDeleteItem: (state, action: PayloadAction<Number>)=>{
               state.StateOfItems = state.StateOfItems.filter(item => item.itemID !== action.payload);
            },
            goUpdateItem: (state, action: PayloadAction<getItem>)=>{
               const updateStateItem = state.StateOfItems.findIndex(item=> item.itemID == action.payload.itemID)
              state.StateOfItems[updateStateItem] = action.payload;
            },
            goContentsItemSearch: (state, action: PayloadAction<getItem[]>)=>{
                state.StateOfItems = action.payload;
                    
                },
            // goPantryItemsSearch: (state, action: PayloadAction<getItem[]>) => {
            //   
            // }

            }
        },
)
export const {goFetchItems, goCreateItem, goDeleteItem, goUpdateItem, goContentsItemSearch} = ItemsReducer.actions;
