import {GET_PARTICIPANTS, SET_PARTICIPANTS} from "../types/groupParticipantTypes";
import {GroupParticipantService} from "../../../features/group/group-participant-service";
import {StatusCodes} from "http-status-codes";
import {IGroupParticipant} from "../../../features/group/group-models";

const groupParticipantService: GroupParticipantService = new GroupParticipantService();

export const getParticipants = (groupId: number, page: number) => (dispatch) => {
    dispatch({
        type: GET_PARTICIPANTS
    })
    groupParticipantService.getGroupParticipantsByGroupId(groupId, page).then(res => {
        if(res.status === StatusCodes.OK){
            dispatch({
                type: SET_PARTICIPANTS,
                payload: res.data as Array<IGroupParticipant>
            })
        }
    })
}