import getItem from "../../DataModels/getItem"
import {EditItem} from "./EditItem"
import {DeleteItem} from "./DeleteItem"
import {Grid, Col, Stack} from "rsuite"

export function SingleItemDisplay(item: getItem)
{
    return(
    <Grid className={'autoWidthGrid'}>
        <div >
            <Col xs={13}>
                <p className={"whiteText"}>
                    Generic Name: {item.genericName}<br/>Brand Name: {item.brand}<br/>Size: {item.size}<br/>Price: {item.price}
                </p>
            </Col>
            <Stack direction={'column'} alignItems={'flex-end'}>
                <EditItem{...item}></EditItem>
                <DeleteItem {...item}></DeleteItem>
            </Stack>
        </div>
    </Grid>
)}

