import { combineReducers } from 'redux'
import chatReducer from "./reducers/chatReducer";
import groupParticipantReducer from "./reducers/groupParticipantReducer";
import groupEventReducer from "./reducers/groupEventReducer";

export default combineReducers({
    chat: chatReducer,
    groupParticipant: groupParticipantReducer,
    groupEvent: groupEventReducer,
})