import {useAppDispatch} from "../../Hooks/hooks";
import {goSetStatus, goSignOut} from "../../slices/LoginReducer";
import{Button} from "rsuite";


export function SignOut () {

    const dispatch = useAppDispatch();
    const SignOut = () => {
        dispatch(goSignOut(undefined));
        dispatch(goSetStatus("notLoggedIn"));
    }

    return (
<div>
    <Button className={"displayBoxButton"} appearance={'primary'} onClick={(event: any) => SignOut()}>Sign Out</Button>
</div>
    )
}