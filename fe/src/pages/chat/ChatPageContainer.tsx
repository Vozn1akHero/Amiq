import React, {Dispatch, useEffect, useState} from 'react';
import ChatPage from './ChatPage';
import {IChat, IChatMessageCreation, IChatPreview, IMessage} from 'features/chat/chat-models';
import {useDispatch, useSelector} from "react-redux";
import {getChatPreviews, createMessage, getChatMessages, deleteMessages} from "store/redux/actions/chatActions"


const ChatPageContainer = () => {
    const chatPreviews: Array<IChatPreview> = useSelector(
        (state:any) => {
            return state.chat.chatPreviews;
        }
    )
    const chatPreviewsLoaded: boolean = useSelector(
        (state:any) => {
            return state.chat.chatPreviewsLoaded
        }
    )
    const initialChatMessagesLoaded: boolean = useSelector(
        (state:any) => {
            return state.chat.initialChatMessagesLoaded
        }
    )
    const messages: Array<IMessage> = useSelector(
        (state:any) => {
            return state.chat.chatMessages
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

    const [selectedChatId, setSelectedChatId] = useState(null);
    const [selectedChat, setSelectedChat] = useState(null);
    const [isChatSelected, setIsChatSelected] = useState(false);
    const [searchInputLoading, setSearchInputLoading] = useState(false);

    const onChatSelection = (selectedChatId: string) => {
        setSelectedChatId(selectedChatId);
        const index = chatPreviews.findIndex(value => value.chatId === selectedChatId);
        const selectedChatPreview = chatPreviews[index];
        dispatch(getChatMessages(selectedChatPreview.chatId, 1));
        const selectedChat:Partial<IChat> = {
            chatId: selectedChatPreview.chatId,
            interlocutor: selectedChatPreview.interlocutor
        }
        setSelectedChat(selectedChat);
    }

    const onDeleteMessages = (ids: Array<string>) => {
        dispatch(deleteMessages(ids))
    }

    const onSearchInputChange = e => {

    }

    return (
        <ChatPage chats={chatPreviews}
                  onSearchInputChange={onSearchInputChange}
                  searchInputLoading={searchInputLoading}
                  onDeleteMessages={onDeleteMessages}
                  messages={messages}
                  onCreateMessage={onCreateMessage}
                  chatPreviewsLoaded={chatPreviewsLoaded}
                  onChatSelection={onChatSelection}
                  chatMessagesLoaded={initialChatMessagesLoaded}
                  selectedChat={selectedChat} />
    );
}

export default ChatPageContainer;
