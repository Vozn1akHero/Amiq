import "./chat-preview-card.scss"
import {getViewDate} from "assets/utils/date-utils";
import {Utils} from "../../core/utils";
import {ChatPreviewMode} from "./chat-enums";

type Props = {
    chatId: string;
    //viewName: string;
    chatPreviewMode: ChatPreviewMode
    name: string;
    surname: string;
    avatarSrc: string;
    text: string;
    hasMedia: boolean;
    writtenByIssuer: boolean;
    date: Date;
    onChatClick(chatId: string) : void;
};
export const ChatPreviewCard = (props: Props) => {
    return (
        <ul className="chat-preview-card uk-comment-list" onClick={() => props.onChatClick(props.chatId)}>
            <li>
                <div className="uk-comment uk-visible-toggle" >
                    {
                        (props.chatPreviewMode === ChatPreviewMode.InterlocutorDataAndMessage ||
                            props.chatPreviewMode === ChatPreviewMode.InterlocutorDataOnly) && <header className="uk-comment-header uk-position-relative">
                            <div className="uk-grid uk-grid-medium uk-flex-middle >" >
                                <div className="uk-width-auto uk-flex-first">
                                    <img className="border-radius-50" src={Utils.getImageSrc(props.avatarSrc)} width="80"
                                         height="80" alt=""/>
                                </div>
                                <div className="uk-width-expand">
                                    <h4 className="uk-comment-title uk-margin-remove">{props.name + " " + props.surname}</h4>
                                    <p className="uk-comment-meta uk-margin-remove-top">{getViewDate(props.date)}
                                    </p>
                                </div>
                            </div>
                        </header>
                    }
                    {
                        props.chatPreviewMode === ChatPreviewMode.InterlocutorDataAndMessage && <div className="uk-comment-body uk-flex">
                            <p>{props.writtenByIssuer && <span>Ty: </span>} {props.text}</p>
                        </div>
                    }
                </div>
            </li>
        </ul>
    );
};
