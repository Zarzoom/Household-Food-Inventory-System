import 'bootstrap/dist/css/bootstrap.css';
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./Pages/Layout";
import Item from "./Pages/Item";
import "./BootstrapOverride.css";
import {Provider} from "react-redux"
import {store} from "./Stores/Store"
import {useAppDispatch} from './Hooks/hooks'
import {fetchItems} from "./Thunks/ItemsThunk"



export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Item/>} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

const root = ReactDOM.createRoot(document.getElementById('root')as HTMLElement);
root.render(
    <Provider store = {store}>
    <App />
    </Provider> 
);