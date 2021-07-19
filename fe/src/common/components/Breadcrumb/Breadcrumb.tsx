import React from 'react';
import { useHistory } from 'react-router-dom';

type Props = {

}

export const Breadcrumb = (props: Props) => {
    const history = useHistory();


    return (
        <div className="breadcrumb-wrapper">
            <ul className="uk-breadcrumb">
                <li><a href="#">Home</a></li>
                <li><a href="#">Linked Category</a></li>
                <li className="uk-disabled"><a>Disabled Category</a></li>
            </ul>
        </div>
    );

}

export default Breadcrumb;
