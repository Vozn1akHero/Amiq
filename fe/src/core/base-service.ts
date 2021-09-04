import HttpClient from "./http-client";

export class BaseService {
    protected apiModule: string;

    protected readonly httpClient : HttpClient;

    constructor() {
        this.httpClient = new HttpClient();
    }

    protected buildApiPath(method: string): string{
        return this.apiModule.concat("/", method);
    }
}
