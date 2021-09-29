import "./group-card.scss"
import {memo} from "react";
import { Link } from "react-router-dom";
import {IGroupCard} from "../group-models";

type GroupCardProps = {
    groupCard: IGroupCard;
    leaveGroup(groupId: number):void;
}

const GroupCard = (props: GroupCardProps) => {
    const avatarBgStyles : any = {
        backgroundImage: "url(" + props.groupCard.avatarSrc + ")"
    }

    return (
        <div className="uk-card uk-card-default uk-card-body group-card">
            <div className="avatar-bg" style={avatarBgStyles}></div>

            <img
                 className="avatar"
                 src={props.groupCard.avatarSrc}
                 sizes="(min-width: 120px) 120px, 100vw" width="120" height="120" alt="" />

            <div className="group-info-wrapper uk-padding">
                <Link to={"/group/" + props.groupCard.groupId}>
                    <h3 className="uk-card-title name">{props.groupCard.name}</h3>
                </Link>
                <span className="group-add-info">{props.groupCard.participants.length} uczestników</span>
                {
                    props.groupCard.subjects &&
                        <span className="group-add-info">{props.groupCard.subjects.join(", ")}</span>
                }

                {/*<span className="group-add-info">3 nowych uczestników</span>
                <span className="group-add-info">13 nowych wpisów</span>*/}

                <div className="controls uk-margin-medium-top">
                    <button className="uk-button uk-button-secondary"
                            onClick={() => props.leaveGroup(props.groupCard.groupId)}>Wyjdź</button>
                    <div className="uk-inline uk-margin-small-left">
                        <button className="uk-button uk-button-default" type="button">więcej</button>
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
        </div>
    );
}

const MemoizedGroupCard = memo(GroupCard);

export default MemoizedGroupCard;
