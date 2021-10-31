import {FriendService} from "../../../features/friend/friend-service";
import {StatusCodes} from "http-status-codes";
import {IFriendship} from "../../../features/friend/friendship-models";
import {
    BEGIN_SEARCHING_FOR_USER_FRIENDS, CLEAR_FOUND_USERS,
    CLEAR_USER_FRIENDS,
    GET_USER_FRIENDS, REMOVE_FRIEND, REMOVE_FRIEND_FROM_STORE, SET_FOUND_USER_FRIENDS,
    SET_USER_FRIENDS
} from "../types/userFriendTypes";
import {IFoundUser} from "../../../features/user/models/found-user";
import {IResponseListOf} from "../../../core/http-client/response-list-of";

const friendService = new FriendService();

export const getUserFriends = (userId: number, page, count) => (dispatch, state) => {
    if(page === 1) dispatch({type: CLEAR_USER_FRIENDS})

    dispatch({
        type: GET_USER_FRIENDS
    })

    friendService.getFriendsByUserId(userId, page, count).then(res => {
        if(res.status === StatusCodes.OK){
            const friends = res.data as IResponseListOf<IFriendship>;

            dispatch({
                type: SET_USER_FRIENDS,
                payload: friends
            })
        }
    })
}

export const searchForFriends = (text: string) => dispatch => {
    if(text.length === 0){
        dispatch({
            type: CLEAR_FOUND_USERS
        })
        return;
    }

    dispatch({
        type: BEGIN_SEARCHING_FOR_USER_FRIENDS
    })

    friendService.search(text).then(res => {
        const foundFriends = res.data as Array<IFoundUser>;
        if(res.status === StatusCodes.OK){
            dispatch({
                type: SET_FOUND_USER_FRIENDS,
                payload: foundFriends
            })
        }
    })
}

export const removeFriend = (friendId: number) => dispatch => {
    dispatch({
        type: REMOVE_FRIEND
    })

    friendService.removeFriend(friendId).then(res => {
        if(res.status === StatusCodes.OK)
            dispatch({
                type: REMOVE_FRIEND_FROM_STORE,
                payload: friendId
            })
    })
}