import getItem from "../../DataModels/getItem"
import {useAppDispatch} from '../../Hooks/hooks'
import {deleteItem} from "../../Thunks/ItemsThunk"
import ObjectAndState from "../../DataModels/ObjectAndState"
import {useState, useEffect, Component, SetStateAction} from "react";



export function DeleteItem(itemAndState: ObjectAndState) {
    
    const ItemForDeleting = itemAndState.itemForGet as getItem;
    
   const StateSubstitute = itemAndState.state
    
    const dispatch = useAppDispatch();
    const deleteItemDispatch = () => {
        const ItemIDForDeleting = ItemForDeleting.itemID;
        dispatch(deleteItem(ItemIDForDeleting));
            };
    
    if(StateSubstitute === 1) {
        return (
            <div>
                <div className="row">
                    <div className="col-md-3 BlueBox">
                        <p>
                            Generic Name: {ItemForDeleting.genericName}<br/>Brand
                            Name: {ItemForDeleting.brand}<br/>Size: {ItemForDeleting.size}<br/>Price: {ItemForDeleting.price}
                        </p>
                        <a className="btn btn-sm" href="#" role="button">Cancel</a>
                        <a className="btn btn-sm" href="#" role="button"
                           onClick={(event: any) => deleteItemDispatch()}>Delete</a>
                    </div>
                </div>
            </div>
        );
    }
    
    else{<></>}

}
