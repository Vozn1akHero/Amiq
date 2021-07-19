import {HttpParams} from "./HttpParams";
import axios from "axios";
import {from, Observable} from 'rxjs';

export class HttpClient {
    public get(params: HttpParams) : Observable<any>{
        const res = axios("get", params)
        return from(res);
    }
}
