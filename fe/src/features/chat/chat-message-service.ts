import {BaseService} from "../../core/base-service";
import {IMessage} from "./chat-models";
import {HttpQueryParams} from "../../core/http-client";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export default class ChatMessageService extends BaseService{
    apiModule = "chat-message"
    service = ServicesDictionary.Chat

    getMessagesByChatId(chatId: string, page: number) {
        const queryParams = new HttpQueryParams()
            .append("chatId", chatId)
            .append("count", 20)
            .append("page", page)
        return this.httpClient.get(this.buildApiPath("list-by-chat"), null, queryParams)
    }

    create(message: Partial<IMessage>){
        return this.httpClient.post(this.buildApiPath(""),
            message,
            null,
            new HttpQueryParams().append("chatId", message.chatId))
    }

    delete(messageIds: Array<string>) {
        return this.httpClient.delete(this.buildApiPath("list"), {
            messageIds
        });
    }

    removeMessageById(messageId: string, chatId: string, userId: number) {
        return this.httpClient.delete(this.buildApiPath(), null, null,
            new HttpQueryParams().append("messageId", messageId)
                .append("chatId", chatId)
                .append("userId", userId))
    }
}