import {
    GET_CHAT_PREVIEWS,
    SET_CHAT_PREVIEWS
} from '../types/chatTypes';
import {IChatPreview} from "features/chat/chat-models";

type ChatState = {
    pending: boolean,
    chatPreviewsLoaded: boolean,
    chatPreviews: Array<IChatPreview>
}

const initialState : ChatState = {
    pending: false,
    chatPreviewsLoaded: false,
    chatPreviews: []
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
        default:
            return state;
    }
}