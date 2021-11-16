import {
    GET_CHAT_MESSAGES,
    GET_CHAT_PREVIEWS, MESSAGE_CREATED, REMOVE_MESSAGE_FROM_STORE, REMOVE_MESSAGES, SET_CHAT_MESSAGES,
    SET_CHAT_PREVIEWS, UPDATE_OR_ADD_CHAT_PREVIEW
} from '../types/chatTypes';
import {IChatPreview, IMessage} from "features/chat/chat-models";

type ChatState = {
    pending: boolean,
    chatPreviewsLoaded: boolean,
    chatPreviews: Array<IChatPreview>,
    chatMessages: Array<IMessage>,
    initialChatMessagesLoaded: boolean
}

const initialState : ChatState = {
    pending: false,
    chatPreviewsLoaded: false,
    chatPreviews: [],
    chatMessages: [],
    initialChatMessagesLoaded: false
};

export default function(state = initialState, action) {
    switch (action.type) {
        case GET_CHAT_PREVIEWS:
            return {
                ...state,
                pending: true
            };
        case SET_CHAT_PREVIEWS:
            return {
                ...state,
                pending: false,
                chatPreviewsLoaded: true,
                chatPreviews: [...action.payload, ...state.chatPreviews]
            };
        case MESSAGE_CREATED:
            return {
                ...state,
                chatMessages: [action.payload, ...state.chatMessages]
            }
        case GET_CHAT_MESSAGES:{
            return {
                ...state,
                chatMessages: []
            }
        }
        case SET_CHAT_MESSAGES:
            return {
                ...state,
                initialChatMessagesLoaded: true,
                chatMessages: [...action.payload, ...state.chatMessages]
            }
        case REMOVE_MESSAGES: {
            return {
                ...state,
                chatMessages: [...state.chatMessages.filter(value => (action.payload as Array<IMessage>).findIndex(e=>e.messageId === value.messageId) === -1)]
            }
        }
        case REMOVE_MESSAGE_FROM_STORE: {
            return {
                ...state,
                chatMessages: state.chatMessages.filter(message => message.messageId !== action.payload)
            }
        }
        case UPDATE_OR_ADD_CHAT_PREVIEW: {
            const chatPreview = action.payload as IChatPreview;
            let nextChatPreviewsState: Array<IChatPreview>;
            if(state.chatPreviews.some(value => value.chatId === chatPreview.chatId)){
                nextChatPreviewsState = state.chatPreviews.map(value => {
                    if(value.chatId === chatPreview.chatId){
                        value = chatPreview
                    }
                    return value;
                })
            } else {
                nextChatPreviewsState = [chatPreview, ...state.chatPreviews];
            }
            return {
                ...state,
                chatPreviews: nextChatPreviewsState
            }
        }
        default:
            return state;
    }
}
