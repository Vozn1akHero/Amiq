import React, {Component} from 'react';
import {IMessage} from "./chat-models";
import "./chat-message.scss"

class ChatMessage extends Component<{message: IMessage, previousMessage: IMessage, viewerId: number}, any> {
    render() {
        return (
            <div className={`chat-message 
                    ${this.props.message.author.userId === this.props.viewerId ? `chat-message--created-by-viewer` : `chat-message--received-by-viewer`}`}>
                {
                    this.props.message.textContent
                }
            </div>
        )
    }
}

export default ChatMessage;