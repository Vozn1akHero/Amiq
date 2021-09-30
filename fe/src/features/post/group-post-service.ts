import {BaseService} from "core/base-service";
import {HttpQueryParams} from "core/http-client";
import {IGroupPost} from "./models/group-post";

export class GroupPostService extends BaseService {
    apiModule = "group-post";

    getPostsByGroupId(groupId: number, page: number) {
        const postsCount: number = 20;
        const queryParams = new HttpQueryParams();
        queryParams.append("groupId", groupId.toString());
        queryParams.append("count", postsCount);
        queryParams.append("page", page.toString());
        return this.httpClient.get(this.buildApiPath("list"), null, queryParams);
    }

    create(groupPost: IGroupPost){
        return this.httpClient.post(this.apiModule, groupPost);
    }
}