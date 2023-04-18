import {PantryListDisplay} from '../Models/PantryContentsModel/PantryListDisplay'
import {PantryContentsDisplay} from '../Models/PantryContentsModel/PantryContentsDisplay'
import {AddPanel} from '../Models/PantryContentsModel/AddPanel'
const MyPantry = () => {
    return (

        <div>
            <div className="row" style={{margin: "4em"}}>
                <div className="col-md-12">
                    <h1 className='text-center' style={{fontFamily: "'Times New Roman', Times, serif"}}>
                        My Pantries
                    </h1>
                </div>
            </div><div className="row">
            <PantryListDisplay></PantryListDisplay><PantryContentsDisplay></PantryContentsDisplay><AddPanel></AddPanel>
        </div>
        </div>
    );
};
export default MyPantry;