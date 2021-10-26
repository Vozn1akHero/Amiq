import {IPost} from "./post";
import {IPostComment} from "./post-comment";

export interface IUserPost extends IPost {
    comments: Array<IPostComment>;
}