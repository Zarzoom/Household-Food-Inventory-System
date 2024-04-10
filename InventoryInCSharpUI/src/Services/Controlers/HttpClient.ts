 import {msalConfig} from '../../AuthConfig';
import {PublicClientApplication} from '@azure/msal-browser';



const msalInstance = new PublicClientApplication(msalConfig);
class HttpClient {

    postData (url: string = "", data: any = {}): Promise<any> {
        // Default options are marked with *
        let content: RequestInit = {
            method: "POST", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                "Authorization" : "Bearer " + "to be fille din "
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            redirect: "follow", // manual, *follow, error
            referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
            body: JSON.stringify(data), // body data type must match "Content-Type" header
        };


        const response: Promise<any> = fetch(url, content);
        return (response); // parses JSON response into native JavaScript objects
    }
    
    getData (url: string="", data: any = {}): Promise<any>{
        let content: RequestInit = {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            redirect: "follow", // manual, *follow, error
            referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        }
        console.log(url);
        const response: Promise<any> = fetch(url, content);
        return (response);
    }
    putData (url: string = "", data: any = {}): Promise<any> {
        // Default options are marked with *
        let content: RequestInit = {
            method: "PUT", // *GET, POST, PUT, DELETE, etc.
            mode: "cors", // no-cors, *cors, same-origin
            cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            redirect: "follow", // manual, *follow, error
            referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
            body: JSON.stringify(data), // body data type must match "Content-Type" header
        };


        const response: Promise<any> = fetch(url, content);
        return (response); // parses JSON response into native JavaScript objects
    }
}
export default HttpClient;