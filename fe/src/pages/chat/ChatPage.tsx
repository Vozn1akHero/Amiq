import React, {Component} from 'react';
import {ChatPreviewCard} from "features/chat/ChatPreviewCard";
import {IChat, IChatPreview, IMessage} from "features/chat/chat-models";
import Chat from "features/chat/Chat";
import "./chat-page.scss"
import {ChatPreviewMode} from "../../features/chat/chat-enums";
import SearchInput from "../../common/components/SearchInput/SearchInput";
import {IPaginatedStoreData} from "../../store/redux/base/paginated-store-data";

type Props = {
    chats: Array<IChatPreview>;
    messages: IPaginatedStoreData<IMessage>;
    selectedChat: IChat;
    chatPreviewsLoaded: boolean;
    searchInputLoading: boolean;
    onSearchInputChange(text:string):void;
    onChatSelection(selectedChatId: string):void;
    onCreateMessage(message: Partial<IMessage>): void;
    onDeleteMessages(ids: Array<string>);
    removeMessageById(messageId: string, chatId: string): void;
    getMoreMessages():void;
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

    onDeleteMessages = (ids: Array<string>) => {
        this.props.onDeleteMessages(ids);
    }

    removeMessageById = (messageId: string, chatId: string) => {
        this.props.removeMessageById(messageId, chatId);
    }

    render() {
        return (
            <div className={`chat-page ${this.props.selectedChat && `uk-flex`}`}>
                <div className={`chats ${this.props.selectedChat && `chat-list-width-when-selected`}`}>
                    <legend className="uk-legend uk-margin-medium-top">Moje wiadomości</legend>
                    <div className="input-search">
                        <div className="uk-margin-medium-top uk-margin-medium-bottom">
                            <SearchInput debounceTime={600}
                                         showSpinner={this.props.searchInputLoading}
                                         onDebounceInputChange={this.props.onSearchInputChange} />
                        </div>
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
                                        {
                                            this.props.selectedChat && <hr className="max-width"/>
                                        }
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
                              onDeleteMessages={this.props.onDeleteMessages}
                              messages={this.props.messages}
                              onCreateMessage={this.props.onCreateMessage}
                              removeMessageById={this.props.removeMessageById}
                              getMoreMessages={this.props.getMoreMessages} />
                    </div>
                }
            </div>
        );
    }
}

export default ChatPage;
