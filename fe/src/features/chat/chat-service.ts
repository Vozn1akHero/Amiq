import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";
import moment from "moment";
import {IMessage, IChatPreview, IChat} from "./chat-models";

/*const chatPreviewsMock : Array<any> = [
    {
        avatarSrc: "https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg",
        viewName: "Tomasz Krzaczyk",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(3, "hours")
    },
    {
        avatarSrc: "https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg",
        viewName: "John Cena",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(2, "days")
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov",
        textMessage: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(2, "days")
    }
]*/

const chatPreviewsMock : Array<IChatPreview> = [
    {
        chatId: "1",
        author: {
            userId: "1",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(3, "hours").toDate()
    },
    {
        chatId: "2",
        author: {
            userId: "2",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "John Cena",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days").toDate()
    },
    {
        chatId: "3",
        author: {
            userId: "3",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Jakis Denis",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days").toDate()
    },
    {
        chatId: "4",
        author: {
            userId: "4",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days").toDate()
    },
    {
        chatId: "5",
        author: {
            userId: "5",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days").toDate()
    },
    {
        chatId: "6",
        author: {
            userId: "6",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days").toDate()
    },
    {
        chatId: "7",
        author: {
            userId: "7",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days").toDate()
    },
    {
        chatId: "8",
        author: {
            userId: "8",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(1, "days").toDate()
    },
    {
        chatId: "9",
        author: {
            userId: "9",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(2, "days").toDate()
    },
    {
        chatId: "10",
        author: {
            userId: "10",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk",
        },
        textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
        hasMedia: false,
        date: moment().subtract(2, "days").toDate()
    }
]

const chatsMock : Array<IChat> = [
    {
        chatId: "1",
        interlocutor: {
            userId: "1",
            avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
            viewName: "Tomasz Krzaczyk"
        },
        messages: [
            {
                author: {
                    userId: "1",
                    avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
                    viewName: "Tomasz Krzaczyk",
                },
                textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
                files: [],
                date: moment().subtract(3, "hours").toDate()
            },
            {
                author: {
                    userId: "1234",
                    avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
                    viewName: "Dima V",
                },
                textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
                files: [],
                date: moment().subtract(5, "hours").toDate()
            },
            {
                author: {
                    userId: "1234",
                    avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
                    viewName: "Dima V",
                },
                textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
                files: [],
                date: moment().subtract(5, "hours").toDate()
            },
            {
                author: {
                    userId: "1234",
                    avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
                    viewName: "Dima V",
                },
                textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
                files: [],
                date: moment().subtract(6, "hours").toDate()
            },
            {
                author: {
                    userId: "1",
                    avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
                    viewName: "Tomasz Krzaczyk",
                },
                textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
                files: [],
                date: moment().subtract(3, "days").toDate()
            },
            {
                author: {
                    userId: "1",
                    avatarSrc: new URL("https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"),
                    viewName: "Tomasz Krzaczyk",
                },
                textContent: "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.",
                files: [],
                date: moment().subtract(3, "days").toDate()
            }
        ]
    }
]


export default class ChatService extends BaseService{
    apiModule = "chat";

    getChatsByUserId(userId: string){
        return chatPreviewsMock;
    }

    getMessagesByChatId(chatId: string) : IChat {
        return chatsMock.find(value => value.chatId === chatId) as IChat;
    }
}
