import {IUser} from "../user/models/user";

export interface IInterlocutor {
    userId: number;
    viewName: string;
    avatarPath: string;
}

export interface IMessage {
    messageId: string;
    chatId: string;
    author: IInterlocutor;
    textContent: string;
    files: Array<IMedia>;
    createdAt: Date;
}

export interface IMedia {
    src: URL;
    type: "png" | "jpg";
}

export interface IChatPreview {
    chatId: string,
    author: Partial<IUser>;
    interlocutor: Partial<IUser>;
    textContent: string;
    hasMedia: boolean;
    date: Date;
    writtenByIssuer: boolean;
}

export interface IChat {
    chatId: string;
    interlocutor: Partial<IUser>;
    messages: Array<IMessage>;
}

export interface IGroupedMessages {

}

export interface IChatMessageCreation {
    chatId: string;
    authorId: number;
    receiverId: number;
    textContent: string;
}