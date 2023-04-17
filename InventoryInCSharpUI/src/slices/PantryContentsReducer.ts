import PantryContents from '../DataModels/PantryContents'
import {useSelector} from 'react-redux'
import {RootState} from '../Stores/Store'
import { createSlice, PayloadAction } from '@reduxjs/toolkit'


interface PantryContentsState{
    StateOfPantryContents: PantryContents[]
    status: 'idle' | 'succeeded' | 'failed',
    error: string| null
    PantryFilter: number| null
}

const initialState: PantryContentsState={
    status:'idle',
    error: null,
    StateOfPantryContents: new Array(),
    PantryFilter: -1
}

export const selectAllPantryContents = (state: RootState) => state.PantryContents.StateOfPantryContents;
export const selectPantryFilter = (state: RootState) => state.PantryContents.PantryFilter;

export const PantryContentsReducer = createSlice({
    name: 'pantryContents',
    initialState,
    reducers: {
        goFetchPantryContents: (state, action: PayloadAction<PantryContents[]>) => {
            state.StateOfPantryContents = action.payload;
            state.status = 'succeeded'
        },
        goFetchItemsInPantry: (state, action: PayloadAction<PantryContents[]>) => {
            state.StateOfPantryContents = action.payload;
        },
        goSetPantryFilter: (state, action: PayloadAction<number>) => {
            state.PantryFilter = action.payload;
        }
    }
})

export const {goFetchPantryContents, goFetchItemsInPantry, goSetPantryFilter} = PantryContentsReducer.actions;