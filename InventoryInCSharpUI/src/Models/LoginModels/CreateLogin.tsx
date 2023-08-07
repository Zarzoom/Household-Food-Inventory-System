import {useEffect, useState} from "react";
import {createLogin} from "../../Thunks/LoginThunks"
import {goSubmitError} from "../../slices/LoginReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import { Button, Input, Modal, Message, useToaster } from 'rsuite';
import Login from "../../DataModels/Login"
import {PLACEMENT} from "rsuite/utils";


export function CreateLogin(){
    const [open, setOpen] = useState(false);
    const [displayed, setDisplayed] = useState("")
    
    const[newUsername, setUsername] = useState("");

    const dispatch = useAppDispatch();
    const newLogin = useAppSelector(state => state.Login.StateOfLogin);
    useEffect(()=>{const error = useAppSelector(state=>(state.Login.error));}, ) 
    const toaster = useToaster();
    
    const newLoginDispatch = () =>{
        const loginToJSONStringify = JSON.stringify(newUsername);
        const loginToJsonParse = JSON.parse(loginToJSONStringify);
        const loginLogin: Login = {username: loginToJsonParse}
        const dispatchCreateLoginPromise = () =>Promise.resolve(dispatch(createLogin(loginLogin)));
            
            dispatchCreateLoginPromise().then(() => {
                if(error !== undefined && error === " The user name has already been taken. Please, choose another.") {
                    setDisplayed("The user name has already been taken. Please, choose another.")

                }
                else if(error !== undefined && error === "Something went wrong. Please try again."){
                    setDisplayed("Something went wrong. Please try again.")
                    
                }
                else if(error === undefined && newLogin !== undefined){
                    setDisplayed("Username: {newLogin.username}/nPassword: {newLogin.password}")
                }
            })
    }
    
    
    




const cancelLoginDispatch = () => {
setUsername("");
    setOpen(false);
}

const closeMessage = () => {
        setDisplayed("")
}

const closeModal = () => {
        setOpen(false);
    dispatch(goSubmitError(undefined));
}

return (
        <div>
            <Button className={"yellowButton"} appearance={ 'primary'} onClick={(event: any)=>setOpen(true)}>Create Login</Button>
            <Modal open={open} onClose={()=>closeModal()}>
                <Modal.Header></Modal.Header>
                <div className= "blueBackground">
                    <Modal.Body>
                        <p>Logins are intended to customize user experience and not for security purposes.</p>
                        <label>Username:</label><br/>
                        <Input placeholder="beans" value={newUsername}
                               onChange={(value: string, event) => setUsername(value)}/><br/>
                        <div> {displayed}</div>
                        <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => newLoginDispatch()}>Add</Button>
                        <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => cancelLoginDispatch()}>Cancel</Button>
                    </Modal.Body>
                </div>
            </Modal>
        </div>
);
};