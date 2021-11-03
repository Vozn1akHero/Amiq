import React, {useEffect, useState} from 'react';
import {Utils} from "../../../../core/utils";
import {GroupEventService} from "../../../../features/group/services/group-event-service";
import {IGroupEvent} from "../../../../features/group/models/group-event";
import {useParams} from "react-router-dom";
import "./group-event-subpage.scss"
import {Routes} from "../../../../core/routing";
import MainEntitySubpage from "../../../main-entity-subpage";
import moment from "moment";
import {GroupEventParticipantService} from "../../../../features/group/services/group-event-participant-service";
import {AxiosResponse} from "axios";
import {StatusCodes} from "http-status-codes";

const GroupEventSubpage = () => {
    const groupEventService = new GroupEventService();
    const groupEventParticipantService = new GroupEventParticipantService();

    const [event, setEvent] = useState<IGroupEvent>(null);
    const [isViewerEventParticipant, setIsViewerEventParticipant] = useState(null);
    const [eventLoaded, setEventLoaded] = useState(false);
    const {groupEventId, groupId} = useParams<any>();

    useEffect(() => {
        getEvent();
    }, []);

    const getEvent = () => {
        groupEventService.getGroupEventById(groupEventId).then(res => {
            const event = res.data as IGroupEvent;
            setEvent(event);
            setIsViewerEventParticipant(event.isRequestCreatorParticipant);
            setEventLoaded(true);
        })
    }

    const onJoinEventBtnClick = () => {
        groupEventParticipantService.join(groupEventId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.OK) {
                setIsViewerEventParticipant(true);
            }
        })
    }

    const onLeaveEventClick = () => {
        groupEventParticipantService.leave(groupEventId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.OK) {
                setIsViewerEventParticipant(false);
            }
        })
    }

    return (
        <div className="group-event-subpage">
            <MainEntitySubpage goBackLink={`/${Routes.groupPageRoutes.baseLink}/${groupId}`}>
                {
                    eventLoaded &&
                    <div className="group-event-subpage__event-card uk-card uk-card-default uk-margin-medium-top">
                        <div className="group-event-subpage__event-card__avatarBg-wrap">
                            <div className="group-event-subpage__event-card__avatarBg"
                                 style={{backgroundImage: "url(" + Utils.getImageSrc(event.avatarSrc) + ")"}}></div>

                            <div className="group-event-subpage__event-card__avatar-wrap">
                                <img className="group-event-subpage__event-card__avatar"
                                     src={Utils.getImageSrc(event.avatarSrc)}
                                     alt=""/>
                            </div>
                        </div>

                        <div className="group-event-subpage__event-card__body">
                            <h3 className="uk-card-title uk-margin-medium-left uk-margin-medium-top uk-text-bold">{event.name}</h3>
                            <div className="uk-margin-medium-left uk-margin-top">
                                <h5 className="uk-text-bold">
                                    Opis
                                </h5>
                                <p>{event.description}</p>
                            </div>
                            <div className="uk-margin-medium-left uk-margin-top">
                                <p>
                                    <span uk-icon="icon:users"></span> {event.groupEventParticipantsCount} osób weźmie
                                    udział
                                </p>
                                <p>
                                    <span
                                        uk-icon="icon:calendar"></span> {moment(event.date).format("MMMM d YYYY, hh:mm")}
                                </p>
                            </div>
                            <div className="group-event-subpage__event-card__controls uk-margin-right uk-margin-bottom">
                                {
                                    isViewerEventParticipant ?
                                        <button className="uk-button uk-button-secondary" onClick={onLeaveEventClick}>
                                            Anuluj udział
                                        </button>
                                        :
                                        <button className="uk-button uk-button-primary" onClick={onJoinEventBtnClick}>
                                            Weź udział
                                        </button>
                                }
                            </div>
                        </div>
                    </div>
                }
            </MainEntitySubpage>
        </div>
    );
};

export default GroupEventSubpage;
