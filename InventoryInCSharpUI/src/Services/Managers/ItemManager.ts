// import {_Item} from '../../DataModels/_Item';
// import HttpClient from '../Controlers/HttpClient'
//
// class ItemManager {
//
//     getAllItems(): Promise<_Item[]> {
//         return fetch('http://localhost:8000/api/Item')
//             .then(response => response.json())
//             .then(response => response as _Item[]);
//        
//     }
//    
//     addItem(newItem: JSON): Promise<any>{
//        const client = new HttpClient();
//         return client.postData("http://localhost:8000/api/Item", newItem);
//     }
//    
//     editItem(updatedItem: JSON): Promise<any>{
//         const client = new HttpClient();
//         return client.putData("http://localhost:8000/api/Item", updatedItem)
//     }
// }
// export default ItemManager;