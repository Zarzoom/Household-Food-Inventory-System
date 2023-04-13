import HttpClient from '../Services/Controlers/HttpClient'
import PantryContents from '../DataModels/PantryContents'
import PantryContentsNoID from '../DataModels/PantryContentsNoID'
import { RootState, AppDispatch, AppThunk } from '../Stores/Store'
import { AnyAction } from 'redux'
import {store} from '../Stores/Store'
import {goFetchPantryContents, goFetchItemsInPantry} from '../slices/PantryContentsReducer'

const client = new HttpClient();

export const fetchPantryContents =
    (): AppThunk =>
        async dispatch => {
    const asynchResponse = await client.getData('http://localhost:8000/api/PantryContents')
        .then(response => response.json())
        .then(response => response as PantryContents[])
            dispatch(
                goFetchPantryContents(asynchResponse)
            )
        };

export const fetchItemsInPantry =
    (pcPantryID: number): AppThunk =>
        async dispatch => {
    const asynchResponse = await client.getData('http://localhost:8000/api/PantryContents/' + pcPantryID)
        .then(response => response.json())
        .then(response => response as PantryContents[])
    dispatch(
        goFetchItemsInPantry(asynchResponse)
    )
        }
        