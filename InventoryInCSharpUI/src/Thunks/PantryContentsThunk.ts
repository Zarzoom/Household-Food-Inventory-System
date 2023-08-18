import HttpClient from '../Services/Controlers/HttpClient'
import PantryContents from '../DataModels/PantryContents'
import PantryContentsNoID from '../DataModels/PantryContentsNoID'
import {AppThunk} from '../Stores/Store'
import {goFetchPantryContents, goFetchItemsInPantry, goUpdatePantryContents, goDeletePantryContents} from '../slices/PantryContentsReducer'

const client = new HttpClient();

export const fetchPantryContents =
    (password: number): AppThunk =>
        async dispatch => {
    const asynchResponse = await client.getData(process.env.REACT_APP_API + '/api/PantryContents/userSearch/' + password)
        .then(response => response.json())
        .then(response => response as PantryContents[])
            console.log(asynchResponse)
            dispatch(
                goFetchPantryContents(asynchResponse)
            )
        };

export const fetchItemsInPantry =
    (pcPantryID: number): AppThunk =>
        async dispatch => {
    const asynchResponse = await client.getData(process.env.REACT_APP_API + '/api/PantryContents/' + pcPantryID)
        .then(response => response.json())
        .then(response => response as PantryContents[])
    dispatch(
        goFetchItemsInPantry(asynchResponse)
    )
        };
export const updatePantryContents =
    (updatedPantryContents: PantryContents): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.putData(process.env.REACT_APP_API + '/api/PantryContents', updatedPantryContents)
        .then(response => response.json())
        .then(response => response as PantryContents);
    dispatch(
        goUpdatePantryContents(asynchResponse)
    )
        };
export const addPantryContents = (newPantryContents: PantryContentsNoID): AppThunk =>
    async dispatch => {
    await client.postData(process.env.REACT_APP_API + '/api/PantryContents', newPantryContents)
        .then(response => response.json())
        .then(response => response as PantryContents);
    dispatch(fetchItemsInPantry(newPantryContents.pcPantryID));
    };
export const deletePantryContents = (PantryContentID: number): AppThunk => 
    async dispatch => {
    await client.postData(process.env.REACT_APP_API + '/api/deletePantryContents/' + PantryContentID);
    dispatch(goDeletePantryContents(PantryContentID));
}
        