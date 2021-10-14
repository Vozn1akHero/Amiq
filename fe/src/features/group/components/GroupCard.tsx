import "./group-card.scss"
import React, {memo} from "react";
import { Link } from "react-router-dom";
import {IGroupCard} from "../group-models";
import devConfig from "dev-config.json"

type GroupCardProps = {
    groupCard: IGroupCard;
    leaveGroup(groupId: number):void;
}

const GroupCard = (props: GroupCardProps) => {
    const avatarSrc = devConfig.monolithUrl + "/" + props.groupCard.avatarSrc;

    const avatarBgStyles : any = {
        backgroundImage: "url(" + avatarSrc + ")"
    }

    return (
        <div className="uk-card uk-card-default uk-card-body group-card">
            <div className="avatar-bg" style={avatarBgStyles}></div>

            <div className="avatar-wrap">
                <img
                    className="avatar-img"
                    src={avatarSrc}
                    sizes="(min-width: 120px) 120px, 100vw" width="120" height="120" alt="" />
            </div>

            <div className="group-info-wrapper uk-padding">
                <Link to={"/group/" + props.groupCard.groupId}>
                    <h3 className="uk-card-title name">{props.groupCard.name}</h3>
                </Link>
                <span className="group-add-info">{props.groupCard.participantsCount} uczestników</span>
                {
                    props.groupCard.subjects &&
                        <span className="group-add-info">{props.groupCard.subjects.join(", ")}</span>
                }

                {/*<span className="group-add-info">3 nowych uczestników</span>
                <span className="group-add-info">13 nowych wpisów</span>*/}

                <div className="controls uk-margin-medium-top">
                    <button className="uk-button uk-button-secondary"
                            onClick={() => props.leaveGroup(props.groupCard.groupId)}>Wyjdź</button>
                    <button className="uk-button uk-button-default uk-margin-small-left">
                        <span uk-icon="icon: more"></span>
                    </button>
                    {/*<div uk-dropdown>*/}
                    {/*    <ul className="uk-nav uk-dropdown-nav">*/}
                    {/*        <li><a href="#">Dodaj do ulubionych</a></li>*/}
                    {/*        <li className="uk-nav-header">Inne</li>*/}
                    {/*        <li><a href="#">Zablokuj</a></li>*/}
                    {/*        <li><a href="#">Zgłoś</a></li>*/}
                    {/*    </ul>*/}
                    {/*</div>*/}
                </div>
            </div>
        </div>
    );
}

const MemoizedGroupCard = memo(GroupCard);

export default MemoizedGroupCard;
