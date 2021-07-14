// @flow
import * as React from 'react';
import moment from 'moment'

type Props = {
    viewName: string;
    avatarSrc: string;
    text: string;
    hasMedia: boolean;
    date: Date;
};
export const ChatPreviewCard = (props: Props) => {
    const getViewDate = (date: Date) => moment(date).fromNow()

    return (
        <ul className="post uk-comment-list">
            <li>
                <article className="uk-comment uk-visible-toggle" >
                    <header className="uk-comment-header uk-position-relative">
                        <div className="uk-grid uk-grid-medium uk-flex-middle >" >
                            <div className="uk-width-auto uk-flex-first">
                                <img className="uk-comment-avatar" src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg" width="80"
                                     height="80" alt=""/>
                            </div>
                            <div className="uk-width-expand">
                                <h4 className="uk-comment-title uk-margin-remove"><a className="uk-link-reset"
                                                                                     href="#">{props.viewName}</a></h4>
                                <p className="uk-comment-meta uk-margin-remove-top"><a className="uk-link-reset"
                                                                                       href="#">{getViewDate(props.date)}</a>
                                </p>
                            </div>
                        </div>
                    </header>
                    <div className="uk-comment-body">
                        <p>{props.text}</p>
                    </div>
                </article>
            </li>
        </ul>
    );
};
