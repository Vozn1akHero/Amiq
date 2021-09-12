import {BehaviorSubject, Subject, take} from "rxjs";
import {IdentityModel} from "./identity-model";
import {atom} from "recoil";

export class AuthStore {
    /*authState = atom({
        key: 'authStore',
        default: null,
    });*/

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

    public static get isLoaded$(){
        return this._isLoaded$.asObservable();
    }

    public static get isLoading$(){
        return this._isLoading$.asObservable();
    }

    public static get identity$(){
        return this._identity$.asObservable();
    }

    public static get identity():IdentityModel{
        return this._identity$.getValue();
    }

    public static configureIdentity(identityModel: IdentityModel){
        console.log(identityModel)
        this._isAuthenticated$.next(identityModel.isAuthenticated);
        this._identity$.next(identityModel);
        this._isLoading$.next(true);
        this._isLoaded$.next(true);
    }
}

