import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";
import {HttpParams, HttpQueryParams} from "core/http-client";

@injectable()
export class GroupService extends BaseService{
    apiModule = "group";

    getByName(name: string) {
        let query = new HttpQueryParams()
            .append("name", name);
        return this.httpClient.get(this.buildApiPath("search"), null, query)
    }

    getGroupById(id: number){
        let params = new HttpParams().append("groupId", id.toString())
        return this.httpClient.get(this.buildApiPath(), params)
    }


}
