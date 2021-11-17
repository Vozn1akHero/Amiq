import {
    CACHE_CHAT_PREVIEWS_ON_SEARCH,
    CREATE_MESSAGE,
    GET_CHAT_MESSAGES,
    GET_CHAT_PREVIEWS, GET_CHAT_PREVIEWS_FROM_CACHE_ON_SEARCH,
    MESSAGE_CREATED, REMOVE_MESSAGE_FROM_STORE, REMOVE_MESSAGES, SET_CHAT_MESSAGES,
    SET_CHAT_PREVIEWS, SET_FOUND_CHATS, START_REMOVING_MESSAGES, UPDATE_OR_ADD_CHAT_PREVIEW
} from "../types/chatTypes"
import ChatService from "features/chat/chat-service";
import {AxiosResponse} from "axios";
import {IChat, IChatMessageCreation, IChatPreview, IMessage} from "features/chat/chat-models";
import ChatMessageService from "features/chat/chat-message-service";
import {StatusCodes} from "http-status-codes";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {IUser} from "../../../features/user/models/user";
import {AuthStore} from "../../custom/auth/auth-store";

const chatService = new ChatService();
const chatMessageService = new ChatMessageService();

export const getChatPreviews = () => (dispatch, useState) => {
        dispatch({
            type: GET_CHAT_PREVIEWS
        });

        chatService.getChatPreviews().then((res: AxiosResponse) => {
            const chatPreviews = res.data as Array<IChatPreview>;

            dispatch({
                type: SET_CHAT_PREVIEWS,
                payload: chatPreviews
            })
        })
}

export const getChatMessages = (chatId:string, page: number)  => (dispatch) => {
    if(page === 1){
        dispatch({
            type: GET_CHAT_MESSAGES
        });
    }
    chatMessageService.getMessagesByChatId(chatId, page).then((res:AxiosResponse) => {
        const messages = res.data as IResponseListOf<IMessage>;
        dispatch({
            type: SET_CHAT_MESSAGES,
            payload: messages
        });
    })
}

export const createMessage = (message: IChatMessageCreation) => (dispatch) => {
    dispatch({
        type: CREATE_MESSAGE
    })

    chatMessageService.create(message).then(res=>{
        if(res.status === StatusCodes.CREATED){
            const createdMessage = res.data as IMessage;
            dispatch({
                type: MESSAGE_CREATED,
                payload: createdMessage
            })
        }
    })
}

export const deleteMessages = (messageIds: Array<string>) => (dispatch) => {
    dispatch({
        type: START_REMOVING_MESSAGES
    })

    chatMessageService.delete(messageIds).then(res => {
        if(res.status === StatusCodes.OK){
            dispatch({
                type: REMOVE_MESSAGES,
                payload: res.data.entities as Array<IMessage>
            })
        }
    })
}

export const removeMessageFromStore = (messageId: string) => dispatch => {
    dispatch({
        type: REMOVE_MESSAGE_FROM_STORE,
        payload: messageId
    })
}

export const addMessageToStore = (message: IMessage) => dispatch => {
    dispatch({
        type: MESSAGE_CREATED,
        payload: message
    })
}

export const removeMessageById = (messageId: string, chatId: string, userId: number) => dispatch => {
    chatMessageService.removeMessageById(
        messageId, chatId, userId
    ).then(res => {
        if(res.status === StatusCodes.OK) {
            dispatch({
                type: REMOVE_MESSAGE_FROM_STORE,
                payload: messageId
            })
        }
    })
}

export const updateOrAddChatPreview = (message: IMessage) => dispatch => {
    console.log(message)
    const chatPreview : IChatPreview = {
        chatId: message.chatId,
        author: message.author,
        interlocutor: AuthStore.identity.userId === message.receiver.userId ? message.author : message.receiver,
        textContent: message.textContent,
        hasMedia: message.files.length > 0,
        date: message.createdAt,
        writtenByIssuer: AuthStore.identity.userId === message.author.userId
    }
    dispatch({
        type: UPDATE_OR_ADD_CHAT_PREVIEW,
        payload: chatPreview
    })
}

export const searchForChats = (text: string) => dispatch => {
    chatService.searchForChats(text).then(res => {
        if(res.status === StatusCodes.OK){
            const data = res.data as Array<IChatPreview>;
            dispatch({
                type: SET_FOUND_CHATS,
                payload: data
            })
        }
    })
}

export const cacheChatPreviewsOnSearch = () => dispatch => {
    dispatch({
        type: CACHE_CHAT_PREVIEWS_ON_SEARCH
    })
}

export const getChatPreviewsFromCacheOnSearch = () => dispatch => {
    dispatch({
        type: GET_CHAT_PREVIEWS_FROM_CACHE_ON_SEARCH
    })
}