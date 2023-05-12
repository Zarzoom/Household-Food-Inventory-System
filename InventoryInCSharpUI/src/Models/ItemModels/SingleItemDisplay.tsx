import getItem from "../../DataModels/getItem"
import {EditItem} from "./EditItem"
import {DeleteItem} from "./DeleteItem"
import {Grid, Col} from "rsuite"



export function SingleItemDisplay(item: getItem)
{
    return(
    <Grid>
        <div >
            <Col xs={11}>
                <p>
                    Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                </p>
            </Col>
            <Col>
                <EditItem{...item}></EditItem>
                <DeleteItem {...item}></DeleteItem>
            </Col>
        </div>
    </Grid>
)}

