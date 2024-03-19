import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route} from "react-router-dom";
import Layout from "./Pages/Layout";
import Item from "./Pages/Item";
import Login from "./Pages/Login";
import {Provider} from "react-redux"
import {store} from "./Stores/Store"
import Pantry from "./Pages/Pantry"
import MyPantry from "./Pages/MyPantry"
import "./StyleSheet.less"
import {PublicClientApplication, EventType} from '@azure/msal-browser';
import {msalConfig} from './AuthConfig';


const msalInstance = new PublicClientApplication(msalConfig);
if(!msalInstance.getActiveAccount() && msalInstance.getAllAccounts().length >0)
{
    msalInstance.setActiveAccount(msalInstance.getActiveAccount()[0]);
}

msalInstance.addEventCallback((event) => {
    if (event.eventType === EventType.LOGIN_SUCCESS && event.payload.account) {
        const account = event.payload.account;
        msalInstance.setActiveAccount(account);
    }
});

export default function App() {
    
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Login/>}/>
                    <Route path = "myPantries" element={<MyPantry/>}/>
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
    <App instance = {msalInstance}/>
    </Provider> 
);