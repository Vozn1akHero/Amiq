import {BehaviorSubject} from "rxjs";
import {IdentityModel} from "./identity-model";

export class AuthStore {
    private static _isAuthenticated = new BehaviorSubject(false);

    public static get isAuthenticated$(){
        return this._isAuthenticated.asObservable();
    }

    public static configureIdentity(identityModel: IdentityModel){
        this._isAuthenticated.next(identityModel.isAuthenticated);
    }
}

