import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";
import moment from "moment";

const chatsMock : Array<any> = [
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
]

@injectable()
export class ChatService extends BaseService{
    getChatsByUserId(userId: string){
        return chatsMock;
    }
}
