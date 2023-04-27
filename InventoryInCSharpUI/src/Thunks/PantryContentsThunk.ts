import HttpClient from '../Services/Controlers/HttpClient'
import PantryContents from '../DataModels/PantryContents'
import PantryContentsNoID from '../DataModels/PantryContentsNoID'
import { RootState, AppDispatch, AppThunk } from '../Stores/Store'
import { AnyAction } from 'redux'
import {store} from '../Stores/Store'
import {goFetchPantryContents, goFetchItemsInPantry, goUpdatePantryContents, goDeletePantryContents} from '../slices/PantryContentsReducer'

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
        };
export const updatePantryContents =
    (updatedPantryContents: PantryContents): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.putData('http://localhost:8000/api/PantryContents', updatedPantryContents)
        .then(response => response.json())
        .then(response => response as PantryContents);
    dispatch(
        goUpdatePantryContents(asynchResponse)
    )
        };
export const addPantryContents = (newPantryContents: PantryContentsNoID): AppThunk =>
    async dispatch => {
    const asynchResponse = await client.postData('http://localhost:8000/api/PantryContents', newPantryContents)
        .then(response => response.json())
        .then(response => response as PantryContents);
    dispatch(fetchItemsInPantry(newPantryContents.pcPantryID));
    };
export const deletePantryContents = (PantryContentID: number): AppThunk => 
    async dispatch => {
    const asynchResponse = await client.postData('http://localhost:8000/api/deletePantryContents/' + PantryContentID);
    dispatch(goDeletePantryContents(PantryContentID));
}
        