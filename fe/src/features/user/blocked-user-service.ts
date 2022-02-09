import {BaseService} from "core/base-service";
import {HttpParams, HttpQueryParams} from "../../core/http-client";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export class BlockedUserService extends BaseService {
    apiModule = "blocked-user";
    service = ServicesDictionary.User

    getBlockedUsers(page: number, count: number){
        return this.httpClient.get(this.buildApiPath("list"), null,
            new HttpQueryParams()
                .append("page", page)
                .append("count", count));
    }

    isUserBlocked(destUserId: number){
        return this.httpClient.get(this.buildApiPath("is-blocked"),
            new HttpParams().append("userId", destUserId));
    }

    blockUser(destUserId:number){
        return this.httpClient.post(this.buildApiPath("block"), null,
            new HttpParams().append("userId", destUserId))
    }

    unblockUser(destUserId:number){
        return this.httpClient.post(this.buildApiPath("unblock"), null,
            new HttpParams().append("userId", destUserId))
    }
}