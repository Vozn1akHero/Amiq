import React, {Component, Dispatch, useEffect, useState} from 'react';
import {container} from "tsyringe";
import ChatService from "features/chat/chat-service";
import ChatPage from './ChatPage';
import {IChat, IChatMessageCreation, IChatPreview, IMessage} from 'features/chat/chat-models';
import {AuthStore} from "../../store/custom/auth/auth-store";
import ChatMessageService from "../../features/chat/chat-message-service";
import {AxiosResponse} from "axios";
import {connect, shallowEqual, useDispatch, useSelector} from "react-redux";
import {getChatPreviews, createMessage} from "store/redux/actions/chatActions"

type State = {
    selectedChatId: string;
    isChatSelected: boolean;
    selectedChat: IChat;
    chatPreviews: Array<IChatPreview>;
    chatPreviewsLoaded: boolean;
    chatMessagesLoaded: boolean;
}

const ChatPageContainer = () => {
    const chatMessageService = new ChatMessageService();

    const chatPreviews: Array<IChatPreview> = useSelector(
        (state:any) => {
            console.log(state)
            return state.chat.chatPreviews;
        }
    )
    let chatPreviewsLoaded: boolean = useSelector(
        (state:any) => {
            return state.chat.chatPreviewsLoaded
        }
    )

    const dispatch: Dispatch<any> = useDispatch();

    useEffect(() => {
        if(!chatPreviewsLoaded){
            dispatch(getChatPreviews());
        }
    }, [])

    const onCreateMessage = (message: IChatMessageCreation) => {
        dispatch(createMessage(message));
    }

    //const [chatPreviews, setchatPreviews] = useState(null);
    const [selectedChatId, setSelectedChatId] = useState(null);
    const [selectedChat, setSelectedChat] = useState(null);
    const [isChatSelected, setIsChatSelected] = useState(false);
    const [chatMessagesLoaded, setChatMessagesLoaded] = useState(false);

    const onChatSelection = (selectedChatId: string) => {
        setSelectedChatId(selectedChatId);
        const index = chatPreviews.findIndex(value => value.chatId === selectedChatId);
        const selectedChatPreview = chatPreviews[index];
        chatMessageService.getMessagesByChatId(selectedChatPreview.chatId, 1).then((res:AxiosResponse) => {
            const messages = res.data as Array<IMessage>;
            const selectedChat:IChat = {
                chatId: selectedChatPreview.chatId,
                interlocutor: selectedChatPreview.interlocutor,
                messages
            }
            setSelectedChat(selectedChat);
            setChatMessagesLoaded(true);
        })
    }

    return (
        <ChatPage chats={chatPreviews}
                  onCreateMessage={onCreateMessage}
                  chatPreviewsLoaded={chatPreviewsLoaded}
                  onChatSelection={onChatSelection}
                  chatMessagesLoaded={chatMessagesLoaded}
                  selectedChat={selectedChat} />
    );
}

export default ChatPageContainer;
