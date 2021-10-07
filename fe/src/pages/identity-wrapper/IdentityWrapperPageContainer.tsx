import React, {Component, useEffect, useState} from 'react';
import AuthService from "core/auth/auth-service";
import {Observable, take} from "rxjs";
import {AuthStore} from "store/custom/auth/auth-store";
import {Routes} from "core/routing";
import IdentityWrapperPage from "./IdentityWrapperPage";
import { IdentityModel } from 'store/custom/auth/identity-model';
import {useHistory, useLocation} from "react-router-dom";
import {AxiosResponse} from "axios";
import {StatusCodes} from "http-status-codes";

const IdentityWrapperPageContainer = () => {
    let authService = new AuthService();

    let history = useHistory();
    const location = useLocation();

    let isAuthenticated$: Observable<boolean> = AuthStore.isAuthenticated$;

    const [authLoaded, setAuthLoaded] = useState(false);

    //const [route, setRoute] = useState("");

    useEffect(() => history.listen( (listener:any) => {
         recheckIdentity(listener.pathname);
    }), [history]);

    useEffect( () => {
        recheckIdentity(location.pathname)
    }, []);

    const recheckIdentity = (pathname) => {
        const identityModel = new IdentityModel();
        authService.validateCredentials().then((res:AxiosResponse) => {
            identityModel.userId = res.data.userId;
            identityModel.name = res.data.userName;
            identityModel.surname = res.data.userSurname;
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
        }).catch((error) => {
            if(error.response)
            {
                if(error.response.status === StatusCodes.UNAUTHORIZED){
                    identityModel.isAuthenticated = false;
                    AuthStore.configureIdentity(identityModel);
                    if(pathname !== Routes.getLink(Routes.authPageRoutes)
                        && pathname !== Routes.getLink(Routes.registrationPageRoutes)) {
                        history.push(Routes.getLink(Routes.authPageRoutes));
                    }
                }
                else {
                    alert("AUTH SERVICE IS NOT AVAILABLE");
                    document.location.reload();
                }
            }
        }).finally(() => {
            setAuthLoaded(true);
        })
    }

    return (
        authLoaded && <IdentityWrapperPage isAuthenticated$={isAuthenticated$} />
    )
}

export default IdentityWrapperPageContainer;