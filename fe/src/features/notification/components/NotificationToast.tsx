import React, {Component} from 'react';
import ReactDOM from "react-dom";
import {INotification} from "../notification-models";
import Notification from "./Notification";

type Props = {
    notification: INotification
}
type State = {

}

class NotificationToast extends Component<Props, State> {
    container = document.getElementById("amiqApp");

    render() {
        return (
            ReactDOM.createPortal(
                <div className="notification-toast">
                    <Notification notification={this.props.notification} />
                </div>,
                this.container
            )
        )
    }
}

export default NotificationToast;
