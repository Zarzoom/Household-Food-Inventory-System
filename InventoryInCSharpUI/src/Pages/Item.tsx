import {ItemDisplay} from "../Models/ItemModels/ItemDisplay"
import {SearchItem} from "../Models/ItemModels/SearchItem"
import AddItemModal from "../Models/ItemModels/AddItemModal"
import {Grid, Container, Header, Content, Footer, Sidebar, Stack, FlexboxGrid, Col, Row, Panel} from "rsuite"
import AddPantryModal from "../Models/PantryModels/AddPantryModal";
import {PantryDisplay} from "../Models/PantryModels/PantryDisplay";


const Item = () => {
    return (

<Grid fluid>
                <Grid fluid>
                    <Row>
                        <div className={"centerHorizontally"}>
                            <Col xsOffset={11} xs={2} >
                            <h1 className={'padding'}>
                        Items
                            </h1>
                            </Col>
                        </div >
                    </Row>
                    <Row>
                        <Col xsOffset={9} xs={6}>
                            <SearchItem></SearchItem>
                        </Col>
                    </Row>
                </Grid>
                    <Grid fluid>
                        <Col xsOffset={8} xs={8}>
                            <Panel bordered={true} header={<Grid><Col xs={2} xsOffset={11}><AddItemModal></AddItemModal></Col></Grid>}>
                                <ItemDisplay></ItemDisplay>
                            </Panel>
                        </Col>
                    </Grid>
</Grid>

    );
};


export default Item;
