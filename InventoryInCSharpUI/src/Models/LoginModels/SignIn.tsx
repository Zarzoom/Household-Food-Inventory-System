import {useEffect, useState} from "react";
import {createLogin} from "../../Thunks/LoginThunks"
import {goSubmitError} from "../../slices/LoginReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import { Button, Input, Modal, Message, useToaster } from 'rsuite';
import Login from "../../DataModels/Login"


export function SignIn(){
    const [open, setOpen] = useState(false);
    // const [displayed, setDisplayed] = useState("")
    let displayed = <p></p>;
    const[signInLogin, setSignInLogin] = useState({
        username: "",
        password: "",
    });

    const updateUsername = (attemptedUsername: string) => {
        setSignInLogin(previousState => {
            return {...previousState, username: attemptedUsername}
        })
    }

    const updatePassword = (attemptedPassword: string) => {
        setSignInLogin(previousState => {
            return {...previousState, password: attemptedPassword}
        })
    }

    const dispatch = useAppDispatch();
    const newLogin = useAppSelector(state => state.Login.StateOfLogin);
    const error = useAppSelector(state=>(state.Login.error));
    const toaster = useToaster();

    const newLoginDispatch = () =>{
        const loginToJSONStringify = JSON.stringify(signInLogin);
        const loginToJsonParse = JSON.parse(loginToJSONStringify);
        const loginLogin: Login = {username: loginToJsonParse}
        dispatch(createLogin(loginLogin));


    }
    if(error !== undefined && error === "Incorrect UserName or Password") {
        displayed = (<p> user name has already been taken. Please, choose another.</p>)
        toaster.push(<Message closable={true} type={"warning"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});
    }
    else if(error !== undefined && error === "Something went wrong. Please try again."){
        displayed = (<p>Something went wrong. Please try again.</p>)
        toaster.push(<Message closable={true} type={"warning"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});

    }
    else if(error === undefined && newLogin !== undefined){
        displayed = (<p>Username: {newLogin.username}/nPassword: {newLogin.password}</p>)
        toaster.push(<Message closable={true} type={"info"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});
    }


    
    const cancelSignInDispatch = () => {
        updatePassword("");
        updateUsername("");
        setOpen(false);
    }

    const closeMessage = () => {
        displayed = (<p/>)
    }

    const closeModal = () => {
        setOpen(false);
        dispatch(goSubmitError(undefined));
    }