import React, {Component} from 'react';
import {GroupService} from "../../features/group/group-service";
import {container} from "tsyringe";
import {ChatService} from "../../features/chat/chat-service";
import ChatPage from './ChatPage';

class ChatPageContainer extends Component {
    readonly chatService : ChatService = container.resolve(ChatService);
    chatsMock: Array<any> = [];

    constructor(props:any) {
        super(props);

        this.chatsMock = this.chatService.getChatsByUserId("");
    }


    render() {
        return (
            <ChatPage chats={this.chatsMock} />
        );
    }
}

export default ChatPageContainer;
