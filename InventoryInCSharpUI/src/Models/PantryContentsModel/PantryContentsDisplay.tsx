import {useAppSelector, useAppDispatch} from '../../Hooks/hooks'
import {useEffect} from "react";
import {fetchPantryContents} from "../../Thunks/PantryContentsThunk"
import Pantry from "../../DataModels/Pantry"
import PantryContents from "../../DataModels/PantryContents"
import {selectAllPantryContents} from "../../slices/PantryContentsReducer"
import {selectPantryByID} from "../../slices/PantriesReducer"
import {SinglePantryContentDisplay} from "./SinglePantryContentDisplay"
import {List, Panel, Col, Grid} from "rsuite";
import {AddPanel} from "./AddPanel"

export const PantryContentsDisplay = () =>{
    const dispatch = useAppDispatch();

    const PantryContentsStatus = useAppSelector(state => state.PantryContents.status);
const CurrentPantryContents = useAppSelector(selectAllPantryContents);
useEffect(() =>{
    if (PantryContentsStatus === 'idle') {
        dispatch(fetchPantryContents());
    }
}, [PantryContentsStatus, dispatch])

    
    let pantryID : number| null = useAppSelector(state => state.PantryContents.PantryFilter)
    let pantryName: string = "All Pantries";


        const pantry = useAppSelector(state=>selectPantryByID(state, pantryID as number))
        const officialPantry= pantry as Pantry;


    // pantry = useAppSelector(state => selectPantryByID(state, actualSinglePantryContent.pcPantryID))
    // let pantryName: string = "No Name"
    if(pantry != null) {
        pantryName = officialPantry.pantryName;
    }
    // console.log(pantryID);
    const PantryItems = CurrentPantryContents.map((pantryContents : PantryContents) =>{
        console.log(pantryContents.pantryContentID);
    return(
        <List.Item className={"blueBackground"} key = {"" +pantryContents.pantryContentID + pantryContents.pcItemID}>
            <SinglePantryContentDisplay{...pantryContents}></SinglePantryContentDisplay>
        </List.Item>
    )})
  
   return(
       <Panel bordered header={<Grid className={'autoWidthGrid'}>
           <Col className= 'text-left' sm={12}>{pantryName}</Col><Col className={'rightAlign'} sm={12}><AddPanel></AddPanel></Col></Grid>}  style={{fontFamily: "'Times New Roman', Times, serif"}}>
           <List>
               {PantryItems}
           </List>
       </Panel>
   )
}