import {BehaviorSubject, Subject, take} from "rxjs";
import {IdentityModel} from "./identity-model";
import AuthService from "../../../core/auth/auth-service";
import {StatusCodes} from "http-status-codes";

export class AuthStore {
    private static _authService: AuthService = new AuthService();
    private static _isLoaded$ = new BehaviorSubject(false);
    private static _isLoading$ = new BehaviorSubject(true);
    private static _isAuthenticated$ = new BehaviorSubject<boolean>(null);
    private static _identity$ = new BehaviorSubject<IdentityModel>(null);

    public static get isAuthenticated$(){
        return this._isAuthenticated$.asObservable();
    }
    public static get isAuthenticated():boolean{
        return this._isAuthenticated$.getValue();
    }
    public static get identity$(){
        return this._identity$.asObservable();
    }
    public static get identity():IdentityModel{
        return this._identity$.getValue();
    }

    public static configureIdentity(identityModel: IdentityModel){
        this._isAuthenticated$.next(identityModel.isAuthenticated);
        this._identity$.next(identityModel);
        this._isLoading$.next(true);
        this._isLoaded$.next(true);
    }

    public static authenticate = (login:string, password: string) => {
        return AuthStore._authService.authenticate(login, password).then(res => {
            if(res.status === StatusCodes.OK){
                let identityModel = new IdentityModel();
                identityModel.isAuthenticated = true;
                AuthStore.configureIdentity(identityModel);
                return true;
            } else return false;
        })
    }

    public static logout = () => {
        if(AuthStore.isAuthenticated){
            return AuthStore._authService.logout().then(res=>{
                if(res.status === StatusCodes.OK){
                    let identityModel = new IdentityModel();
                    identityModel.isAuthenticated = false;
                    AuthStore.configureIdentity(identityModel);
                }
            });
        }
    }
}

