
import HttpClient from '../Services/Controlers/HttpClient'
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import getItem from '../DataModels/getItem'
import Item from '../DataModels/Item'
import {AppThunk } from '../Stores/Store'
import {goFetchItems, goCreateItem, goDeleteItem, goUpdateItem} from '../slices/ItemsReducer'
import {fetchPantryContents} from "../Thunks/PantryContentsThunk"

const client = new HttpClient();
export const fetchItems = createAsyncThunk(
        'items/fetchItems',
                async (arg, thunkAPI) => (
                        goFetchItems(await client.getData(process.env.REACT_APP_API + '/api/Item')))
                )
      

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
    (deleteItemID: Number): AppThunk =>
        async dispatch =>{
    await client.putData(process.env.REACT_APP_API + '/api/Item/deleteItem/'+ deleteItemID);
    dispatch(
        goDeleteItem(deleteItemID)
    );
    //this dispatch makes sure that the pantry contents state is updated so that pantry contents page will load properly.
    dispatch(
        fetchPantryContents()
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
        