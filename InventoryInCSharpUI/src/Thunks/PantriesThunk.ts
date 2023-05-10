import HttpClient from '../Services/Controlers/HttpClient'
import Pantry from '../DataModels/Pantry'
import PantryNoID from '../DataModels/PantryNoID'
import { RootState, AppDispatch, AppThunk } from '../Stores/Store'
import { AnyAction } from 'redux'
import {store} from '../Stores/Store'
import {goFetchPantries, goCreatePantry, goUpdatePantry, goDeletePantry, goContentsPantrySearch} from '../slices/PantriesReducer'

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
        };
export const createPantry =
    (newPantry: PantryNoID): AppThunk =>
        async dispatch => {
        const asyncResponse = await client.postData('http://localhost:8000/api/Pantry', newPantry)
            .then(response => response.json())
            .then(response => response as Pantry);
        dispatch(
            goCreatePantry(asyncResponse)
        )
        };
export const updatePantry =
    (updatedPantry: Pantry): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.putData('http://localhost:8000/api/Pantry', updatedPantry)
        .then(response => response.json())
        .then(response => response as Pantry);
    dispatch(
        goUpdatePantry(asynchResponse)
    )
};

export const deletePantry =
    (deletePantryID: Number): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.putData('http://localhost:8000/api/Pantry/deletePantry/'+ deletePantryID);
    dispatch(
        goDeletePantry(deletePantryID)
    )
        };

export const contentsPantrySearch =
    (search: String): AppThunk =>
    async dispatch =>{
        const asynchResponse = await client.getData('http://localhost:8000/api/Pantry/search/' + search)
            .then(response => response.json())
            .then(response => response as Pantry[]);
        dispatch(
            goContentsPantrySearch(asynchResponse)
        )
    }
    