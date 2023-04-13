import { configureStore } from '@reduxjs/toolkit'
import {ItemsReducer, goFetchItems} from '../slices/ItemsReducer'
import {PantriesReducer} from '../slices/PantriesReducer'
import { AnyAction } from 'redux'
import { ThunkAction } from 'redux-thunk'
import getItem from '../DataModels/getItem'
import {PantryContentsReducer} from '../slices/PantryContentsReducer'


export const store = configureStore({
    reducer: {
        Items: ItemsReducer.reducer,
        Pantry: PantriesReducer.reducer,
        PantryContents: PantryContentsReducer.reducer
    }
})
 // export const initStoreDecision = (preloadedState)

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export type AppSelector = typeof store.dispatch
export type AppThunk<ReturnType = void> = ThunkAction<
ReturnType,
    RootState,
    unknown,
    AnyAction
>

