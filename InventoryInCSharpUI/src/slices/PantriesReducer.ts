import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {useSelector} from 'react-redux'
import {RootState} from '../Stores/Store'
import Pantry from '../DataModels/Pantry'
import PantryNoID from '../DataModels/PantryNoID'

interface PantryState{
    StateOfPantry: Pantry[]
    status: 'idle' | 'succeeded' | 'search'| 'failed',
    search: string,
    error: string | null
}

const initialState: PantryState={
    status: 'idle',
    error: null,
    search: "",
    StateOfPantry: []
}

export const selectAllPantries = (state: RootState) => state.Pantry.StateOfPantry;
export const selectPantryByID = (state: RootState, ID: number) => state.Pantry.StateOfPantry.find(pantry => pantry.pantryID === ID)
export const selectContainsSearch = (state: RootState) => state.Pantry.StateOfPantry.filter(pantry=>pantry.pantryName.toLowerCase().includes(state.Pantry.search.toLowerCase()));

export const PantriesReducer = createSlice({
    name: 'pantry',
    initialState,
    reducers: {
        goFetchPantries: (state, action: PayloadAction<Pantry[]>) => {
            state.StateOfPantry = action.payload;
            state.status = 'succeeded'
        },
        goCreatePantry: (state , action: PayloadAction<Pantry>) =>{
            state.StateOfPantry.push(action.payload);
        },
        goUpdatePantry: (state, action: PayloadAction<Pantry>) => {
            const updateStatePantry = state.StateOfPantry.findIndex(pantry => pantry.pantryID == action.payload.pantryID)
    state.StateOfPantry[updateStatePantry] = action.payload;
        },
        goDeletePantry: (state, action: PayloadAction<Number>) =>{
            state.StateOfPantry = state.StateOfPantry.filter(pantry => pantry.pantryID !== action.payload);
        },
        goContentsPantrySearch: (state, action: PayloadAction<Pantry[]>) =>{
            state.StateOfPantry = action.payload;
        },
        goSetSearch: (state, action: PayloadAction<string>) =>{
            state.search = action.payload
        }
    }
    }
    )
export const {goFetchPantries, goCreatePantry, goUpdatePantry, goDeletePantry, goContentsPantrySearch, goSetSearch} = PantriesReducer.actions;