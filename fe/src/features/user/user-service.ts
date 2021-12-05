import {HttpParams} from "core/http-client";
import {BaseService} from "core/base-service";

export default class UserService extends BaseService{
    apiModule = "user";

    getById(userId: number){
        let params = new HttpParams()
            .append("userId", userId);
        return this.httpClient.get(this.apiModule, params)
    }

    changeAvatar(file){
        return this.httpClient.uploadFile(this.buildApiPath("change-avatar"), {file})
    }
}
