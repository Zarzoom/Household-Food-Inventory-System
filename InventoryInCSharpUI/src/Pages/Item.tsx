import {ItemDisplay} from "../Models/ItemModels/ItemDisplay"
import {SearchItem} from "../Models/ItemModels/SearchItem"
import AddItemModal from "../Models/ItemModels/AddItemModal"
import {Grid, Container, Header, Content, Footer} from "rsuite"


const Item = () => {
    return (
        <Grid>
            <div>
                <div className="row" style={{margin: "4em"}}>
                        <Header className='text-center' style={{fontFamily: "'Times New Roman', Times, serif"}}>
                            Items
                        </Header>
                </div>
                <Content>
                <ItemDisplay></ItemDisplay><AddItemModal></AddItemModal><SearchItem></SearchItem>
                </Content>
            </div>
        </Grid>
    );
};


export default Item;

