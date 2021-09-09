import {HttpParams} from "./HttpParams";
import axios, {AxiosRequestConfig, AxiosResponse} from "axios";
import {from, Observable} from 'rxjs';
import {HttpQueryParams} from "./HttpQueryParams";

export class HttpClient {
    //private readonly URL : string = "http://localhost:10709/api/";

    instance = axios.create({
        baseURL: "http://localhost:10709/api/",
        withCredentials: true,
    });

    constructor() {
        this.instance.interceptors.request.use((config) => {
            config.headers['request-startTime'] = process.hrtime()
            return config
        })

        this.instance.interceptors.response.use((response) => {
            const start = response.config.headers['request-startTime']
            const end = process.hrtime(start)
            const milliseconds = Math.round((end[0] * 1000) + (end[1] / 1000000))
            response.headers['request-duration'] = milliseconds
            return response
        })
    }

    public get<T>(path: string, params?: HttpParams, queryParams?: HttpQueryParams) : Promise<AxiosResponse<T>>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.method = "get";

        axiosRequestConfig.params = "";
        axiosRequestConfig.params += params != null ? params.toStrParams() : "";
        axiosRequestConfig.params += queryParams != null ? queryParams.toStringQuery() : "";
        axiosRequestConfig.withCredentials = true;

        const res = this.instance.request<any, AxiosResponse<T>>(axiosRequestConfig);

        return res;
    }

    public post(path: string, data?: any, params?: HttpParams, queryParams?: HttpQueryParams) :  Promise<AxiosResponse>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.method = "post";

        axiosRequestConfig.params = "";
        axiosRequestConfig.params += params != null ? params.toStrParams() : "";
        axiosRequestConfig.params += queryParams != null ? queryParams.toStringQuery() : "";

        if(data !== null) axiosRequestConfig.data = data;

        //return axios.request(axiosRequestConfig);
        return this.instance.request(axiosRequestConfig);
    }

    public delete(path: string, data?: any, params?: HttpParams, queryParams?: HttpQueryParams) :  Promise<AxiosResponse>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.method = "delete";

        axiosRequestConfig.params = "";
        axiosRequestConfig.params += params != null ? params.toStrParams() : "";
        axiosRequestConfig.params += queryParams != null ? queryParams.toStringQuery() : "";

        if(data !== null) axiosRequestConfig.data = data;

        return this.instance.request(axiosRequestConfig);
    }

    public put(path: string, data?: any, params?: HttpParams, queryParams?: HttpQueryParams) :  Promise<AxiosResponse>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = path;
        axiosRequestConfig.method = "put";

        axiosRequestConfig.params = "";
        axiosRequestConfig.params += params != null ? params.toStrParams() : "";
        axiosRequestConfig.params += queryParams != null ? queryParams.toStringQuery() : "";

        if(data !== null) axiosRequestConfig.data = data;

        return this.instance.request(axiosRequestConfig);
    }
}
