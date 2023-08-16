import getItem from "../../DataModels/getItem";
import {useAppSelector} from '../../Hooks/hooks'
import {selectContainsSearch} from "../../slices/ItemsReducer"
import {SingleItemDisplay} from "./SingleItemDisplay"
import {List} from "rsuite";

export const ItemDisplay =() => {

   const AllItems = useAppSelector(state => selectContainsSearch(state))
    const renderedAllItems = AllItems.map((item: getItem) => { return (
        <List.Item className={"cardBackground"} key={item.itemID}>
            <SingleItemDisplay {...item}></SingleItemDisplay>
        </List.Item>
            )})
    return(
            <List>
                {renderedAllItems}
            </List>
    )
}




