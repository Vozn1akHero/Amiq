import React, {FC, Component} from 'react';
import { Route, Redirect } from "react-router-dom";

interface Props {
    component: Component,
    auth: boolean
}

const GuardedRoute : FC<Props> = ({ component, auth, ...rest }) => (
    <Route {...rest} render={(props) => (
        auth
            ? <Component {...props} />
            : <Redirect to='/' />
    )} />
)

export default GuardedRoute;
