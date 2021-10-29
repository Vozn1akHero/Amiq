import {IUser} from "../user/models/user";

export interface IFriendship{
    userId: number;
    name: string;
    surname: string;
    avatarPath: string;
}

export interface IFriendRequest {
    friendRequestId: string;
    creator: Partial<IUser>;
    receiver: Partial<IUser>;
}