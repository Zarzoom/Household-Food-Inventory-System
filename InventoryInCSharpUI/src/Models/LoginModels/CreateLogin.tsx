import {useEffect, useState} from "react";
import {createLogin} from "../../Thunks/LoginThunks"
import { goSubmitError} from "../../slices/LoginReducer"
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'
import { Button, Input, Modal } from 'rsuite';
import {useDispatch, useSelector} from "react-redux";
import {selectItemsByID} from "../../slices/ItemsReducer";
import {fetchItems} from "../../Thunks/ItemsThunk";

export function CreateLogin(){
    const [open, setOpen] = useState(false);
    
    const[newUsername, setUsername] = useState("");

    const dispatch = useAppDispatch();
    
    const newLoginDispatch = () =>{
        const loginToJSONStringify = JSON.stringify(newUsername);
        const loginToJsonParse = JSON.parse(loginToJSONStringify);
        dispatch(createLogin(loginToJsonParse));

        if(error !== undefined) {
            alert("The user name has already been taken. Please, choose another.")
            dispatch(goSubmitError(undefined))
        }
    }
    
    
    const error = useAppSelector(state=>(state.Login.error))
    




const cancelLoginDispatch = () => {
setUsername("");
    setOpen(false);
}


return (
        <div>
            <Button className={"yellowButton"} appearance={ 'primary'} onClick={(event: any)=>setOpen(true)}>Create Login</Button>
            <Modal open={open} onClose={()=>setOpen(false)}>
                <Modal.Header></Modal.Header>
                <div className= "blueBackground">
                    <Modal.Body>

                        <label>Username:</label><br/>
                        <Input placeholder="beans" value={newUsername}
                               onChange={(value: string, event) => setUsername(value)}/><br/>
                       
                        <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => newLoginDispatch()}>Add</Button>
                        <Button className={"yellowButton"} appearance={'primary'} onClick={(event: any) => cancelLoginDispatch()}>Cancel</Button>
                    </Modal.Body>
                </div>
            </Modal>
        </div>
);
};