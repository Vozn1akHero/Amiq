import React, {Component} from 'react';
import {ChatPreviewCard} from "features/chat/ChatPreviewCard";
import {IChat, IChatPreview, IMessage} from "features/chat/chat-models";
import Chat from "features/chat/Chat";
import "./chat-page.scss"
import {ChatPreviewMode} from "../../features/chat/chat-enums";

type Props = {
    chats: Array<IChatPreview>;
    selectedChat: IChat;
    chatPreviewsLoaded: boolean;
    chatMessagesLoaded: boolean;
    onChatSelection(selectedChatId: string):void;
    onCreateMessage(message: Partial<IMessage>): void;
}

type State = {
    chatPreviewMode: ChatPreviewMode
}

class ChatPage extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            chatPreviewMode: ChatPreviewMode.InterlocutorDataAndMessage
        }
    }

    shouldComponentUpdate(nextProps: Readonly<Props>, nextState: Readonly<State>, nextContext: any): boolean {
        console.log(nextProps)
        return true;
    }

    onChatSelection = (chatId: string) => {
        this.props.onChatSelection(chatId);
    }

    componentDidMount() {
        console.log(this.props.selectedChat)
    }

    togglePreviewMode = (chatPreviewMode: ChatPreviewMode) => {
        this.setState( {
            chatPreviewMode
        })
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
                    <div className="controls">
                        <button onClick={() => this.togglePreviewMode(ChatPreviewMode.InterlocutorDataAndMessage)}
                            className="uk-button-small uk-button controls__preview-mode--data-message uk-margin-small-right">X</button>
                        <button onClick={() => this.togglePreviewMode(ChatPreviewMode.InterlocutorDataOnly)}
                            className="uk-button-small uk-button controls__preview-mode-btn controls__preview-mode--data">Y</button>
                    </div>
                    <div className={`uk-grid ${this.props.selectedChat ? `uk-child-width-1-1` : `uk-child-width-1-3`}`}>
                        {
                            this.props.chatPreviewsLoaded && this.props.chats.map((value, i) =>
                                {
                                    return <div key={i} className="uk-margin-top">
                                        <ChatPreviewCard avatarSrc={value.interlocutor.avatarPath}
                                                         name={value.interlocutor.name}
                                                         surname={value.interlocutor.surname}
                                                         chatPreviewMode={this.state.chatPreviewMode}
                                                         date={value.date}
                                                         text={value.textContent}
                                                         hasMedia={value.hasMedia}
                                                         writtenByIssuer={value.writtenByIssuer}
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
                        <Chat chat={this.props.selectedChat}
                              onCreateMessage={this.props.onCreateMessage}
                              chatMessagesLoaded={this.props.chatMessagesLoaded} />
                    </div>
                }
            </div>
        );
    }
}

export default ChatPage;
