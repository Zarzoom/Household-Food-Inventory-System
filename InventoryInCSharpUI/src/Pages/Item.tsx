import {ItemDisplay} from "../Models/ItemModels/ItemDisplay"
import {SearchItem} from "../Models/ItemModels/SearchItem"
import AddItemModal from "../Models/ItemModels/AddItemModal"


const Item = () => {
    return (
        
            <div>
                <div className="row" style={{margin: "4em"}}>
                    <div className="col-md-12">
                        <h1 className='text-center' style={{fontFamily: "'Times New Roman', Times, serif"}}>
                            Items
                        </h1>
                    </div>
                </div><div className="row">
                <ItemDisplay></ItemDisplay><AddItemModal></AddItemModal><SearchItem></SearchItem>
                </div>
            </div>
    );
};


export default Item;

