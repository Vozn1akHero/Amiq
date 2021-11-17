import "./group-card.scss"
import React, {memo, useEffect, useState} from "react";
import { Link } from "react-router-dom";
import {IGroupCard, IGroupUserParams} from "../../models/group-models";
import devConfig from "../../../../dev-config.json"
import SimpleDropdown from "common/components/SimpleDropdown/SimpleDropdown";
import IDropdownOption from "../../../../common/components/SimpleDropdown/IDropdownOption";
import {GroupService} from "../../services/group-service";

type GroupCardProps = {
    groupCard: IGroupCard;
    toggleGroupVisibility(groupId: number, isVisible: boolean):void;
    leaveGroup(groupId: number):void;
    joinGroup(groupId: number):void;
}

const GroupCard = (props: GroupCardProps) => {
    const groupService : GroupService = new GroupService();
    const [userParamsLoaded, setUserParamsLoaded] = useState(false);
    const [dropdownOptions, setDropdownOptions] = useState<Array<IDropdownOption>>([]);
    const avatarSrc = devConfig.monolithUrl + "/" + props.groupCard.avatarSrc;

    const avatarBgStyles : any = {
        backgroundImage: "url(" + avatarSrc + ")"
    }

    const handleShowMoreOptionsClick = (option: IDropdownOption) => {
    }

    const getMoreUserParamsDropdownValues = () => {
        if(!userParamsLoaded)
            groupService.getUserParams(props.groupCard.groupId).then(res => {
                const data = res.data as IGroupUserParams;
                const dropdownValues : Array<IDropdownOption> = [];
                const dropdownOption : IDropdownOption = {
                    id: 1,
                    text: data.isHidden ? "Pokaż" : "Ukryj",
                    event: () => {
                        if(props.groupCard.isHidden)
                            props.toggleGroupVisibility(props.groupCard.groupId, true);
                        else props.toggleGroupVisibility(props.groupCard.groupId, false);
                    }
                }
                dropdownValues.push(dropdownOption);
                setDropdownOptions(dropdownValues);
            }).finally(() => {
                setUserParamsLoaded(true);
            })
    }

    /*const moreOptionsDropdownValues : Array<IDropdownOption> =  [
        {
            id: 1,
            text: props.groupCard.isHidden ? "Pokaż" : "Ukryj",
            event: () => {
                if(props.groupCard.isHidden)
                    props.toggleGroupVisibility(props.groupCard.groupId, true);
                else props.toggleGroupVisibility(props.groupCard.groupId, false);
            }
        },
    ];*/

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

                <div className="controls uk-margin-medium-top uk-flex">
                    {
                        props.groupCard.isRequestCreatorParticipant ? <button className="uk-button uk-button-secondary"
                                                                               onClick={() => props.leaveGroup(props.groupCard.groupId)}>Wyjdź</button>
                            :
                            <button className="uk-button uk-button-secondary"
                                    onClick={() => props.joinGroup(props.groupCard.groupId)}>Dołącz</button>
                    }
                    <div className="uk-margin-small-left">
                        <SimpleDropdown icon="more"
                                        isStatic={false}
                                        onDropdownMouseOver={getMoreUserParamsDropdownValues}
                                        options={dropdownOptions}
                                        areOptionsLoaded={userParamsLoaded}
                                        handleOptionClick={handleShowMoreOptionsClick} />
                    </div>
                </div>
            </div>
        </div>
    );
}

const MemoizedGroupCard = memo(GroupCard);

export default MemoizedGroupCard;
