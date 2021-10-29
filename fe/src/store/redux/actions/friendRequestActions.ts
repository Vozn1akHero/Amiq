import {FriendRequestService} from "features/friend/friend-request-service";
import {
    ACCEPT_FRIEND_REQUEST,
    REJECT_FRIEND_REQUEST,
    GET_RECEIVED_FRIEND_REQUESTS,
    REMOVE_ACCEPTED_FRIEND_REQUEST,
    REMOVE_REJECTED_FRIEND_REQUEST,
    SET_RECEIVED_FRIEND_REQUESTS,
    CANCEL_FRIEND_REQUEST,
    REMOVE_CANCELED_FRIEND_REQUEST,
    GET_SENT_FRIEND_REQUESTS,
    SET_SENT_FRIEND_REQUESTS
} from "../types/friendRequestTypes";
import {StatusCodes} from "http-status-codes";
import {IFriendRequest, IFriendship} from "../../../features/friend/friendship-models";
import {FriendRequestType} from "../../../features/friend/friend-request-type";

const friendRequestService = new FriendRequestService();

export const getReceivedFriendRequests = () => dispatch => {
    dispatch({
        type: GET_RECEIVED_FRIEND_REQUESTS
    })

    friendRequestService.getFriendRequests(FriendRequestType.Receiver).then(res=>{
        if(res.status === StatusCodes.OK){
            const friendRequests = res.data as Array<IFriendRequest>
            dispatch({
                type: SET_RECEIVED_FRIEND_REQUESTS,
                payload: friendRequests
            })
        }
    })
}

export const getSentFriendRequests = () => dispatch => {
    dispatch({
        type: GET_SENT_FRIEND_REQUESTS
    })

    friendRequestService.getFriendRequests(FriendRequestType.Creator).then(res=>{
        if(res.status === StatusCodes.OK){
            const friendRequests = res.data as Array<IFriendRequest>
            dispatch({
                type: SET_SENT_FRIEND_REQUESTS,
                payload: friendRequests
            })
        }
    })
}

export const cancelFriendRequest = (friendRequestId: string) => (dispatch, state) => {
    dispatch({
        type: CANCEL_FRIEND_REQUEST
    })

    friendRequestService.cancelFriendRequest(friendRequestId).then(res => {
        if(res.status === StatusCodes.OK) {
            dispatch({
                type: REMOVE_CANCELED_FRIEND_REQUEST,
                payload: friendRequestId
            })
        }
    })
}

export const acceptFriendRequest = (friendRequestId: string) => (dispatch, state) => {
    dispatch({
        type: ACCEPT_FRIEND_REQUEST
    })

    friendRequestService.acceptFriendRequest(friendRequestId).then(res => {
        if(res.status === StatusCodes.OK) {
            dispatch({
                type: REMOVE_ACCEPTED_FRIEND_REQUEST,
                payload: friendRequestId
            })
        }
    })
}

export const rejectFriendRequest = (friendRequestId: string) => (dispatch, state) => {
    dispatch({
        type: REJECT_FRIEND_REQUEST
    })

    friendRequestService.rejectFriendRequest(friendRequestId).then(res => {
        if(res.status === StatusCodes.OK) {
            dispatch({
                type: REMOVE_REJECTED_FRIEND_REQUEST,
                payload: friendRequestId
            })
        }
    })
}