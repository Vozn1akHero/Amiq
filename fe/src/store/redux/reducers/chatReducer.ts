import {
    CACHE_CHAT_PREVIEWS_ON_SEARCH,
    GET_CHAT_MESSAGES,
    GET_CHAT_PREVIEWS,
    GET_CHAT_PREVIEWS_FROM_CACHE_ON_SEARCH,
    MESSAGE_CREATED,
    REMOVE_MESSAGE_FROM_STORE,
    REMOVE_MESSAGES,
    SET_CHAT_MESSAGES,
    SET_CHAT_PREVIEWS,
    SET_FOUND_CHATS,
    UPDATE_OR_ADD_CHAT_PREVIEW
} from '../types/chatTypes';
import {IChatPreview, IMessage} from "features/chat/chat-models";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {IPaginatedStoreData} from "../base/paginated-store-data";

type ChatState = {
    pending: boolean,
    chatPreviewsLoaded: boolean,
    chatPreviews: Array<IChatPreview>,
    cachedChatPreviewsOnSearch: Array<IChatPreview>,
    chatMessages: IPaginatedStoreData<IMessage>
}

const initialState : ChatState = {
    pending: false,
    chatPreviewsLoaded: false,
    chatPreviews: [],
    cachedChatPreviewsOnSearch: [],
    chatMessages: {
        entities: [],
        loaded: false,
        loading: false,
        currentPage: 1,
        length: 0
    }
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
                chatMessages: {
                    ...state.chatMessages,
                    entities: [action.payload, ...state.chatMessages.entities]
                }
            }
        case GET_CHAT_MESSAGES:{
            return {
                ...state,
                chatMessages: {
                    loading: true,
                    loaded: false,
                    entities: [],
                    length: 0,
                    currentPage: 1
                }
            }
        }
        case SET_CHAT_MESSAGES:
            const data = action.payload as IResponseListOf<IMessage>;
            return {
                ...state,
                chatMessages: {
                    length: data.length,
                    entities: [...state.chatMessages.entities, ...data.entities],
                    currentPage: state.chatMessages.currentPage+1,
                    loading: false,
                    loaded: true
                }
            }
        case REMOVE_MESSAGES: {
            return {
                ...state,
                chatMessages: {
                    ...state.chatMessages,
                    entities: state.chatMessages.entities.filter(value => (action.payload as Array<IMessage>).findIndex(e=>e.messageId === value.messageId) === -1)
                }
            }
        }
        case REMOVE_MESSAGE_FROM_STORE: {
            return {
                ...state,
                chatMessages: {
                    ...state.chatMessages,
                    entities: state.chatMessages.entities.filter(message => message.messageId !== action.payload)
                }
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
        case SET_FOUND_CHATS: {
            return {
                ...state,
                chatPreviews: action.payload
            }
        }
        case CACHE_CHAT_PREVIEWS_ON_SEARCH: {
            return {
                ...state,
                cachedChatPreviewsOnSearch: state.chatPreviews
            }
        }
        case GET_CHAT_PREVIEWS_FROM_CACHE_ON_SEARCH: {
            return  {
                ...state,
                chatPreviews: state.cachedChatPreviewsOnSearch
            }
        }
        default:
            return state;
    }
}
