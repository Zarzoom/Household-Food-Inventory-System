import {ItemDisplay} from "../Models/ItemModels/ItemDisplay"
import {SearchItem} from "../Models/ItemModels/SearchItem"
import AddItemModal from "../Models/ItemModels/AddItemModal"
import {Grid, Container, Header, Content, Footer, Sidebar, Stack} from "rsuite"


const Item = () => {
    return (
        <Grid>
                <Header className='text-center' style={{fontFamily: "'Times New Roman', Times, serif"}}>
                <Stack alignItems={"center"} justifyContent={"center"}>
                        <Stack.Item alignSelf={"center"}>
                            <h1>
                        Items
                            </h1>
                        </Stack.Item>
                        <Stack.Item alignSelf={"center"}>
                            <SearchItem></SearchItem>
                        </Stack.Item>
                </Stack>
                </Header>
                <Content>
                <ItemDisplay></ItemDisplay><AddItemModal></AddItemModal>
                </Content>
        </Grid>
    );
};


export default Item;

