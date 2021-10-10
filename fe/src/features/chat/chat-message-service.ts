import {BaseService} from "../../core/base-service";
import {IChat, IMessage} from "./chat-models";
import {Observable} from "rxjs";
import {HttpQueryParams} from "../../core/http-client";

export default class ChatMessageService extends BaseService{
    apiModule = "chat-message"

    getMessagesByChatId(chatId: string, page: number) {
        const queryParams = new HttpQueryParams()
            .append("chatId", chatId)
            .append("count", 20)
            .append("page", page)
                //return chatsMock.find(value => value.chatId === chatId) as IChat;
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
}