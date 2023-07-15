import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {RootState} from '../Stores/Store'
import getItem from '../DataModels/getItem'
import StatusString from '../DataModels/StatusString'

interface ItemsState{
    StateOfItems: getItem[],
    search: string,
    status: StatusString,
    error: string | null
}

const initialState: ItemsState={
    status: 'idle',
    error: null,
    search: "",
    StateOfItems: []
}

export const selectItemsByID = (state: RootState, ID: number) => state.Items.StateOfItems.find(item=> item.itemID === ID);
export const selectContainsSearch = (state: RootState) => state.Items.StateOfItems.filter(item=>item.brand.toLowerCase().includes(state.Items.search.toLowerCase()) || item.genericName.toLowerCase().includes(state.Items.search.toLowerCase()));
// export const findItemArrayByID = (state: RootState, Items: getItem[]) => Items.map((item: getItem)=>state.Items.StateOfItems.find(stateItem => stateItem.itemID === item.itemID));


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
               const updateStateItem = state.StateOfItems.findIndex(item=> item.itemID === action.payload.itemID)
              state.StateOfItems[updateStateItem] = action.payload;
            },
            goContentsItemSearch: (state, action: PayloadAction<getItem[]>)=>{
                state.StateOfItems = action.payload;
                    
                },
            goSetSearch: (state, action:PayloadAction<string>) =>{
               state.search = action.payload;
            },
            goSetStatus: (state, action: PayloadAction<string>) =>{
               const NewStatus: StatusString = action.payload as StatusString;
               state.status = NewStatus;
            }
            }
        },
)
export const {goFetchItems, goCreateItem, goDeleteItem, goUpdateItem, goSetSearch} = ItemsReducer.actions;
