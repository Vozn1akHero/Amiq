import {HttpParams} from "./HttpParams";
import axios from "axios";
import {from, Observable} from 'rxjs';

export abstract class HttpClient {
    protected get(params: HttpParams) : Observable<any>{
        const res = axios("get", params)
        return from(res);
    }
}