import React, {Component, ReactElement} from 'react';
import { Link } from 'react-router-dom';
import {INotification} from "../notification-models";
import "./notification.scss"
import moment from "moment";
import {Utils} from "../../../core/utils";

type Props = {
    notification: INotification;
}

type State = {

}

class Notification extends Component<Props, State> {
    render() {
        return (
            <Link to={this.props.notification.link} className="uk-link-text">
                <div className="notification uk-card uk-card-default uk-card-body uk-background-default">
                    <article className="uk-comment uk-visible-toggle" >
                        <header className="uk-comment-header uk-position-relative">
                            <div className="uk-grid uk-grid-medium uk-flex-middle >" >
                                <div className="uk-width-auto uk-flex-first">
                                    <img className="notification__image border-radius-50"
                                         src={Utils.getImageSrc(this.props.notification.imageSrc)}
                                         alt=""/>
                                </div>
                                <div className="uk-width-expand">
                                    <p className="uk-comment-meta uk-margin-remove-top">
                                        {moment(this.props.notification.createdAt).fromNow()}
                                    </p>
                                </div>
                            </div>
                        </header>
                        <div className="uk-comment-body">
                            <p>
                                <div dangerouslySetInnerHTML={{__html: `${this.props.notification.text}`}} />
                            </p>
                        </div>
                    </article>
                </div>
            </Link>
        );
    }
}

export default Notification;