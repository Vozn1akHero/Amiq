import HttpClient from "./http-client";

export class BaseService {
    public httpClient : HttpClient;

    constructor() {
        this.httpClient = new HttpClient();
    }
}
