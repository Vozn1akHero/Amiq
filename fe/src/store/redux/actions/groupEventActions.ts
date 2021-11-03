import {GroupEventService} from "features/group/services/group-event-service";
import {
    CANCEL_EVENT,
    GET_GROUP_EVENTS,
    REOPEN_EVENT,
    SET_CANCELLED_EVENT, SET_EVENT_VISIBILITY,
    SET_GROUP_EVENTS, SET_REOPEN_EVENT
} from "../types/groupEventTypes";
import {IGroupEvent} from "features/group/models/group-event";
import {IResponseListOf} from "core/http-client/response-list-of";
import {StatusCodes} from "http-status-codes";
import {IIdBasedPersistentDataEntry} from "../base/id-based-persistent-data";

const groupEventService = new GroupEventService();

export const getGroupEvents = (groupId: number, page: number, count: number) => (dispatch) => {
    dispatch({
        type: GET_GROUP_EVENTS
    })

    groupEventService.getGroupEvents(groupId, page, count).then(res => {
        const result : IIdBasedPersistentDataEntry<IResponseListOf<IGroupEvent>> = {
            id: groupId,
            data: res.data as IResponseListOf<IGroupEvent>
        }
        dispatch({
            type: SET_GROUP_EVENTS,
            payload: result
        })
    })
}

export const cancelEventById = (groupId: number, groupEventId: string) => (dispatch) => {
    dispatch({
        type: CANCEL_EVENT
    })
    groupEventService.cancel(groupId, groupEventId).then(res => {
        if(res.status === StatusCodes.OK) {
            const payload:IIdBasedPersistentDataEntry<Partial<IGroupEvent>> = {
                id: groupId,
                data: {groupEventId}
            }
            dispatch({
                type: SET_CANCELLED_EVENT,
                payload
            })
        }
    })
}

export const reopenEventById = (groupId: number, groupEventId: string) => (dispatch) => {
    dispatch({
        type: REOPEN_EVENT
    })
    groupEventService.reopen(groupId, groupEventId).then(res => {
        if(res.status === StatusCodes.OK) {
            const payload:IIdBasedPersistentDataEntry<Partial<IGroupEvent>> = {
                id: groupId,
                data: {groupEventId}
            }
            dispatch({
                type: SET_REOPEN_EVENT,
                payload
            })
        }
    })
}

export const setEventVisibility = (groupId: number, groupEventId: string, isVisible: boolean) => (dispatch) => {
    groupEventService.setEventVisibility(groupId, groupEventId, isVisible).then(res => {
        if(res.status === StatusCodes.OK) {
            const payload:IIdBasedPersistentDataEntry<Partial<IGroupEvent>> = {
                id: groupId,
                data: {groupEventId}
            }
            dispatch({
                type: SET_EVENT_VISIBILITY,
                payload
            })
        }
    })
}