import Pantry from "../../DataModels/Pantry";
import {useAppSelector} from '../../Hooks/hooks'
import {selectContainsSearch} from "../../slices/PantriesReducer"
import {SinglePantryDisplay} from "./SinglePantryDisplay"
import {List} from "rsuite";

/**
* Summary: Gets state of Pantries and iterates over them to create a SinglePantryDisplay for each of them. It displays all of the SinglePantryDisplays as a list in the Pantry display component.
 * return: List component with all the SinglePantryDisplays as children.
 */
export const PantryDisplay =() => {

   const AllPantries = useAppSelector(state=> selectContainsSearch(state))
    const renderedAllPantries = AllPantries.map((pantry: Pantry) => { return (

            <List.Item className={"cardBackground"} key={"" + pantry.pantryID}>
                <SinglePantryDisplay {...pantry}></SinglePantryDisplay>
            </List.Item>
            )})
    return(
            <List>
                {renderedAllPantries}
            </List>
    )
}




