import {Message, useToaster} from "rsuite";
import {useEffect, useState} from "react";
import {useAppDispatch, useAppSelector} from '../../Hooks/hooks'


export function ErrorToast() {
    const error = useAppSelector(state => (state.Login.error));
    const newLogin = useAppSelector(state => state.Login.StateOfLogin);
    
    const toaster = useToaster();
    const [displayed, setDisplayed] = useState("")

    const closeMessage = () => {
        setDisplayed("")
    }
useEffect(()=>{    
    if (error !== undefined && error === " The user name has already been taken. Please, choose another.") {
    setDisplayed("The user name has already been taken. Please, choose another.")
    useToaster().push(<Message closable={true} type={"info"} duration={100000}
                               onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});
} else if (error !== undefined && error === "Something went wrong. Please try again.") {
    setDisplayed("Something went wrong. Please try again.")
    useToaster().push(<Message closable={true} type={"info"} duration={100000}
                               onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});

} else if (error === undefined && newLogin !== undefined) {
    setDisplayed("Username: {newLogin.username}/nPassword: {newLogin.password}")
    useToaster().push(<Message closable={true} type={"info"} duration={100000}
                               onClose={(event: any) => closeMessage()}>{displayed}</Message>, {placement: 'topCenter'});
}
}, [newLogin, error])

}