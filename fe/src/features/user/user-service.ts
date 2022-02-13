import {HttpParams} from "core/http-client";
import {BaseService} from "core/base-service";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export default class UserService extends BaseService{
    apiModule = "user";
    service = ServicesDictionary.User

    getById(userId: number){
        let params = new HttpParams()
            .append("userId", userId);
        return this.httpClient.get(this.buildApiPath(), params)
    }

    changeAvatar(file){
        return this.httpClient.uploadFile(this.buildApiPath("change-avatar"), {file})
    }
}
