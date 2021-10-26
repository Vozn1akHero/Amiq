import {IPostComment} from "./post-comment";
import {IUser} from "../../user/models/user";

export interface IPost {
    postId: string;
    //userId: number;
    author: Partial<IUser>,
    textContent: string;
    editedBy?: number;
    editedAt: Date;
    createdAt: Date;
    avatarPath: string;
    hasMoreCommentsThanRecent: boolean;
}