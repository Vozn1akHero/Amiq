import React, {Component} from 'react';
import {container} from "tsyringe";
import {ChatService} from "features/chat/chat-service";
import ChatPage from './ChatPage';
import { IChat } from 'features/chat/chat-models';

type State = {
    selectedChatId: string;
    isChatSelected: boolean;
    selectedChat: IChat;
}

type Props = {}

class ChatPageContainer extends Component<Props, State> {

    readonly chatService : ChatService = container.resolve(ChatService);
    chatsMock: Array<any> = [];

    constructor(props:Props) {
        super(props);
        this.state = {
            selectedChatId: null,
            isChatSelected: false,
            selectedChat: null
        }
        this.chatsMock = this.chatService.getChatsByUserId("1234");
    }

    onChatSelection = (chatId: string) => {
        this.setState({
            selectedChatId: chatId,
            isChatSelected: true
        },() => {
            this.setState({
                selectedChat: this.chatService.getMessagesByChatId(this.state.selectedChatId)
            })
        })
        console.log(this.state.selectedChat)
    }

    render() {
        return (
            <ChatPage chats={this.chatsMock}
                      onChatSelection={this.onChatSelection}
                      selectedChat={this.state.selectedChat} />
        );
    }
}

export default ChatPageContainer;
