import 'bootstrap/dist/css/bootstrap.css';
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./Pages/Layout";
import Item from "./Pages/Item";
import "./ReactSuiteOverride.css";
import {Provider} from "react-redux"
import {store} from "./Stores/Store"
import {useAppDispatch} from './Hooks/hooks'
import {fetchItems} from "./Thunks/ItemsThunk"
import Pantry from "./Pages/Pantry"
import MyPantry from "./Pages/MyPantry"