import { combineReducers } from 'redux'
import chatReducer from "./reducers/chatReducer";

export default combineReducers({
    chat: chatReducer
})