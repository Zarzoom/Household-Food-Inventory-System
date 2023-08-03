import HttpClient from '../Services/Controlers/HttpClient'
import Login from "../DataModels/Login"
import {AppThunk} from "../Stores/Store";
import {goCreateLogin, goFetchLogin, goSubmitError} from "../slices/LoginReducer";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {useSelector} from "react-redux";


const client = new HttpClient();


export const createLogin =
    (newLogin: Login): AppThunk => 
        async dispatch => {
            const asynchResponse = await client.postData(process.env.REACT_APP_API + '/api/Login', newLogin)
                .then(response => response.json())
                .then(response => response as string)
            if(asynchResponse.includes( "The user name has already been taken. Please, choose another.")){
                dispatch(
                        goSubmitError(asynchResponse)
                )
            }
            else{
                const sendResponse = JSON.parse(asynchResponse);
                dispatch(goCreateLogin(sendResponse))
            }
            
        };
export const fetchLogin =
    (attemptedLogin: Login): AppThunk =>
        async dispatch => {
            const asynchResponse = await client.getData(process.env.REACT_APP_API + '/api/Login/LoginSearch')
                .then(response => response.json())
                .then(response => response as Login);
            dispatch(
                    goFetchLogin(asynchResponse)
            )
        };
