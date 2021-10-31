import React, {Component} from 'react';
import { Link } from 'react-router-dom';
import {INotification} from "../notification-models";
import "./notification.scss"
import moment from "moment";

type Props = {
    notification: INotification;
}

type State = {

}

class Notification extends Component<Props, State> {
    render() {
        return (
            <div className="notification uk-margin-small-top uk-margin-small-bottom">
                <article className="uk-comment uk-visible-toggle" >
                    <header className="uk-comment-header uk-position-relative">
                        <div className="uk-grid uk-grid-medium uk-flex-middle >" >
                            <div className="uk-width-auto uk-flex-first">
                                <Link to={this.props.notification.link ? this.props.notification.link : ""}>
                                    <img className="notification__image border-radius-50" src={this.props.notification.photoSrc}
                                         alt=""/>
                                </Link>
                            </div>
                            <div className="uk-width-expand">
                                <p className="uk-comment-meta uk-margin-remove-top">
                                    <a className="uk-link-reset" href="#">{moment(this.props.notification.createdAt).fromNow()}</a>
                                </p>
                            </div>
                        </div>
                    </header>
                    <div className="uk-comment-body">
                        <p>
                            {this.props.notification.text}
                        </p>
                    </div>
                </article>
            </div>
        );
    }
}

export default Notification;