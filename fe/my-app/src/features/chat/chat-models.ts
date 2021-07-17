
export interface IInterlocutor {
    userId: string;
    viewName: string;
    avatarSrc: URL;
}

export interface IMessage {
    author: IInterlocutor;
    textContent: string;
    files: Array<IMedia>;
    date: Date;
}

export interface IMedia {
    src: URL;
    type: "png" | "jpg";
}

export interface IChatPreview {
    chatId: string,
    author: IInterlocutor;
    textContent: string;
    hasMedia: boolean;
    date: Date;
}

export interface IChat {
    chatId: string;
    interlocutor: IInterlocutor;
    messages: Array<IMessage>;
}

export interface IGroupedMessages {

}