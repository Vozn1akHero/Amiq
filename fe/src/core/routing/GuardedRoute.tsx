import {FC, Component, ComponentClass} from 'react';
import { Route, Redirect } from "react-router-dom";
import {AuthStore} from "../../store/custom/auth/auth-store";

const GuardedRoute = ({ component: Component, ...rest }) => {
    const isAuthenticated : boolean = AuthStore.isAuthenticated;
    return (
        <Route
            {...rest}
            render={(props) =>
                isAuthenticated ? <Component {...props} />
                    : <Redirect to="/login" />
            }
        />
    );
}
/* */
export default GuardedRoute;
