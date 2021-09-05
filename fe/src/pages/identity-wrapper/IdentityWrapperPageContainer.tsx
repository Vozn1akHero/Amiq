import React, {Component, useEffect, useState} from 'react';
import AuthService from "core/auth/auth-service";
import {Observable, take} from "rxjs";
import {AuthStore} from "store/auth/auth-store";
import {Routes} from "core/routing";
import IdentityWrapperPage from "./IdentityWrapperPage";
import { IdentityModel } from 'store/auth/identity-model';
import {useHistory, useLocation} from "react-router-dom";
import {AxiosResponse} from "axios";
import {StatusCodes} from "http-status-codes";

const IdentityWrapperPageContainer = () => {
    let authService = new AuthService();

    let history = useHistory();
    const location = useLocation();

    let isAuthenticated$: Observable<boolean> = AuthStore.isAuthenticated$;

    //const [route, setRoute] = useState("");

    useEffect(() => history.listen( (listener:any) => {
         recheckIdentity(listener.pathname);
    }), [history]);

    useEffect( () => {
        recheckIdentity(location.pathname)
    }, []);

    const recheckIdentity = (pathname) => {
        authService.validateCredentials().then((res:AxiosResponse) => {
            const identityModel = new IdentityModel();
            identityModel.isAuthenticated = res.status === StatusCodes.OK;
            AuthStore.configureIdentity(identityModel);
            if (!identityModel.isAuthenticated) {
                if(pathname !== Routes.getLink(Routes.authPageRoutes)
                    && pathname !== Routes.getLink(Routes.registrationPageRoutes)) {
                    history.push(Routes.getLink(Routes.authPageRoutes));
                }
            } else {
                if(pathname === Routes.getLink(Routes.authPageRoutes)
                    || pathname === Routes.getLink(Routes.registrationPageRoutes)){
                    history.push(Routes.getLink(Routes.profilePageRoutes));
                }
            }
        }).catch(() => {
            if(pathname !== Routes.getLink(Routes.authPageRoutes)
                && pathname !== Routes.getLink(Routes.registrationPageRoutes)) {
                history.push(Routes.getLink(Routes.authPageRoutes));
            }
        })
    }

    return (
        <IdentityWrapperPage isAuthenticated$={isAuthenticated$} />
    )
}

export default IdentityWrapperPageContainer;