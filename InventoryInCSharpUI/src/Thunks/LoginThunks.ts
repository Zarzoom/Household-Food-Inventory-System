import HttpClient from '../Services/Controlers/HttpClient'
import Login from "../DataModels/Login"
import {AppThunk} from "../Stores/Store";
import {goCreateLogin, goFetchLogin} from "../slices/LoginReducer";

const client = new HttpClient();

export const createLogin =
    (newLogin: Login): AppThunk => 
        async dispatch => {
            const asynchResponse = await client.postData(process.env.REACT_APP_API + '/api/Login', newLogin)
                .then(response => response.json())
                .then(response => response as Login);
            dispatch(
                goCreateLogin(asynchResponse)
            )
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
        }