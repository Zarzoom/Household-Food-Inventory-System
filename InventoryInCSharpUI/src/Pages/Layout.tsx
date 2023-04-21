import { Outlet, Link } from "react-router-dom";
import {useAppSelector, useAppDispatch} from '../Hooks/hooks'
import {useSelector} from 'react-redux'
import {store} from "../Stores/Store"
import {useState, useEffect, Component} from "react";
import {fetchItems} from "../Thunks/ItemsThunk"
import {fetchPantries} from "../Thunks/PantriesThunk"
import {fetchPantryContents} from "../Thunks/PantryContentsThunk"
const Layout = () => {
    const dispatch = useAppDispatch();
    // const PantryContentsStatus = useAppSelector(state => state.PantryContents.status);
    // useEffect(() =>{
    //     if (PantryContentsStatus === 'idle') {
    //         const PantryContentsList = dispatch(fetchPantryContents());
    //     }
    // }, [PantryContentsStatus, dispatch])
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
            <ul className="nav">
                <li className="nav-item">
                    <Link className="nav-link" to="/">Home</Link>
                </li>
                <li className="nav-item">
                    <Link className="nav-link" to="/pantry">Pantry</Link>
                </li>
                <li className="nav-item">
                    <Link className="nav-link" to="/item">Items</Link>
                </li>
            </ul>
            {display}
        </>
    )
};

export default Layout;