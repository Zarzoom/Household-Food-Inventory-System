import getItem from "../../DataModels/getItem"
import 'rsuite/dist/rsuite.min.css';
import Button from 'rsuite/Button';
export function SingleItemForPantryContents(item: getItem){
    return(
        <div className="SingleItemDisplay">
            <div className="BlueBox" key ={"" + item.itemID}>
                <p>
                    Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                </p>
                {/*<Button appearance={'primary'} color={'cyan'} onClick={}>Add</Button>*/}
            </div>
        </div> 
    )
}