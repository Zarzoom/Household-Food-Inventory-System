import {useState, useEffect} from "react";
import {_Item} from "../../DataModels/_Item";
import ItemManager from "../../Services/Managers/ItemManager"


const newItemManager = new ItemManager();
var itemArray: _Item[] = new Array();

function ItemDisplay() {
    const [getAllItems, setGetAllItems] = useState<_Item[]>(new Array());
    const [state, setState] = useState('');
    // const PromiseItemArray = newItemManager.getAllItems();
    // const ItemArray = await Promise.all(PromiseItemArray);
 
    useEffect(() => {
        setState('success');
        
        newItemManager.getAllItems().then(itemArray => setGetAllItems(itemArray));
    }, []);
    
    
    
    return (
        <div className="col-md-3">
            
                {state === 'loading' ?
                    (
                        <p>loading</p>
                    ) : (
                        <p>
                            {getAllItems.map((item:_Item) => (<div className="BlueBox">
                                <p> 
                                    Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                                </p>
                                {/*<a className="btn btn-sm" href="#" role="button" onClick={()=>EditItem(item)}>Edit</a>*/}
                                <a className="btn btn-sm" href="#"
                                   role="button">Delete</a>
                            </div>))}
                        </p>
                    )}
        </div>
    );
};
export default ItemDisplay;


