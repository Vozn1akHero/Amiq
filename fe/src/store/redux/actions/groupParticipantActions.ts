import {
    BLOCK_USER_IN_GROUP,
    CLEAR_GROUP_PARTICIPANTS,
    GET_PARTICIPANTS,
    REMOVE_GROUP_PARTICIPANT,
    SET_PARTICIPANTS
} from "../types/groupParticipantTypes";
import {GroupParticipantService} from "../../../features/group/services/group-participant-service";
import {StatusCodes} from "http-status-codes";
import {IGroupParticipant} from "../../../features/group/models/group-models";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {AxiosResponse} from "axios";

const groupParticipantService: GroupParticipantService = new GroupParticipantService();

export const getGroupParticipants = (groupId: number, page: number) => (dispatch) => {
    dispatch({
        type: GET_PARTICIPANTS
    })
    groupParticipantService.getGroupParticipantsByGroupId(groupId, page).then(res => {
        if (page === 1) {
            dispatch({
                type: CLEAR_GROUP_PARTICIPANTS
            })
        }
        if (res.status === StatusCodes.OK) {
            const data = res.data as IResponseListOf<IGroupParticipant>;
            dispatch({
                type: SET_PARTICIPANTS,
                payload: data
            })
        }
    })
}

export const blockUserInGroup = (userId: number, groupId: number) => dispatch => {
    groupParticipantService.blockUser(userId, groupId).then((res:AxiosResponse) => {
        if(res.status === StatusCodes.OK){
            dispatch({
                type: BLOCK_USER_IN_GROUP,
                payload: {userId, groupId}
            })
        }
    })
}

export const deleteGroupParticipant = (userId: number, groupId: number) => dispatch => {
    groupParticipantService.removeParticipant(userId, groupId).then((res:AxiosResponse) => {
        if(res.status === StatusCodes.OK){
            dispatch({
                type: REMOVE_GROUP_PARTICIPANT,
                payload: {userId, groupId}
            })
        }
    })
}