import {BaseService} from "core/base-service";
import {HttpQueryParams} from "../../core/http-client";

export class NotificationService extends  BaseService {
    apiModule = "notification";

    getAll(page: number, count: number){
        return this.httpClient.get(this.apiModule, null, new HttpQueryParams()
            .append("page", page)
            .append("count", count))
    }

    anyNotReadExist(){
        return this.httpClient.get(this.buildApiPath("any-not-read-exist"));
    }

    setAllNotificationsAsRead(){
        return this.httpClient.put(this.buildApiPath("set-all-read"));
    }
}
