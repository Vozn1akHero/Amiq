import {BaseService} from "core/base-service";
import {HttpQueryParams} from "core/http-client";
import {ICreateGroupPost, IGroupPost} from "./models/group-post";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export class GroupPostService extends BaseService {
    apiModule = "group-post";
    service = ServicesDictionary.Post

    getPostsByGroupId(groupId: number, page: number) {
        const postsCount: number = 10;
        const queryParams = new HttpQueryParams();
        queryParams.append("groupId", groupId.toString());
        queryParams.append("count", postsCount);
        queryParams.append("page", page.toString());
        return this.httpClient.get(this.buildApiPath("list"), null, queryParams);
    }

    create(groupPost: ICreateGroupPost){
        return this.httpClient.post(this.apiModule, groupPost);
    }
}