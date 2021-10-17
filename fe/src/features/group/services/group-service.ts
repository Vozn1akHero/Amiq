import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";
import {HttpParams, HttpQueryParams} from "core/http-client";
import {IGroupData} from "../models/group-models";

//@injectable()
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

    edit(data: Partial<IGroupData>) {
        return this.httpClient.put(this.buildApiPath("edit"), data);
    }

    getUserParams(groupId: number) {
        return this.httpClient.get(this.buildApiPath("user-params"),
            new HttpParams().append("groupId", groupId))
    }
}
