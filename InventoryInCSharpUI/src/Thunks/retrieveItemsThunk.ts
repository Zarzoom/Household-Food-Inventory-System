import {createSlice} from '@reduxjs/toolkit';
import HttpClient from '../Services/Controlers/HttpClient'
import getItem from '../DataModels/getItem'
import Item from '../DataModels/Item'
import { RootState, AppDispatch, AppThunk } from '../Stores/ItemListStore'
import {goFetchItems} from '../slices/ItemsReducer'

import { AnyAction } from 'redux'
import {store} from '../Stores/ItemListStore'

const client = new HttpClient();
export const fetchItems =
    (newItem: Item): AppThunk => async dispatch => {

        const asyncResponse = await client.getData('http://localhost:8000/api/Item')
            .then(response => response.json())
            .then(response => response as getItem[]);
        dispatch(
            goFetchItems(asyncResponse)
        )
}
