import {HttpParams} from "./HttpParams";
import axios, {AxiosRequestConfig, AxiosResponse} from "axios";
import {from, Observable} from 'rxjs';
import {HttpQueryParams} from "./HttpQueryParams";

export class HttpClient {
    private readonly URL : string = "http://localhost:10709/api/";

    public get<T>(path: string, params?: HttpParams, queryParams?: HttpQueryParams) : Promise<AxiosResponse<T>>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = this.URL + path;
        axiosRequestConfig.method = "get";

        axiosRequestConfig.params = "";
        axiosRequestConfig.params += params != null ? params.toStrParams() : "";
        axiosRequestConfig.params += queryParams != null ? queryParams.toStringQuery() : "";

        const res = axios.request<any, AxiosResponse<T>>(axiosRequestConfig);

        return res;
    }

    public post(path: string, data?: any, params?: HttpParams, queryParams?: HttpQueryParams) :  Promise<AxiosResponse>{
        let axiosRequestConfig : AxiosRequestConfig = {};
        axiosRequestConfig.url = this.URL + path;
        axiosRequestConfig.method = "post";

        axiosRequestConfig.params = "";
        axiosRequestConfig.params += params != null ? params.toStrParams() : "";
        axiosRequestConfig.params += queryParams != null ? queryParams.toStringQuery() : "";

        if(data !== null) axiosRequestConfig.data = data;

        return axios.request(axiosRequestConfig);
    }


}
