import HttpClient from "./http-client";
import ArgumentNullException from "./exceptions/argument-null-exception";
import devConfig from "dev-config.json"

export class BaseService {
    protected apiModule: string;
    protected service: string;

    protected readonly httpClient : HttpClient;

    constructor() {
        this.httpClient = new HttpClient();
    }

    protected buildApiPath(method: string = ""): string{
        if(devConfig.useMicroservices){
            //if(!this.service) throw new ArgumentNullException("service");
            console.log(this.service + "/" + this.apiModule + (method && "/" + method))
            return this.service + "/" + this.apiModule + (method && "/" + method);
        }
        else {
            const base = "api";

            /*if (method) {
                return base.concat(this.apiModule.concat(method));
            } else {
                return this.apiModule.concat("/api/");
            }*/

            return `/${base}/${this.apiModule}${method ? "/" + method : ""}`
        }
    }
}