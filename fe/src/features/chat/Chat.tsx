import React, {Component} from 'react';
import {IChat, IChatMessageCreation, IMessage} from "./chat-models";
import "./chat.scss"
import {ChatPreviewMode} from "./chat-enums";
import ChatMessage from "./ChatMessage";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {Utils} from "../../core/utils";
import MessageCreationForm from "./components/MessageCreationForm";
import { Link } from 'react-router-dom';
import {Routes} from "../../core/routing";
import {DateUtils} from "../../assets/utils/date-utils";

type State = {
    parsedChat: Array<Array<IMessage>>;
    selectedMessageIds: Array<string>;
}

type Props = {
    chat: IChat;
    messages: Array<IMessage>;
    chatMessagesLoaded: boolean;
    onDeleteMessages(ids: Array<string>);
    onCreateMessage(message: IChatMessageCreation): void;
    removeMessageById(messageId: string, chatId: string):void;
}

class Chat extends Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            parsedChat: [],
            selectedMessageIds: []
        }
    }

    getGroupedMessages = () => {
        let parsedChat: Array<Array<IMessage>> = []
        let previousMessage: IMessage = null;
        let localIndex: number = -1;
        const SECONDS_TO_BE_GROUPED = 30;
        let currentGroup: Array<IMessage> = [];
        const messages = this.props.messages;
        console.log(messages)
        for(let message of messages){
            /*localIndex++;
            if(localIndex === 0) {
                currentGroup.push(message);
                previousMessage = message;
            } else if(localIndex >= 1) {
                if(message.author.userId === previousMessage.author.userId){
                    if(Utils.getDifferenceBetweenDates(message.createdAt, previousMessage.createdAt) < SECONDS_TO_BE_GROUPED){
                        currentGroup.push(message);
                        previousMessage = message;
                    } else {
                        parsedChat.push(currentGroup);
                        currentGroup = [];
                        currentGroup.push(message)
                        localIndex = -1;
                        previousMessage = message;
                    }
                } else {
                    parsedChat.push(currentGroup);
                    currentGroup = [];
                    currentGroup.push(message)
                    localIndex = -1;
                    previousMessage = message;
                }
            }*/

            if(previousMessage && message.author.userId === previousMessage.author.userId){
                if(Utils.getDifferenceBetweenDates(message.createdAt, previousMessage.createdAt) < SECONDS_TO_BE_GROUPED){
                    currentGroup.push(message);
                    previousMessage = message;
                } else {
                    parsedChat.push(currentGroup);
                    currentGroup = [];
                    currentGroup.push(message)
                    previousMessage = message;
                }
            } else {
                if(currentGroup.length > 0) currentGroup = [];
                currentGroup.push(message)

                parsedChat.push(currentGroup);

                previousMessage = message;
            }
        }

        /*if(currentGroup.length > 0){
            parsedChat.push(currentGroup);
        }*/

        console.log(parsedChat)

        return parsedChat;
    }

    onMessageCreationFormBlur = (content: string) => {

    }

    onMessageCreationFormSubmit = (text: string) => {
        const {chat} = this.props;
        let data : IChatMessageCreation = {
            receiverId: chat.interlocutor.userId,
            authorId: AuthStore.identity.userId,
            textContent: text,
            chatId: chat.chatId
        }
        this.props.onCreateMessage(data);
    }

    onMessageSelection = (messageId: string) => {
        this.setState({
            selectedMessageIds: [messageId, ...this.state.selectedMessageIds]
        })
    }

    onMessageDeselection = (messageId: string) => {
        this.setState({
            selectedMessageIds: [...this.state.selectedMessageIds.filter(value => value === messageId)]
        })
    }

    deleteSelectedMessages = () => {
        this.props.onDeleteMessages(this.state.selectedMessageIds);
    }

    removeMessageById = (messageId: string) => {
        this.props.removeMessageById(messageId, this.props.chat.chatId);
    }

    // TODO
    renderChat = () => {
        let result: Array<HTMLElement>;
        let previousMessage: IMessage;
        let div: HTMLDivElement;
        this.props.messages.forEach((message, index) => {
            let node: Array<HTMLElement>;

        })
        return result;
    }

    render() {
        return (
            <div className="chat">
                <header className="chat__interlocutor-data">
                    <div className="uk-grid uk-grid-medium" >
                        <div className="uk-width-auto uk-flex-first">
                            <Link to={`${Routes.getBaseLink(Routes.profilePageRoutes)}/${this.props.chat.interlocutor.userId}`}>
                                <img className="border-radius-50"
                                     src={Utils.getImageSrc(this.props.chat.interlocutor.avatarPath)}
                                     width="80"
                                     height="80" alt=""/>
                            </Link>
                        </div>
                        <div className="uk-width-expand">
                            <h4 className="uk-comment-title uk-margin-remove">
                                <Link className="uk-link-text" to={`${Routes.getBaseLink(Routes.profilePageRoutes)}/${this.props.chat.interlocutor.userId}`}>
                                    {this.props.chat.interlocutor.name + " " + this.props.chat.interlocutor.surname}
                                </Link>
                            </h4>
                        </div>
                    </div>
                </header>
                <hr className="max-width"/>
                <div className="chat__messages max-width uk-grid uk-width-1-1">
                    {
                        this.state.selectedMessageIds.length > 0 &&
                            <div className="chat__controls">
                                    <button onClick={this.deleteSelectedMessages} className="chat__remove-selected-messages-btn uk-icon-button uk-margin-small-right" uk-icon="trash"></button>
                            </div>
                    }
                    {/*{
                        this.getGroupedMessages().map((group) => {
                            return group.map((value, index) => {
                                return <ChatMessage message={value}
                                                    key={index}
                                                    removeMessage={this.removeMessage}
                                                    deselectMessage={this.onMessageDeselection}
                                                    selectMessage={this.onMessageSelection}
                                                    isAuthorDataVisible={index === 0}
                                                    viewerId={AuthStore.identity.userId} />
                            })
                        })
                    }*/}
                    {
                        this.props.messages.map((value, index) => {
                            return <ChatMessage message={value}
                                                key={index}
                                                removeMessage={this.removeMessageById}
                                                deselectMessage={this.onMessageDeselection}
                                                selectMessage={this.onMessageSelection}
                                                isAuthorDataVisible={true}
                                                viewerId={AuthStore.identity.userId} />
                        })
                    }
                </div>
                <MessageCreationForm onFormBlur={this.onMessageCreationFormBlur}
                                     onSubmit={this.onMessageCreationFormSubmit}
                                     isFocused={true} />
            </div>
        );
    }
}

export default Chat;
