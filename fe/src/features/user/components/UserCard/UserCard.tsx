import {Utils} from "../../../../core/utils";
import React, {useEffect} from "react";
import { Link } from "react-router-dom";
import "./user-card.scss"
import {IUserCardControl} from "./IUserCardControl";

type Props = {
    userId: number;
    name: string;
    surname: string;
    avatarPath: string;
    controls: Array<IUserCardControl>;
}

const UserCard = (props: Props) => {
    const avatarBgStyles = {
        backgroundImage: "url(" + Utils.getImageSrc(props.avatarPath) + ")"
    }

    /*const handleControlClick = (e, ) => {
        e.preventDefault();
        value.event();
    }*/
    useEffect(()=>{
        console.log(props.controls)
    }, [])

    return (
        <div className="user-card uk-card uk-card-default uk-card-body" style={{zIndex: 1, overflow: "hidden"}}>
            <div className="user-card__avatar-wrap">
                <div className="user-card__avatar-bg" style={avatarBgStyles}></div>

                <Link to={`/profile/${props.userId}`}>
                    <img style={{borderRadius: '50%', border: "3px solid white", marginBottom: "3rem"}}
                         src={Utils.getImageSrc(props.avatarPath)}
                         sizes="(min-width: 120px) 120px, 100vw" width="120" height="120" alt="" />
                </Link>
            </div>

            <div className="user-card__name-wrap">
                <Link to={`/profile/${props.userId}`}>
                    <h3 className="uk-card-title user-card__user-name">
                        {props.name} {props.surname}
                    </h3>
                </Link>
                <div className="user-card__controls">
                    {
                        props.controls.map((value, index) =>
                            <a key={index}
                               uk-tooltip={value.tooltip}
                               onClick={e => {
                                   e.preventDefault();
                                   value.event(props.userId);
                               }}
                               uk-icon={value.icon}
                               className="uk-icon-link uk-margin-small-left" />
                        )
                    }
                </div>
            </div>
        </div>
    );
}

export default UserCard;
