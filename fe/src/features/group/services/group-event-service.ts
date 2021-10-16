import {BaseService} from "core/base-service";
import {HttpParams, HttpQueryParams} from "core/http-client";
import {IGroupEvent} from "../models/group-event";

export class GroupEventService extends BaseService {
    apiModule = "group-event";

    getGroupEvents(groupId: number) {
        const query = new HttpQueryParams()
            .append("groupId", groupId);
        return this.httpClient.get(this.buildApiPath("all"),null,query);
    }

    deleteById(groupEventId: string){
        const params = new HttpParams().append("groupEventId", groupEventId);
        return this.httpClient.delete(this.buildApiPath(), null, params);
    }

    create(data: Partial<IGroupEvent>){
        return this.httpClient.post(this.buildApiPath(), data);
    }

    edit(data: Partial<IGroupEvent>){
        return this.httpClient.put(this.buildApiPath(), data);
    }

    cancel(groupId: number, groupEventId: string){
        const params = new HttpQueryParams()
            .append("groupId", groupId)
            .append("groupEventId", groupEventId);
        return this.httpClient.put(this.buildApiPath("cancel"), null, null, params);
    }

    reopen(groupId: number, groupEventId: string){
        const params = new HttpQueryParams()
            .append("groupId", groupId)
            .append("groupEventId", groupEventId);
        return this.httpClient.put(this.buildApiPath("reopen"), null, null, params);
    }

    setEventVisibility(groupId: number, groupEventId: string, isVisible: boolean){
        const params = new HttpQueryParams()
            .append("groupId", groupId)
            .append("isVisible", isVisible ? "1" : "0")
            .append("groupEventId", groupEventId);
        return this.httpClient.put(this.buildApiPath("visibility"), null, null, params);
    }
}