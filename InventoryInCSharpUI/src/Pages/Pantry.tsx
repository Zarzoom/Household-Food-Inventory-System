import {PantryDisplay} from "../Models/PantryModels/PantryDisplay"
import {SearchPantry} from "../Models/PantryModels/SearchPantry"
import AddPantryModal from "../Models/PantryModels/AddPantryModal"
import {Grid, Row, Col, Panel} from "rsuite";
import {SearchItem} from "../Models/ItemModels/SearchItem";
import {ItemDisplay} from "../Models/ItemModels/ItemDisplay";
const Pantry = () => {
    return (

        <Grid fluid>
            <Row>
            <div className={"centerHorizontally"}>
                <Col xsOffset={11} xs={2}>
                    <h1 className={"padding"}>
                        Pantries
                    </h1>
                </Col>
            </div>
            </Row>
            <Row>
                <Col xsOffset={9} xs={6}>
                    <SearchPantry></SearchPantry>
                </Col>
            </Row>
            <Grid fluid>
                <Col xsOffset={8} xs={8}>
                    <Panel bordered={true} header={<Grid><Col xs={2} xsOffset={11}><AddPantryModal></AddPantryModal></Col></Grid>}>
                        <PantryDisplay></PantryDisplay>
                    </Panel>
                </Col>
            </Grid>
        </Grid>

    );
};
export default Pantry;