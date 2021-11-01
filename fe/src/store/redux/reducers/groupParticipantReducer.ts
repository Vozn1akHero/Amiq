import {IGroupParticipant} from "../../../features/group/models/group-models";
import {CLEAR_GROUP_PARTICIPANTS, GET_PARTICIPANTS, SET_PARTICIPANTS} from "../types/groupParticipantTypes";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {IPaginatedStoreData} from "../base/paginated-store-data";

type GroupParticipantsState = {
    /*participants: Array<IGroupParticipant>;
    participantsLoaded: boolean;
    participantsLength: number;
    currentPage: number;*/
    groupParticipants: IPaginatedStoreData<IGroupParticipant>
}

const initialState : GroupParticipantsState = {
    groupParticipants: {
        entities: [],
        loaded: false,
        loading: false,
        currentPage: 1,
        length: 0
    }
}

export default function(state:GroupParticipantsState = initialState, action) {
    switch (action.type) {
        case CLEAR_GROUP_PARTICIPANTS:
            return {
                ...state,
                groupParticipants: initialState.groupParticipants
            }
        case SET_PARTICIPANTS:
        {
            const data = action.payload as IResponseListOf<IGroupParticipant>;
            return {
                ...state,
                groupParticipants: {
                    loaded: true,
                    entities: [...state.groupParticipants.entities, ...data.entities],
                    length: data.length,
                    currentPage: state.groupParticipants.currentPage+1,
                    loading: false
                }
            }
        }
        default:
            return state;
    }
}