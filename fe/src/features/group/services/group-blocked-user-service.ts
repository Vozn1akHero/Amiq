import {BaseService} from "../../../core/base-service";
import {HttpParams, HttpQueryParams} from "../../../core/http-client";
import {ServicesDictionary} from "../../../core/dictionaries/modules-dictionary";

export class GroupBlockedUserService extends BaseService{
    apiModule = "group-blocked-user";
    service = ServicesDictionary.Group

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