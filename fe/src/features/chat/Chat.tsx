import React, {Component, ReactElement} from 'react';
import {IChat, IChatMessageCreation, IMessage} from "./chat-models";
import "./chat.scss"
import ChatMessage from "./ChatMessage";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {Utils} from "../../core/utils";
import MessageCreationForm from "./components/MessageCreationForm";
import {Link} from 'react-router-dom';
import {Routes} from "../../core/routing";
import {DateUtils} from "../../assets/utils/date-utils";
import moment from "moment";

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
    removeMessageById(messageId: string, chatId: string): void;
}

class Chat extends Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            parsedChat: [],
            selectedMessageIds: []
        }
    }

    onMessageCreationFormBlur = (content: string) => {

    }

    onMessageCreationFormSubmit = (text: string) => {
        const {chat} = this.props;
        let data: IChatMessageCreation = {
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
    renderChat = (messages: Array<IMessage>) => {
        let result: Array<ReactElement>;
        let previousMessage: IMessage;
        let div: HTMLDivElement;
        let node: Array<HTMLElement>;
        let parsedChat: Array<Array<IMessage>> = [];
        const SECONDS_TO_BE_GROUPED = 30;
        let currentGroup: Array<IMessage> = [];

        // dzielenie wiadomości na grupy
        messages.forEach((message, index) => {
            if (index > 0) previousMessage = message[index - 1];

            if (index === 0) {
                currentGroup.push(message);
            } else if (message.author.userId === previousMessage.author.userId) {
                if (Utils.getDifferenceBetweenDates(message.createdAt, previousMessage.createdAt) < SECONDS_TO_BE_GROUPED) {
                    currentGroup.push(message);
                } else {
                    parsedChat.push(currentGroup);
                    currentGroup = [];
                    currentGroup.push(message);
                }
            } else if (message.author.userId !== previousMessage.author.userId) {
                if (currentGroup.length > 0) {
                    parsedChat.push(currentGroup);
                    currentGroup = [];
                }
                currentGroup.push(message);
            }
        })

        //let previousSubgroupMessageCreationTime
        const DAYS_TO_SHOW_DATE_BETWEEN_SUBGROUPS = 3 * 24 * 60 * 60;
        parsedChat.forEach((subgroup, index) => {
            const creationTime: Date = subgroup[0].createdAt;
            if (DateUtils.getDifferenceBetweenDatesInDays(creationTime, moment().toDate()) > DAYS_TO_SHOW_DATE_BETWEEN_SUBGROUPS) {
                let subgroupCreationTime: ReactElement = <div className="chat__messages__subgroup-creation-time">
                    {moment(creationTime).format("YYYY-MM-DD")}
                </div>;
                result.push(subgroupCreationTime);
                subgroup.forEach((message, messageIndex) => {
                    result.push(<ChatMessage message={message}
                                             key={messageIndex}
                                             removeMessage={this.removeMessageById}
                                             deselectMessage={this.onMessageDeselection}
                                             selectMessage={this.onMessageSelection}
                                             isAuthorDataVisible={messageIndex > 0}
                                             viewerId={AuthStore.identity.userId}/>);
                })
            }
        })

        return result;
    }

    render() {
        return (
            <div className="chat">
                <header className="chat__interlocutor-data">
                    <div className="uk-grid uk-grid-medium">
                        <div className="uk-width-auto uk-flex-first">
                            <Link
                                to={`${Routes.getBaseLink(Routes.profilePageRoutes)}/${this.props.chat.interlocutor.userId}`}>
                                <img className="border-radius-50"
                                     src={Utils.getImageSrc(this.props.chat.interlocutor.avatarPath)}
                                     width="80"
                                     height="80" alt=""/>
                            </Link>
                        </div>
                        <div className="uk-width-expand">
                            <h4 className="uk-comment-title uk-margin-remove">
                                <Link className="uk-link-text"
                                      to={`${Routes.getBaseLink(Routes.profilePageRoutes)}/${this.props.chat.interlocutor.userId}`}>
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
                            <button onClick={this.deleteSelectedMessages}
                                    className="chat__remove-selected-messages-btn uk-icon-button uk-margin-small-right"
                                    uk-icon="trash" />
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
                    {/*{
                        this.props.messages.map((value, index) => {
                            return <ChatMessage message={value}
                                                key={index}
                                                removeMessage={this.removeMessageById}
                                                deselectMessage={this.onMessageDeselection}
                                                selectMessage={this.onMessageSelection}
                                                isAuthorDataVisible={true}
                                                viewerId={AuthStore.identity.userId}/>
                        })
                    }*/}
                    {this.renderChat(this.props.messages)}
                </div>
                <MessageCreationForm onFormBlur={this.onMessageCreationFormBlur}
                                     onSubmit={this.onMessageCreationFormSubmit}
                                     isFocused={true}/>
            </div>
        );
    }
}

export default Chat;
