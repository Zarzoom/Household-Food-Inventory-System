import {PantryDisplay} from "../Models/PantryModels/PantryDisplay"
import {SearchPantry} from "../Models/PantryModels/SearchPantry"
import AddPantryModal from "../Models/PantryModels/AddPantryModal"
import {Grid, Row, Col, Panel, Stack} from "rsuite";
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
                <Col xsOffset={4} xs={16} mdOffset={5} md={14} lgOffset={7} lg={10} xxlOffset={8} xxl={8}>
                    <SearchPantry></SearchPantry>
                </Col>
            </Row>
            <Grid fluid>
                <Col xs={24} smOffset={3} sm={18} mdOffset={4} md={16} lgOffset={6} lg={12} xxlOffset={7} xxl={10}>
                    <Panel bordered={true} header={<Stack justifyContent={'flex-end'}><AddPantryModal></AddPantryModal></Stack>}>
                        <PantryDisplay></PantryDisplay>
                    </Panel>
                </Col>
            </Grid>
        </Grid>
    );
};
export default Pantry;