import React, {Component} from 'react';
import {ChatPreviewCard} from "features/chat/chat-preview-card";
import {IChat, IChatPreview} from "features/chat/chat-models";
import Chat from "features/chat/Chat";

type Props = {
    chats: Array<IChatPreview>;
    selectedChat: IChat;
    onChatSelection(selectedChatId: string, isChatSelected: boolean):void;
}

type State = {
}

class ChatPage extends Component<Props, State> {
    onChatSelection = (chatId: string) => {
        this.props.onChatSelection(chatId, true);
    }

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
                                    <ChatPreviewCard avatarSrc={value.author.avatarSrc.toString()}
                                                     viewName={value.author.viewName}
                                                     date={value.date}
                                                     text={value.textContent}
                                                     hasMedia={value.hasMedia}
                                                     chatId={value.chatId}
                                                     onChatClick={this.onChatSelection}/>
                                </div>
                            }
                        )
                    }
                </div>
                <div className="selected-chat">
                    <hr className="uk-divider-vertical"/>
                    {
                        this.props.selectedChat && <Chat chat={this.props.selectedChat} />
                    }
                </div>
            </div>
        );
    }
}

export default ChatPage;
