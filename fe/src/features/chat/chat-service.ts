import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";
import moment from "moment";
import {IMessage, IChatPreview, IChat} from "./chat-models";
import {HttpQueryParams} from "../../core/http-client";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export default class ChatService extends BaseService{
    apiModule = "chat";
    service = ServicesDictionary.Chat
    /*getChatsByUserId(userId: string){
        return chatPreviewsMock;
    }*/

    getChatPreviews() {
        return this.httpClient.get(this.buildApiPath("previews"))
    }

    /*getMessagesByChatId(chatId: string) : IChat {
        //return chatsMock.find(value => value.chatId === chatId) as IChat;
        return this.httpClient.get(this.buildApiPath(this.apiModule, ""))
    }*/
    searchForChats(text: string) {
        return this.httpClient.get(this.buildApiPath("search"),
            null, new HttpQueryParams().append("text", text))
    }
}
