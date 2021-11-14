import {IGroupParticipant} from "../../../features/group/models/group-models";
import {
    BLOCK_USER_IN_GROUP,
    CLEAR_GROUP_PARTICIPANTS,
    REMOVE_GROUP_PARTICIPANT,
    SET_PARTICIPANTS
} from "../types/groupParticipantTypes";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {IPaginatedStoreData} from "../base/paginated-store-data";

type GroupParticipantsState = {
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
        case BLOCK_USER_IN_GROUP: {
            const nextParticipantsState = state.groupParticipants.entities
                .filter(value => value.userId !== action.payload.userId)
            return {
                ...state,
                groupParticipants: {
                    ...state.groupParticipants,
                    entities: nextParticipantsState
                }
            }
        }
        case REMOVE_GROUP_PARTICIPANT: {
            return {
                ...state,
                groupParticipants: {
                    ...state.groupParticipants,
                    entities: state.groupParticipants.entities
                        .filter(value => value.userId !== action.payload.userId)
                }
            }
        }
        default:
            return state;
    }
}