export interface IFoundUser {
    userId: number;
    name: string;
    surname: string;
    avatarPath: string;
    blockedByIssuer: boolean;
    issuerBlocked: boolean;
    isIssuerFriend: boolean;
    issuerReceivedFriendRequest: boolean;
    issuerSentFriendRequest: boolean;
}