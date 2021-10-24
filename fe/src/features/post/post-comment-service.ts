import {BaseService} from "core/base-service";
import {HttpParams, HttpQueryParams} from "core/http-client";
import {IPostComment, IPostCommentCreation} from "./models/post-comment";

export class PostCommentService extends BaseService {
    apiModule = "post-comment"

    getPostComments(postId: string, page){
        const params = new HttpParams()
            .append("postId", postId);
        const queryParams = new HttpQueryParams()
            .append("count", 20)
            .append("page", page);
        return this.httpClient.get(this.buildApiPath("list"), params, queryParams);
    }

    delete(postCommentId: string){
        const params = new HttpParams().append("postCommentId", postCommentId)
        return this.httpClient.delete(this.buildApiPath(), params)
    }

    create(data:IPostCommentCreation) {
        return this.httpClient.post(this.apiModule, data)
    }
}