import {useAppSelector} from '../../Hooks/hooks'
import Pantry from "../../DataModels/Pantry"
import {selectAllPantries} from "../../slices/PantriesReducer"
import {SinglePantryButton} from "./SinglePantryButton"
import {Panel, Stack} from "rsuite"


export const PantryListDisplay = () => {
    const AllPantries = useAppSelector(selectAllPantries);
    
    const renderedAllPantries = AllPantries.map((pantry: Pantry) => {
        return(
        <Stack.Item key={""+ pantry.pantryID +pantry.pantryName}>
            <SinglePantryButton{...pantry}></SinglePantryButton>
        </Stack.Item>
    )})
    return(
    <Panel bordered={true} header={"Pantries"} className={"centerHorizontally"}>
        <Stack direction={"column"} wrap={false} justifyContent={"space-between"} spacing={2}>
        {renderedAllPantries}
        </Stack>
    </Panel>
    )
}