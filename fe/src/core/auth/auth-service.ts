import {BaseService} from "../base-service";
import {HttpParams} from "../http-client";
import {DtoRegister} from "./dto-register";
import {catchError, map, Observable, of} from "rxjs";
import {DtoCredentialsValidation} from "./dto-credentials-validation";
import {AxiosResponse} from "axios";
import {StatusCodes} from "http-status-codes";
import {DtoLogin} from "./dto-login";

export default class AuthService extends BaseService {
    apiModule : string = "auth";

    validateCredentials() {
        let out = new DtoCredentialsValidation();
        this.httpClient.get<any>(this.buildApiPath("validate-credentials")).then((res:AxiosResponse<any>) => {
            out.isAuthenticated = res.status === StatusCodes.OK;
        })
        return out;
    }

    authenticate(login: string, password: string) {
        let dto = new DtoLogin();
        dto.login = login;
        dto.password = password;
        return this.httpClient.post(this.buildApiPath("authenticate"), dto);
    }

    register(dtoRegister: DtoRegister){
        return this.httpClient.post(this.buildApiPath("register"), dtoRegister);
    }
}