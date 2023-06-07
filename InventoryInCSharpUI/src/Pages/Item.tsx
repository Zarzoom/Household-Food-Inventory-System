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
                        <Col xsOffset={4} xs={16} smOffset={4} sm={16} xxlOffset={9} xxl={6}>
                            <SearchItem></SearchItem>
                        </Col>
                    </Row>
                </Grid>
                <Grid fluid>
                    <Col xs={24} smOffset={3} sm={18} xxlOffset={8} xxl={8}>
                        <Panel bordered={true} header={<Grid><Col  xsOffset={11} xs={2} xxl={2} xxlOffset={11}><AddItemModal></AddItemModal></Col></Grid>}>
                            <ItemDisplay></ItemDisplay>
                        </Panel>
                    </Col>
                </Grid>
</Grid>

    );
};


export default Item;
