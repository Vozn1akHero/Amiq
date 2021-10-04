import React, {Component} from 'react';
import {IChat, IMessage} from "./chat-models";
import {getViewDate} from "assets/utils/date-utils";
import "./chat.scss"
import {ChatPreviewMode} from "./chat-enums";
import ChatMessage from "./ChatMessage";
import {AuthStore} from "../../store/auth/auth-store";

type State = {

}

type Props = {
    chat: IChat;
    chatMessagesLoaded: boolean;
}

class Chat extends Component<Props, State> {
    //userId = "1234"

    render() {
        return (
            <div className="chat">
                <header className="chat__interlocutor-data">
                    <div className="uk-grid uk-grid-medium" >
                        <div className="uk-width-auto uk-flex-first">
                            <img className="border-radius-50" src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg" width="80"
                                 height="80" alt=""/>
                        </div>
                        <div className="uk-width-expand">
                            <h4 className="uk-comment-title uk-margin-remove"><a className="uk-link-reset"
                                                                                 href="#">{this.props.chat.interlocutor.name + " " + this.props.chat.interlocutor.surname}</a></h4>
                            <p className="uk-comment-meta uk-margin-remove-top"><a className="uk-link-reset"
                                                                                   href="#">{getViewDate(this.props.chat.messages[0]?.date)}</a>
                            </p>
                        </div>
                    </div>
                </header>
                <hr className="max-width uk-margin-small-left"/>
                <div className="uk-grid uk-width-1-1">
                    {
                        this.props.chat.messages.map((value,i) => {
                            return <div key={i} className="uk-margin-top chat__message-wrapper">
                                <ChatMessage message={value}
                                              previousMessage={i > 0 ? this.props.chat.messages[i-1] : null}
                                              viewerId={AuthStore.identity.userId} />
                            </div>
                        })
                    }
                </div>
            </div>
        );
    }
}

export default Chat;
