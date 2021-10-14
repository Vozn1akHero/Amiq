import React, {Component} from 'react';
import {IChat, IChatMessageCreation, IMessage} from "./chat-models";
import {getViewDate} from "assets/utils/date-utils";
import "./chat.scss"
import {ChatPreviewMode} from "./chat-enums";
import ChatMessage from "./ChatMessage";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {Utils} from "../../core/utils";
import MessageCreationForm from "./components/MessageCreationForm";
import { Link } from 'react-router-dom';
import {Routes} from "../../core/routing";

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
}

class Chat extends Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            parsedChat: [],
            selectedMessageIds: []
        }
    }


    getDifferenceBetweenDates = (t1: Date, t2: Date) : number => {
        console.log(t1, t2)
        let dif = new Date(t1).getTime() - new Date(t2).getTime();
        let Seconds_from_T1_to_T2 = dif / 1000;
        let seconds_Between_Dates = Math.abs(Seconds_from_T1_to_T2);
        return seconds_Between_Dates;
    }

    getGroupedMessages = () => {
        let parsedChat: Array<Array<IMessage>> = []
        let previousMessage: IMessage = null;
        let localIndex: number = -1;
        const SECONDS_TO_BE_GROUPED = 10;
        let currentGroup: Array<IMessage> = [];
        const messages = this.props.messages;
        console.log(messages)
        for(let message of messages){
            localIndex++;

            if(localIndex === 0) {
                currentGroup.push(message);
                previousMessage = message;
            } else if(localIndex >= 1) {
                if(message.author.userId === previousMessage.author.userId){
                    if(this.getDifferenceBetweenDates(message.createdAt, previousMessage.createdAt) < SECONDS_TO_BE_GROUPED){
                        currentGroup.push(message)
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
            }
        }

        if(currentGroup.length > 0){
            parsedChat.push(currentGroup);
        }

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

    render() {
        return (
            <div className="chat">
                <header className="chat__interlocutor-data">
                    <div className="uk-grid uk-grid-medium" >
                        <div className="uk-width-auto uk-flex-first">
                            <Link to={`${Routes.getSimpleLink(Routes.profilePageRoutes)}/${this.props.chat.interlocutor.userId}`}>
                                <img className="border-radius-50"
                                     src={Utils.getImageSrc(this.props.chat.interlocutor.avatarPath)}
                                     width="80"
                                     height="80" alt=""/>
                            </Link>
                        </div>
                        <div className="uk-width-expand">
                            <h4 className="uk-comment-title uk-margin-remove">
                                <Link className="uk-link-text" to={`${Routes.getSimpleLink(Routes.profilePageRoutes)}/${this.props.chat.interlocutor.userId}`}>
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
                    {
                        this.getGroupedMessages().map((group) => {
                            return group.map((value, index) => {
                                return <ChatMessage message={value}
                                                    key={index}
                                                    onMessageDeselection={this.onMessageDeselection}
                                                    onMessageSelection={this.onMessageSelection}
                                                    isAuthorDataVisible={index === 0}
                                                    viewerId={AuthStore.identity.userId} />
                            })
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
