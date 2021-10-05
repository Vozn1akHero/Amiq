import React, {Component} from 'react';
import {container} from "tsyringe";
import ChatService from "features/chat/chat-service";
import ChatPage from './ChatPage';
import {IChat, IChatPreview, IMessage} from 'features/chat/chat-models';
import {AuthStore} from "../../store/auth/auth-store";
import ChatMessageService from "../../features/chat/chat-message-service";
import {AxiosResponse} from "axios";

type State = {
    selectedChatId: string;
    isChatSelected: boolean;
    selectedChat: IChat;
    chatPreviews: Array<IChatPreview>;
    chatPreviewsLoaded: boolean;
    chatMessagesLoaded: boolean;
}

type Props = {}

class ChatPageContainer extends Component<Props, State> {
    readonly chatService = new ChatService();
    chatMessageService = new ChatMessageService();
    userId: number;

    constructor(props:Props) {
        super(props);

        this.state = {
            selectedChatId: null,
            isChatSelected: false,
            selectedChat: null,
            chatPreviews: [],
            chatMessagesLoaded: false,
            chatPreviewsLoaded: false
        }

        this.userId = AuthStore.identity.userId;
        //this.chatsMock = this.chatService.getChatsByUserId("1234");
    }

    componentDidMount() {
        this.setChatPreviews();
    }

    setChatPreviews = () => {
        this.chatService.getChatPreviews().then((res:AxiosResponse) => {
            const chatPreviews =  res.data as Array<IChatPreview>;
            this.setState({
                chatPreviews,
                chatPreviewsLoaded: true
            })
        })
    }

    onChatSelection = (chatId: string) => {
        this.setState({
            selectedChatId: chatId,
            isChatSelected: true,
            chatMessagesLoaded: false
        },() => {
            const index = this.state.chatPreviews.findIndex(value => value.chatId === this.state.selectedChatId);
            const selectedChatPreview = this.state.chatPreviews[index];
            this.chatMessageService.getMessagesByChatId(selectedChatPreview.chatId, 1).then((res:AxiosResponse) => {
                const messages = res.data as Array<IMessage>;
                const selectedChat:IChat = {
                    chatId: selectedChatPreview.chatId,
                    interlocutor: selectedChatPreview.interlocutor,
                    messages
                }
                this.setState({
                    selectedChat,
                    chatMessagesLoaded: true
                })
            })
            console.log(this.state.selectedChat)
        })
    }

    render() {
        return (
            <ChatPage chats={this.state.chatPreviews}
                      chatPreviewsLoaded={this.state.chatPreviewsLoaded}
                      onChatSelection={this.onChatSelection}
                      chatMessagesLoaded={this.state.chatMessagesLoaded}
                      selectedChat={this.state.selectedChat} />
        );
    }
}

export default ChatPageContainer;
