import Pantry from "../../DataModels/Pantry";
import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {selectContainsSearch} from "../../slices/PantriesReducer"
import {SinglePantryDisplay} from "./SinglePantryDisplay"
import {List} from "rsuite";




export const PantryDisplay =() => {

   const AllPantries = useAppSelector(state=> selectContainsSearch(state))
    const renderedAllPantries = AllPantries.map((pantry: Pantry) => { return (

            <List.Item className={"blueBackground"} key={"" + pantry.pantryID}>
                <SinglePantryDisplay {...pantry}></SinglePantryDisplay>
            </List.Item>
            )})
    return(
                <List>
                    {renderedAllPantries}
                </List>
    )
}




