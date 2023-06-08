import Pantry from "../../DataModels/Pantry"
import {EditPantry} from "./EditPantry"
import {DeletePantry} from "./DeletePantry"
import 'reactjs-popup/dist/index.css';
import {Grid, Col, Stack} from "rsuite";

/**
 * Parameter: Takes in a Pantry object that will be used to fill in the labels in the component.
* Return: Returns a component to display a singular Pantry object.
 */
export function SinglePantryDisplay(pantry: Pantry)
{

    return(

       <Grid className={'autoWidthGrid'}>
            <div>
                <Col xs={11}>
                    <p>
                        Pantry Name: {pantry.pantryName}
                    </p>
                </Col>
                <Stack direction={'column'} alignItems={'flex-end'}>
                    <EditPantry{...pantry}></EditPantry>
                    <DeletePantry{...pantry}></DeletePantry>
                </Stack>
            </div>
       </Grid>

    )}

