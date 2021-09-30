import React, {Component} from 'react';

type Props = {
    link: string;
    title: string;
    text: string;
}
type State = {

}

class NotificationPopup extends Component<State, Props> {
    render() {
        return (
            <div className="notification-popup">

            </div>
        );
    }
}

export default NotificationPopup;
