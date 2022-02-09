import {BaseService} from "../../../core/base-service";
import {HttpParams} from "../../../core/http-client";
import {ServicesDictionary} from "../../../core/dictionaries/modules-dictionary";

export class GroupEventParticipantService extends BaseService {
    apiModule = "group-event-participant";
    service = ServicesDictionary.Group

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