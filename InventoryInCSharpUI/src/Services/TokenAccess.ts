
import {PublicClientApplication, EventType, AuthenticationResult, InteractionRequiredAuthError} from '@azure/msal-browser';

import {msalConfig} from '../AuthConfig';

const myMSALObj = new PublicClientApplication(msalConfig);
const protectedResources = {
    todolistApi: {
        endpoint: 'http://localhost:5000/api/todolist',
        scopes: {
            read: ['api://Enter_the_Web_Api_Application_Id_Here/Todolist.Read'],
            write: ['api://Enter_the_Web_Api_Application_Id_Here/Todolist.ReadWrite'],
        },
    },
};

function getTokenRedirect(request) {
    /**
     * See here for more info on account retrieval:
     * https://github.com/AzureAD/microsoft-authentication-library-for-js/blob/dev/lib/msal-common/docs/Accounts.md
     */
    request.account = myMSALObj.getAccountByUsername(username);
    return myMSALObj.acquireTokenSilent(request).catch((error) => {
        console.error(error);
        console.warn('silent token acquisition fails. acquiring token using popup');
        if (error instanceof InteractionRequiredAuthError) {
            // fallback to interaction when silent call fails
            return myMSALObj.acquireTokenRedirect(request);
        } else {
            console.error(error);
        }
    });
}
export default async function getToken() {
    let tokenResponse;

        tokenResponse = await getTokenRedirect({
            scopes: [...protectedResources.todolistApi.scopes.read],
        });


    if (!tokenResponse) {
        return null;
    }

    return tokenResponse.accessToken;
}