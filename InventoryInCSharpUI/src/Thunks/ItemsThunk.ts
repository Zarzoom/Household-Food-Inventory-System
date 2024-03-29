
import HttpClient from '../Services/Controlers/HttpClient'
import getItem from '../DataModels/getItem'
import Item from '../DataModels/Item'
import {AppThunk } from '../Stores/Store'
import {goFetchItems, goCreateItem, goDeleteItem, goUpdateItem, goSetStatus} from '../slices/ItemsReducer'
import {fetchPantryContents} from "../Thunks/PantryContentsThunk"


const client = new HttpClient();
export const fetchItems =
    (password: number): AppThunk => 
        async dispatch => {
            const asyncResponse = await client.getData(process.env.REACT_APP_API + '/api/Item/userSearch/'+ password)
                .then(response => response.json())
                .then(response => response as getItem[]);
            dispatch(
                goFetchItems(asyncResponse)
            )
        };

export const createItem =
    (newItem:Item): AppThunk =>
        async dispatch =>{
            const asynchResponse = await client.postData(process.env.REACT_APP_API + '/api/Item', newItem)
                .then(response => response.json())
                .then(response => response as getItem);
            dispatch(
                goCreateItem(asynchResponse)
            )
        };

export const deleteItem =
    (deleteItemID: Number, deleteItemPassword: number): AppThunk =>
        async dispatch =>{
            await client.putData(process.env.REACT_APP_API + '/api/Item/deleteItem/'+ deleteItemID);
            dispatch(
                goDeleteItem(deleteItemID)
            );
            //this dispatch makes sure that the pantry contents state is updated so that pantry contents page will load properly.
            dispatch(
                fetchPantryContents(deleteItemPassword)
            )
        };

export const updateItem =
    (updatedItem:getItem): AppThunk =>
        async dispatch =>{
            const asynchResponse = await client.putData(process.env.REACT_APP_API + '/api/Item', updatedItem)
                .then(response => response.json())
                .then(response => response as getItem);
            dispatch(
                goUpdateItem(asynchResponse)
            )
        };

export const updateItemStatus =
        (statusUpdate: string): AppThunk =>
                dispatch =>{
    dispatch(goSetStatus(statusUpdate))
                }

// export const contentsItemSearch =
//     (search: String): AppThunk =>
//         async dispatch =>{
//     const asynchResponse = await client.getData(process.env.REACT_APP_API + '/api/Item/search/' + search)
//         .then(response => response.json())
//         .then(response => response as getItem[]);
//     dispatch(
//         goContentsItemSearch(asynchResponse)
//     )
//         };

// export const getSelectedItemData = (selector: typeof useAppSelector) => (dispatch: Function, selector: typeof useAppSelector) => {return selector(state => selectContainsSearch(state, dispatch.arguments))}
        