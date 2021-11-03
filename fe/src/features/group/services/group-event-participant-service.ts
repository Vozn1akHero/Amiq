import {BaseService} from "../../../core/base-service";
import {HttpParams} from "../../../core/http-client";

export class GroupEventParticipantService extends BaseService {
    apiModule = "group-event-participant";

    join(groupEventId: string){
        return this.httpClient.post(this.buildApiPath("join"), null,
            new HttpParams().append("groupEventId", groupEventId))
    }

    leave(groupEventId: string){
        return this.httpClient.delete(this.buildApiPath("leave"),
            null,
            new HttpParams().append("groupEventId", groupEventId))
    }
}