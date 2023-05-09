import {ItemDisplay} from "../Models/ItemModels/ItemDisplay"
import {SearchItem} from "../Models/ItemModels/SearchItem"
import AddItemModal from "../Models/ItemModels/AddItemModal"
import {Grid, Container, Header, Content, Footer, Sidebar, Stack, FlexboxGrid, Col, Row} from "rsuite"


const Item = () => {
    return (

<Grid fluid>
                <Grid fluid>
                    <Row>
                        <div >
                            <Col xsOffset={11} xs={2}>
                            <h1>
                        Items
                            </h1>
                            </Col>
                            <Col xsOffset={7} xs={4}>
                                <SearchItem></SearchItem>
                            </Col>
                        </div >
                    </Row>
                    <Row>
                        <Col xs={4}>
                            <SearchItem></SearchItem>
                        </Col>
                    </Row>
                </Grid>
                    <Grid fluid>
                        <Col xs={2}>
                            <AddItemModal></AddItemModal>
                        </Col>
                        <Col xsOffset={6} xs={8}>
                        <ItemDisplay></ItemDisplay>
                        </Col>
                    </Grid>
</Grid>

    );
};


export default Item;
