import { Outlet, Link } from "react-router-dom";
import {useAppSelector} from '../Hooks/hooks'
import {Navbar, Nav, Button} from 'rsuite';
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { InteractionStatus, InteractionType, InteractionRequiredAuthError, AccountInfo } from "@azure/msal-browser";
import { MsalAuthenticationTemplate, useMsal } from "@azure/msal-react";


const Layout = () => {
   
    const LoginStatus = useAppSelector(state => state.Login.status) as string;

    let display = <Outlet/>
    
            return (
        <>
            <MsalAuthenticationTemplate
                    interactionType={InteractionType.Redirect}
            >
            </MsalAuthenticationTemplate>
            <AuthenticatedTemplate>
                <Navbar>
                    <Nav>

                        <Nav.Item className="nav-item">
                            <Link className="nav-link" to="/myPantries">MyPantry</Link>
                        </Nav.Item>
                        <Nav.Item className="nav-item">
                            <Link className="nav-link" to="/pantry">Pantry</Link>
                        </Nav.Item>
                        <Nav.Item className="nav-item">
                            <Link className="nav-link" to="/item">Items</Link>
                        </Nav.Item>
                    </Nav>
                </Navbar>
                    {display}
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <p>Please Sign in.</p>
                <Button >Login</Button>
            </UnauthenticatedTemplate>
        </>

    )
};

export default Layout;