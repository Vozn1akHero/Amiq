import {BaseService} from "core/base-service";
import {HttpParams, HttpQueryParams} from "core/http-client";
import {IGroupPostCommentCreation, IPostComment, IPostCommentCreation} from "./models/post-comment";

export class PostCommentService extends BaseService {
    apiModule = "post-comment"

    getPostComments(postId: string, page){
        const params = new HttpParams()
            .append("postId", postId);
        const queryParams = new HttpQueryParams()
            .append("count", 20)
            .append("page", page);
        return this.httpClient.get(this.buildApiPath("user-post-comments"), params, queryParams);
    }

    getGroupPostComments(postId: string, page){
        const params = new HttpParams()
            .append("postId", postId);
        const queryParams = new HttpQueryParams()
            .append("commentType", 20)
            .append("count", 20)
            .append("page", page);
        return this.httpClient.get(this.buildApiPath("group-post-comments"), params, queryParams);
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