import {IGroupData} from "../../group/models/group-models";
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
    mainParentId: string;
    children: Array<IPostComment>;
    authorVisibilityType: string;
}

export interface IPostCommentCreation {
    postId: string;
    textContent: string;
    groupId?: number;
    parentId: string;
    mainParentId: string;
    authorVisibilityType: string;
}