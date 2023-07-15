import HttpClient from '../Services/Controlers/HttpClient'
import Pantry from '../DataModels/Pantry'
import PantryNoID from '../DataModels/PantryNoID'
import {AppThunk } from '../Stores/Store'
import {goFetchPantries, goCreatePantry, goUpdatePantry, goDeletePantry, goContentsPantrySearch} from '../slices/PantriesReducer'

const client = new HttpClient();

export const fetchPantries =
    (): AppThunk =>
        async dispatch => {
            const asyncResponse = await client.getData(process.env.REACT_APP_API + '/api/Pantry')
                .then(response => response.json())
                .then(response => response as Pantry[]);
            dispatch(
                goFetchPantries(asyncResponse)
            )
        };
export const createPantry =
    (newPantry: PantryNoID): AppThunk =>
        async dispatch => {
        const asyncResponse = await client.postData(process.env.REACT_APP_API + '/api/Pantry', newPantry)
            .then(response => response.json())
            .then(response => response as Pantry);
        dispatch(
            goCreatePantry(asyncResponse)
        )
        };
export const updatePantry =
    (updatedPantry: Pantry): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.putData(process.env.REACT_APP_API + '/api/Pantry', updatedPantry)
        .then(response => response.json())
        .then(response => response as Pantry);
    dispatch(
        goUpdatePantry(asynchResponse)
    )
};

export const deletePantry =
    (deletePantryID: Number): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.putData(process.env.REACT_APP_API + '/api/Pantry/deletePantry/'+ deletePantryID);
    dispatch(
        goDeletePantry(deletePantryID)
    )
        };

export const contentsPantrySearch =
    (search: String): AppThunk =>
    async dispatch =>{
        const asynchResponse = await client.getData(process.env.REACT_APP_API + '/api/Pantry/search/' + search)
            .then(response => response.json())
            .then(response => response as Pantry[]);
        dispatch(
            goContentsPantrySearch(asynchResponse)
        )
    }
    