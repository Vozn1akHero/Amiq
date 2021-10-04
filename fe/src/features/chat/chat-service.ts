import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";
import moment from "moment";
import {IMessage, IChatPreview, IChat} from "./chat-models";

export default class ChatService extends BaseService{
    apiModule = "chat";

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
}
