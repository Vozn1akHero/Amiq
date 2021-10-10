import * as React from 'react';
import {INotification} from "../notification-models";
import Notification from "./Notification";
import "./notification-list.scss"

type Props = {

};
type State = {

};

export class NotificationList extends React.Component<Props, State> {
    mock : Array<INotification> = [{
        notificationId: "",
        text: "tdsad",
        createdAt: new Date(),
        photoSrc: "https://www.cgi.com/sites/default/files/styles/hero_banner/public/space_astronaut.jpg?itok=k2oFRHrr",
        link: null
    }, {
        notificationId: "",
        text: "hgfhgfh",
        createdAt: new Date(),
        photoSrc: "https://www.cgi.com/sites/default/files/styles/hero_banner/public/space_astronaut.jpg?itok=k2oFRHrr",
        link: null
    }, {
        notificationId: "",
        text: "mnbnmbnmbnm",
        createdAt: new Date(),
        photoSrc: "https://www.cgi.com/sites/default/files/styles/hero_banner/public/space_astronaut.jpg?itok=k2oFRHrr",
        link: null
    }
    ]

    render() {
        return (
            <div className="notification-list uk-card uk-card-default uk-card-body uk-width-1-2@m">
                <ul className="uk-list uk-list-divider">
                    {
                        this.mock.map((value, index) => {
                            return <li>
                                <Notification notification={value} key={index} />
                            </li>
                        })
                    }
                </ul>
            </div>
        );
    };
};