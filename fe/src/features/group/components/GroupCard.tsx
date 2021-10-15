import "./GroupCard/group-card.scss"
import React, {memo} from "react";
import { Link } from "react-router-dom";
import {IGroupCard} from "../group-models";
import devConfig from "dev-config.json"
import SimpleDropdown from "../../../common/components/SimpleDropdown/SimpleDropdown";
import IDropdownOption from "../../../common/components/SimpleDropdown/IDropdownOption";

type GroupCardProps = {
    groupCard: IGroupCard;
    leaveGroup(groupId: number):void;
}

const GroupCard = (props: GroupCardProps) => {
    const avatarSrc = devConfig.monolithUrl + "/" + props.groupCard.avatarSrc;

    const avatarBgStyles : any = {
        backgroundImage: "url(" + avatarSrc + ")"
    }

    const handleShowMoreOptionsClick = (option: IDropdownOption) => {

    }

    const moreOptionsDropdownValues : Array<IDropdownOption> =  [
        {id: 1, text: "Ukryj"},
        {id: 2, text: "Zablokuj"},
    ];

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
                <span className="group-add-info">
                    <span uk-icon="icon:users" className="uk-margin-small-left"></span> {props.groupCard.participantsCount} uczestników
                </span>
                {
                    props.groupCard.subjects &&
                        <span className="group-add-info">{props.groupCard.subjects.join(", ")}</span>
                }

                {/*<span className="group-add-info">3 nowych uczestników</span>
                <span className="group-add-info">13 nowych wpisów</span>*/}

                <div className="controls uk-margin-medium-top uk-flex">
                    <button className="uk-button uk-button-secondary"
                            onClick={() => props.leaveGroup(props.groupCard.groupId)}>Wyjdź</button>
                    <div className="uk-margin-small-left">
                        <SimpleDropdown icon="more"
                                        options={moreOptionsDropdownValues}
                                        handleOptionClick={handleShowMoreOptionsClick} />
                    </div>
                </div>
            </div>
        </div>
    );
}

const MemoizedGroupCard = memo(GroupCard);

export default MemoizedGroupCard;
