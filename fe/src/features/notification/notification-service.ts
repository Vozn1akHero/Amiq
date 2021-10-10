import {BaseService} from "core/base-service";
import {HttpQueryParams} from "../../core/http-client";

export class NotificationService extends  BaseService {
    apiModule = "notification";

    getAll(page: number, length: number){
        return this.httpClient.get(this.apiModule, null, new HttpQueryParams()
            .append("page", page)
            .append("length", length))
    }
}
