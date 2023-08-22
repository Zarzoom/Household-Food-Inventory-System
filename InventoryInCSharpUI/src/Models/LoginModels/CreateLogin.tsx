import {useState} from "react";
import {createLogin} from "../../Thunks/LoginThunks"
import {goSubmitError} from "../../slices/LoginReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import { Button, Input, Modal, Message, useToaster } from 'rsuite';
import Login from "../../DataModels/Login"


export function CreateLogin(){
    const [open, setOpen] = useState(false);
    let displayed = <p></p>;
    const[newUsername, setUsername] = useState("");

    const dispatch = useAppDispatch();
    const newLogin = useAppSelector(state => state.Login.StateOfLogin);
    const error = useAppSelector(state=>(state.Login.error));
    const status = useAppSelector(state => (state.Login.status));
    const toaster = useToaster();

    const newLoginDispatch = () =>{
        const loginToJSONStringify = JSON.stringify(newUsername);
        const loginToJsonParse = JSON.parse(loginToJSONStringify);
        const loginLogin: Login = {username: loginToJsonParse}
        dispatch(createLogin(loginLogin));
          
    }
    if(error !== undefined && error === "The user name has already been taken. Please, choose another." && open === true) {
        displayed = (<p> user name has already been taken. Please, choose another.</p>)
        toaster.push(<Message closable={true} type={"warning"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});
    }
    else if(error !== undefined && error === "Something went wrong. Please try again." && open === true){
        displayed = (<p>Something went wrong. Please try again.</p>)
        toaster.push(<Message closable={true} type={"warning"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});
       
    }
    else if(error === undefined && newLogin !== undefined && status === "notLoggedIn" && open === true){
        displayed = (<p>Username: {newLogin.username}/nPassword: {newLogin.password}</p>)
        toaster.push(<Message closable={true} type={"info"} duration={100000}
                              onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});
    }





const cancelLoginDispatch = () => {
setUsername("");
    setOpen(false);
}

const closeMessage = () => {
        displayed = (<p/>)
}

const closeModal = () => {
        setOpen(false);
    dispatch(goSubmitError(undefined));
}

    const openModal = () => {
        setOpen(true)
        dispatch(goSubmitError(undefined));
    }

return (
        <div>
            <Button className={"displayBoxButton"} appearance={ 'primary'} onClick={(event: any)=>{openModal()}}>Create Login</Button>
            <Modal open={open} onClose={()=>closeModal()}>
                <Modal.Header></Modal.Header>
                <div className= "modalBackground">
                    <Modal.Body>
                        <p>Logins are intended to customize user experience and not for security purposes.</p>
                        <label>Username:</label><br/>
                        <Input placeholder="beans" value={newUsername}
                               onChange={(value: string, event) => setUsername(value)}/><br/>
                        <div> {displayed}</div>
                        <Button className={"modalButton"} appearance={'primary'} onClick={(event: any) => newLoginDispatch()}>Add</Button>
                        <Button className={"modalButton"} appearance={'primary'} onClick={(event: any) => cancelLoginDispatch()}>Cancel</Button>
                    </Modal.Body>
                </div>
            </Modal>
        </div> 
);
};