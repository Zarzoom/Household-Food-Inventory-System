import Pantry from "../../DataModels/Pantry"
import {useAppDispatch} from '../../Hooks/hooks'
import {deletePantry} from "../../Thunks/PantriesThunk"
import ObjectAndState from "../../DataModels/ObjectAndState"
import {useState, useEffect, Component, SetStateAction} from "react";



export function DeletePantry(pantryForDelete: Pantry) {

    
    const dispatch = useAppDispatch();
    const deletePantryDispatch = () => {
        const PantryIDForDeleting = pantryForDelete.pantryID;
        dispatch(deletePantry(PantryIDForDeleting));
            };
    
    //Cancel button does not work.
        return (
            <div>
                <div className="row">
                    <div className="col-md-3 BlueBox">
                        <p>
                            Pantry Name: {pantryForDelete.pantryName}
                        </p>
                        <a className="btn btn-sm" href="#" role="button">Cancel</a>
                        <a className="btn btn-sm" href="#" role="button"
                           onClick={(event: any) => deletePantryDispatch()}>Delete</a>
                    </div>
                </div>
            </div>
        );

}
