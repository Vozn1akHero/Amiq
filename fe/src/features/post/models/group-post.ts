import {IPost} from "./post";
import {IGroupPostComment, IPostComment} from "./post-comment";

export interface IGroupPost extends IPost {
    groupId: number;
    groupName: string;
    comments: Array<IGroupPostComment&IPostComment>;
}