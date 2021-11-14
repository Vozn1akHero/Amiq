import {IGroupData} from "../../group/models/group-models";
import {IUser} from "../../user/models/user";

export interface IPostComment {
    commentId: string;
    postId: string;
    textContent: string;
    author: Partial<IUser>;
    createdAt: Date;
    parentCommentId: string;
    parentCommentAuthor: Partial<IUser>;
    mainParentCommentId: string;
    children: Array<IGroupPostComment&IPostComment>;
    isRemoved: boolean;
}

export interface IGroupPostComment extends IPostComment {
    group: Partial<IGroupData>;
    groupPostCommentId: string;
    authorVisibilityType: string;
    groupCommentParentId: string;
    groupCommentMainParentId: string;
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
    groupCommentParentId: string;
    groupCommentMainParentId: string;
}