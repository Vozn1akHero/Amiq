import {IGroupData} from "../../group/models/group-models";
import {IUser} from "../../user/models/user";

export interface IPostComment {
    commentId: string;
    postId: string;
    textContent: string;
    author: Partial<IUser>;
    createdAt: Date;
    parentCommentId: string;
    mainParentCommentId: string;
    children: Array<IGroupPostComment&IPostComment>;
}

export interface IGroupPostComment extends IPostComment {
    group: Partial<IGroupData>;
    authorVisibilityType: string;
}

export interface IPostCommentCreation {
    postId: string;
    textContent: string;
    parentId: string;
    mainParentId: string;
}

export interface IGroupPostCommentCreation extends IPostCommentCreation {
    authorVisibilityType: string;
    groupId: number;
}