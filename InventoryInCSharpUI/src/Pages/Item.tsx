import ItemDisplay from "../Models/ItemModels/ItemDisplay"
import NewItem from "../Models/ItemModels/NewItem"

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
                <ItemDisplay></ItemDisplay><NewItem></NewItem>
                </div>
            </div>



    );
};


export default Item;

