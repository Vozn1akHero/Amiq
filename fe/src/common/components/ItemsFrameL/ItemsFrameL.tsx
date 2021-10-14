import './items-frame-l.scss'
import {IUserInFrame} from "./IUserInFrame";
import {Utils} from "core/utils";
import {Link} from "react-router-dom";
import React from "react";

type Props = {
    title: string;
    //items: {imageSrc: string, title: string}[];
    items: Array<IUserInFrame>;
    callbackText: string;
    icon?: string;
};
export const ItemsFrameL = (props: Props) => {
    return (
        <div className="uk-card uk-card-default uk-card-body items-frame-l">
            <span className="uk-card-header uk-margin-small-left">
                {
                    props.icon && <span className="uk-margin-small-right" uk-icon={`icon: ${props.icon}`}></span>
                }
                {props.title}
            </span>
            <div className="uk-margin-small-top">
                {
                    props.items.length > 0 ? <div className="items-wrapper">
                        {
                            props.items.map(value => <Link to={value.link}>
                                <div className="frame-item">
                                    <img className="avatar border-radius-50"
                                         src={Utils.getImageSrc(value.imagePath)} />
                                    {/* <span>{value.viewName}</span>*/}
                                </div>
                            </Link>)
                        }
                        </div>
                        : <span>{props.callbackText}</span>
                }
            </div>
        </div>
    );
};
