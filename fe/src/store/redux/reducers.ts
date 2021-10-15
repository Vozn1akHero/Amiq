import { combineReducers } from 'redux'
import chatReducer from "./reducers/chatReducer";
import groupParticipantReducer from "./reducers/groupParticipantReducer";

export default combineReducers({
    chat: chatReducer,
    groupParticipant: groupParticipantReducer
})