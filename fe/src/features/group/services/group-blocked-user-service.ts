import {BaseService} from "../../../core/base-service";
import {HttpParams, HttpQueryParams} from "../../../core/http-client";

export class GroupBlockedUserService extends BaseService{
    apiModule = "group-blocked-user";

    getBlockedUsers(groupId: number) {
        return this.httpClient.get(this.buildApiPath("list"),
            new HttpParams().append("groupId", groupId))
    }

    blockUser(userId: number, groupId: number) {
        return this.httpClient.post(this.buildApiPath("block"),
            null,
            null,
            new HttpQueryParams().append("userId", userId).append("groupId", groupId));
    }
}