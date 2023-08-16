import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./Pages/Layout";
import Item from "./Pages/Item";
import {Provider} from "react-redux"
import {store} from "./Stores/Store"
import Pantry from "./Pages/Pantry"
import MyPantry from "./Pages/MyPantry"
import "./StyleSheet.less"

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