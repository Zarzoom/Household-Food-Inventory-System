import { configureStore } from '@reduxjs/toolkit'
import {ItemsReducer} from '../slices/ItemsReducer'
import {PantriesReducer} from '../slices/PantriesReducer'
import { AnyAction } from 'redux'
import { ThunkAction } from 'redux-thunk'
import {PantryContentsReducer} from '../slices/PantryContentsReducer'
import {LoginReducer} from "../slices/LoginReducer";


export const store = configureStore({
    reducer: {
        Items: ItemsReducer.reducer,
        Pantry: PantriesReducer.reducer,
        PantryContents: PantryContentsReducer.reducer,
        Login: LoginReducer.reducer
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

