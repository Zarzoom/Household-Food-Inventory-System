import getItem from "./getItem"
import Item from "./Item"
import Pantry from "./Pantry"
import PantryNoID from "./PantryNoID"
type ObjectAndState = {
    itemForGet?: getItem
    item?: Item
    pantry?: Pantry
    pantryNoID?: PantryNoID
    state: Number
}
export default ObjectAndState;