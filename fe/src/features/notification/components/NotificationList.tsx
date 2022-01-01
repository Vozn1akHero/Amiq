import * as React from 'react';
import {RefObject, useEffect, useState} from 'react';
import {INotification} from "../notification-models";
import Notification from "./Notification";
import "./notification-list.scss"
import {NotificationService} from "../notification-service";
import {AxiosResponse} from "axios";
import {StatusCodes} from "http-status-codes";
import {IResponseListOf} from "../../../core/http-client/response-list-of";

type Props = {
    notifRef: RefObject<HTMLDivElement>;
};
type State = {
    loaded: boolean;
    notifications: Array<INotification>;
    page: number;
};

export const NotificationList = (props:Props) => {
    const notificationService:NotificationService = new NotificationService();
    const notificationsCount: number = 10;

    const [notificationsState, setNotificationsState] = useState<State>({
        loaded: false,
        notifications: [],
        page: 1
    });

    useEffect(() => {
        getNotifications();
    }, [])

    const getNotifications = () => {
        notificationService.getAll(notificationsState.page, notificationsCount).then((res:AxiosResponse) => {
            if(res.status === StatusCodes.OK){
                const {entities} = res.data as IResponseListOf<INotification>;
                setNotificationsState({
                    loaded: true,
                    notifications: [...notificationsState.notifications, ...entities],
                    page: notificationsState.page+1
                })
            }
        })
    }

    return (
        <div className="notification-list uk-card uk-card-default uk-card-body uk-width-1-2@m" ref={props.notifRef}>
            <legend className="uk-legend uk-margin-bottom">Powiadomienia</legend>
            <ul className="uk-list">
                {
                    notificationsState.notifications.map((value, index)  => {
                        return <li key={index} className={index > 0 ? `uk-margin-top` : ``}>
                            <Notification notification={value}  />
                        </li>
                    })
                }
                <button className="uk-button-link"
                        onClick={getNotifications}>Wyświetl więcej</button>
            </ul>
        </div>
    );
};