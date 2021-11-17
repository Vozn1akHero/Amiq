import React, {Component, MouseEvent} from 'react';
import {IMessage} from "./chat-models";
import "./chat-message.scss"
import {Utils} from "../../core/utils";
import {Link} from 'react-router-dom';
import {DateUtils} from "../../assets/utils/date-utils";
import {AuthStore} from "../../store/custom/auth/auth-store";

type State = {
    isSelected: boolean;
    messageControlsVisible: boolean;
}

type Props = {
    message: IMessage;
    isAuthorDataVisible: boolean;
    selectMessage(messageId: string): void;
    deselectMessage(messageId: string): void;
    removeMessage(messageId: string): void;
}

class ChatMessage extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            isSelected: false,
            messageControlsVisible: false
        }
    }

    onMessageSelection = (e:MouseEvent) => {
        console.log(e.currentTarget)
        if(!e.currentTarget.classList.contains(".chat-message__controls__remove-msg"))
            this.setState({
                isSelected: !this.state.isSelected
            }, () => {
                if (this.state.isSelected)
                    this.props.selectMessage(this.props.message.messageId)
                else this.props.deselectMessage(this.props.message.messageId)
            })
    }

    onMessageMouseOver = () => {
        this.setState({
            messageControlsVisible: true
        })
    }

    onMessageMouseOut = () => {
        this.setState({
            messageControlsVisible: false
        })
    }

    render() {
        return (
            <div onMouseOver={this.onMessageMouseOver}
                 onMouseLeave={this.onMessageMouseOut}
                 className={`chat-message 
                    ${this.props.message.author.userId === AuthStore.identity.userId ? `chat-message--created-by-viewer` : `chat-message--received-by-viewer`}
                    ${this.state.isSelected ? `uk-card-default` : ``}
                    `}>
                {
                    this.props.isAuthorDataVisible &&
                    <Link to={`/user/${this.props.message.author.userId}`} className="uk-float-left">
                        <img className="chat-message__avatar border-radius-50"
                             src={Utils.getImageSrc(this.props.message.author.avatarPath)}
                             width="80"
                             height="80"
                             alt=""/>
                    </Link>
                }
                <div className="uk-flex">
                    <div onClick={(e: MouseEvent) => {
                        if(this.props.message.author.userId === AuthStore.identity.userId)
                        {
                            this.onMessageSelection(e)
                        }
                    }}
                         className={`chat-message__text-bg uk-card uk-card-default uk-card-body ${this.props.isAuthorDataVisible ? `uk-float-left uk-margin-small-left` : ``}`}>
                        <div className={`chat-message__text`}>
                            {
                                this.props.message.textContent
                            }
                        </div>
                    </div>
                    <div className="uk-margin-left uk-margin-medium-top uk-text-small">
                        {DateUtils.getViewDate(this.props.message.createdAt)}
                    </div>
                    <div className="uk-margin-left uk-margin-medium-top uk-text-small">
                        {
                            (this.state.messageControlsVisible && this.props.message.author.userId === AuthStore.identity.userId) && <div className={`chat-message__controls`}>
                                <button onClick={() => {
                                        this.props.removeMessage(this.props.message.messageId)
                                    }} className="chat-message__controls__remove-msg uk-icon-button uk-margin-small-right" uk-icon="trash"></button>
                            </div>
                        }
                    </div>
                </div>
            </div>
        )
    }
}

export default ChatMessage;