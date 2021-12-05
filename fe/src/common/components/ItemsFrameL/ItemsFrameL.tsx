import './items-frame-l.scss'
import {Link} from "react-router-dom";
import React, {useEffect} from "react";

type Props = {
    title: string;
    //items: {imageSrc: string, title: string}[];
    items?: Array<any>;
    icon?: string;
    displayHeaderAsLink?: boolean;
    link?: string;
    children?: React.ReactNode,
};
export const ItemsFrameL = (props: Props) => {
    /*useEffect(() => {
        console.log(props.children)
    }, [props.children])*/

    return (
        <div className="uk-card uk-card-default uk-card-body items-frame-l">
            <span className="uk-card-header uk-margin-small-left">
                {
                    props.icon && <span className="uk-margin-small-right" uk-icon={`icon: ${props.icon}`}></span>
                }
                {
                    props.displayHeaderAsLink ? <Link className="uk-link uk-link-muted" to={props.link}>{props.title}</Link> : props.title
                }
            </span>
            <div className="uk-margin-small-top">
                {
                    props.children
                }
            </div>
        </div>
    );
};
