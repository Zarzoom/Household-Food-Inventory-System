import { configureStore } from '@reduxjs/toolkit'
import {ItemsReducer, goFetchItems} from '../slices/ItemsReducer'
import { AnyAction } from 'redux'
import { ThunkAction } from 'redux-thunk'
import getItem from '../DataModels/getItem'

export const store = configureStore({
    reducer: {
        Items: ItemsReducer.reducer,
        
    }
})
// export type RootState = ReturnType<typeof store.getState>
// export type AppDispatch = typeof store.dispatch
// export type AppThunk<ReturnType = Promise<getItem>> = ThunkAction<
//     ReturnType,
//     RootState,
//     unknown,
//     AnyAction
// >

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export type AppSelector = typeof store.dispatch
export type AppThunk<ReturnType = void> = ThunkAction<
ReturnType,
    RootState,
    unknown,
    AnyAction
>

