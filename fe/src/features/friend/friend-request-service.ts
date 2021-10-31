import {BaseService} from "../../core/base-service";
import {HttpParams, HttpQueryParams} from "../../core/http-client";
import {FriendRequestType} from "./friend-request-type";

export class FriendRequestService extends BaseService {
    apiModule = "friend-request"

    getFriendRequests(friendRequestType: FriendRequestType) {
        let friendRequestTypeStr: string = friendRequestType.toString();
        return this.httpClient.get(this.buildApiPath("friend-requests"),
            null,
            new HttpQueryParams().append("friendRequestType", friendRequestTypeStr))
    }

    sendFriendRequest(receiverId: number) {
        return this.httpClient.post(this.buildApiPath(""), {
            receiverId
        })
    }

    cancelFriendRequest(friendRequestId: string) {
        return this.httpClient.post(this.buildApiPath("cancel-friend-request"),
            null,
            new HttpParams().append("friendRequestId", friendRequestId))
    }

    cancelFriendRequestByDestUserId(destUserId: number) {
        return this.httpClient.post(this.buildApiPath("cancel-friend-request"),
            null,
            new HttpParams().append("destUserId", destUserId))
    }

    acceptFriendRequest(friendRequestId: string) {
        return this.httpClient.post(this.buildApiPath("accept-friend-request"),
            null,
            new HttpParams().append("friendRequestId", friendRequestId))
    }

    acceptFriendRequestByDestUserId(destUserId: number) {
        return this.httpClient.post(this.buildApiPath("accept-friend-request"),
            null,
            new HttpParams().append("destUserId", destUserId))
    }

    rejectFriendRequest(friendRequestId: string) {
        return this.httpClient.post(this.buildApiPath("reject-friend-request"),
            null,
            new HttpParams().append("friendRequestId", friendRequestId))
    }

    rejectFriendRequestByDestUserId(destUserId: number) {
        return this.httpClient.post(this.buildApiPath("reject-friend-request"),
            null,
            new HttpParams().append("destUserId", destUserId))
    }
}