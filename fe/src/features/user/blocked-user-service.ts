import {BaseService} from "core/base-service";
import {HttpParams} from "../../core/http-client";

export class BlockedUserService extends BaseService {
    apiModule = "blocked-user";

    getBlockedUsers(){
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