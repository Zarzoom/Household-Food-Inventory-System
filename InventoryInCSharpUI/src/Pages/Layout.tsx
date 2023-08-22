﻿import { Outlet, Link } from "react-router-dom";
import {useAppSelector, useAppDispatch} from '../Hooks/hooks'
import {useState, useEffect} from "react";
import {fetchItems, updateItemStatus} from "../Thunks/ItemsThunk"
import {fetchPantries, updatePantryStatus} from "../Thunks/PantriesThunk"
import { Navbar, Nav, Header, Content} from 'rsuite';

const Layout = () => {
    // const dispatch = useAppDispatch();
    const LoginStatus = useAppSelector(state => state.Login.status) as string;
    // const userLoginSelector = useAppSelector(state => state.Login.StateOfLogin);
    // const [loggedIn, setLoggedIn] = useState(false);
    //
    // const ItemStatus = useAppSelector(state => state.Items.status)
    // useEffect(()=> {
    //     if(ItemStatus === 'idle' && userLoginSelector !== undefined) {
    //         const user = userLoginSelector;
    //         const userPasswordString = "${user.password}"
    //         const userPassword = +userPasswordString;
    //         const ItemList = dispatch(fetchItems(userPassword));
    //     }
    // }, [ItemStatus, dispatch])
    // const PantryStatus = useAppSelector(state => state.Pantry.status)
    // useEffect(()=> {
    //     if (PantryStatus === 'idle' && userLoginSelector !== undefined) {
    //         const user = userLoginSelector;
    //         const userPasswordString = "${user.password}"
    //         const userPassword = +userPasswordString;
    //         const ItemList = dispatch(fetchPantries(userPassword))
    //     }
    // }, [PantryStatus, dispatch])
    let display = <Outlet/>
    // if (PantryStatus === 'idle' || ItemStatus === 'idle'){
    //     display = <p>Loading....</p>
    // }


            return (
        <>
        <Navbar>
            <Nav>
                <Nav.Item className="nav-item">
                    <Link className="nav-link" to="/">Login</Link>
                </Nav.Item>
                <Nav.Item className="nav-item">
                    <Link className="nav-link" to={LoginStatus !== "notLoggedIn" ?"/myPantries":"#"}>MyPantry</Link>
                </Nav.Item>
                <Nav.Item className="nav-item">
                    <Link className="nav-link" to={LoginStatus !== "notLoggedIn" ?"/pantry":"#"}>Pantry</Link>
                </Nav.Item>
                <Nav.Item className="nav-item">
                    <Link className="nav-link" to={LoginStatus !== "notLoggedIn" ?"/item":"#"}>Items</Link>
                </Nav.Item>
            </Nav>
        </Navbar>
            {display}
        </>

    )
};

export default Layout;