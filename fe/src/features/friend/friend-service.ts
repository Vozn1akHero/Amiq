import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";
import {HttpParams, HttpQueryParams} from "../../core/http-client";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

@injectable()
export class FriendService extends BaseService{
    apiModule = "friendship"
    service = ServicesDictionary.Friendship

    getFriendsByUserId(userId: number, page: number, count: number){
        let params = new HttpParams().append("userId", userId);
        let query = new HttpQueryParams()
            .append("page", page)
            .append("count", count);
        return this.httpClient.get(this.buildApiPath("friend-list"), params, query )
    }

    search(text: string){
        let query = new HttpQueryParams()
            .append("text", text)
            .append("page", 1)
            .append("count", 10);
        return this.httpClient.get(this.buildApiPath("search"), null, query)
    }

    removeFriend(friendId: number) {
        return this.httpClient.delete(this.buildApiPath(), null, new HttpParams().append("friendId", friendId))
    }
}
