﻿import {ItemDisplay} from "../Models/ItemModels/ItemDisplay"
import {SearchItem} from "../Models/ItemModels/SearchItem"
import AddItemModal from "../Models/ItemModels/AddItemModal"
import AddPantryModal from "../Models/PantryModels/AddPantryModal";
import {PantryDisplay} from "../Models/PantryModels/PantryDisplay";
import {Grid, Stack, Col, Row, Panel} from "rsuite"

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
                    <Col xsOffset={4} xs={16} mdOffset={5} md={14} lgOffset={7} lg={10} xxlOffset={8} xxl={8}>
                        <SearchItem></SearchItem>
                    </Col>
                </Row>
            </Grid>
            <Grid fluid>
                <Col xs={24} smOffset={3} sm={18} mdOffset={4} md={16} lgOffset={6} lg={12} xxlOffset={7} xxl={10}>
                    <Panel bordered={true} header={<Stack justifyContent={'flex-end'}><AddItemModal></AddItemModal></Stack>}>
                        <ItemDisplay></ItemDisplay>
                    </Panel>
                </Col>
            </Grid>
        </Grid>
    );
};


export default Item;
