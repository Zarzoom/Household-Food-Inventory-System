import { Outlet, Link } from "react-router-dom";
import {useAppSelector, useAppDispatch} from '../Hooks/hooks'
import {useState, useEffect} from "react";
import {fetchItems} from "../Thunks/ItemsThunk"
import {fetchPantries} from "../Thunks/PantriesThunk"
import { Navbar, Nav, Header, Content} from 'rsuite';

const Layout = () => {
    const dispatch = useAppDispatch();
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
    // const LoginStatus = useAppSelector(state => state.Login.status)
    // useEffect(()=> {
    //     if(LoginStatus === 'notLoggedIn'){
    //         display = 
    //     }
    // })
    return (
        <>
        <Navbar>
            <Nav>
                <Nav.Item className="nav-item">
                    <Link className="nav-link" to="/">Home</Link>
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
        </>

    )
};

export default Layout;