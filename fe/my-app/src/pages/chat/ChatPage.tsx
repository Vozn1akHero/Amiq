import React, {Component} from 'react';
import {ChatPreviewCard} from "features/chat/chat-preview-card";

type Props = {
    chats: Array<any>
}

class ChatPage extends Component<Props, any> {
    render() {
        return (
            <div className="chat-page">
                <legend className="uk-legend uk-margin-medium-top">Moje wiadomości</legend>
                <div className="input-search">
                    <div className="uk-margin-medium-top uk-margin-medium-bottom">
                        <input className="uk-input" type="text" placeholder="Szukaj"/>
                    </div>
                </div>
                <div className="uk-grid uk-child-width-1-3">
                    {
                        this.props.chats.map((value, i) =>
                            {
                                return <div key={i} className="uk-margin-top">
                                    <ChatPreviewCard avatarSrc={value.avatarSrc} viewName={value.viewName} date={value.date} text={value.textMessage} hasMedia={value.hasMedia} />
                                </div>
                            }
                        )
                    }
                </div>
            </div>
        );
    }
}

export default ChatPage;
