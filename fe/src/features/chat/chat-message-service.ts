import {BaseService} from "../../core/base-service";
import {IChat, IMessage} from "./chat-models";
import {Observable} from "rxjs";

export default class ChatMessageService extends BaseService{
    getMessagesByChatId(chatId: string) {
        //return chatsMock.find(value => value.chatId === chatId) as IChat;
        return this.httpClient.get(this.buildApiPath("list-by-chat"))
    }
}