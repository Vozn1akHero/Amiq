import {IFriendship} from "features/friend/friendship-models";
import {
    BEGIN_SEARCHING_FOR_USER_FRIENDS,
    CLEAR_FOUND_USERS,
    CLEAR_USER_FRIENDS,
    REMOVE_FRIEND_FROM_STORE,
    SET_FOUND_USER_FRIENDS,
    SET_USER_FRIENDS
} from "../types/userFriendTypes";
import {IFoundUser} from "../../../features/user/models/found-user";
import {IResponseListOf} from "../../../core/http-client/response-list-of";


type UserFriendReducer = {
    userFriendsLoaded: boolean;
    userFriends: Array<IFriendship>;
    userFriendsLength: number;
    userFriendsCurrentPage: number;
    searching: boolean;
    foundFriends: Array<IFoundUser>;
    foundUsers: Array<IFoundUser>;
}

const initialState: UserFriendReducer = {
    userFriendsLoaded: false,
    userFriends: [],
    userFriendsLength: 0,
    userFriendsCurrentPage: 1,
    searching: false,
    foundFriends: [],
    foundUsers: [],
}

export default function (state: UserFriendReducer = initialState, action) {
    switch (action.type) {
        case SET_USER_FRIENDS: {
            const friends = action.payload as IResponseListOf<IFriendship>;
            return {
                ...state,
                userFriendsLoaded: true,
                userFriends: [...state.userFriends, ...friends.entities],
                userFriendsLength: friends.length,
                userFriendsCurrentPage: state.userFriendsCurrentPage + 1
            }
        }
        case CLEAR_USER_FRIENDS:
            return {
                ...state,
                userFriendsLoaded: false,
                userFriendsLength: 0,
                userFriendsCurrentPage: 1,
                userFriends: []
            }
        case BEGIN_SEARCHING_FOR_USER_FRIENDS:
            return {
                ...state,
                searching: true
            }
        case SET_FOUND_USER_FRIENDS: {
            return {
                ...state,
                searching: false,
                foundFriends: action.payload.foundFriends,
                foundUsers: action.payload.foundUsers
            }
        }
        case CLEAR_FOUND_USERS: {
            return {
                ...state,
                foundFriends: [],
                foundUsers: []
            }
        }
        case REMOVE_FRIEND_FROM_STORE: {
            return {
                ...state,
                userFriends: state.userFriends.filter(e => e.userId !== action.payload),
                foundFriends: state.foundFriends.filter(e => e.userId !== action.payload),
                foundUsers: state.foundUsers.filter(e => e.userId !== action.payload)
            }
        }
        default:
            return state;
    }
}