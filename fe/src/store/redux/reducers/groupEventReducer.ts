import {IGroupEvent} from "features/group/models/group-event";
import {SET_CANCELLED_EVENT, SET_EVENT_VISIBILITY, SET_GROUP_EVENTS, SET_REOPEN_EVENT} from "../types/groupEventTypes";


type GroupEventState = {
    groupEvents: Array<IGroupEvent>;
    groupEventsLoaded: boolean;
    length: number;
}

const initialState : GroupEventState = {
    groupEvents: [],
    groupEventsLoaded: false,
    length: 0
}

export default function(state:GroupEventState = initialState, action) {
    switch (action.type) {
        case SET_GROUP_EVENTS:
            return {
                ...state,
                groupEventsLoaded: true,
                groupEvents: action.payload
            }
        case SET_CANCELLED_EVENT:{
            return {
                ...state,
                groupEvents: state.groupEvents.filter(value => {
                    if(value.groupEventId === action.payload.groupEventId){
                        value.isCancelled = true;
                    }
                    return value;
                })
            }
        }
        case SET_REOPEN_EVENT:{
            return {
                ...state,
                groupEvents: state.groupEvents.filter(value => {
                    if(value.groupEventId === action.payload.groupEventId){
                        value.isCancelled = false;
                    }
                    return value;
                })
            }
        }
        case SET_EVENT_VISIBILITY:{
            return {
                ...state,
                groupEvents: state.groupEvents.filter(value => {
                    if(value.groupEventId === action.payload.groupEventId){
                        value.isHidden = action.payload.isHidden;
                    }
                    return value;
                })
            }
        }
        default:
            return state;
    }
}