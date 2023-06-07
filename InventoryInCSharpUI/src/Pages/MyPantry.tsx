import {PantryListDisplay} from '../Models/PantryContentsModel/PantryListDisplay'
import {PantryContentsDisplay} from '../Models/PantryContentsModel/PantryContentsDisplay'
import {AddPanel} from '../Models/PantryContentsModel/AddPanel'
import {Grid, Container, Header, Content, Footer, Sidebar, Stack, FlexboxGrid, Col, Row} from "rsuite"

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
                <Col xs={3}>
                    <PantryListDisplay></PantryListDisplay>
                </Col>
                <Col xsOffset={3} xs={9}>
                    <PantryContentsDisplay></PantryContentsDisplay>
                </Col>
            </Row>
        </Grid>
    );
};
export default MyPantry;