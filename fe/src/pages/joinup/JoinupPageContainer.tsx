import React, {Component} from 'react';
import JoinupPage from './JoinupPage';
import {DtoRegister} from "core/auth/dto-register";
import AuthService from "../../core/auth/auth-service";

class JoinupPageContainer extends Component {
    authService: AuthService = new AuthService();

    register(dto: DtoRegister) {
        this.authService.register(dto).then(res => {

        })
    }

    render() {
        return (
            <JoinupPage register={this.register.bind} />
        );
    }
}

export default JoinupPageContainer;