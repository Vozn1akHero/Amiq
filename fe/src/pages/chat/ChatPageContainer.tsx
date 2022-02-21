import React, {Dispatch, useEffect, useState} from 'react';
import ChatPage from './ChatPage';
import {IChat, IChatMessageCreation, IChatPreview, IMessage} from 'features/chat/chat-models';
import {useDispatch, useSelector} from "react-redux";
import * as chatActions from "store/redux/actions/chatActions";
import {
    addMessageToStore, cacheChatPreviewsOnSearch,
    createMessage,
    deleteMessages,
    getChatMessages,
    getChatPreviews, getChatPreviewsFromCacheOnSearch,
    removeMessageFromStore, searchForChats,
    updateOrAddChatPreview
} from "store/redux/actions/chatActions";
import {useHistory} from "react-router-dom";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import devConfig from "../../dev-config.json";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {IPaginatedStoreData} from "../../store/redux/base/paginated-store-data";
import io, {Socket} from "socket.io-client";
import { DefaultEventsMap } from '@socket.io/component-emitter';


const ChatPageContainer = () => {
    const chatPreviews: Array<IChatPreview> = useSelector(
        (state: any) => {
            return state.chat.chatPreviews;
        }
    )
    const chatPreviewsLoaded: boolean = useSelector(
        (state: any) => {
            return state.chat.chatPreviewsLoaded
        }
    )
    const messages: IPaginatedStoreData<IMessage> = useSelector(
        (state: any) => {
            return state.chat.chatMessages
        }
    )

    const dispatch: Dispatch<any> = useDispatch();
    const history = useHistory();
    const [selectedChatId, setSelectedChatId] = useState(null);
    const [selectedChat, setSelectedChat] = useState(null);
    const [isChatSelected, setIsChatSelected] = useState(false);
    const [searchInputLoading, setSearchInputLoading] = useState(false);
    const [signalRChatHubConnection, setSignalRChatHubConnection] = useState<HubConnection>(null);
    const [activeSearch, setActiveSearch] = useState(false);

    //let socketIOConnection;
    const [wsConnection, setWSConnection] = useState(null);// = new WebSocket("ws://localhost:14039/chat-ws");

    useEffect(() => {
        if(wsConnection)
            wsConnection.onmessage = listenToNodeWSEvents
    }, [wsConnection])

    useEffect(() => {
        //console.log(new URLSearchParams(history.location.search).get('to'))
        if(devConfig.useMicroservices) {
            setWSConnection(new WebSocket(devConfig.wsMicroservicesUrl + "/chat-ws"));
        }
        else {
            setupSignalRConnection();
        }

        if (!chatPreviewsLoaded) {
            dispatch(getChatPreviews());
        }
    }, [])

    const setupSignalRConnection = () => {
        const PushMessage: string = 'PushMessage';
        const DeleteMessage: string = 'DeleteMessage';

        const chatHubConnection = new HubConnectionBuilder()
            .withUrl(devConfig.monolithUrl + '/hub/chat')
            .withAutomaticReconnect()
            .build();
        setSignalRChatHubConnection(chatHubConnection);

        chatHubConnection.start()
            .then(res => {
                chatHubConnection.on(PushMessage, (message: IMessage) => {
                    dispatch(updateOrAddChatPreview(message));
                    if (message.author.userId !== AuthStore.identity.userId) {
                        dispatch(addMessageToStore(message));
                    }
                });
                chatHubConnection.on(DeleteMessage, (messageId: string) => {
                    dispatch(removeMessageFromStore(messageId))
                });
            })
            .catch(ex => {
                alert(ex);
            });
    }

    const setupSocketIOConnection = () => {
        //socketIOConnection = socketIOClient(devConfig.microservicesUrl + "/chat");
        /*socketIOConnection = io("http://localhost:14039/chat-ws/", {
            transports: ['websocket'], upgrade: false
        });

        socketIOConnection.on("PushMessage", data => {
            console.log(data)
        });
        socketIOConnection.on("DeleteMessage", data => {

        });*/

        //let ws = new WebSocket("ws://localhost:14039/chat-ws");

        //setWSConnection(new WebSocket("ws://localhost:14039/chat-ws"));
    }

    const listenToNodeWSEvents = e => {
        const {data} = e;
        const parsedData = JSON.parse(data);
        const event = parsedData.event;
        const message = parsedData.body;

        switch(event){
            case "PushMessage": {
                dispatch(updateOrAddChatPreview(message));
                if (message.author.userId !== AuthStore.identity.userId) {
                    dispatch(addMessageToStore(message));
                }
                break;
            }
            case "DeleteMessage": {
                dispatch(removeMessageFromStore(message.messageId));
                break;
            }
        }
    }

    const onCreateMessage = (message: IChatMessageCreation) => {
        dispatch(createMessage(message));
    }

    const selectChat = (chatId: string) => {
        if(devConfig.useMicroservices){
            if(selectedChatId){
                wsConnection.send(JSON.stringify({
                    event: "RemoveFromGroupAsync",
                    chatId: selectedChatId
                }))
            }
            wsConnection.send(JSON.stringify({
                event: "JoinGroupAsync",
                chatId
            }))
        }
        else {
            if (selectedChatId)
                signalRChatHubConnection.invoke("RemoveFromGroupAsync", selectedChatId)
                    .catch(ex => {
                        alert(ex);
                    });
            signalRChatHubConnection.invoke("JoinGroupAsync", chatId)
                .catch(ex => {
                    alert(ex);
                });
        }

        setSelectedChatId(chatId);
        const index = chatPreviews.findIndex(value => value.chatId === chatId);
        const selectedChatPreview = chatPreviews[index];
        dispatch(getChatMessages(selectedChatPreview.chatId, 1));
        const selectedChat: Partial<IChat> = {
            chatId: selectedChatPreview.chatId,
            interlocutor: selectedChatPreview.interlocutor
        }
        setSelectedChat(selectedChat);
    }

    const onDeleteMessages = (ids: Array<string>) => {
        dispatch(deleteMessages(ids))
    }

    const onSearchInputChange = (text: string) => {
        const shouldSearchBeActive: boolean = text?.length>0;
        if(!activeSearch && shouldSearchBeActive){
            dispatch(cacheChatPreviewsOnSearch())
        } else if(activeSearch && !shouldSearchBeActive) {
            dispatch(getChatPreviewsFromCacheOnSearch())
        }
        if(!activeSearch) setActiveSearch(true);
        setActiveSearch(shouldSearchBeActive);
        if(shouldSearchBeActive){
            dispatch(chatActions.searchForChats(text));
        }
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
                  getMoreMessages={() => {
                      dispatch(getChatMessages(selectedChat.chatId, messages.currentPage))
                  }}
                  selectedChat={selectedChat}/>
    );
}

export default ChatPageContainer;