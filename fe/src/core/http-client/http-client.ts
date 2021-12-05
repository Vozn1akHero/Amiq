import {HttpParams} from "./HttpParams";
import axios, {AxiosRequestConfig, AxiosResponse} from "axios";
import {HttpQueryParams} from "./HttpQueryParams";
import devConfig from "dev-config.json"

export class HttpClient {
    instance = axios.create({
        baseURL: devConfig.monolithUrl + "/api/",
        withCredentials: true,
    });

    constructor() {
        /*this.instance.interceptors.request.use((config) => {
            config.headers['request-startTime'] = process.hrtime()
            return config
        })

        this.instance.interceptors.response.use((response) => {
            const start = response.config.headers['request-startTime']
            const end = process.hrtime(start)
            const milliseconds = Math.round((end[0] * 1000) + (end[1] / 1000000))
            response.headers['request-duration'] = milliseconds
            return response
        })*/
    }

    public get<T>(path: string, params?: HttpParams, queryParams?: HttpQueryParams) : Promise<AxiosResponse<T>>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.url += params != null ? "/" + params.toStrParams() : "";
        axiosRequestConfig.method = "get";
        if(queryParams) axiosRequestConfig.params = queryParams.toObject();
        axiosRequestConfig.withCredentials = true;
        return this.instance.request<any, AxiosResponse<T>>(axiosRequestConfig);
    }

    public post(path: string, data?: any, params?: HttpParams, queryParams?: HttpQueryParams) :  Promise<AxiosResponse>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.url += params != null ? "/" + params.toStrParams() : "";
        axiosRequestConfig.method = "post";
        if(queryParams) axiosRequestConfig.params = queryParams.toObject();
        if(data !== null) axiosRequestConfig.data = data;
        return this.instance.request(axiosRequestConfig);
    }

    public delete(path: string, data?: any, params?: HttpParams, queryParams?: HttpQueryParams) :  Promise<AxiosResponse>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.url += params != null ? "/" + params.toStrParams() : "";
        axiosRequestConfig.method = "delete";
        if(queryParams)
            axiosRequestConfig.params = queryParams.toObject();

        if(data !== null) axiosRequestConfig.data = data;

        return this.instance.request(axiosRequestConfig);
    }

    public put(path: string, data?: any, params?: HttpParams, queryParams?: HttpQueryParams) :  Promise<AxiosResponse>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.url += params != null ? "/" + params.toStrParams() : "";
        axiosRequestConfig.method = "put";

        if(queryParams)
            axiosRequestConfig.params = queryParams.toObject();

        if(data !== null) axiosRequestConfig.data = data;

        return this.instance.request(axiosRequestConfig);
    }

    public uploadFile(path: string, options: {
        file: File, params?: HttpParams, queryParams?: HttpQueryParams
    }): Promise<AxiosResponse> {
        const {file, params, queryParams} = options;
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.url += params != null ? "/" + params.toStrParams() : "";
        axiosRequestConfig.method = "post";
        axiosRequestConfig.headers = {
            "Content-Type": "multipart/form-data"
        }

        if(queryParams)
            axiosRequestConfig.params = queryParams.toObject();

        const content = new FormData();
        content.append("file", file);

        axiosRequestConfig.data = content;

        return this.instance.request(axiosRequestConfig);
    }
}
