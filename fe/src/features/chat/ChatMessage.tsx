import React, {Component} from 'react';
import {IMessage} from "./chat-models";
import "./chat-message.scss"
import {Utils} from "../../core/utils";
import { Link } from 'react-router-dom';

type State = {
    isSelected: boolean;
}

type Props = {
    message: IMessage;
    isAuthorDataVisible: boolean;
    //previousMessage: IMessage,
    viewerId: number;
    //onMessageSelection(messageId: string):void;
}

class ChatMessage extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            isSelected: false
        }
    }

    render() {
        return (
            <div onClick={() => this.setState({
                    isSelected: !this.state.isSelected
            })}
                 className={`chat-message 
                    ${this.props.message.author.userId === this.props.viewerId ? `chat-message--created-by-viewer` : `chat-message--received-by-viewer`}
                    ${this.state.isSelected && `uk-card-default`}
                    `}>
                {
                    this.props.isAuthorDataVisible && <Link to={`/user/${this.props.message.author.userId}`} className="uk-float-left">
                        <img className="chat-message__avatar border-radius-50"
                             src={Utils.getImageSrc(this.props.message.author.avatarPath)}
                             width="80"
                             height="80"
                             alt=""/>
                    </Link>
                }
                <div className={`chat-message__text-bg uk-card uk-card-default uk-card-body ${this.props.isAuthorDataVisible && `uk-float-left uk-margin-small-left`}`}>
                    <p className="chat-message__text">
                        {
                            this.props.message.textContent
                        }
                    </p>
                </div>
            </div>
        )
    }
}

export default ChatMessage;