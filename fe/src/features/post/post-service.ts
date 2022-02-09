import {BaseService} from "../../core/base-service";
import {HttpParams} from "../../core/http-client";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export class PostService extends BaseService {
    apiModule = "post"
    service = ServicesDictionary.Post

    removePostById(postId: string){
        const params = new HttpParams()
            .append("postId", postId);
        return this.httpClient.delete(this.apiModule, null, params);
    }
}