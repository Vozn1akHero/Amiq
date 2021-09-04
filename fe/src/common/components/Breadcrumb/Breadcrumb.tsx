import React, {useEffect, useState} from 'react';
import { useHistory } from 'react-router-dom';
import {AuthStore} from "../../../store/auth/auth-store";

type Props = {

}

export const Breadcrumb = (props: Props) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(()=>{
        AuthStore.isAuthenticated$.subscribe(value => {
            setIsAuthenticated(value);
        });
    }, [])

    return (
        <>
            {
                isAuthenticated && <div className="breadcrumb-wrapper">
                    <ul className="uk-breadcrumb">
                        <li><a href="#">Home</a></li>
                        <li><a href="#">Linked Category</a></li>
                        <li className="uk-disabled"><a>Disabled Category</a></li>
                    </ul>
                </div>
            }
        </>
    );

}

export default Breadcrumb;
