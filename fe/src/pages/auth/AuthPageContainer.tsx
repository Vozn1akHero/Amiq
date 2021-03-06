import React, {Component} from 'react';
import AuthPage from "./AuthPage";
import AuthService from "../../core/auth/auth-service";
import {StatusCodes} from "http-status-codes";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {IdentityModel} from "../../store/custom/auth/identity-model";
import {useHistory, withRouter} from 'react-router-dom';

class AuthPageContainer extends Component<any,any> {
    authService: AuthService = new AuthService();

    /*authenticate = (login:string, password: string) => {
        this.authService.authenticate(login, password).then(res => {
            if(res.status === StatusCodes.OK){
                let identityModel = new IdentityModel();
                identityModel.isAuthenticated = true;
                AuthStore.configureIdentity(identityModel);
                this.props.history.push('/profile')
            }
        })
    }*/
    authenticate = (login:string, password: string) => {
        AuthStore.authenticate(login, password).then((result:boolean)=>{
            if(result){
                this.props.history.push('/profile')
            } else {
                alert("Nie udało się zalogować");
            }
        })
    }

    render() {
        return (
            <AuthPage authenticate={this.authenticate} />
        );
    }
}

export default withRouter(AuthPageContainer);