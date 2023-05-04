import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./Pages/Layout";
import Item from "./Pages/Item";
import {Provider} from "react-redux"
import {store} from "./Stores/Store"
import {useAppDispatch} from './Hooks/hooks'
import {fetchItems} from "./Thunks/ItemsThunk"
import Pantry from "./Pages/Pantry"
import MyPantry from "./Pages/MyPantry"

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<MyPantry/>}/>
                    <Route path = "item" element={<Item/>} />
                    <Route path = "pantry" element={<Pantry/>} />
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