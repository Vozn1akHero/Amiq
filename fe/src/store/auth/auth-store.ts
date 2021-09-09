import {BehaviorSubject, Subject} from "rxjs";
import {IdentityModel} from "./identity-model";

export class AuthStore {
    private static _isLoaded = new BehaviorSubject(false);
    private static _isLoading = new BehaviorSubject(true);
    private static _isAuthenticated = new Subject<boolean>();
    private static _identity = new BehaviorSubject<IdentityModel>(null);

    public static get isAuthenticated$(){
        return this._isAuthenticated.asObservable();
    }

    public static get isLoaded$(){
        return this._isLoaded.asObservable();
    }

    public static get isLoading$(){
        return this._isLoading.asObservable();
    }

    public static get identity$(){
        return this._identity.asObservable();
    }

    public static get identity():IdentityModel{
        return this._identity.getValue();
    }

    public static configureIdentity(identityModel: IdentityModel){
        this._isAuthenticated.next(identityModel.isAuthenticated);
        this._identity.next(identityModel);
        this._isLoading.next(true);
        this._isLoaded.next(true);
    }
}

