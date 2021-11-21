import {IGroupEvent} from "features/group/models/group-event";
import {SET_CANCELLED_EVENT, SET_EVENT_VISIBILITY, SET_GROUP_EVENTS, SET_REOPEN_EVENT} from "../types/groupEventTypes";
import {IPaginatedStoreData} from "../base/paginated-store-data";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {IIdBasedPersistentData, IIdBasedPersistentDataEntry} from "../base/id-based-persistent-data";


type GroupEventState = {
    groupEvents: IIdBasedPersistentData<IPaginatedStoreData<IGroupEvent>>
}
const initialState : GroupEventState = {
    groupEvents: {
        entries: []
    }
}
export default function(state:GroupEventState = initialState, action) {
    switch (action.type) {
        case SET_GROUP_EVENTS:
        {
            const payload : IIdBasedPersistentDataEntry<IResponseListOf<IGroupEvent>> = action.payload;
            const foundGroup:IIdBasedPersistentDataEntry<IPaginatedStoreData<IGroupEvent>> =
                state.groupEvents.entries.filter(value => value.id === +payload.id)[0];
            const nextState = foundGroup ? state.groupEvents.entries.map(group => {
                    if(group.id === +payload.id){
                        group.data = {
                            loaded: true,
                            loading: false,
                            currentPage: group.data.currentPage + 1,
                            entities: [...group.data.entities, ...payload.data.entities],
                            length: payload.data.length
                        }
                    }
                return group;
            }) : [...state.groupEvents.entries, {
                id: payload.id,
                data: {
                    loaded: true,
                    loading: false,
                    currentPage: 1,
                    entities: payload.data.entities,
                    length: payload.data.length
                }
            } as IIdBasedPersistentDataEntry<IPaginatedStoreData<IGroupEvent>>];

            return {
                ...state,
                groupEvents: {
                    entries: nextState
                }
            }
        }
        case SET_CANCELLED_EVENT:{
            const payload : IIdBasedPersistentDataEntry<Partial<IGroupEvent>> = action.payload;
            return {
                ...state,
                groupEvents: {
                    entities: state.groupEvents?.entries.map(group => {
                        if(group.id === payload.id) {
                            group.data.entities = group.data.entities.map(groupEvent => {
                                if (groupEvent.groupEventId === payload.data.groupEventId) {
                                    groupEvent.isCancelled = true;
                                }
                                return groupEvent;
                            })
                        }
                        return group;
                    })
                }
            }
        }
        case SET_REOPEN_EVENT:{
            const payload : IIdBasedPersistentDataEntry<Partial<IGroupEvent>> = action.payload;
            return {
                ...state,
                groupEvents: {
                    entities: state.groupEvents?.entries.map(group => {
                        if(group.id === payload.id) {
                            group.data.entities = group.data.entities.map(groupEvent => {
                                if (groupEvent.groupEventId === payload.data.groupEventId) {
                                    groupEvent.isCancelled = false;
                                }
                                return groupEvent;
                            })
                        }
                        return group;
                    })
                }
            }
        }
        case SET_EVENT_VISIBILITY:{
            const payload : IIdBasedPersistentDataEntry<Partial<IGroupEvent>> = action.payload;
            return {
                ...state,
                groupEvents: {
                    entities: state.groupEvents?.entries.map(group => {
                        if(group.id === payload.id) {
                            group.data.entities = group.data.entities.map(groupEvent => {
                                if (groupEvent.groupEventId === payload.data.groupEventId) {
                                    groupEvent.isHidden = action.payload.isHidden;
                                }
                                return groupEvent;
                            })
                        }
                        return group;
                    })
                }
            }
        }
        default:
            return state;
    }
}