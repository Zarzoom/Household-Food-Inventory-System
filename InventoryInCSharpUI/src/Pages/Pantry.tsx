import {PantryDisplay} from "../Models/PantryModels/PantryDisplay"
import {SearchPantry} from "../Models/PantryModels/SearchPantry"
import NewPantry from "../Models/PantryModels/NewPantry"
const Pantry = () => {
    return (

        <div>
            <div className="row" style={{margin: "4em"}}>
                <div className="col-md-12">
                    <h1 className='text-center' style={{fontFamily: "'Times New Roman', Times, serif"}}>
                        Pantries
                    </h1>
                </div>
            </div><div className="row">
            <PantryDisplay></PantryDisplay><NewPantry></NewPantry><SearchPantry></SearchPantry>
        </div>
        </div>
    );
};
export default Pantry;