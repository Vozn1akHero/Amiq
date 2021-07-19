import {FC, Component, ComponentClass} from 'react';
import { Route, Redirect } from "react-router-dom";

interface Props {
    component: ComponentClass,
    authenticated: boolean,
    path: string
}

const GuardedRoute : FC<Props> = ({ component, authenticated, path, ...rest }) => (
    <Route {...rest} path={path} render={(props) => (
        authenticated
            ? <Component {...props} />
            : <Redirect to='/' />
    )} />
)

export default GuardedRoute;
