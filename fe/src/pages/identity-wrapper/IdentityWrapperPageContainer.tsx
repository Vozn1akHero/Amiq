import React, {Component, useEffect, useState} from 'react';
import AuthService from "core/auth/auth-service";
import {Observable, take} from "rxjs";
import {AuthStore} from "store/auth/auth-store";
import {DtoCredentialsValidation} from "core/auth/dto-credentials-validation";
import {Routes} from "core/routing";
import IdentityWrapperPage from "./IdentityWrapperPage";
import { IdentityModel } from 'store/auth/identity-model';
import {useHistory, useLocation} from "react-router-dom";

const IdentityWrapperPageContainer = () => {
    let authService = new AuthService();

    let history = useHistory();
    const location = useLocation();

    let isAuthenticated$: Observable<boolean> = AuthStore.isAuthenticated$;

    //const [route, setRoute] = useState("");

    useEffect(() => history.listen((listener:any) => {
        console.log("ROUTE CHANGED")
        recheckIdentity(listener.pathname);
    }), [history]);

    useEffect(() => {
        recheckIdentity(location.pathname);
    }, []);

    const recheckIdentity = (pathname) => {
        if(pathname !== Routes.getRouteAsString(Routes.authPageRoutes)) {
            let res: DtoCredentialsValidation = authService.validateCredentials();
            const identityModel = new IdentityModel();
            identityModel.isAuthenticated = res.isAuthenticated;
            AuthStore.configureIdentity(identityModel);
            if (!res.isAuthenticated) {
                history.push(Routes.getRouteAsString(Routes.authPageRoutes));
            }
        }
    }

    return (
        <IdentityWrapperPage isAuthenticated$={isAuthenticated$} />
    )
}

export default IdentityWrapperPageContainer;