import {IPost} from "./post";

export interface IGroupPost extends IPost {
    groupId: number;
    groupName: string;
}