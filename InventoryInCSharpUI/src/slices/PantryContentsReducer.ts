import PantryContents from '../DataModels/PantryContents'
import {RootState} from '../Stores/Store'
import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import StatusString from '../DataModels/StatusString'

interface PantryContentsState{
    StateOfPantryContents: PantryContents[]
    status: StatusString,
    error: string| null
    PantryFilter: number| null
}

const initialState: PantryContentsState={
    status:'idle',
    error: null,
    StateOfPantryContents: [],
    PantryFilter: -1
}

export const selectAllPantryContents = (state: RootState) => state.PantryContents.StateOfPantryContents;

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
        },
        goUpdatePantryContents: (state, action: PayloadAction<PantryContents>) => {
        const updateStatePantryContent = state.StateOfPantryContents.findIndex(pantryContents => pantryContents.pantryContentID === action.payload.pantryContentID);
        state.StateOfPantryContents[updateStatePantryContent] = action.payload;
        },
        goDeletePantryContents: (state, action: PayloadAction<number>) =>{
            state.StateOfPantryContents = state.StateOfPantryContents.filter(PantryContents => PantryContents.pantryContentID !== action.payload);
        },
        goSetStatus: (state, action: PayloadAction<string>) =>{
            const NewStatus: StatusString = action.payload as StatusString;
            state.status = NewStatus;
        }
    }
})

export const {goFetchPantryContents, goFetchItemsInPantry, goSetPantryFilter, goUpdatePantryContents, goDeletePantryContents, goSetStatus} = PantryContentsReducer.actions;