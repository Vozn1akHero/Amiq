import React, {Dispatch, useEffect, useState} from 'react';
import ChatPage from './ChatPage';
import {IChat, IChatMessageCreation, IChatPreview, IMessage} from 'features/chat/chat-models';
import {useDispatch, useSelector} from "react-redux";
import {
    getChatPreviews,
    createMessage,
    getChatMessages,
    deleteMessages,
    addMessageToStore, removeMessageFromStore
} from "store/redux/actions/chatActions";
import * as chatActions from "store/redux/actions/chatActions";
import {useHistory} from "react-router-dom";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import devConfig from "../../dev-config.json";
import {AuthStore} from "../../store/custom/auth/auth-store";


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
    const history = useHistory();
    const [selectedChatId, setSelectedChatId] = useState(null);
    const [selectedChat, setSelectedChat] = useState(null);
    const [isChatSelected, setIsChatSelected] = useState(false);
    const [searchInputLoading, setSearchInputLoading] = useState(false);
    const [signalRChatHubConnection, setSignalRChatHubConnection] = useState<HubConnection>(null)

    useEffect(() => {
        //console.log(new URLSearchParams(history.location.search).get('to'))
        setupSignalRConnection();

        if(!chatPreviewsLoaded){
            dispatch(getChatPreviews());
        }
    }, [])


    const setupSignalRConnection = () => {
        const PushMessage : string = 'PushMessage';
        const DeleteMessage : string = 'DeleteMessage';

        const chatHubConnection = new HubConnectionBuilder()
            .withUrl(devConfig.monolithUrl + '/hub/chat')
            .withAutomaticReconnect()
            .build();
        setSignalRChatHubConnection(chatHubConnection);

        chatHubConnection.start()
            .then(res => {
                chatHubConnection.on(PushMessage, (message: IMessage) => {
                    if(message.author.userId !== AuthStore.identity.userId)
                        dispatch(addMessageToStore(message))
                });
                chatHubConnection.on(DeleteMessage, (messageId: string) => {
                    dispatch(removeMessageFromStore(messageId))
                });
            })
            .catch(ex => {
                alert(ex);
            });
    }

    const onCreateMessage = (message: IChatMessageCreation) => {
        dispatch(createMessage(message));
    }

    const selectChat = (chatId: string) => {
        if(selectedChatId)
            signalRChatHubConnection.invoke("RemoveFromGroupAsync", selectedChatId)
                .catch(ex => {
                    alert(ex);
                });
        signalRChatHubConnection.invoke("JoinGroupAsync", chatId)
            .catch(ex => {
                alert(ex);
            });

        setSelectedChatId(chatId);
        const index = chatPreviews.findIndex(value => value.chatId === chatId);
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

    const removeMessageById = (messageId: string, chatId: string) => {
        dispatch(chatActions.removeMessageById(messageId, chatId, AuthStore.identity.userId))
    }

    return (
        <ChatPage chats={chatPreviews}
                  onSearchInputChange={onSearchInputChange}
                  searchInputLoading={searchInputLoading}
                  onDeleteMessages={onDeleteMessages}
                  removeMessageById={removeMessageById}
                  messages={messages}
                  onCreateMessage={onCreateMessage}
                  chatPreviewsLoaded={chatPreviewsLoaded}
                  onChatSelection={selectChat}
                  chatMessagesLoaded={initialChatMessagesLoaded}
                  selectedChat={selectedChat} />
    );
}

export default ChatPageContainer;