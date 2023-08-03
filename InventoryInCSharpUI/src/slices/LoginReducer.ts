import {createSlice, isRejectedWithValue, PayloadAction} from '@reduxjs/toolkit'
import {RootState} from '../Stores/Store'
import Login from '../DataModels/Login'
import StatusString from '../DataModels/StatusString'
import {createLogin} from "../Thunks/LoginThunks";
import {useSelector} from "react-redux";


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
        },
        goSubmitError: (state, action:PayloadAction<string|undefined>)=>{
            state.error = action.payload;
        },
        goFetchLogin: (state, action:PayloadAction<Login>) =>{
            if(state.StateOfLogin == action.payload){
                state.status = 'idle';
                state.StateOfLogin = action.payload;
            }
            else{ state.error = "Incorrect UserName or Password"}
        },
        
},

}
        
)

export const {goCreateLogin, goFetchLogin, goSubmitError} = LoginReducer.actions;