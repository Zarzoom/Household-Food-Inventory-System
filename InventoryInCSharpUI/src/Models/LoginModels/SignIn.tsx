import { useState} from "react";
import {validateLogin} from "../../Thunks/LoginThunks"
import {goSubmitError} from "../../slices/LoginReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import { Button, Input, Modal, Message, useToaster } from 'rsuite';
import {fetchItems} from "../../Thunks/ItemsThunk";
import {fetchPantries} from "../../Thunks/PantriesThunk";
import {fetchPantryContents} from "../../Thunks/PantryContentsThunk";



export function SignIn() {
    const [open, setOpen] = useState(false);
    let displayedSignIn = <p></p>;
    const [signInLogin, setSignInLogin] = useState({
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
    const error = useAppSelector(state => (state.Login.error));
    const status = useAppSelector(state => (state.Login.status));
    const toaster = useToaster();

    const newLoginDispatch = () => {
        const loginToJSONStringify = JSON.stringify(signInLogin);
        const loginToJsonParse = JSON.parse(loginToJSONStringify);
        dispatch(validateLogin(loginToJsonParse));
    }
    if (error !== undefined && error === "Username or password is incorrect." && open === true) {
        displayedSignIn = (<p>"Username or password is incorrect.</p>)
        toaster.push(<Message closable={true} type={"warning"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayedSignIn}</Message>, {placement: 'topCenter'});
    } else if (error !== undefined && error === "Something went wrong. Please try again." && open === true) {
        displayedSignIn = (<p>Something went wrong. Please try again.</p>)
        toaster.push(<Message closable={true} type={"warning"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayedSignIn}</Message>, {placement: 'topCenter'});

    } else if (error === undefined && newLogin !== undefined && status === "idle" && open === true) {
        dispatch(fetchItems(+signInLogin.password));
        dispatch(fetchPantries(+signInLogin.password));
        dispatch(fetchPantryContents(+signInLogin.password));
        displayedSignIn = (<p>You are logged in.</p>)
        toaster.push(<Message closable={true} type={"info"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayedSignIn}</Message>, {placement: 'topCenter'});
    }


    const cancelSignInDispatch = () => {
        updatePassword("");
        updateUsername("");
        setOpen(false);
    }

    const closeMessage = () => {
        displayedSignIn = (<p/>)
    }

    const closeModal = () => {
        displayedSignIn = (<p/>);
        setOpen(false);
        dispatch(goSubmitError(undefined));
    }
    const openModal = () => {
        setOpen(true)
        dispatch(goSubmitError(undefined));
    }

    return (
            <div>
                <Button className={"yellowButton"} appearance={ 'primary'} onClick={(event: any)=>{openModal()}}>Sign In</Button>
                <Modal open={open} onClose={()=>closeModal()}>
                    <Modal.Header></Modal.Header>
                    <div className= "blueBackground">
                        <Modal.Body>
                            <p>Logins are intended to customize user experience and not for security purposes.</p>
                            <label>Username:</label><br/>
                            <Input placeholder="Username" value={signInLogin.username}
                                   onChange={(value: string, event) => updateUsername(value)}/><br/>
                            <Input placeholder="00000" value={signInLogin.password}
                                   onChange={(value: string, event) => updatePassword(value)}/><br/>
                            <div> {displayedSignIn}</div>
                            <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => newLoginDispatch()}>Login</Button>
                            <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => cancelSignInDispatch()}>Cancel</Button>
                        </Modal.Body>
                    </div>
                </Modal>
            </div>
    );
}