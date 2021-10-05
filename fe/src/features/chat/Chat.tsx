import React, {Component} from 'react';
import {IChat, IMessage} from "./chat-models";
import {getViewDate} from "assets/utils/date-utils";
import "./chat.scss"
import {ChatPreviewMode} from "./chat-enums";
import ChatMessage from "./ChatMessage";
import {AuthStore} from "../../store/auth/auth-store";
import {Utils} from "../../core/utils";

type State = {
    parsedChat: Array<Array<IMessage>>
}

type Props = {
    chat: IChat;
    chatMessagesLoaded: boolean;
}

class Chat extends Component<Props, State> {
    //userId = "1234"
    getDifferenceBetweenDates = (t1: Date, t2: Date) : number => {
        //console.log(t1, t2)
        let dif = new Date(t1).getTime() - new Date(t2).getTime();
        let Seconds_from_T1_to_T2 = dif / 1000;
        let Seconds_Between_Dates = Math.abs(Seconds_from_T1_to_T2);
        return Seconds_Between_Dates;
    }

    getGroupedMessages = () => {
        let parsedChat: Array<Array<IMessage>> = []
        let previousMessage: IMessage;
        let localIndex: number = -1;
        const SECONDS_TO_BE_GROUPED = 10;
        let currentGroup: Array<IMessage> = [];
        for(let message of this.props.chat.messages){
            console.log(message)
            localIndex++;

            if(localIndex === 0) {
                currentGroup.push(message);
            } else if(localIndex >= 1) {
                if(message.author.userId === previousMessage.author.userId){
                    if(this.getDifferenceBetweenDates(message.createdAt, previousMessage.createdAt) < SECONDS_TO_BE_GROUPED){
                        currentGroup.push(message)
                    }
                } else {
                    parsedChat.push(currentGroup);
                    currentGroup = [];
                    localIndex = -1;
                    break;
                }
            }

            previousMessage = message;
        }
        /*this.props.chat.messages.forEach((message,i) => {
            localIndex++;

            if(localIndex === 0) {
                currentGroup.push(message);
            } else if(localIndex >= 1) {
                if(message.author.userId === previousMessage.author.userId){
                    if(localIndex > 1 && this.getDifferenceBetweenDates(message.date, previousMessage.date) < SECONDS_TO_BE_GROUPED){
                        currentGroup.push(message)
                    }
                } else {
                    parsedChat.push(currentGroup);
                    currentGroup = [];
                    localIndex = -1;
                }
            }

            previousMessage = message;
        })*/
        return parsedChat;
    }

    render() {
        return (
            <div className="chat">
                <header className="chat__interlocutor-data">
                    <div className="uk-grid uk-grid-medium" >
                        <div className="uk-width-auto uk-flex-first">
                            <img className="border-radius-50"
                                 src={Utils.getImageSrc(this.props.chat.interlocutor.avatarPath)}
                                 width="80"
                                 height="80" alt=""/>
                        </div>
                        <div className="uk-width-expand">
                            <h4 className="uk-comment-title uk-margin-remove"><a className="uk-link-reset"
                                                                                 href="#">{this.props.chat.interlocutor.name + " " + this.props.chat.interlocutor.surname}</a></h4>
                            <p className="uk-comment-meta uk-margin-remove-top"><a className="uk-link-reset"
                                                                                   href="#">{getViewDate(this.props.chat.messages[0]?.createdAt)}</a>
                            </p>
                        </div>
                    </div>
                </header>
                <hr className="max-width uk-margin-small-left"/>
                <div className="uk-grid uk-width-1-1">
                    {/*{
                        this.props.chat.messages.map((value,i) => {
                            return <div key={i} className="uk-margin-top chat__message-wrapper">
                                <ChatMessage message={value}
                                              previousMessage={i > 0 ? this.props.chat.messages[i-1] : null}
                                              viewerId={AuthStore.identity.userId} />
                            </div>
                        })
                    }*/}
                    {
                        this.getGroupedMessages().map((group) => {
                            return group.map((value, index) => {
                                return <ChatMessage message={value}
                                                    key={index}
                                                    isAuthorDataVisible={index === 0}
                                                    viewerId={AuthStore.identity.userId} />
                            })
                        })
                    }
                </div>
            </div>
        );
    }
}

export default Chat;
