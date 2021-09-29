import {BaseService} from "../../core/base-service";
import {HttpQueryParams} from "../../core/http-client";

export class GroupParticipantService extends BaseService {
    apiModule = "group-participant"

    getUserGroups(userId: number){
       return this.httpClient.get(this.buildApiPath("user-groups"))
    }

    getGroupParticipantsByGroupId(groupId: number, page: number) {
        const count: number = 4;
        const queryParams = new HttpQueryParams();
        queryParams.append("groupId", groupId.toString());
        queryParams.append("count", count);
        queryParams.append("page", page.toString());
        return this.httpClient.get(this.buildApiPath("list"), null);
    }
}