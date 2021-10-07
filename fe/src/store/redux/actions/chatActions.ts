import {CREATE_MESSAGE, GET_CHAT_PREVIEWS, MESSAGE_CREATED, SET_CHAT_PREVIEWS} from "../types/chatTypes"
import ChatService from "features/chat/chat-service";
import {AxiosResponse} from "axios";
import {IChatMessageCreation, IChatPreview, IMessage} from "features/chat/chat-models";
import ChatMessageService from "features/chat/chat-message-service";
import {StatusCodes} from "http-status-codes";

const chatService = new ChatService();
const chatMessageService = new ChatMessageService();

export const getChatPreviews = () => async (dispatch, useState) => {
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
