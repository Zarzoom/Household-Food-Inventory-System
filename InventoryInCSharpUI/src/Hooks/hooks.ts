import {TypedUseSelectorHook, useDispatch, useSelector} from 'react-redux'
import {RootState, AppDispatch} from '../Stores/Store'

type DispatchFunction = () => AppDispatch                                                       
export const useAppDispatch: DispatchFunction = useDispatch

export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector