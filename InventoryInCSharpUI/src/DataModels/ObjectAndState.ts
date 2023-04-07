import getItem from "./getItem"
import Item from "./Item"
type ObjectAndState = {
    itemForGet?: getItem
    item?: Item
    state: Number
}
export default ObjectAndState;