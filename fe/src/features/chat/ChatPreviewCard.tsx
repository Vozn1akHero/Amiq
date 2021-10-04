import "./chat-preview-card.scss"
import moment from 'moment'
import {MouseEvent} from "react";
import { getViewDate } from "assets/utils/date-utils";
import {Utils} from "../../core/utils";

type Props = {
    chatId: string;
    //viewName: string;
    name: string;
    surname: string;
    avatarSrc: string;
    text: string;
    hasMedia: boolean;
    date: Date;
    onChatClick(chatId: string) : void;
};
export const ChatPreviewCard = (props: Props) => {
    return (
        <ul className="chat-preview-card uk-comment-list" onClick={() => props.onChatClick(props.chatId)}>
            <li>
                <article className="uk-comment uk-visible-toggle" >
                    <header className="uk-comment-header uk-position-relative">
                        <div className="uk-grid uk-grid-medium uk-flex-middle >" >
                            <div className="uk-width-auto uk-flex-first">
                                <img className="border-radius-50" src={Utils.getImageSrc(props.avatarSrc)} width="80"
                                     height="80" alt=""/>
                            </div>
                            <div className="uk-width-expand">
                                <h4 className="uk-comment-title uk-margin-remove"><a className="uk-link-reset"
                                                                                     href="#">{props.name + " " + props.surname}</a></h4>
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
