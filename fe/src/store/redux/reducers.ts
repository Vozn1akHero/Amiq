import { combineReducers } from 'redux'
import chatReducer from "./reducers/chatReducer";
import groupParticipantReducer from "./reducers/groupParticipantReducer";
import groupEventReducer from "./reducers/groupEventReducer";
import userFriendReducer from "./reducers/userFriendReducer";
import postReducer from "./reducers/postReducer";

export default combineReducers({
    chat: chatReducer,
    groupParticipant: groupParticipantReducer,
    groupEvent: groupEventReducer,
    post: postReducer,
    userFriend: userFriendReducer
})