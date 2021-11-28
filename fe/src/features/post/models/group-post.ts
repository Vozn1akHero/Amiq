import {IPost} from "./post";
import {IGroupPostComment, IPostComment} from "./post-comment";

export interface IGroupPost extends IPost {
    groupId: number;
    groupName: string;
    visibleAsCreatedByAdmin: boolean;
    comments: Array<IGroupPostComment&IPostComment>;
}

export interface ICreateGroupPost extends Partial<IGroupPost> {
    createAsAdmin: boolean;
}