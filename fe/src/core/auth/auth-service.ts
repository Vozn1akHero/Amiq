import {BaseService} from "../base-service";
import {DtoRegister} from "./dto-register";
import {DtoLogin} from "./dto-login";

export default class AuthService extends BaseService {
    apiModule : string = "auth";

    validateCredentials() {
        return this.httpClient.get<any>(this.buildApiPath("validate-credentials"));
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

    logout() {
        return this.httpClient.post(this.buildApiPath("logout"));
    }

    changePassword(oldPassword: string, newPassword: string){
        return this.httpClient.put(this.buildApiPath("change-password"),
            {oldPassword, newPassword});
    }

    changeEmail(email:string){
        return this.httpClient.put(this.buildApiPath("change-email"), {email});
    }
}