import {createSlice, PayloadAction} from '@reduxjs/toolkit'
import Login from '../DataModels/Login'
import StatusString from '../DataModels/StatusString'


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
        goSignOut: (state, action) =>{
            state.StateOfLogin = undefined;
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

export const {goCreateLogin, goValidateLogin, goSubmitError, goSetStatus, goSignOut} = LoginReducer.actions;