import {BaseService} from "core/base-service";
import "reflect-metadata";
import {injectable} from "tsyringe";

const groupsMock : Array<any> = [
    {
        id: 1,
        avatarSrc: "https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg",
        viewName: "Tomasz Krzaczyk"
    },
    {
        id: 2,
        avatarSrc: "https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg",
        viewName: "John Cena"
    },
    {
        id: 3,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        id: 4,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        id: 5,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        id: 34,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        id: 35,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        id: 36,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        id: 37,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    },
    {
        id: 38,
        avatarSrc: "https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_570xN.1814091683_i9j2.jpg",
        viewName: "Vitalii Denysov"
    }
]

@injectable()
export class GroupService extends BaseService{
    apiModule = "group";

    getGroupsByUserId(userId: string){
        return groupsMock;

    }
}
