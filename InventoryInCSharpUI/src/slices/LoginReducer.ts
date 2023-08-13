import {createSlice, isRejectedWithValue, PayloadAction} from '@reduxjs/toolkit'
import {RootState} from '../Stores/Store'
import Login from '../DataModels/Login'
import StatusString from '../DataModels/StatusString'
import {createLogin} from "../Thunks/LoginThunks";
import {useSelector} from "react-redux";
import {stat} from "fs";


interface LoginState{
    StateOfLogin: Login|undefined,
    status: StatusString,
    error: string| undefined
}

const initialState: LoginState={
    status: 'notLoggedIn',
    error: undefined,
    StateOfLogin: undefined,
}
// export function errorState(state: RootState) {
//     const errorStatus = useSelector((state: LoginState) => {state.error})
// }



export const LoginReducer = createSlice({
    name: 'login',
    initialState,
    reducers:{
        goCreateLogin: (state, action: PayloadAction<Login>)=>{
            state.StateOfLogin = action.payload; 
            console.log(state.StateOfLogin);
        },
        goSubmitError: (state, action:PayloadAction<string|undefined>)=>{
            state.error = action.payload;
            console.log(state.error);
        },
        goSetStatus: (state, action: PayloadAction<string>) =>{
            const NewStatus: StatusString = action.payload as StatusString;
            state.status = NewStatus;
        },
        goValidateLogin: (state, action:PayloadAction<Login>) =>{
            state.StateOfLogin = action.payload;
        },
        
},

}
        
)

export const {goCreateLogin, goValidateLogin, goSubmitError, goSetStatus} = LoginReducer.actions;