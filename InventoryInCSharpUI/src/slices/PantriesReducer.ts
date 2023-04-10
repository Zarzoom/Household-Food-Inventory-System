import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {useSelector} from 'react-redux'
import {RootState} from '../Stores/Store'
import Pantry from '../DataModels/Pantry'
import PantryNoID from '../DataModels/PantryNoID'

interface PantryState{
    StateOfPantry: Pantry[]
    status: 'idle' | 'succeeded' | 'search'| 'failed',
    error: string | null
}

const initialState: PantryState={
    status: 'idle',
    error: null,
    StateOfPantry: new Array()
}

export const selectAllPantries = (state: RootState) => state.Pantry.StateOfPantry;

export const PantriesReducer = createSlice({
    name: 'pantry',
    initialState,
    reducers: {
        goFetchPantries: (state, action: PayloadAction<Pantry[]>) => {
            state.StateOfPantry = action.payload;
            state.status = 'succeeded'
        },
    }
    },
    )
export const {goFetchPantries} = PantriesReducer.actions;