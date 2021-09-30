import {HttpParams} from "core/http-client";
import {BaseService} from "core/base-service";

export default class UserService extends BaseService{
    apiModule = "user";

    public getById(userId: string){
        let params = new HttpParams()
            .append("userId", userId);
        return this.httpClient.get(this.apiModule, params)
    }
}
