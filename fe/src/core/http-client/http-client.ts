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


}
