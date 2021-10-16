import {IGroupParticipant} from "../../../features/group/models/group-models";
import {GET_PARTICIPANTS, SET_PARTICIPANTS} from "../types/groupParticipantTypes";

type GroupParticipantsState = {
    participants: Array<IGroupParticipant>;
    participantsLoaded: boolean;
}

const initialState : GroupParticipantsState = {
    participants: [],
    participantsLoaded: false
}

export default function(state:GroupParticipantsState = initialState, action) {
    switch (action.type) {
        case SET_PARTICIPANTS:
            return {
                ...state,
                participantsLoaded: true,
                participants: action.payload
            }
        default:
            return state;
    }
}