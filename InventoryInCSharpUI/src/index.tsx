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
import {PublicClientApplication, EventType, AuthenticationResult} from '@azure/msal-browser';
import { IPublicClientApplication } from "@azure/msal-browser";
import {msalConfig} from './AuthConfig';
import {MsalProvider, useMsal} from "@azure/msal-react";
import {useEffect} from "react";




const msalInstance = new PublicClientApplication(msalConfig);
if(!msalInstance.getActiveAccount() && msalInstance.getAllAccounts().length >0)
{
    msalInstance.setActiveAccount(msalInstance.getAllAccounts()[0]);
}

msalInstance.addEventCallback((event) => {
    if ((event.eventType === EventType.LOGIN_SUCCESS || event.eventType === EventType.ACQUIRE_TOKEN_SUCCESS || event.eventType === EventType.SSO_SILENT_SUCCESS) && event.payload) {
        const payload = event.payload as AuthenticationResult;
        const account = payload.account;

        msalInstance.setActiveAccount(account);
    }
});


type AppProps = {pca: IPublicClientApplication};
export default function App({pca}: AppProps) {
    return (

            <MsalProvider instance={pca}>
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
                </MsalProvider>
    );
}

const root = ReactDOM.createRoot(document.getElementById('root')as HTMLElement);
root.render(

            <Provider store = {store}>
                <App pca = {msalInstance}/>
            </Provider>
);