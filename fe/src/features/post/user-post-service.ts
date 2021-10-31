import {BaseService} from "core/base-service";
import {HttpParams, HttpQueryParams} from "../../core/http-client";
import {IUserPost} from "./models/user-post";

export class UserPostService extends BaseService {
    apiModule = "user-post";

    getUserPosts(userId: number, page: number, length: number){
        const params = new HttpParams()
            .append("userId", userId.toString());
        const queryParams = new HttpQueryParams()
            .append("count", length)
            .append("page", page);
        return this.httpClient.get(this.buildApiPath("list"), params, queryParams);
    }

    create(post: Partial<IUserPost>) {
        return this.httpClient.post(this.apiModule, post);
    }
}