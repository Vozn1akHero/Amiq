import {IGroupData} from "../../group/group-models";
import {IUser} from "../../user/models/user";

export interface IPostComment {
    commentId: string;
    postId: string;
    authorId: number;
    textContent: string;
    group: Partial<IGroupData>;
    author: Partial<IUser>;
    createdAt: Date;
    parentId: string;
    children: Array<IPostComment>;
}