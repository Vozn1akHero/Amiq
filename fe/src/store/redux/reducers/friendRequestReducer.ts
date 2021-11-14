import {IFriendRequest} from "features/friend/friendship-models";
import {
    REMOVE_ACCEPTED_FRIEND_REQUEST, REMOVE_CANCELED_FRIEND_REQUEST,
    REMOVE_REJECTED_FRIEND_REQUEST,
    SET_RECEIVED_FRIEND_REQUESTS, SET_SENT_FRIEND_REQUESTS
} from "../types/friendRequestTypes";

type FriendRequestState = {
    receivedFriendRequestsLoaded: boolean;
    receivedFriendRequests: Array<IFriendRequest>;
    sentFriendRequestsLoaded: boolean;
    sentFriendRequests: Array<IFriendRequest>;
}

const initialState : FriendRequestState = {
    receivedFriendRequestsLoaded: false,
    receivedFriendRequests: [],
    sentFriendRequestsLoaded: false,
    sentFriendRequests: []
}

export default function (state: FriendRequestState = initialState, action) {
    switch(action.type){
        case SET_RECEIVED_FRIEND_REQUESTS: {
            return {
                ...state,
                receivedFriendRequestsLoaded: true,
                receivedFriendRequests: action.payload
            }
        }
        case SET_SENT_FRIEND_REQUESTS: {
            return {
                ...state,
                sentFriendRequestsLoaded: true,
                sentFriendRequests: action.payload
            }
        }
        case REMOVE_REJECTED_FRIEND_REQUEST: {
            return {
                ...state,
                receivedFriendRequests: state.receivedFriendRequests.filter(e=>e.friendRequestId !== action.payload)
            }
        }
        case REMOVE_ACCEPTED_FRIEND_REQUEST: {
            return {
                ...state,
                receivedFriendRequests: state.receivedFriendRequests.filter(e=>e.friendRequestId !== action.payload)
            }
        }
        case REMOVE_CANCELED_FRIEND_REQUEST: {
            return {
                ...state,
                sentFriendRequests: state.sentFriendRequests.filter(e=>e.friendRequestId !== action.payload)
            }
        }
        default:
            return state;
    }
}