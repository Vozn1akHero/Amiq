import React, {Component} from 'react';
import {IChat, IInterlocutor, IMessage} from "./chat-models";
import {getViewDate} from "assets/utils/date-utils";

class Chat extends Component<{chat: IChat}, any> {
    static Message = class extends Component<{message: IMessage}, any> {
        render() {
            return (
                <div className="chat__message">
                    {
                        this.props.message.textContent
                    }
                </div>
            )
        }
    }

    render() {
        return (
            <div className="chat">
                <header className="selected-chat__interlocutor-data">
                    <div className="uk-grid uk-grid-medium uk-flex-middle >" >
                        <div className="uk-width-auto uk-flex-first">
                            <img className="border-radius-50" src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg" width="80"
                                 height="80" alt=""/>
                        </div>
                        <div className="uk-width-expand">
                            <h4 className="uk-comment-title uk-margin-remove"><a className="uk-link-reset"
                                                                                 href="#">{this.props.chat.interlocutor.viewName}</a></h4>
                            <p className="uk-comment-meta uk-margin-remove-top"><a className="uk-link-reset"
                                                                                   href="#">{getViewDate(this.props.chat.messages[0]?.date)}</a>
                            </p>
                        </div>
                    </div>
                </header>
                <div className="uk-grid uk-width-1-1">
                    {
                        this.props.chat.messages.map(value => {
                            return <div className="uk-margin-top">
                                <Chat.Message message={value} />
                            </div>
                        })
                    }
                </div>
            </div>
        );
    }
}

export default Chat;
