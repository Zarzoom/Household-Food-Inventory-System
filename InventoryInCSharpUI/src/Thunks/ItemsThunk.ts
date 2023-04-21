
import HttpClient from '../Services/Controlers/HttpClient'
import getItem from '../DataModels/getItem'
import Item from '../DataModels/Item'
import { RootState, AppDispatch, AppThunk } from '../Stores/Store'
import {goFetchItems, goCreateItem, goDeleteItem, goUpdateItem, goContentsItemSearch, selectContainsSearch} from '../slices/ItemsReducer'
import { AnyAction } from 'redux'
import {store} from '../Stores/Store'
import {useAppSelector, useAppDispatch} from '../Hooks/hooks'


const client = new HttpClient();
export const fetchItems =
    (): AppThunk => 
        async dispatch => {

                const asyncResponse = await client.getData('http://localhost:8000/api/Item')
                    .then(response => response.json())
                    .then(response => response as getItem[]);
                dispatch(
                    goFetchItems(asyncResponse)
                )
        };

export const createItem =
    (newItem:Item): AppThunk =>
        async dispatch =>{
        const asynchResponse = await client.postData('http://localhost:8000/api/Item', newItem)
            .then(response => response.json())
            .then(response => response as getItem);
            dispatch(
                goCreateItem(asynchResponse)
            )
        };

export const deleteItem =
    (deleteItemID: Number): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.putData('http://localhost:8000/api/Item/deleteItem/'+ deleteItemID);
    dispatch(
        goDeleteItem(deleteItemID)
    )
};

export const updateItem =
    (updatedItem:getItem): AppThunk =>
        async dispatch =>{
            const asynchResponse = await client.putData('http://localhost:8000/api/Item', updatedItem)
                .then(response => response.json())
                .then(response => response as getItem);
            dispatch(
                goUpdateItem(asynchResponse)
            )
        };

export const contentsItemSearch =
    (search: String): AppThunk =>
        async dispatch =>{
    const asynchResponse = await client.getData('http://localhost:8000/api/Item/search/' + search)
        .then(response => response.json())
        .then(response => response as getItem[]);
    dispatch(
        goContentsItemSearch(asynchResponse)
    )
        };

export const getSelectedItemData = (selector: typeof useAppSelector) => (dispatch: Function, selector: typeof useAppSelector) => {return selector(state => selectContainsSearch(state, dispatch.arguments))}
        