import {BaseService} from "../../../core/base-service";
import {HttpParams, HttpQueryParams} from "../../../core/http-client";

export class GroupParticipantService extends BaseService {
    apiModule = "group-participant"

    getUserGroups(page: number, filterType: number){
        const count: number = 10;
        const queryParams = new HttpQueryParams();
        queryParams.append("filterType", filterType);
        queryParams.append("count", count);
        queryParams.append("page", page.toString());
        return this.httpClient.get(this.buildApiPath("user-groups"), null, queryParams)
    }

    leaveGroup(groupId: number) {
        const queryParams = new HttpQueryParams();
        queryParams.append("groupId", groupId);
        return this.httpClient.delete(this.buildApiPath("leave"), null, null, queryParams)
    }

    getGroupParticipantsByGroupId(groupId: number, page: number) {
        const count: number = 10;
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

    removeParticipant(userId: number, groupId: number) {
        return this.httpClient.post(this.buildApiPath(),
            null,
            null,
            new HttpQueryParams().append("userId", userId).append("groupId", groupId));
    }

    joinGroup(groupId: number) {
        return this.httpClient.post(this.buildApiPath("join"),
            null,
            new HttpParams().append("groupId", groupId));
    }

}