import {PantryListDisplay} from '../Models/PantryContentsModel/PantryListDisplay'
import {PantryContentsDisplay} from '../Models/PantryContentsModel/PantryContentsDisplay'
import {AddPanel} from '../Models/PantryContentsModel/AddPanel'
import {Grid, Container, Stack, Col, Row} from "rsuite"

const MyPantry = () => {
    return (
        <Grid fluid>
            <Row>
                <div className={"centerHorizontally"}>
                    <Col xsOffset={10} xs={4} >
                        <h1 className={"padding"}>
                            My Pantries
                        </h1>
                    </Col>
                </div >
            </Row>
            <Row>
                <Col sm={5} lg={3}>
                    <PantryListDisplay></PantryListDisplay>
                </Col>
                <Col smOffset={1} sm={16} lgOffset={4} lg={10} xxlOffset={5} xxl={8}>
                    <PantryContentsDisplay></PantryContentsDisplay>
                </Col>
            </Row>
        </Grid>
    );
};
export default MyPantry;