import {IPost} from "./post";
import {IUser} from "../../user/models/user";

export interface IUserPost extends IPost {
    /*postId: number;
    authorId: number;
    text: string;
    name: string;
    surname: string;*/
    author: Partial<IUser>;
}