import {IFriendship} from "features/friend/friendship-models";
import {
    BEGIN_SEARCHING_FOR_USER_FRIENDS, CLEAR_FOUND_USERS,
    CLEAR_USER_FRIENDS,
    SET_FOUND_USER_FRIENDS,
    SET_USER_FRIENDS
} from "../types/userFriendTypes";
import {IFoundUser} from "../../../features/user/models/found-user";


type UserFriendReducer = {
    userFriendsLoaded: boolean;
    userFriends: Array<IFriendship>;
    searching: boolean;
    foundFriends: Array<IFoundUser>;
    foundUsers: Array<IFoundUser>;
}

const initialState : UserFriendReducer = {
    userFriendsLoaded: false,
    userFriends: [],
    searching: false,
    foundFriends: [],
    foundUsers: [],
}

export default function(state:UserFriendReducer = initialState, action) {
    switch (action.type) {
        case SET_USER_FRIENDS:
            return {
                ...state,
                userFriendsLoaded: true,
                userFriends: [...action.payload, ...state.userFriends]
            }
        case CLEAR_USER_FRIENDS:
            return {
                ...state,
                userFriendsLoaded: false,
                userFriends: []
            }
        case BEGIN_SEARCHING_FOR_USER_FRIENDS:
            return {
                ...state,
                searching: true
            }
        case SET_FOUND_USER_FRIENDS:
            return {
                ...state,
                searching: false,
                foundFriends: action.payload.foundFriends,
                foundUsers: action.payload.foundUsers
            }
        case CLEAR_FOUND_USERS: {
            return {
                ...state,
                foundFriends: [],
                foundUsers: []
            }
        }
        default:
            return state;
    }
}