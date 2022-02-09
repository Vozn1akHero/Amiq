import {BaseService} from "core/base-service";
import {HttpParams, HttpQueryParams} from "core/http-client";
import {IGroupPostCommentCreation, IPostComment, IPostCommentCreation} from "./models/post-comment";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export class PostCommentService extends BaseService {
    apiModule = "post-comment"
    service = ServicesDictionary.Post

    getPostComments(postId: string, page:number, count: number){
        const queryParams = new HttpQueryParams()
            .append("postId", postId)
            .append("count", count)
            .append("page", page);
        return this.httpClient.get(this.buildApiPath("user-post-comments"), null, queryParams);
    }

    getGroupPostComments(postId: string, page: number, count: number){
        const queryParams = new HttpQueryParams()
            .append("postId", postId)
            .append("commentType", 20)
            .append("count", count)
            .append("page", page);
        return this.httpClient.get(this.buildApiPath("group-post-comments"), null, queryParams);
    }

    delete(postCommentId: string){
        const params = new HttpParams().append("postCommentId", postCommentId)
        return this.httpClient.delete(this.buildApiPath(), null, params)
    }

    create(data:IPostCommentCreation) {
        return this.httpClient.post(this.apiModule, data)
    }

    createGroupPostComment(groupPostCommentCreation:IGroupPostCommentCreation){
        return this.httpClient.post(this.buildApiPath("group-post-comment"), groupPostCommentCreation)
    }
}