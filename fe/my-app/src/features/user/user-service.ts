import { Singleton } from "core/singleton";
import HttpClient, {HttpParams} from "core/http-client";

@Singleton
export default class UserService extends HttpClient {
    public getById(userId: string){
        let params = new HttpParams()
            .append("userId", userId);
        return this.get(params)
    }
}