import React, {Component} from 'react';
import {ChatPreviewCard} from "features/chat/ChatPreviewCard";
import {IChat, IChatPreview} from "features/chat/chat-models";
import Chat from "features/chat/Chat";
import "./chat-page.scss"

type Props = {
    chats: Array<IChatPreview>;
    selectedChat: IChat;
    onChatSelection(selectedChatId: string):void;
}

type State = {
}

class ChatPage extends Component<Props, State> {
    onChatSelection = (chatId: string) => {
        this.props.onChatSelection(chatId);
    }

    componentDidMount() {
        console.log(this.props.selectedChat)
    }

    render() {
        return (
            <div className="chat-page uk-flex">
                <div className={`chats ${this.props.selectedChat && `chat-list-width-when-selected`}`}>
                    <legend className="uk-legend uk-margin-medium-top">Moje wiadomości</legend>
                    <div className="input-search">
                        <div className="uk-margin-medium-top uk-margin-medium-bottom">
                            <input className="uk-input" type="text" placeholder="Szukaj"/>
                        </div>
                    </div>
                    <div className={`uk-grid ${this.props.selectedChat ? `uk-child-width-1-1` : `uk-child-width-1-3`}`}>
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
                </div>

                {
                    this.props.selectedChat && <div className="selected-chat uk-flex chat-width-when-selected">
                        <hr className="uk-divider-vertical max-height uk-margin-large-left uk-margin-large-right"/>
                        <Chat chat={this.props.selectedChat} />
                    </div>
                }

            </div>
        );
    }
}

export default ChatPage;
