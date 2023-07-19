import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import {RootState} from '../Stores/Store'
import Login from '../DataModels/Login'
import StatusString from '../DataModels/StatusString'
import login from "../DataModels/Login";
import {ItemsReducer} from "./ItemsReducer";


interface LoginState{
    StateOfLogin: Login|undefined,
    status: StatusString,
    error: string| null
}

const initialState: LoginState={
    status: 'notLoggedIn',
    error: null,
    StateOfLogin: undefined,
}

export const LoginReducer = createSlice({
    name: 'login',
    initialState,
    reducers:{
        goCreateLogin: (state, action: PayloadAction<Login>)=>{
            state.StateOfLogin = action.payload; 
        },
        goFetchLogin: (state, action:PayloadAction<Login>) =>{
            if(state.StateOfLogin == action.payload){
                state.status = 'idle';
                state.StateOfLogin = action.payload;
            }
            else{ state.error = "Incorrect UserName or Password"}
        },
        
    }
})

export const {goCreateLogin, goFetchLogin} = LoginReducer.actions;