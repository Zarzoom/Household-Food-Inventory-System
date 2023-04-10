import HttpClient from '../Services/Controlers/HttpClient'
import Pantry from '../DataModels/Pantry'
import PantryNoID from '../DataModels/PantryNoID'
import { RootState, AppDispatch, AppThunk } from '../Stores/Store'
import { AnyAction } from 'redux'
import {store} from '../Stores/Store'
import {goFetchPantries} from '../slices/PantriesReducer'

const client = new HttpClient();

export const fetchPantries =
    (): AppThunk =>
        async dispatch => {
            const asyncResponse = await client.getData('http://localhost:8000/api/Pantry')
                .then(response => response.json())
                .then(response => response as Pantry[]);
            dispatch(
                goFetchPantries(asyncResponse)
            )
        }