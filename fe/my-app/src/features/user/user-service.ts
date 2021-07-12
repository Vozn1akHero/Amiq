import { Singleton } from "core/singleton";
import {HttpParams} from "core/http-client";
import {BaseService} from "core/base-service";

@Singleton
export default class UserService extends BaseService{
    public getById(userId: string){
        let params = new HttpParams()
            .append("userId", userId);
        return this.httpClient.get(params)
    }
}
