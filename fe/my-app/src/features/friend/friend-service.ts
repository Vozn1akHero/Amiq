import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";

const friendsMock : Array<any> = [
    {
        avatarSrc: "https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg",
        viewName: "Tomasz Krzaczyk"
    },
    {
        avatarSrc: "https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg",
        viewName: "John Cena"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    }
]

@injectable()
export class FriendService extends BaseService{
    getFriendsByUserId(userId: string){
        return friendsMock;
    }
}
