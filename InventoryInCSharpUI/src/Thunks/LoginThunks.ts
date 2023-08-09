import HttpClient from '../Services/Controlers/HttpClient'
import Login from "../DataModels/Login"
import {AppThunk} from "../Stores/Store";
import {goCreateLogin, goValidateLogin, goSubmitError} from "../slices/LoginReducer";
import {createAsyncThunk} from "@reduxjs/toolkit";
import {useSelector} from "react-redux";


const client = new HttpClient();


export const createLogin =
    (newLogin: Login): AppThunk => 
        async dispatch => {
            const asynchResponse = await client.postData(process.env.REACT_APP_API + '/api/Login', newLogin)
                .then(response => {
                    if(response.status !== 200){
                        console.log(response);
                        return response.text();
                    }
                    else {
                        return response.json() as Login;
                    }
                console.log(response);
                })
            const Response = asynchResponse
            if(typeof Response === "string" && Response.includes( "The user name has already been taken. Please, choose another.")){
                dispatch(
                        goSubmitError( Response as string)
                )
                
            }
            else if(typeof Response === "string"){
                dispatch(
                        goSubmitError("Something went wrong. Please try again." as string)
                )
            }
            else{
                dispatch(goSubmitError(undefined))
                dispatch(goCreateLogin(asynchResponse));
            }
            
        };
export const validateLogin =
    (attemptedLogin: Login): AppThunk =>
        async dispatch => {
            const asynchResponse = await client.getData(process.env.REACT_APP_API + '/api/Login/LoginSearch')
                .then(response => response.json())
                .then(response => response as Login);
            dispatch(
                    goValidateLogin(asynchResponse)
            )
        };
