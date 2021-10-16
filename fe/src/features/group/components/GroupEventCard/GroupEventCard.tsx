import React from 'react';
import {IGroupEvent} from "../../models/group-event";
import devConfig from "dev-config.json";
import IDropdownOption from "common/components/SimpleDropdown/IDropdownOption";
import SimpleDropdown from "common/components/SimpleDropdown/SimpleDropdown";

type Props = {
    groupEventData: IGroupEvent;
    onCancelClick(groupEventId: string):void;
    onReopenEventClick(groupEventId: string):void;
    onHideClick(groupEventId: string):void;
    onMakeVisibleClick(groupEventId: string):void;
}

const GroupEventCard = (props:Props) => {
    const avatarSrc = devConfig.monolithUrl + "/" + props.groupEventData.avatarSrc;

    const avatarBgStyles : any = {
        backgroundImage: "url(" + avatarSrc + ")"
    }

    const handleShowMoreOptionsClick = (option: IDropdownOption) => {
    }

    const onToggleEventStatusClick = () => {
    }

    const onCancelClick = () => {
        props.onCancelClick(props.groupEventData.groupEventId);
    }

    const onReopenEventClick = () => {
        props.onReopenEventClick(props.groupEventData.groupEventId);
    }

    const moreOptionsDropdownValues : Array<IDropdownOption> =  [
        {
            id: 1,
            text: props.groupEventData.isHidden ? "Pokaż" : "Ukryj",
            event: () => {
                if(props.groupEventData.isHidden)
                    props.onMakeVisibleClick(props.groupEventData.groupEventId);
                else props.onHideClick(props.groupEventData.groupEventId);
            }
        },
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
                <h3 className="uk-card-title name">{props.groupEventData.name}</h3>
                <span className="group-add-info">
                    <span uk-icon="icon:users" className="uk-margin-small-left"></span> {props.groupEventData.participantsCount} weźmie udział
                </span>

                <div className="controls uk-margin-medium-top uk-flex">
                    <button className={`uk-button ${props.groupEventData.isCancelled ? `uk-button-primary` : `uk-button-secondary`}`}
                            onClick={() => {
                                if(props.groupEventData.isCancelled)
                                    onReopenEventClick();
                                else
                                    onCancelClick();
                            }}>{props.groupEventData.isCancelled ? <span>Aktywuj</span> : <span>Anuluj</span>}</button>
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

export default GroupEventCard;