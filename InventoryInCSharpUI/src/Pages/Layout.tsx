import { Outlet, Link } from "react-router-dom";
import {useAppSelector, useAppDispatch} from '../Hooks/hooks'
import {useState, useEffect} from "react";
import {fetchItems, updateItemStatus} from "../Thunks/ItemsThunk"
import {fetchPantries, updatePantryStatus} from "../Thunks/PantriesThunk"
import { Navbar, Nav, Header, Content} from 'rsuite';

const Layout = () => {
    const dispatch = useAppDispatch();
    const LoginStatus = useAppSelector(state => state.Login.status) as string;
    const [loggedIn, setLoggedIn] = useState(false);
    
    useEffect(()=> {
        if (LoginStatus === "notLoggedIn"){
            setLoggedIn(false)
        }
    }, [LoginStatus])
    const ItemStatus = useAppSelector(state => state.Items.status)
    useEffect(()=> {
        if(ItemStatus === 'idle') {
            const ItemList = dispatch(fetchItems())
        }
    }, [ItemStatus, dispatch])
    const PantryStatus = useAppSelector(state => state.Pantry.status)
    useEffect(()=> {
        if (PantryStatus === 'idle') {
            const ItemList = dispatch(fetchPantries())
        }
    }, [PantryStatus, dispatch])
    let display = <Outlet/>
    if (PantryStatus === 'idle' || ItemStatus === 'idle'){
        display = <p>Loading....</p>
    }


            return (
        <>
        <Navbar>
            <Nav>
                <Nav.Item className="nav-item">
                    <Link className="nav-link" to="/">Login</Link>
                </Nav.Item>
                <Nav.Item className="nav-item" disabled={loggedIn}>
                    <Link className="nav-link" to="/myPantry">MyPantry</Link>
                </Nav.Item>
                <Nav.Item className="nav-item" disabled={loggedIn}>
                    <Link className="nav-link" to="/pantry">Pantry</Link>
                </Nav.Item>
                <Nav.Item className="nav-item" disabled={loggedIn}>
                    <Link className="nav-link" to="/item">Items</Link>
                </Nav.Item>
            </Nav>
        </Navbar>
    {display}
        </>

    )
};

export default Layout;