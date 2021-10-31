import {IDescriptionBlock} from "common/models/description-block";

export interface IUser {
    userId: number;
    name: string;
    surname: string;
    status: string;
    birthdate?: Date;
    avatarPath: string;
    shortDescription: string;
    userDescriptionBlocks: Array<IDescriptionBlock>;
    blockedByIssuer: boolean;
    issuerBlocked: boolean;
    issuerReceivedFriendRequest: boolean;
    issuerSentFriendRequest: boolean;
    isIssuerFriend: boolean;
}