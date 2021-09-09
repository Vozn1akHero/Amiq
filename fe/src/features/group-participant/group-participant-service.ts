import {BaseService} from "../../core/base-service";
import {HttpQueryParams} from "../../core/http-client";

export class GroupParticipantService extends BaseService {
    apiModule = "group-participant";

    getGroupParticipantsByGroupId(groupId: number, page: number) {
        const count: number = 4;
        const queryParams = new HttpQueryParams();
        queryParams.append("groupId", groupId.toString());
        queryParams.append("count", count);
        queryParams.append("page", page.toString());
        return this.httpClient.get(this.buildApiPath("list"), null);
    }

    blockParticipant(){

    }

    deleteParticipant(){

    }
}