import {BaseService} from "../../core/base-service";
import {HttpParams, HttpQueryParams} from "../../core/http-client";

export class GroupParticipantService extends BaseService {
    apiModule = "group-participant"

    getUserGroups(page: number){
        const count: number = 10;
        const queryParams = new HttpQueryParams();
        queryParams.append("count", count);
        queryParams.append("page", page.toString());
        return this.httpClient.get(this.buildApiPath("user-groups"), null, queryParams)
    }

    leaveGroup(userId: number, groupId: number) {
        const queryParams = new HttpQueryParams();
        queryParams.append("userId", userId.toString());
        queryParams.append("groupId", groupId.toString());
        return this.httpClient.delete(this.buildApiPath("leave"))
    }

    getGroupParticipantsByGroupId(groupId: number, page: number) {
        const count: number = 4;
        const params = new HttpParams().append("groupId", groupId.toString());
        const queryParams = new HttpQueryParams();
        queryParams.append("count", count);
        queryParams.append("page", page.toString());
        return this.httpClient.get(this.buildApiPath("list"), params, queryParams);
    }

    getViewerRole(userId: number, groupId: number) {
        let query = new HttpQueryParams()
            .append("userId", userId)
            .append("groupId", groupId);
        return this.httpClient.get(this.buildApiPath("viewer-role"), null, query)
    }
}