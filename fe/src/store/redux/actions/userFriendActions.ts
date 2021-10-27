import {FriendService} from "../../../features/friend/friend-service";
import {AuthStore} from "../../custom/auth/auth-store";
import {StatusCodes} from "http-status-codes";
import {IFriendship} from "../../../features/friend/friendship-models";
import {
    BEGIN_SEARCHING_FOR_USER_FRIENDS, CLEAR_FOUND_USERS,
    CLEAR_USER_FRIENDS,
    GET_USER_FRIENDS, SET_FOUND_USER_FRIENDS,
    SET_USER_FRIENDS
} from "../types/userFriendTypes";
import {IFoundUser} from "../../../features/user/models/found-user";

const friendService = new FriendService();

export const getUserFriends = (userId: number, page, count) => (dispatch, state) => {
    if(page === 1) dispatch({type: CLEAR_USER_FRIENDS})

    dispatch({
        type: GET_USER_FRIENDS
    })

    friendService.getFriendsByUserId(AuthStore.identity.userId, page, count).then(res => {
        if(res.status === StatusCodes.OK){
            const friends = res.data as Array<IFriendship>;
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