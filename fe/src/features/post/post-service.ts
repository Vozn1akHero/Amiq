import {BaseService} from "../../core/base-service";
import {HttpParams} from "../../core/http-client";

export class PostService extends BaseService {
    apiModule = "post"

    removePostById(postId: string){
        const params = new HttpParams()
            .append("postId", postId);
        return this.httpClient.delete("post", params);
    }


}